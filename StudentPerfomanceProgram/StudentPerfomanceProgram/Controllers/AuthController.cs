using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentPerfomance.IdentityServer.Data.Constants;
using StudentPerfomance.IdentityServer.Models;
using StudentPerfomance.IdentityServer.ViewModels;

namespace StudentPerfomance.IdentityServer.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<SPUser> _signInManager;
        private readonly UserManager<SPUser> _userManager;
        private readonly RoleManager<SPRole> _roleManager;
        private readonly IIdentityServerInteractionService _interactionService;

        public AuthController(
            UserManager<SPUser> userManager,
            SignInManager<SPUser> signInManager,
            RoleManager<SPRole> roleManager,
            IIdentityServerInteractionService interactionService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _interactionService = interactionService;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);
            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var externalProviders = await _signInManager.GetExternalAuthenticationSchemesAsync();
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalProviders = externalProviders
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if ((await _userManager.FindByNameAsync(vm.Username)) == null)
                    ModelState.AddModelError(nameof(vm.Username), "wrong email");
                else if ((await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, false, false)).Succeeded)
                    return Redirect(vm.ReturnUrl);
                else
                    ModelState.AddModelError(nameof(vm.Password), "wrong password");
            }
            else
                ModelState.AddModelError("", "wrong email and/or password");

            vm.ExternalProviders = await _signInManager.GetExternalAuthenticationSchemesAsync();
            return View(vm);
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            return View(new RegisterViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var user = new SPUser { UserName = vm.Email };
            var result = await _userManager.CreateAsync(user, vm.Password);

            if (result.Succeeded)
            {
                await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Role, IdentityData.Student));
                await _signInManager.SignInAsync(user, false);

                return Redirect(vm.ReturnUrl);
            }

            return View();
        }

        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUri = Url.Action(nameof(ExteranlLoginCallback), "Auth", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUri);
            return Challenge(properties, provider);
        }

        public async Task<IActionResult> ExteranlLoginCallback(string returnUrl)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction("Login");
            }

            if ((await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false)).Succeeded)
            {
                return Redirect(returnUrl);
            }

            var username = info.Principal.FindFirst(ClaimTypes.Email).Value;
            return View("ExternalRegister", new ExternalRegisterViewModel
            {
                Username = username,
                ReturnUrl = returnUrl
            });
        }

        public async Task<IActionResult> ExternalRegister(ExternalRegisterViewModel vm)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction("Login");
            }

            var user = new SPUser { UserName = vm.Username };
            if (!(await _userManager.CreateAsync(user)).Succeeded)
            {
                return View(vm);
            }

            await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Role, IdentityData.Student));
            if (!(await _userManager.AddLoginAsync(user, info)).Succeeded)
            {
                return View(vm);
            }

            await _signInManager.SignInAsync(user, false);
            return Redirect(vm.ReturnUrl);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentPerfomance.Dal.Entities;

namespace StudentPerfomance.IdentityServer.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<Users> _signInManager;
        private readonly IIdentityServerInteractionService _identityInteractionService;
        private readonly UserManager<Users> _userManager;

        public AuthController(SignInManager<Users> signInManager, UserManager<Users> userManager, IIdentityServerInteractionService serverInteractionService)
        {
            _identityInteractionService = serverInteractionService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var externalProviders = await _signInManager.GetExternalAuthenticationSchemesAsync();
            return View(new LoginViewModel { ReturnUrl = returnUrl, ExternalProviders = externalProviders });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var res = await _signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, false, false);

            if (res.Succeeded)
            {
                return Redirect(loginViewModel.ReturnUrl);
            }
            else if (res.IsLockedOut)
            {

            }


            return View();
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

            var user = new Users
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                UserName = vm.Username,
                Email = vm.Email
            };

            var res = await _userManager.CreateAsync(user, vm.Password);

            await _userManager.AddClaimAsync(user, new Claim("rc.garndma", "big.cookie"));
            await _userManager.AddClaimAsync(user, new Claim("rc.api.garndma", "big.api.cookie"));


            if (res.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);

                return Redirect(vm.ReturnUrl);
            }


            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();

            var logoutRequest = await _identityInteractionService.GetLogoutContextAsync(logoutId);

            if (string.IsNullOrEmpty(logoutRequest.PostLogoutRedirectUri))
            {
                return RedirectToAction("Index", "Home");
            }

            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }

        public async Task<IActionResult> ExternalLogin(string provider, string returnUrl)
        {
            var redirectUri = Url.Action(nameof(ExternalLoginCallback), "Auth", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUri);


            return Challenge(properties, provider);
        }

        public async Task<IActionResult> ExternalLoginCallback(string returnUrl)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction("Login");
            }

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

            if (result.Succeeded)
            {
                return Redirect(returnUrl);
            }

            var username = info.Principal.FindFirst(ClaimTypes.Name.Replace(" ", "_")).Value;

            return View("ExternalRegister", new ExternalRegisterViewModel { Username = username, ReturnUrl = returnUrl });
        }

        public async Task<IActionResult> ExternalRegister(ExternalRegisterViewModel vm)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction("Login");
            }

            var user = new Users
            {
                UserName = vm.Username
            };

            var res = await _userManager.CreateAsync(user);

            await _userManager.AddClaimAsync(user, new Claim("rc.garndma", "big.cookie"));
            await _userManager.AddClaimAsync(user, new Claim("rc.api.garndma", "big.api.cookie"));


            if (!res.Succeeded)
            {
                return View(vm);
            }

            var addLoginResult = await _userManager.AddLoginAsync(user, info);

            if (!addLoginResult.Succeeded)
            {
                return View(vm);
            }

            await _signInManager.SignInAsync(user, false);

            return Redirect(vm.ReturnUrl);
        }
    }
}
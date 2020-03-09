using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentPerfomance.IdentityServer.Models;

namespace StudentPerfomance.IdentityServer.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<SPUser> _signInManager;
        private readonly IIdentityServerInteractionService _identityInteractionService;
        private readonly UserManager<SPUser> _userManager;

        public AuthController(SignInManager<SPUser> signInManager, UserManager<SPUser> userManager, IIdentityServerInteractionService serverInteractionService)
        {
            _identityInteractionService = serverInteractionService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet("{returnUrl}")]
        public async Task<ActionResult<LoginViewModel>> Login(string returnUrl)
        {
            var externalProviders = await _signInManager.GetExternalAuthenticationSchemesAsync();

            return Ok(new LoginViewModel { ReturnUrl = returnUrl, ExternalProviders = externalProviders });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid && (await _signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, false, false)).Succeeded)
            {
                return NoContent();
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new SPUser { UserName = vm.Email };
            //await _userManager.AddClaimAsync(user, new Claim("rc.garndma", "big.cookie"));
            //await _userManager.AddClaimAsync(user, new Claim("rc.api.garndma", "big.api.cookie"));

            var registerResult = await _userManager.CreateAsync(user, vm.Password);

            if (registerResult.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);

                return Ok();
            }
            else
            {
                return BadRequest(registerResult.Errors);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();

            var logoutRequest = await _identityInteractionService.GetLogoutContextAsync(logoutId);

            if (string.IsNullOrEmpty(logoutRequest.PostLogoutRedirectUri))
            {
                return NoContent();
            }

            return Ok(logoutRequest.PostLogoutRedirectUri);
        }

        public IActionResult ExternalLogin(string provider, string returnUrl)
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
                return RedirectToAction(nameof(Login));
            }

            if ((await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false)).Succeeded)
            {
                return Ok(returnUrl);
            }

            var username = info.Principal.FindFirst(ClaimTypes.Name.Replace(" ", "_")).Value;

            return Ok(new ExternalRegisterViewModel { Username = username, ReturnUrl = returnUrl });
        }

        public async Task<IActionResult> ExternalRegister(ExternalRegisterViewModel vm)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            var user = new SPUser { UserName = vm.Username };
            //await _userManager.AddClaimAsync(user, new Claim("rc.garndma", "big.cookie"));
            //await _userManager.AddClaimAsync(user, new Claim("rc.api.garndma", "big.api.cookie"));

            if (!(await _userManager.CreateAsync(user)).Succeeded | !(await _userManager.AddLoginAsync(user, info)).Succeeded)
            {
                return BadRequest(vm);
            }

            await _signInManager.SignInAsync(user, false);

            return Ok();
        }
    }
}
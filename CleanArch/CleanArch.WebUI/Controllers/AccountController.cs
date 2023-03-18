using Microsoft.AspNetCore.Mvc;

using CleanArch.Domain.Account;
using CleanArch.WebUI.ViewModels;

namespace CleanArch.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticate _authentication;

        public AccountController(IAuthenticate authenticate)
        {
            _authentication = authenticate;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
                {
                    ReturnUrl = returnUrl
                }
            );
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (await _authentication.AuthenticateAsync(model.Email, model.Password))
            {
                if (string.IsNullOrEmpty(model.ReturnUrl)) return RedirectToAction("Index", "Home");

                return Redirect(model.ReturnUrl);
            }

            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt");
                
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Register()
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (await _authentication.RegisterUserAsync(model.Email, model.Password))
                return Redirect("/");

            else
            {
                ModelState.AddModelError(string.Empty, "Invalid register attempt");

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _authentication.Logout();

            return Redirect("/Account/Login");
        }
    }
}

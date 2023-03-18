using Microsoft.AspNetCore.Identity;

using CleanArch.Domain.Account;

namespace CleanArch.Infra.Data.Identity
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticateService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {

            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<bool> AuthenticateAsync(string email, string password)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(email, password, false, false);

            return result.Succeeded;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> RegisterUserAsync(string email, string password)
        {
            ApplicationUser applicationUser = new()
            {
                UserName = email,
                Email = email
            };

            IdentityResult result = await _userManager.CreateAsync(applicationUser, password);

            if (result.Succeeded) await _signInManager.SignInAsync(applicationUser, false);

            return result.Succeeded;
        }
    }
}

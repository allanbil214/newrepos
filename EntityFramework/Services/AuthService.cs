using EFApp.Models;
using EFApp.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EFApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private ApplicationUser? _currentUser;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public bool IsLoggedIn => _currentUser != null;
        public ApplicationUser? CurrentUser => _currentUser;

        public async Task<(bool Success, string Message, ApplicationUser? User)> LoginAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return (false, "User not found.", null);

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded)
            {
                _currentUser = user;
                return (true, $"Welcome, {user.FirstName}!", user);
            }

            return (false, "Invalid password.", null);
        }

        public async Task<(bool Success, string Message)> RegisterAsync(string username, string email, string firstName, string lastName, string password)
        {
            var user = new ApplicationUser
            {
                UserName = username,
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                return (true, $"User {firstName} {lastName} registered successfully!");
            }

            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return (false, $"Registration failed: {errors}");
        }

        public void LogoutAsync()
        {
            // await _signInManager.SignOutAsync();
            _currentUser = null;
        }
    }
}
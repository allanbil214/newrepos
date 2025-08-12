using EFApp.Models;

namespace EFApp.Interfaces
{
    public interface IAuthService
    {
        Task<(bool Success, string Message, ApplicationUser? User)> LoginAsync(string username, string password);
        Task<(bool Success, string Message)> RegisterAsync(string username, string email, string firstName, string lastName, string password);
        void LogoutAsync();
        bool IsLoggedIn { get; }
        ApplicationUser? CurrentUser { get; }
    }
}
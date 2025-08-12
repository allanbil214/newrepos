using EFApp.Models;

namespace EFApp.Interfaces
{
    public interface IUserService
    {
        Task<(ApplicationUser? user, string messages)> CreateUserAsync(string username, string email, string firstName, string lastName, string password);
        Task<List<ApplicationUser>> GetUsersWithPostsAsync();
        Task<List<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser> GetCurrenUserAsync(string Id);
        Task<string> UpdateUserAsync(string Id, string newUsername, string newEmail, string newFirstName, string newLastName);
        Task<string> DeleteUserAsync(string Id);
        Task<bool> CheckPasswordAsync(string id, string oldPassword);
        Task<string> ChangePasswordAsync(string Id, string oldPassword, string newPassword); 
    }
    
}
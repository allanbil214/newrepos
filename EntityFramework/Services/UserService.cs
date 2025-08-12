using EFApp.Data;
using EFApp.Models;
using EFApp.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EFApp.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;

        public UserService(UserManager<ApplicationUser> userManager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _context = appDbContext;
        }
        public async Task<(ApplicationUser? user, string messages)> CreateUserAsync(string username, string email, string firstName, string lastName, string password)
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
                return (user, $"Created user: {firstName} {lastName}");
            }
            else
            {
                var errorMessage = $"Failed to create user: {string.Join(", ", result.Errors.Select(e => e.Description))}";
                return (null, errorMessage);
            }
        }

        public async Task<List<ApplicationUser>> GetUsersWithPostsAsync()
        {
            return await _context.Users
                .Include(u => u.Posts)
                .Where(u => u.Posts.Any(p => p.IsDeleted == false))
                .ToListAsync();
        }

        public async Task<string> DeleteUserAsync(string Id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == Id);

            if (user == null)
            {
                return $"Users with id: {Id} not exist.";
            }

            user.IsDeleted = true;

            await _context.SaveChangesAsync();

            return $"Users with id: {Id} is deleted.";
        }

        public async Task<string> UpdateUserAsync(string Id, string newUsername, string newEmail, string newFirstName, string newLastName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == Id);

            if (user == null)
            {
                return $"Users with id: {Id} not exist.";
            }

            user.UserName = newUsername;
            user.Email = newEmail;
            user.FirstName = newFirstName;
            user.LastName = newLastName;

            await _context.SaveChangesAsync();

            return $"Users with id: {Id} is updated.";
        }

        public async Task<ApplicationUser> GetCurrenUserAsync(string Id)
        {
            return await _context.Users
                .Where(u => u.Id == Id)
                .FirstOrDefaultAsync();
        }
        
        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.Posts)
                .ToListAsync();
        }

        public async Task<bool> CheckPasswordAsync(string id, string oldPassword)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return false;

            if (!await _userManager.CheckPasswordAsync(user, oldPassword))
            {
                return false;
            }

            return true;
        }

        public async Task<string> ChangePasswordAsync(string id, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return $"User with id: {id} does not exist.";


            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return $"Password change failed: {errors}";
            }

            return $"User with id: {id} has updated their password.";
        }

    }
}
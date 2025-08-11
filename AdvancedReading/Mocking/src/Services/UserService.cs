// src/MyApp/Services/UserService.cs
using Mocking.Models;
using Mocking.Repositories;

namespace Mocking.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("User ID must be positive", nameof(id));

            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<bool> CreateUserAsync(string name, string email)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty", nameof(name));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty", nameof(email));

            if (!IsValidEmail(email))
                throw new ArgumentException("Invalid email format", nameof(email));

            // Check if user already exists with this email
            var existingUsers = await _userRepository.GetAllAsync();
            if (existingUsers.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
                return false; // User already exists

            // Create new user
            var user = new User
            {
                Name = name,
                Email = email,
                CreatedAt = DateTime.UtcNow
            };

            var userId = await _userRepository.CreateAsync(user);
            return userId > 0;
        }

        public async Task<bool> UpdateUserEmailAsync(int id, string newEmail)
        {
            if (id <= 0)
                throw new ArgumentException("User ID must be positive", nameof(id));

            if (string.IsNullOrWhiteSpace(newEmail))
                throw new ArgumentException("Email cannot be empty", nameof(newEmail));

            if (!IsValidEmail(newEmail))
                throw new ArgumentException("Invalid email format", nameof(newEmail));

            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return false;

            user.Email = newEmail;
            return await _userRepository.UpdateAsync(user);
        }

        private static bool IsValidEmail(string email)
        {
            return email.Contains('@') && email.Contains('.');
        }
    }
}
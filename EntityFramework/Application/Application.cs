using EFApp.Data;
using EFApp.Interfaces;
using EFApp.Services;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;

namespace EFApp.Application
{
    public class MainApplication
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IPostService _postService;
        private readonly IDisplayService _displayService;
        private readonly AppDbContext _context;

        private bool IsManagingUser = false;

        public MainApplication(IAuthService authService, IUserService userService, IPostService postService,
                             IDisplayService displayService, AppDbContext context)
        {
            _authService = authService;
            _userService = userService;
            _postService = postService;
            _displayService = displayService;
            _context = context;
        }

        public async Task RunAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            
            Console.WriteLine("=== Welcome to Blog Console App ===");

            while (true)
            {
                if (!_authService.IsLoggedIn)
                {
                    await ShowAuthMenuAsync();
                }
                else if (IsManagingUser == false)
                {
                    await ShowMainMenuAsync();
                }
                else if (IsManagingUser == true)
                {
                    await ShowManageUsersMenu();
                }
            }
        }

        private async Task ShowAuthMenuAsync()
        {
            Console.Clear();
            Console.WriteLine("\n--- Authentication Menu ---");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("3. Exit");
            Console.Write("Choose option: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await LoginAsync();
                    break;
                case "2":
                    await RegisterAsync();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }

        private async Task ShowMainMenuAsync()
        {
            Console.WriteLine($"\n--- Main Menu (Welcome, {_authService.CurrentUser?.FirstName}) ---");
            Console.WriteLine("1. View My Posts");
            Console.WriteLine("2. View All Posts");
            Console.WriteLine("3. Create Post");
            Console.WriteLine("4. Update Post");
            Console.WriteLine("5. Delete Post");
            Console.WriteLine("6. Manage Users");
            Console.WriteLine("0. Logout");
            Console.Write("Choose option: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await ViewMyPostsAsync();
                    break;
                case "2":
                    await ViewAllPostsAsync();
                    break;
                case "3":
                    await CreatePostAsync();
                    break;
                case "4":
                    await UpdatePostAsync();
                    break;
                case "5":
                    await DeletePostAsync();
                    break;
                case "6":
                    IsManagingUser = true;
                    await ShowManageUsersMenu();
                    break;
                case "0":
                    LogoutAsync();
                    break;
                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }

        private async Task LoginAsync()
        {
            Console.Write("Username: ");
            var username = Console.ReadLine() ?? "";
            Console.Write("Password: ");
            var password = ReadPassword();
            
            var (success, message, _) = await _authService.LoginAsync(username, password);
            Console.WriteLine(message);
        }

        private async Task RegisterAsync()
        {
            Console.WriteLine("Username: ");
            var username = Console.ReadLine() ?? "";
            Console.Write("Email: ");
            var email = Console.ReadLine() ?? "";
            Console.Write("First Name: ");
            var firstName = Console.ReadLine() ?? "";
            Console.Write("Last Name: ");
            var lastName = Console.ReadLine() ?? "";
            Console.Write("Password: ");
            var password = ReadPassword();

            var (success, message) = await _authService.RegisterAsync(username, email, firstName, lastName, password);
            Console.WriteLine(message);
        }

        private async Task ViewMyPostsAsync()
        {
            var posts = await _postService.GetPostsByUserAsync(_authService.CurrentUser!.Id);
            Console.WriteLine("\n=== My Posts ===");
            Console.WriteLine(_displayService.FormatPostsByUser(posts));
        }

        private async Task ViewAllPostsAsync()
        {
            var posts = await _postService.GetAllPostsAsync();
            Console.WriteLine("\n=== All Posts ===");
            Console.WriteLine(_displayService.FormatAllPosts(posts));
        }

        private async Task CreatePostAsync()
        {
            Console.Write("Post Title: ");
            var title = Console.ReadLine() ?? "";
            Console.Write("Post Content: ");
            var content = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(title))
            {
                var message = await _postService.CreatePostAsync(_authService.CurrentUser!.Id, title, content);
                Console.WriteLine(message);
            }
            else
            {
                Console.WriteLine("Title cannot be empty!");
            }
        }

        private async Task UpdatePostAsync()
        {
            await ViewMyPostsAsync();
            Console.Write("Enter Post ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int postId))
            {
                var postContext = _context.Posts.Where(p => p.Id == postId).FirstOrDefault();
                Console.Write("New Title: ");
                var title = Console.ReadLine() ?? postContext.Title;
                Console.Write("New Content: ");
                var content = Console.ReadLine() ?? postContext.Content;

                var message = await _postService.UpdatePostAsync(postId, title, content);
                Console.WriteLine(message);
            }
            else
            {
                Console.WriteLine("Invalid Post ID!");
            }
        }

        private async Task DeletePostAsync()
        {
            await ViewMyPostsAsync();
            Console.Write("Enter Post ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int postId))
            {
                Console.Write("Are you sure? (y/N): ");
                var confirm = Console.ReadLine()?.ToLower();
                if (confirm == "y" || confirm == "yes")
                {
                    var message = await _postService.DeletePostAsync(postId);
                    Console.WriteLine(message);
                }
            }
            else
            {
                Console.WriteLine("Invalid Post ID!");
            }
        }

        private async Task ViewAllUsersAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            Console.WriteLine("\n=== All Users ===");
            Console.WriteLine(_displayService.FormatAllUsers(users));
        }

        private async Task ShowManageUsersMenu()
        {
            Console.WriteLine($"\n--- Main Menu (Welcome, {_authService.CurrentUser?.FirstName}) ---");
            Console.WriteLine("1. View My Data");
            Console.WriteLine("2. View All Users");
            Console.WriteLine("3. Update User");
            Console.WriteLine("4. Delete User");
            Console.WriteLine("5. Change Password");
            Console.WriteLine("0. Back");
            Console.Write("Choose option: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await ViewMyDataAsync();
                    break;
                case "2":
                    await ViewAllUsersAsync();
                    break;
                case "3":
                    await UpdateUserAsync();
                    break;
                case "4":
                    await DeleteUserAsync();
                    break;
                case "5":
                    await ChangePasswordAsync();
                    break;
                case "0":
                    IsManagingUser = false;
                    break;
                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }

        private async Task ViewMyDataAsync()
        {
            var users = await _userService.GetCurrenUserAsync(_authService.CurrentUser!.Id);
            Console.WriteLine("\n=== My Data ===");
            Console.WriteLine(_displayService.FormatThisUser(users));
        }

        private async Task ViewSelectedUserDataAsync(string Id)
        {
            var users = await _userService.GetCurrenUserAsync(Id);
            Console.WriteLine("\n=== My Data ===");
            Console.WriteLine(_displayService.FormatThisUser(users));
        }

        private async Task UpdateUserAsync()
        {
            Console.Write("Enter User ID to update: ");
            string? userId = Console.ReadLine();
            if (!string.IsNullOrEmpty(userId))
            {
                await ViewSelectedUserDataAsync(userId);
                var userContext = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
                Console.Write("New Username: ");
                var userName = Console.ReadLine() ?? userContext.UserName;
                Console.Write("New Email: ");
                var email = Console.ReadLine() ?? userContext.Email;
                Console.Write("New First Name: ");
                var firstName = Console.ReadLine() ?? userContext.FirstName;
                Console.Write("New Last Name: ");
                var lastName = Console.ReadLine() ?? userContext.LastName;

                var message = await _userService.UpdateUserAsync(userId, userName, email, firstName, lastName);
                Console.WriteLine(message);
            }
            else
            {
                Console.WriteLine("Invalid User ID!");
            }
        }

        private async Task ChangePasswordAsync()
        {
            string? userId = _authService.CurrentUser!.Id;
            if (!string.IsNullOrEmpty(userId))
            {
                await ViewSelectedUserDataAsync(userId);
                var userContext = _context.Users.Where(u => u.Id == userId).FirstOrDefault();

                Console.Write("Old Password: ");
                var oldPassword = ReadPassword();
                Console.Write("New Password: ");
                var newPassword = ReadPassword();

                if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
                {
                    Console.WriteLine("Password(s) can't be empty!");
                    return;
                }

                if (await _userService.CheckPasswordAsync(userId, oldPassword))
                {
                    var message = await _userService.ChangePasswordAsync(userId, oldPassword, newPassword);
                    Console.WriteLine(message);
                }
                else
                {
                    Console.WriteLine("Old and new password do not match.");
                }
            }
            else
            {
                Console.WriteLine("Invalid User ID!");
            }
        }

        private async Task DeleteUserAsync()
        {
            await ViewMyPostsAsync();
            Console.Write("Enter User ID to delete: ");
            string? userId = Console.ReadLine();
            if (!string.IsNullOrEmpty(userId))
            {
                Console.Write("Are you sure? (y/N): ");
                var confirm = Console.ReadLine()?.ToLower();
                if (confirm == "y" || confirm == "yes")
                {
                    var message = await _userService.DeleteUserAsync(userId);
                    Console.WriteLine(message);
                }
            }
            else
            {
                Console.WriteLine("Invalid User ID!");
            }
        }

        private void LogoutAsync()
        {
            _authService.LogoutAsync();
            Console.WriteLine("Logged out successfully!");
        }

        private static string ReadPassword()
        {
            var password = "";
            ConsoleKeyInfo key;
            
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password[0..^1];
                    Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter);
            
            Console.WriteLine();
            return password;
        }
    }
}
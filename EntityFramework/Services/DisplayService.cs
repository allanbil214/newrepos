using EFApp.Models;
using EFApp.Interfaces;
using EFApp.Data;
using System.Text;
using System.Runtime.CompilerServices;

namespace EFApp.Services
{
    public class DisplayService : IDisplayService
    {
        public string FormatUsersWithPosts(List<ApplicationUser> users)
        {
            if (!users.Any())
            {
                return "Users not found.";
            }

            var sb = new StringBuilder();
            foreach (var user in users)
            {
                sb.AppendLine($"User: {user.FirstName} {user.LastName} [{user.Email} | {user.UserName}] - {user.Posts.Count} posts.");
                foreach (var post in user.Posts)
                {
                    sb.AppendLine($"  - {post.Title} ({post.CreatedAt:yyyy-MM-dd})");
                }
                sb.AppendLine();
            }
            return sb.ToString().TrimEnd();
        }

        public string FormatPostsByUser(List<Post> posts)
        {
            if (!posts.Any())
            {
                return "No posts found for this user.";
            }

            var sb = new StringBuilder();
            foreach (var post in posts)
            {
                sb.AppendLine($"Post: {post.Title} by {post.User.FirstName} {post.User.LastName}");
                sb.AppendLine($"Content: {post.Content}");
                sb.AppendLine($"Created at: {post.CreatedAt:yyyy:MM:dd}");
                sb.AppendLine();
            }
            return sb.ToString().TrimEnd();
        }

        public string FormatAllPosts(List<Post> posts)
        {
            if (!posts.Any())
            {
                return "No posts found.";
            }

            var sb = new StringBuilder();
            foreach (var post in posts)
            {
                sb.AppendLine($"ID: {post.Id}, Title: {post.Title}, Author: {post.User.FirstName} {post.User.LastName}");
            }
            return sb.ToString().TrimEnd();
        }

        public string FormatThisUser(ApplicationUser user)
        {
            if (user == null)
            {
                return "Users not found.";
            }

            var sb = new StringBuilder();
            sb.AppendLine($"Id: {user.Id}");
            sb.AppendLine($"User: {user.FirstName} {user.LastName} [{user.Email} | {user.UserName}] - {user.Posts.Count} posts.");
            foreach (var post in user.Posts)
            {
                sb.AppendLine($"  - {post.Title} ({post.CreatedAt:yyyy-MM-dd})");
            }

            return sb.ToString().TrimEnd();
        }

        public string FormatAllUsers(List<ApplicationUser> users)
        {
            if (!users.Any())
            {
                return "Users not found.";
            }

            var sb = new StringBuilder();
            foreach (var user in users)
            {
                sb.AppendLine($"Id: {user.Id}");
                sb.AppendLine($"User: {user.FirstName} {user.LastName} [{user.Email} | {user.UserName}] - {user.Posts.Count} posts.");
                foreach (var post in user.Posts)
                {
                    sb.AppendLine($"  - {post.Title} ({post.CreatedAt:yyyy-MM-dd})");
                }
                sb.AppendLine();
            }
            return sb.ToString().TrimEnd();
        }

    }
}
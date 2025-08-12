using EFApp.Models;

namespace EFApp.Interfaces
{
    public interface IPostService
    {
        Task<string> CreatePostAsync(string userId, string title, string content);
        Task<List<Post>> GetPostsByUserAsync(string userId);
        Task<List<Post>> GetAllPostsAsync();
        Task<string> UpdatePostAsync(int Id, string newTitle, string newContent);
        Task<string> DeletePostAsync(int postId);
    }
}
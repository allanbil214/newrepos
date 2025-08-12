using EFApp.Models;
using EFApp.Interfaces;
using EFApp.Data;
using Microsoft.EntityFrameworkCore;

namespace EFApp.Services
{
    public class PostService : IPostService
    {
        private readonly AppDbContext _dbContext;

        public PostService(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<string> CreatePostAsync(string userId, string title, string content)
        {
            var post = new Post()
            {
                UserId = userId,
                Title = title,
                Content = content
            };

            _dbContext.Add(post);
            await _dbContext.SaveChangesAsync();
            return $"Created post: {title}";
        }

        public async Task<List<Post>> GetPostsByUserAsync(string userId)
        {
            return await _dbContext.Posts
            .Include(p => p.User)
            .Where(p => p.UserId == userId)
            .Where(p => p.IsDeleted == false)
            .ToListAsync();
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await _dbContext.Posts
            .Include(p => p.User)
            .Where(p => p.IsDeleted == false)
            .ToListAsync();
        }

        public async Task<string> UpdatePostAsync(int Id, string newTitle, string newContent)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == Id);

            if (post == null)
            {
                return $"Post with ID {Id} not found";
            }

            post.Title = newTitle;
            post.Content = newContent;

            await _dbContext.SaveChangesAsync();
            return $"Updated post with Id: {Id}";
        }

        public async Task<string> DeletePostAsync(int Id)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == Id);

            if (post == null)
            {
                return $"Post with ID {Id} not found";
            }

            post.IsDeleted = true;

            await _dbContext.SaveChangesAsync();
            return $"Deleted post with Id: {Id}";
        }
    }
}
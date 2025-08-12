using System.ComponentModel.DataAnnotations;

namespace EFApp.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null;
    }
}
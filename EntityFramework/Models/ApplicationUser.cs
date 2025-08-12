using Microsoft.AspNetCore.Identity;

namespace EFApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        
        // Navigation property for relationship
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
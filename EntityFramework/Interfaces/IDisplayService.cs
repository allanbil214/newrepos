using EFApp.Models;

namespace EFApp.Interfaces
{
    public interface IDisplayService
    {
        string FormatUsersWithPosts(List<ApplicationUser> users);
        string FormatPostsByUser(List<Post> posts);
        string FormatAllPosts(List<Post> posts);

        string FormatThisUser(ApplicationUser user);
        string FormatAllUsers(List<ApplicationUser> users);
    }
}
using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class AppUser: IdentityUser
    {
        public string ProfilePicture { get; set; }
    }
}
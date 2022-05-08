using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class AppUser: IdentityUser
    {
        public string ProfilePicture { get; set; }
    }
}
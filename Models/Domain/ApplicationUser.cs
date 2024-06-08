using Microsoft.AspNetCore.Identity;

namespace ALPHII.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public int credit { get; set; }
    }
}

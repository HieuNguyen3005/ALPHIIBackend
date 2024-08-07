using Microsoft.AspNetCore.Identity;

namespace ALPHII.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Credit { get; set; }
        // Foreign key
        public Guid? PlanId { get; set; }

        public Plan? Plan { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}

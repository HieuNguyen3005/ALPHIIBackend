using System.ComponentModel.DataAnnotations;

namespace ALPHII.Models.DTO
{
    public class RegisterRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Roles { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Credit { get; set; }

        public Guid? PlanId { get; set; }
    }
}

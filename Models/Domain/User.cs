namespace ALPHII.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Credit { get; set; }
        // Foreign key
        public Guid PlanId { get; set; }

        public Plan Plan { get; set; }

        public ICollection<Task> Tasks { get; set; }
    }
}

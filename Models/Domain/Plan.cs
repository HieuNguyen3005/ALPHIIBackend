namespace ALPHII.Models.Domain
{
    public class Plan
    {
        public Guid Id { get; set; }

        public string NamePlan { get; set; }

        public int CostPerYear { get; set; }

        public int CostPerMonth { get; set; }

        public ICollection<User> Users { get; set; }
    }
}

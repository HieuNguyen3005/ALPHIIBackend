namespace ALPHII.Models.Domain
{
    public class Tool
    {
        public Guid Id { get; set; }

        public string ToolName { get; set; }

        public Plan BasePlan { get; set; }

        public int Price { get; set; }

        public ICollection<Task> Tasks { get; set; }
    }
}

namespace ALPHII.Models.Domain
{
    public class Project
    {
        public Guid Id { get; set; }
        public string ProjectName { get; set; }

        // State: 0 - Not Started; 1 - In processing; 2 - completed
        public int State { get; set; }

        public string Note { get; set; }

        // Foreign key and nevigation property ( 1 - n: User - Task)

        public Guid UserId { get; set; }

        public User User { get; set; }

        // Foreign key and nevigation property( 1 - n: Tool - Task)

        public Guid ToolId { get; set; }

        public Tool Tool { get; set; }

        // Nevigation property ( 1 - 1 : Task - VMTask)

        public VMProject VMProject { get; set; }
    }
}

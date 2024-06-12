
namespace ALPHII.Models.Domain
{
    public class VMProject
    {

        public Guid Id { get; set; }

        public Guid ImageInputId { get; set; }

        public Guid ImageMaskId { get; set; }

        public string PositivePrompt { get; set; }

        public string NegativePrompt { get; set; }

        public string TextDescription { get; set; }

        // Foreign key and nevigation property
        public Guid ProjectId { get; set; }

        public Project Project { get; set; }  
    }
}

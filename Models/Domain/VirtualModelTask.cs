namespace ALPHII.Models.Domain
{
    public class VirtualModelTask
    {
        public Guid Id { get; set; }

        public string TextDescription { get; set; }

        public string PositivePrompt { get; set; }

        public string NegativePrompt { get; set; }

        public string ImageLink { get; set; }

        public string MaskLink { get; set; }
    }
}

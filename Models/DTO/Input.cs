namespace ALPHII.Models.DTO
{
    public class Input
    {
        public string model_type { get; set; }

        public int n_steps { get; set; }

        public float image_scale { get; set; }

        public string human_base64 { get; set; }

        public string cloth_base64 { get; set; }

        public int n_samples { get; set; }

        public int task_category {  get; set; }
    }
}

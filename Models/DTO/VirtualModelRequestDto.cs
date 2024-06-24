namespace ALPHII.Models.DTO
{
    public class VirtualModelRequestDto{
        public string base_image_url { get; set; }

        public string mask_image_url {get; set; }

        public string user_prompt {get; set; }

        public string positive_prompt {get; set; }

        public string negative_prompt {get; set; }

        public string text_description {get; set; }
    }
}
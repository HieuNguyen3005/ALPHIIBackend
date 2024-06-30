namespace ALPHII.Models.DTO
{
    public class VirtualModelInput
    {
        public string img_base64 { get; set; }

        public string mask_bask64 { get; set; }

        public string user_prompt { get; set; }

        public string negative_promt { get; set; }

        public int quality { get; set; }

        public int n_sample { get; set; }
    }
}

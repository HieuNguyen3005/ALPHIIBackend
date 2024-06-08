namespace ALPHII.Models.DTO
{
    public class VirtualTryOnOutputDto
    {
        public int delayTime { get; set; }
        public int executionTime { get; set; }

        public string id { get; set; }

        public Output output { get; set; }

        public string status { get; set; }
    }
}

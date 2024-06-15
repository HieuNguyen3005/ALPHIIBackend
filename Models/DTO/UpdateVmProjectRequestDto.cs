namespace ALPHII.Models.DTO
{
    public class UpdateVmProjectRequestDto
    {
        public string ProjectName { get; set; }

        public int State { get; set; }

        public string Note { get; set; }

        VmProjectDto VmProjectDto { get; set; }    
    }
}

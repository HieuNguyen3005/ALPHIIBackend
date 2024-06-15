namespace ALPHII.Models.DTO
{
    public class GetVmProjectRequest : GetAllProjectRequest
    {
        public Guid ProjectId { get; set; }
    }
}

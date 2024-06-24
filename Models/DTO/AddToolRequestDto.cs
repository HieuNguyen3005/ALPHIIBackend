using System.ComponentModel.DataAnnotations;

namespace ALPHII.Models.DTO
{
    public class AddToolRequestDto
    {
        public string ToolName { get; set; }

        public int Price { get; set; }

        public Guid BasePlanId {get; set;}
    }
}

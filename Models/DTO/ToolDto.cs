using ALPHII.Models.Domain;
using ALPHII.Models.DTO;

namespace ALPHII.Models.DTO
{
    public class ToolDto
    {
        public string ToolName { get; set; }

        public int Price { get; set; }

        public PlanDto BasePlan {get; set;}
    }
}

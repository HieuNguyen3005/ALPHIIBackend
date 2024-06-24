using ALPHII.Models.Domain;
using ALPHII.Models.DTO;

namespace ALPHII.Models.DTO
{
    public class PlanDto
    {
        public string NamePlan { get; set; }

        public int CostPerYear { get; set; }

        public int CostPerMonth { get; set; }

        public int ResetCreditNumber {get; set; }
    }
}

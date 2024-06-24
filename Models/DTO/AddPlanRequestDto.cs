using System.ComponentModel.DataAnnotations;

namespace ALPHII.Models.DTO
{
    public class AddPlanRequestDto
    {
        public string NamePlan { get; set; }

        public int CostPerYear { get; set; }

        public int CostPerMonth { get; set; }

        public int ResetCreditNumber {get; set; }
    }
}

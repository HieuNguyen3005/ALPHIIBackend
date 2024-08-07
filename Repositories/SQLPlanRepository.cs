using ALPHII.Data;
using ALPHII.Models.Domain;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ALPHII.Repositories
{
    public class SQLPlanRepository : IPlanRepository
    {
        private readonly ALPHIIAuthDbContext dbContext;
        public SQLPlanRepository(ALPHIIAuthDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Plan> CreatePlanAsync(Plan Plan)
        {
            await dbContext.Plans.AddAsync(Plan);
            await dbContext.SaveChangesAsync();
            return Plan;
        }

        public async Task<Plan?> DeletePlanAsync(Guid id)
        {
            var existionPlan = await dbContext.Plans.FirstOrDefaultAsync(x => x.Id == id);
            if (existionPlan == null)
            {
                return null;
            }
            dbContext.Plans.Remove(existionPlan);
            await dbContext.SaveChangesAsync();
            return existionPlan;
        }

        public async Task<List<Plan>> GetAllPlanAsync()
        {
            return await dbContext.Plans.ToListAsync();
        }

        public async Task<Plan?> GetPlanByIdAsync(Guid id)
        {
            var existionPlan = await dbContext.Plans.FirstOrDefaultAsync(x => x.Id == id);
            if (existionPlan == null)
            {
                return null;
            }
            return existionPlan;
        }

        public async Task<Plan?> UpdatePlanAsync(Guid id, Plan Plan)
        {
            var existionPlan = await dbContext.Plans.FirstOrDefaultAsync(x => x.Id == id);
            if (existionPlan == null)
            {
                return null;
            }
            existionPlan.NamePlan = Plan.NamePlan;
            existionPlan.CostPerYear = Plan.CostPerYear;
            existionPlan.CostPerMonth = Plan.CostPerMonth;
            existionPlan.CreditResetNumber = Plan.CreditResetNumber;
            await dbContext.SaveChangesAsync();
            return existionPlan;
        }
    }
}

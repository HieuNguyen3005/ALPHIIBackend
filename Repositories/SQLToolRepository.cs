using ALPHII.Data;
using ALPHII.Models.Domain;
using ALPHII.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace ALPHII.Repositories
{
    public class SQLToolRepository : IToolRepository
    {
        private readonly ALPHIIBackendDbContext dbContext;
        public SQLToolRepository(ALPHIIBackendDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Tool>> GetAllToolAsync()
        {
            return await dbContext.Tools.ToListAsync();
        }

        public async Task<Tool> CreateToolAsync(Tool tool)
        {
            await dbContext.Tools.AddAsync(tool);
            await dbContext.SaveChangesAsync();
            return tool;
        }
        
        public async Task<Tool?> DeleteToolAsync(Guid id)
        {
            var existionTool = await dbContext.Tools.FirstOrDefaultAsync(x => x.Id == id);
            if (existionTool == null)
            {
                return null;
            }
            dbContext.Tools.Remove(existionTool);
            await dbContext.SaveChangesAsync();
            return existionTool;
        }

        public async Task<Tool?> GetToolByIdAsync(Guid id)
        {
            var existionTool = await dbContext.Tools.FirstOrDefaultAsync(x => x.Id == id);
            if (existionTool == null)
            {
                return null;
            }
            return existionTool;
        }

        public async Task<Tool?> UpdateToolAsync(Guid id, UpdateToolRequestDto updateToolRequestDto)
        {
            var existionTool = await dbContext.Tools.FirstOrDefaultAsync(x => x.Id == id);
            if (existionTool == null)
            {
                return null;
            }
            existionTool.ToolName = updateToolRequestDto.ToolName;
            existionTool.BasePlanId = updateToolRequestDto.BasePlanId;
            existionTool.Price = updateToolRequestDto.Price;
            await dbContext.SaveChangesAsync();
            return existionTool;
        }
    }
}

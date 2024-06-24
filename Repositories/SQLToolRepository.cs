namespace ALPHII.Repositories
{
    public class SQLToolRepository : IToolRepository
    {
        private readonly ALPHIIBackendDbContext dbContext;
        public SQLToolRepository(ALPHIIBackendDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Tool> CreateToolAsync(Tool tool)
        {
            await dbContext.Tool.AddAsync(tool);
            await dbContext.SaveChangesAsync();
            return tool;
        }
        
        public async Task<Tool?> DeleteToolAsync(Guid id)
        {
            var existionTool = await dbContext.Tool.FirstOrDefaultAsync(x => x.Id == id);
            if (existionTool == null)
            {
                return null;
            }
            dbContext.Tool.Remove(existionTool);
            await dbContext.SaveChangesAsync();
            return existionTool;
        }

        public async Task<Tool?> GetToolByIdAsync(Guid id)
        {
            var existionTool = await dbContext.Tool.FirstOrDefaultAsync(x => x.Id == id);
            if (existionTool == null)
            {
                return null;
            }
            return existionTool;
        }

        public async Task<Tool?> UpdateToolAsync(Guid id, UpdateToolRequestDto updateToolRequestDto)
        {
            var existionTool = await dbContext.Tool.FirstOrDefaultAsync(x => x.Id == id);
            if (existionTool == null)
            {
                return null;
            }
            existionTool.ToolName = updateToolRequestDto.ToolName;
            existionTool.BasePlan = updateToolRequestDto.BasePlan;
            existionTool.Price = updateToolRequestDto.Price;
            await dbContext.SaveChangesAsync();
            return existionTool;
        }
    }
}

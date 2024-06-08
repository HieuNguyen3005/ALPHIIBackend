using ALPHII.Data;
using ALPHII.Models.Domain;
using ALPHII.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace ALPHII.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly ALPHIIBackendDbContext dbContext;
        public SQLRegionRepository(ALPHIIBackendDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existionRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existionRegion == null)
            {
                return null;
            }

            dbContext.Remove(existionRegion);
            return existionRegion;
        }

        public async Task<List<Region>> GetAllAsync(string? filterOn, string? filterQuery)
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existionRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(existionRegion == null)
            {
                return null;
            }

            existionRegion.Code = region.Code;
            existionRegion.Name = region.Name;
            existionRegion.RegionImageUrl = region.RegionImageUrl;
            await dbContext.SaveChangesAsync();

            return existionRegion;
        }
    }
}

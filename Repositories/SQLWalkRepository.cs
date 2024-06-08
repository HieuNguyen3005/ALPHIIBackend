using ALPHII.Data;
using ALPHII.Models.Domain;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ALPHII.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly ALPHIIBackendDbContext dbContext;
        public SQLWalkRepository(ALPHIIBackendDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existionWalk = await dbContext.Walks.Include("Region").Include("Difficulty").FirstOrDefaultAsync(x => x.Id == id);
            if (existionWalk == null)
            {
                return null;
            }
            dbContext.Walks.Remove(existionWalk);
            await dbContext.SaveChangesAsync();
            return existionWalk;
        }

        public async Task<List<Walk>> GetAllWalkAsync(int pageNumber, int pageSize,string? filterOn, string? filterQuery, string? sortBy, bool isAscending = true)
        {
            var walkDomainModels = dbContext.Walks.Include("Region").Include("Difficulty").AsQueryable();
            if((string.IsNullOrWhiteSpace(filterOn) == false) && (string.IsNullOrWhiteSpace(filterQuery)== false))
            {
                if(filterOn!.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walkDomainModels = walkDomainModels.Where(x => x.Name.Contains(filterQuery!));
                }
            }

            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walkDomainModels = isAscending ? walkDomainModels.OrderBy(x=> x.Name) : walkDomainModels.OrderByDescending(x => x.Name);
                }
                else if(sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walkDomainModels = isAscending ? walkDomainModels.OrderBy(x => x.LengthInKm) : walkDomainModels.OrderByDescending(x => x.LengthInKm);
                }
            }
            // Paging
            var skip = (pageNumber - 1)* pageSize;
            return await walkDomainModels.Skip(skip).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            var existionWalk = await dbContext.Walks.Include("Region").Include("Difficulty").FirstOrDefaultAsync(x => x.Id == id);
            if (existionWalk == null)
            {
                return null;
            }
            return existionWalk;
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existionWalk = await dbContext.Walks.Include("Region").Include("Difficulty").FirstOrDefaultAsync(x => x.Id == id);
            if (existionWalk == null)
            {
                return null;
            }
            existionWalk.Name = walk.Name;
            existionWalk.Description = walk.Description;
            existionWalk.LengthInKm = walk.LengthInKm;
            existionWalk.WalkImageUrl = walk.WalkImageUrl;
            existionWalk.DifficultyId = walk.DifficultyId;
            existionWalk.RegionId = walk.RegionId;
            await dbContext.SaveChangesAsync();

            return existionWalk;
        }
    }
}

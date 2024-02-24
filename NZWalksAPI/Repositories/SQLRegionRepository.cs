using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {

        private readonly NZWalksDbContext _nZWalksDbContext;

        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            _nZWalksDbContext = dbContext;   //  " this._nZWalksDbContext = dbContext" is also correct

        }

       

        public async Task<List<Region>> GetAllAsync()
        {

           return await _nZWalksDbContext.Regions.ToListAsync();
            
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await _nZWalksDbContext.Regions.FirstOrDefaultAsync( x => x.Id == id);
        }


        public async Task<Region> CreateAsync(Region region)
        {
            await _nZWalksDbContext.Regions.AddAsync(region);
            await _nZWalksDbContext.SaveChangesAsync();
            return region;
            
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await _nZWalksDbContext.Regions.FirstOrDefaultAsync(  x => x.Id == id);

            if (existingRegion == null) 
            { 
                return null;
            }

            existingRegion.Code=region.Code;
            existingRegion.Name=region.Name;
            existingRegion.RegionImageUrl =region.RegionImageUrl;

            await _nZWalksDbContext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await _nZWalksDbContext.Regions.FirstOrDefaultAsync( x => x.Id == id); 

            if (existingRegion == null)
            {
                return null;
            }

            _nZWalksDbContext.Regions.Remove(existingRegion);
            await _nZWalksDbContext.SaveChangesAsync() ;
            return existingRegion;
        }
    }
}

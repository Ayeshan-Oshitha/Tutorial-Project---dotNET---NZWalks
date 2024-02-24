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
    }
}

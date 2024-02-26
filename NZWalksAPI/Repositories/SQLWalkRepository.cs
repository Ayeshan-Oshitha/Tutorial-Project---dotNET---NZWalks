using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext _nZWalksDbContext;

        public SQLWalkRepository(NZWalksDbContext nZWalksDbContext)
        {
            this._nZWalksDbContext = nZWalksDbContext;
        }




        public async Task<Walk> CreateAsync(Walk walk)
        {
            
            await _nZWalksDbContext.Walks.AddAsync(walk);
            await _nZWalksDbContext.SaveChangesAsync();

            return walk;

        }



        public async Task<List<Walk>> GetAllAsync()
        {
            return await _nZWalksDbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();

            // ToString make above type safe , we can use below code
            // return await _nZWalksDbContext.Walks.Include(x => x.Difficulty)
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await _nZWalksDbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync( x => x.Id == id);
        }
    }
}

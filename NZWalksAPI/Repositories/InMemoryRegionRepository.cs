using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{


    // Instead of SQLserverRepository, Someone can use InmemoryRegionRepositories
    // Just for demonstrartion
    public class InMemoryRegionRepository : IRegionRepository
    {
        public async Task<List<Region>> GetAllAsync()
        {
            return new List<Region>
            {
                new Region()
                {
                    Id = Guid.NewGuid(),
                    Code = "Random Region",
                    Name = "Random Name"
                };

            };
        }
    }
}

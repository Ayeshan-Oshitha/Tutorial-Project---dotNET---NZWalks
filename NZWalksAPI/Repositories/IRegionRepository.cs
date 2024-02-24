using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public interface IRegionRepository
    {



       Task<List<Region>> GetAllAsync();

        //nullable region as a return type - There will be no region returning to that ID, so It could be a null
        Task<Region?> GetByIdAsync(Guid id);


        Task<Region> CreateAsync(Region region);


        Task<Region?> UpdateAsync(Guid id,Region region);

        Task<Region?> DeleteAsync(Guid id);
    }
}

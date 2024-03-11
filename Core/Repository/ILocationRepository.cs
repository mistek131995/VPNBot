using Core.Model.Location;

namespace Core.Repository
{
    public interface ILocationRepository
    {
        public Task<Location> GetByIdAsync(int id);
        public Task<List<Location>> GetByIdsAsync(List<int> ids);
        public Task<List<Location>> GetAllAsync();
        public Task<Location> GetByNameAsync(string name);
        public Task<Location> GetByTagAsync(string tag);
        public Task<Location> GetByServerIdAsync(int serverId);

        public Task<int> AddAsync(Location country);
        public Task UpdateAsync(Location country);
    }
}

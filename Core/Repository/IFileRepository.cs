using File = Core.Model.File.File;

namespace Core.Repository
{
    public interface IFileRepository
    {
        public Task<List<File>> GetAllAsync();
        public Task<File> GetByTagAsync(string tag);
        public Task<int> AddAsync(File file);
        public Task UpdateAsync(File file);
    }
}

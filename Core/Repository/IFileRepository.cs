using File = Core.Model.File.File;

namespace Core.Repository
{
    public interface IFileRepository
    {
        public Task<File> GetByTagAsync(string tag);
        public Task<int> AddAsync(File file);
    }
}

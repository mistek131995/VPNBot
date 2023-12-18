using File = Core.Model.File.File;

namespace Core.Repository
{
    public interface IFileRepository
    {
        public Task<File> GetByShortNameAsync(string shortName);
        public Task<int> AddAsync(File file);
    }
}

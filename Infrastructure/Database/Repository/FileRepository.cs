using Core.Repository;
using File = Core.Model.File.File;

namespace Infrastructure.Database.Repository
{
    public class FileRepository(Context context) : IFileRepository
    {
        public Task<int> AddAsync(File file)
        {
            throw new NotImplementedException();
        }

        public Task<File> GetByShortNameAsync(string shortName)
        {
            throw new NotImplementedException();
        }
    }
}

using Core.Repository;
using File = Core.Model.File.File;

namespace Infrastructure.Database.Repository
{
    public class FileRepository(Context context) : IFileRepository
    {
        public async Task<int> AddAsync(File file)
        {
            var newFile = new Entity.File()
            {
                Name = file.Name,
                ContentType = file.ContentType,
                Data = file.Data,
                Tag = file.Tag,
                Version = file.Version,
            };

            await context.AddAsync(newFile);
            await context.SaveChangesAsync();

            return newFile.Id;
        }

        public Task<File> GetByShortNameAsync(string shortName)
        {
            throw new NotImplementedException();
        }
    }
}

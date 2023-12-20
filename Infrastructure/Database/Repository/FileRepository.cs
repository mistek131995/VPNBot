using Core.Repository;
using Microsoft.EntityFrameworkCore;
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
                Tag = file.Tag,
                Version = file.Version,
            };

            await context.AddAsync(newFile);
            await context.SaveChangesAsync();

            return newFile.Id;
        }

        public async Task<File> GetByTagAsync(string shortName)
        {
            return await context.Files
                .Select(x => new File(
                    x.Id, 
                    x.Tag, 
                    x.Name, 
                    x.ContentType, 
                    x.Version)
                ).FirstOrDefaultAsync();
        }
    }
}

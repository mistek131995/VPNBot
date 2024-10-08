﻿using Core.Repository;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using File = Core.Model.File.File;

namespace Infrastructure.Database.Repository
{
    public class FileRepository(ContextFactory context) : IFileRepository
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

        public Task<List<File>> GetAllAsync()
        {
            return context.Files
                .Select(x => new File(x.Id, x.Tag, x.Name, x.ContentType, x.Version))
                .ToListAsync();
        }

        public async Task<File> GetByTagAsync(string tag)
        {
            var file = await context.Files.FirstOrDefaultAsync(f => f.Tag == tag);

            if (file == null)
                return null;

            return new File(file.Id, file.Tag, file.Name, file.ContentType, file.Version);
        }

        public async Task UpdateAsync(File file)
        {
            var fileInDb = await context.Files.FirstOrDefaultAsync(x => x.Id == file.Id);

            fileInDb.Name = file.Name;
            fileInDb.ContentType = file.ContentType;
            fileInDb.Version = file.Version;

            await context.SaveChangesAsync();
        }
    }
}

using Application.ControllerService.Common;
using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.Admin.FileManager.CreateDirectory
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, string>
    {
        public async Task<string> HandlingAsync(Request request)
        {
            var mainPath = AppDomain.CurrentDomain.BaseDirectory;
            var filesPath = Path.Combine(mainPath, "wwwroot", "files");

            Directory.CreateDirectory(Path.Combine(filesPath, "test"));

            var test = Directory.GetDirectories(filesPath);

            return string.Join(" | ", test);
        }
    }
}

using Application.ControllerService.Common;
using Core.Common;
using Renci.SshNet;
using File = Core.Model.File.File;

namespace Service.ControllerService.Service.UploadFile
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var settings = await repositoryProvider.SettingsRepositroy.GetSettingsAsync();

            var connectionInfo = new ConnectionInfo(
                "185.215.186.224", 
                settings.SSHServerLogin, 
                new PasswordAuthenticationMethod(
                    settings.SSHServerLogin, 
                    settings.SSHServerPassword)
                );

            using var client = new SftpClient(connectionInfo);
            using var stream = new MemoryStream(request.Data);

            client.Connect();

            if (!client.Exists($"{settings.FileBasePath}/{request.Tag}"))
                client.CreateDirectory($"{settings.FileBasePath}/{request.Tag}");

            //FileBasePath - /home/build/wwwroot/files
            client.UploadFile(stream, $"{settings.FileBasePath}/{request.Tag}/{request.Name}", null);

            client.Disconnect();

            await repositoryProvider.FileRepository.AddAsync(new File(0, request.Tag, request.Name, request.ContentType, request.Version));

            return true;
        }
    }
}

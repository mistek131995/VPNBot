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

            using (var client = new SftpClient(connectionInfo))
            {
                try
                {
                    client.Connect();

                    using var stream = new MemoryStream(request.Data);

                    //FileBasePath - /home/build/wwwroot/files
                    client.UploadFile(stream, $"{settings.FileBasePath}/{request.Tag}/{request.Name}", null);

                    client.Disconnect();
                }catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            }

            return true;
        }
    }
}

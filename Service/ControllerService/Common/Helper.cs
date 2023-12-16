using Core.Model.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QRCoder;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Service.ControllerService.Common
{
    public class Helper
    {
        public static byte[] GetAccessQrCode(Access access, string ipAddress)
        {
            var accessString = $"vless://{access.Guid}@{ipAddress}:{access.Port}?type={access.Network}&security={access.Security}&fp={access.Fingerprint}&pbk={access.PublicKey}&sni={access.ServerName}&sid={access.ShortId}&spx=%2F#{access.AccessName}";

            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(accessString, QRCodeGenerator.ECCLevel.H);
            var qRCode = new PngByteQRCode(qrCodeData);
            return qRCode.GetGraphic(20);
        }

        public static string CreateJwtToken(User user, IConfiguration configuration)
        {
            var claims = new List<Claim>()
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString()),
                new Claim("role", ((int)user.Role).ToString())
            };

            var jwtOptions = configuration.GetSection("JWT");

            var jwt = new JwtSecurityToken(
                    issuer: jwtOptions["ISSUER"],
                    audience: jwtOptions["AUDIENCE"],
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)), // время действия 1 день
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions["KEY"])), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}

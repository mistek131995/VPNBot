using Database.Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;
using Telegram.Bot.Extensions.LoginWidget;
using VpnBotApi.Common;

namespace VpnBotApi.ControllerHandler.Auth
{
    public class Handler(IRepositoryProvider repositoryProvider, IConfiguration configuration) : IControllerHandler<Query,string>
    {
        private readonly IRepositoryProvider repositoryProvider = repositoryProvider;

        private HMACSHA256 _hmac;
        private static readonly DateTime _unixStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public long AllowedTimeOffset = 30;

        public async Task<string> HandlingAsync(Query query)
        {
            var telegramBotToken = configuration.GetSection("TelegramBotToken").Value;

            //var fields = new SortedDictionary<string, string>()
            //{
            //    { "auth_date", query.AuthDate.ToString() },
            //    { "first_name", query.FirstName },
            //    { "id", query.TelegramUserId.ToString() },
            //    { "username", query.UserName },
            //    { "hash", query.Hash }
            //};

            //using (SHA256 sha256 = SHA256.Create())
            //{
            //    _hmac = new HMACSHA256(sha256.ComputeHash(Encoding.ASCII.GetBytes(telegramBotToken)));

            //    var test = CheckAuthorization(fields);
            //}

            return string.Empty;
        }

        private Authorization CheckAuthorization(SortedDictionary<string, string> fields)
        {
            if (fields == null) throw new ArgumentNullException(nameof(fields));
            if (fields.Count < 3) return Authorization.MissingFields;

            if (!fields.ContainsKey(Field.Id) ||
                !fields.TryGetValue(Field.AuthDate, out string authDate) ||
                !fields.TryGetValue(Field.Hash, out string hash)
            ) return Authorization.MissingFields;

            if (hash.Length != 64) return Authorization.InvalidHash;

            if (!long.TryParse(authDate, out long timestamp))
                return Authorization.InvalidAuthDateFormat;

            if (Math.Abs(DateTime.UtcNow.Subtract(_unixStart).TotalSeconds - timestamp) > AllowedTimeOffset)
                return Authorization.TooOld;

            fields.Remove(Field.Hash);
            StringBuilder dataStringBuilder = new StringBuilder(256);
            foreach (var field in fields)
            {
                if (!string.IsNullOrEmpty(field.Value))
                {
                    dataStringBuilder.Append(field.Key);
                    dataStringBuilder.Append('=');
                    dataStringBuilder.Append(field.Value);
                    dataStringBuilder.Append('\n');
                }
            }
            dataStringBuilder.Length -= 1; // Remove the last \n

            byte[] signature = _hmac.ComputeHash(Encoding.UTF8.GetBytes(dataStringBuilder.ToString()));

            for (int i = 0; i < signature.Length; i++)
            {
                if (hash[i * 2] != 87 + (signature[i] >> 4) + ((((signature[i] >> 4) - 10) >> 31) & -39)) return Authorization.InvalidHash;
                if (hash[i * 2 + 1] != 87 + (signature[i] & 0xF) + ((((signature[i] & 0xF) - 10) >> 31) & -39)) return Authorization.InvalidHash;
            }

            return Authorization.Valid;
        }

        public enum Authorization
        {
            InvalidHash,
            MissingFields,
            InvalidAuthDateFormat,
            TooOld,
            Valid
        }

        private static class Field
        {
            public const string AuthDate = "auth_date";
            public const string Id = "id";
            public const string Hash = "hash";
        }
    }
}

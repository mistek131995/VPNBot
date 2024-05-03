using Application.ControllerService.Common;
using Infrastructure.Migrations;
using MD5Hash;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.Payment.ePayCore.GetLink
{
    internal class Service : IControllerService<Request, string>
    {
        public async Task<string> HandlingAsync(Request request)
        {

            var orderId = "test-" + new Random().Next(0, 99900).ToString();

            var dictionary = new Dictionary<string, string>
            {
                { "epc_merchant_id", "103783" },
                { "epc_commission", "1" },
                { "epc_amount", "5" },
                { "epc_currency_code", "USD" },
                { "epc_order_id", orderId },
                { "epc_success_url", "https://lockvpn.me/" },
                { "epc_cancel_url", "https://lockvpn.me/" },
                { "epc_status_url", "https://lockvpn.me/" },
                { "epc_sign", GetSha256Hash($"103783:5:USD:{orderId}:24041986") },
            };

            //var client = new HttpClient();
            //var response = await client.PostAsync("https://api.epaycore.com/checkout/form", new FormUrlEncodedContent(dictionary));

            //var paymentPage = await response.Content.ReadAsStringAsync();

            //var file = await response.Content.ReadAsStreamAsync();

            //byte[] buffer = new byte[16 * 1024];
            //using var memoryStream = new MemoryStream();
            //int read;
            //while ((read = file.Read(buffer, 0, buffer.Length)) > 0)
            //{
            //    memoryStream.Write(buffer, 0, read);
            //}


            return string.Join("&", dictionary.Select(x => $"{x.Key}={x.Value}"));
        }

        public static string GetSha256Hash(string inputString)
        {
            // Преобразование строки в массив байтов
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);

            // Создание объекта SHA256
            using (SHA256 sha256 = SHA256.Create())
            {
                // Вычисление хеша
                byte[] hashBytes = sha256.ComputeHash(bytes);

                // Преобразование байтов хеша в строку
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}

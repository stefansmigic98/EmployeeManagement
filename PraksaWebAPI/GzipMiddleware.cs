using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PraksaWebAPI
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class GzipMiddleware
    {
        private readonly RequestDelegate _next;

        public GzipMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.Keys.Contains("Accept-Encoding"))
            {

                var request = context.Request;
                
                var stream = request.Body;// currently holds the original stream                    
                var originalContent = new StreamReader(stream).ReadToEndAsync().GetAwaiter().GetResult();
                var notModified = true;
                try
                {
                   
                    var decrypyed = DecryptString("b14ca5898a4e4133bbce2ea2315a1916", originalContent);
                    var requestData = Encoding.UTF8.GetBytes(decrypyed);
                    stream = new MemoryStream(requestData);
                    notModified = false;
                    
                }
                catch
                {
                    
                }
                if (notModified)
                {
                    
                    var requestData = Encoding.UTF8.GetBytes(originalContent);
                    stream = new MemoryStream(requestData);
                }

                request.Body = stream;
            }

            await _next.Invoke(context);
        }

        public static string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (System.Security.Cryptography.Aes aes = System.Security.Cryptography.Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class GzipMiddlewareExtensions
    {
        public static IApplicationBuilder UseGzipMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GzipMiddleware>();
        }
    }

}

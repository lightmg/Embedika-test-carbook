using System;
using System.IO;
using System.Threading.Tasks;
using CarsCatalog.Helpers;
using Microsoft.AspNetCore.Http;

namespace CarsCatalog.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly string logsFolderPath;
        private readonly RequestDelegate next;

        public RequestResponseLoggingMiddleware(RequestDelegate next, string logsFolderPath)
        {
            this.next = next;
            this.logsFolderPath = logsFolderPath;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.EnableBuffering();
            var formattedRequest = "Request: \n" + await ReadStreamAndResetReader(context.Request.Body);
            SaveLogToFile(formattedRequest);

            var originalResponseBodyStream = context.Response.Body;
            using var responseStream = new MemoryStream();
            context.Response.Body = responseStream;

            await next.Invoke(context);

            var formattedResponse = "Response: \n" + $"{context.Response.StatusCode}: " +
                                    await ReadStreamAndResetReader(responseStream);
            await responseStream.CopyToAsync(originalResponseBodyStream);
            context.Response.Body = originalResponseBodyStream;

            SaveLogToFile(formattedResponse);
        }

        private void SaveLogToFile(string log)
        {
            var filePath = $"{logsFolderPath}/{DateTime.Now:dd-MM-yyyy}.log";
            using var sw = PathHelpers.OpenWrite(filePath);
            sw.WriteLine(log);
        }

        private static async Task<string> ReadStreamAndResetReader(Stream body)
        {
            var sr = new StreamReader(body);
            var result = await sr.ReadToEndAsync();
            body.Seek(0, SeekOrigin.Begin);
            return result;
        }
    }
}
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using CarsCatalog.Helpers;
using Microsoft.AspNetCore.Http;

namespace CarsCatalog.Middleware
{
    public class HttpStatisticsMiddleware : IDisposable
    {
        private readonly StreamWriter fileStream;
        private readonly RequestDelegate next;

        public HttpStatisticsMiddleware(RequestDelegate next, string logFolder)
        {
            this.next = next;
            var now = DateTime.Now;
            fileStream = PathHelpers.OpenWrite(Path.Combine(logFolder, $"{now:dd-MM-yyyy}.stat"));
            WriteBeginOfSessionAsync(now);
        }

        public void Dispose()
        {
            fileStream?.Dispose();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var record = new Record
            {
                RequestTime = DateTime.Now,
                RequestPath = context.Request.Path
            };

            var stopwatch = new Stopwatch();
            await next.Invoke(context);
            stopwatch.Stop();

            record.ResponseAfter = stopwatch.Elapsed;
            record.ResponseHttpCode = context.Response.StatusCode;

            WriteRecordAsync(record);
        }

        private async void WriteBeginOfSessionAsync(DateTime startTime)
        {
            await fileStream.WriteLineAsync($"Session started at {startTime}");
            await fileStream.FlushAsync();
        }

        private async void WriteRecordAsync(Record record)
        {
            await fileStream.WriteLineAsync($"\t\t[{record.RequestTime}]: {record.RequestPath}, " +
                                            $"response [{record.ResponseHttpCode}] " +
                                            $"after {record.ResponseAfter.TotalMilliseconds} milliseconds");
            await fileStream.FlushAsync();
        }

        private class Record
        {
            public DateTime RequestTime { get; set; }
            public string RequestPath { get; set; }
            public TimeSpan ResponseAfter { get; set; }
            public int ResponseHttpCode { get; set; }
        }
    }
}
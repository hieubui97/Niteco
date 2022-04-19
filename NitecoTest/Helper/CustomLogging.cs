using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace NitecoTest.Helper
{
    public class CustomLogging
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerFactory _loggerFactory;
        private readonly string _pathLog = $"app-log.txt"; //IF *NIX OS CONVERT \\ TO //
        public CustomLogging(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _loggerFactory = loggerFactory;
            //INIT FILE LOG
        }
        public async Task InvokeAsync(HttpContext context)
        {
            //IOHelper.CreateFileObject(PATH_LOG);

            var logger = _loggerFactory.CreateLogger<CustomLogging>();
            try
            {
                //logger.LogInformation("Starting logging");

                //writen log
                var message = new MessageLogging
                {
                    Date = DateTime.Now.ToShortDateString(),
                    Time = DateTime.Now.ToLongTimeString(),
                    Category = "HTTP_REQUEST",
                    Message = string.Format($"IP: {context.Request.Host.Host} - {context.Request.Method} - {context.Request.Path}  - {context.Request.Body}")
                };
                logger.LogInformation(JsonConvert.SerializeObject(message));

                //Save Data
                //logger.LogInformation("Saved log");

                IoHelper.WriteToTextFile(_pathLog, JsonConvert.SerializeObject(message), true);
                await _next(context);
            }
            catch (Exception ex)
            {
                logger.LogError($"Exeception throw: {ex.Message}");
            }
        }
    }


    // Custom message log
    public class MessageLogging
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public string Category { get; set; }
        public object Message { get; set; }
    }
}

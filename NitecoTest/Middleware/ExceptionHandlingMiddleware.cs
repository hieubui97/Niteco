using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NitecoTest.Helper;

namespace NitecoTest.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private static RequestDelegate _requestDelegate;
        private static readonly string _pathLog = $"app-log.txt"; //IF *NIX OS CONVERT \\ TO //
        public ExceptionHandlingMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                HandleException(context, ex);
            }
        }
        private static void HandleException(HttpContext context, Exception ex)
        {
            var errorMessage = JsonConvert.SerializeObject(new { Date= DateTime.Now.ToShortDateString(), Time = DateTime.Now.ToLongTimeString(), Category = "GENERAL EXCEPTION", Message = ex.Message });

            IoHelper.WriteToTextFile(_pathLog, errorMessage, true);
        }
    }
}

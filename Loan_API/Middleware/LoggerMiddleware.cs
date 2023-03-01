using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

namespace Loan_API.Middleware
{
  public class LoggerMiddleware
  {

    private readonly RequestDelegate _next;
    private readonly ILogger _logger;
    public LoggerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
      _next = next;
      _logger = loggerFactory.CreateLogger<LoggerMiddleware>();
    }
    public async Task InvokeAsync(HttpContext context)
    {

      try
      {
        await _next(context);
      }
      catch (Exception ex)
      {
        _logger.LogError($"Something went wrong: {ex.Message}");

      }
    }
  }
}

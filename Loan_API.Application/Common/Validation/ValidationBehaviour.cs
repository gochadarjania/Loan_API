using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Loan_API.Application.Common.Validation
{
  public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
  {
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators, IHttpContextAccessor httpContextAccessor)
    {
      _validators = validators;
      _httpContextAccessor = httpContextAccessor;
    }
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
      try
      {
        var validationResults = _validators.Select(v => v.Validate(request));

        var failures = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Any())
        {
          var errorList = failures.Select(f => new Error(f.PropertyName, f.ErrorMessage)).ToList();
          var errorResponse = new ErrorResponse("One or more validation errors occurred.", errorList);

          _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
          await _httpContextAccessor.HttpContext.Response.WriteAsJsonAsync(errorResponse);

          return default;
        }

        return await next();
      }
      catch (Exception ex)
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await _httpContextAccessor.HttpContext.Response.WriteAsJsonAsync(new ErrorResponse(ex.Message));
        return default;
      }
    }
    public class Error
    {
      public string PropertyName { get; set; }
      public string ErrorMessage { get; set; }

      public Error(string propertyName, string errorMessage)
      {
        PropertyName = propertyName;
        ErrorMessage = errorMessage;
      }
    }

    public class ErrorResponse
    {
      public ErrorResponse(string message, List<Error> errors = null)
      {
        Message = message;
        Errors = errors ?? new List<Error>();
      }

      public string Message { get; set; }
      public List<Error> Errors { get; set; }
    }
  }
}

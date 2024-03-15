using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ValidationException = FluentValidation.ValidationException;

namespace Core.Extensions
{
	public class ExceptionMiddleware
	{
		private RequestDelegate _next;

		public ExceptionMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (System.Exception e)
			{
				await HandleExceptionAsync(context, e);
			}
		}

		private Task HandleExceptionAsync(HttpContext context, Exception e)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			string message = "Internal Server Error";
			IEnumerable<ValidationFailure> errors;
			if (e.GetType() == typeof(ValidationException))
			{
				message = e.Message;
				errors = ((ValidationException)e).Errors;
				context.Response.StatusCode = 400;
				return context.Response.WriteAsync(new ValidationErrorDetails
				{
					Errors = errors,
					Message = message,
					StatusCode = 400
				}.ToString());
			}

			return context.Response.WriteAsync(new ErrorDetails
			{
				StatusCode = context.Response.StatusCode,
				Message = message
			}.ToString());
		}
	}
}

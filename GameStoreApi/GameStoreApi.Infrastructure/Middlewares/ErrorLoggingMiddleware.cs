using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Http;
using GameStoreApi.Infrastructure.Middlewares.Interfaces;
using GameStoreApi.Data.Errors;
using GameStoreApi.Infrastructure.Middlewares.Models;
using GameStoreApi.Infrastructure.DomainValidation.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GameStoreApi.Infrastructure.Middlewares
{
	public class ErrorLoggingMiddleware
	{
		private readonly RequestDelegate next;

		public ErrorLoggingMiddleware(RequestDelegate next)
		{
			this.next = next;
		}

		public async Task Invoke(HttpContext context, IErrorService errorService)
		{
			try
			{
				await next(context);
			}
			catch (Exception error)
			{
				var response = context.Response;

				response.ContentType = "application/json";
				var responseModel = ApiResponse<string>.Fail(error.Message);
				response.StatusCode = error switch
				{
					SomeException => (int)HttpStatusCode.BadRequest,
					KeyNotFoundException => (int)HttpStatusCode.NotFound,
					_ => (int)HttpStatusCode.InternalServerError,
				};
				responseModel.StatusCode = response.StatusCode;

				var newError = new Error()
				{
					StatusCode = responseModel.StatusCode,
					Message = error is DomainErrorException customException ? customException.ErrorMessages[0].DomainErrorCode.ToString() : error.Message,
					Verb = response.HttpContext.Request.Method,
					Url = response.HttpContext.Request.Path
				};

				await errorService.AddError(newError);

				var statusCode = error is DomainErrorException ? HttpStatusCode.UnprocessableEntity : HttpStatusCode.InternalServerError;
				await HandleExceptionAsync(context, statusCode, error);
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, object content)
		{
			context.Response.StatusCode = (int)statusCode;
			context.Response.ContentType = "application/json";
			context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
			var responseText = JsonConvert.SerializeObject(content, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

			return context.Response.WriteAsync(responseText);
		}
	}
}

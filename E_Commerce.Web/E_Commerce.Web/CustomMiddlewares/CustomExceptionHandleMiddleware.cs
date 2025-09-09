using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Http;
using Shared_DTOs_.ErrorModels;
using System.Net;
using System.Text.Json;

namespace E_Commerce.Web.CustomMiddlewares
{
    public class CustomExceptionHandleMiddleware
    {

        //1.Set Status Code For response

        //attributes
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandleMiddleware> _logger;

        public CustomExceptionHandleMiddleware(RequestDelegate next, ILogger<CustomExceptionHandleMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async  Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
                HandleNotFoundEndPointAsync(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            #region Change Formate response
            #region //1.Set Status Code For Response: 
            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError,
            };
            //httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            #endregion

            #region 2.Set content type for response
            httpContext.Response.ContentType = "application/json";
            #endregion

            #region 3.Make Response Object
            var Response = new ErrorToReturn()
            {
                StatusCode = httpContext.Response.StatusCode,
                ErrorMessage = ex.Message
            };
            #endregion

            #region 4.Return Response Object As Json
            //1. first way
            var ResponseJson = JsonSerializer.Serialize(Response);
            await httpContext.Response.WriteAsync(ResponseJson);
            //2. Second Way
            //await httpContext.Response.WriteAsJsonAsync(Response);
            #endregion
            #endregion
        }

        private static void HandleNotFoundEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var respose = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"The EndPoint {httpContext.Request.Path} is not Found",
                };
            }
        }

        
    }
}

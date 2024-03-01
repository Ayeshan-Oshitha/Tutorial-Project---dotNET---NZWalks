using System.Net;

namespace NZWalksAPI.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly RequestDelegate _requestDelegate;

        public ExceptionHandlerMiddleware( ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate requestDelegate)
        {
            _logger = logger;
            _requestDelegate = requestDelegate;
        }


        public async Task InvokeAsync(HttpContext httpContext)
        {

            try
            {
                await _requestDelegate(httpContext);
            }
            catch (Exception ex)
            {

                var errorId = Guid.NewGuid();

                // Log This Exception
                _logger.LogError(ex, $"{errorId} : {ex.Message}");

                // Return A Custom Error response
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something went wrong! We are looking into resolving this"
                };

                await httpContext.Response.WriteAsJsonAsync( error );
            }
        }
    }
}

using BancoPichincha.API.Viewmodel;
using Microsoft.AspNetCore.Diagnostics;

namespace BancoPichincha.API.Middleware
{
    public static class ExceptionHandler
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if(contextFeature != null)
                    {
                        await context.Response.WriteAsync(new Error
                        {
                            StatusCode = context.Response.StatusCode,
                            MensajeError = "Internal Server Error, para más información revisar los Logs"
                        }.ToString());
                    }
                });
            });
        }
    }
}

namespace AutoresApplication2.Middlewares
{


    public static class LogearRespuestaHttpMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogearRespuestaHttp(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LogearRespuestaHttpMiddleware>();
        }
    }


    public class LogearRespuestaHttpMiddleware
    {
        private readonly RequestDelegate siguiente;
        private readonly ILogger<LogDefineOptions> logger;

        public LogearRespuestaHttpMiddleware(RequestDelegate siguiente, ILogger<LogDefineOptions> logger )
        {
            this.siguiente = siguiente;
            this.logger = logger;
        }

        //invoke 

        public async Task InvokeAsync(HttpContext context)
        {
            using (var memorystream = new MemoryStream())
            {
                var cuerpoOriginal = context.Response.Body;
                context.Response.Body = memorystream;

                await siguiente(context);

                memorystream.Seek(0, SeekOrigin.Begin);
                string respuesta = new StreamReader(memorystream).ReadToEnd();

                memorystream.Seek(0, SeekOrigin.Begin);

                await memorystream.CopyToAsync(cuerpoOriginal);

                context.Response.Body = cuerpoOriginal;

                logger.LogInformation(respuesta);
            }
        }
    }
}

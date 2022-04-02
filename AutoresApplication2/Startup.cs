using AutoresApplication2.Filtros;
using AutoresApplication2.Middlewares;
using AutoresApplication2.Servicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace AutoresApplication2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        { 
            services.AddControllers(opciones =>
              {
                  opciones.Filters.Add(typeof(FiltroDeExcepcion));                
              }
            ).AddJsonOptions(x => 
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            services.AddEndpointsApiExplorer();

            services.AddTransient<IServicio, ServicioA>();
            services.AddScoped<ServicioScoped>();
            services.AddTransient<ServicioTrassient>();
            services.AddSingleton<ServicioSingleton>();

            services.AddTransient<MiFiltroDeAccion>();
            services.AddHostedService<EscribirEnArchivo>();

            services.AddHttpContextAccessor();

            services.AddResponseCaching();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));

            

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {

            app.UseLogearRespuestaHttp();

            app.Map("/ruta1", app =>
            {
                app.Run(async context =>
                {
                    await context.Response.WriteAsync("Estoy interceptando la tuberia");
                });
            });

           

            if (env.IsDevelopment())
            {

            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseResponseCaching();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            endpoints.MapControllers());
        }

    }
}

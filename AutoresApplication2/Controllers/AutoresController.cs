using AutoresApplication2.Entidades;
using AutoresApplication2.Filtros;
using AutoresApplication2.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoresApplication2.Controllers
{


    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IServicio servicio;
        private readonly ServicioTrassient servicioTrassient;
        private readonly ServicioScoped servicioScoped;
        private readonly ServicioSingleton servicioSingleton;
        private readonly ILogger<AutoresController> logger;

        public AutoresController(ApplicationDbContext context, IServicio servicio, ServicioTrassient servicioTrassient,
            ServicioScoped servicioScoped, ServicioSingleton servicioSingleton, ILogger<AutoresController> logger)
        {
            this.context = context;
            this.servicio = servicio;
            this.servicioTrassient = servicioTrassient;
            this.servicioScoped = servicioScoped;
            this.servicioSingleton = servicioSingleton;
            this.logger = logger;
        }


        [HttpGet("Guid")]
        //[ResponseCache(Duration = 10 )]
        //[Authorize]
        [ServiceFilter(typeof(MiFiltroDeAccion))]
        public ActionResult ObtenerGuids()
        {
            return Ok(new
            {
                AutoresController_Transient = servicioTrassient.Guid,
                ServicioA_Transient = servicio.ObtenerServicioTransient(),
                AutoresController_Scoped = servicioScoped.Guid,
                ServicioA_Scoped = servicio.ObtenerServicioScoped(),
                AutoresController_Singleton = servicioSingleton.Guid,
                ServicioA_Singleton = servicio.ObtenerServicioSingleton()
            });
        }

        [HttpGet()]
        [ServiceFilter(typeof(MiFiltroDeAccion))]
        public async Task<List<Autor>> Get()
        {
            

            logger.LogInformation("Estamos Obteniendo los autores");
            logger.LogWarning("este es un mensaje de prueba");

            var entidades = await context.Autores.ToListAsync();

            return entidades;
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<Autor>> Get(string nombre)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));


            if (autor == null)
            {
                return NotFound("no encontrado");
            }

            return autor;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] Autor autor)
        {
            context.Add(autor);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Autor autor)
        {

            if (autor.Id != id)
            {
                return BadRequest();

            }

            var existe = await context.Autores.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Update(autor);
            await context.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Autores.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Autor() { Id = id });

            await context.SaveChangesAsync();

            return NoContent();
        }

    }
}

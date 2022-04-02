using AutoresApplication2.Entidades;
using AutoresApplication2.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoresApplication2.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IServicio servicio;

        public LibrosController(ApplicationDbContext context, IServicio servicio )
        {
            this.context = context;
            this.servicio = servicio;
        }


        [HttpGet]
        public async Task<List<Libro>> Get()
        {
            var entidades = await context.Libros.Include(x => x.Autor).ToListAsync();

            return entidades;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Libro>> Get(int id)
        {
            var entidades = await context.Libros.Include(x => x.Autor).FirstOrDefaultAsync(x => x.Id == id);

            if (entidades == null)
            {
                return NotFound();
            }

            return entidades;
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromForm] Libro libro)
        {
            var existeAutor = await context.Autores.AnyAsync(x => x.Id == libro.AutorId);

            if (!existeAutor)
            {
                return BadRequest($"No existe el autor de Id: {libro.AutorId}");

            }

            context.Add(libro);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm]Libro libro)
        {
            if (libro.Id != id)
            {
                return NotFound();
            }

            var entidades = await context.Libros.AnyAsync(x => x.Id == id);

            if (!entidades)
            {
                return BadRequest();
            }

            context.Update(libro);
            await context.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Libros.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Libro() { Id = id});

            await context.SaveChangesAsync();

            return NoContent();
        }

    }
}

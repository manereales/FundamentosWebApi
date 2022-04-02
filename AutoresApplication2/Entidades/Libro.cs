using System.ComponentModel.DataAnnotations;

namespace AutoresApplication2.Entidades
{
    public class Libro
    {
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        public int AutorId { get; set; }
        public Autor Autor { get; set; }
    }
}

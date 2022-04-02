using AutoresApplication2.Validations;
using System.ComponentModel.DataAnnotations;

namespace AutoresApplication2.Entidades
{
    public class Autor
    {

        public int Id { get; set; }

        [Required]
        [StringLength(120)]
        [PimerLetraMayuscula]
        public string Nombre { get; set; }

        public List<Libro> Libros { get; set; }

    }
}

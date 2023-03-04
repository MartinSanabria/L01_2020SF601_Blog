using System.ComponentModel.DataAnnotations;

namespace L01_2020SF601.Models
{
    public class Publicacion
    {
        [Key]
        public int publicacionId { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public int usuarioId { get; set; }

    }
}

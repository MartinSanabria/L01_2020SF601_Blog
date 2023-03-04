using System.ComponentModel.DataAnnotations;

namespace L01_2020SF601.Models
{
    public class Comentario
    {
        [Key]
        public int comentarioId { get; set; }
        public int publicacionId { get; set; }
        public string comentario { get; set; }
        public int usuarioId { get; set; }

    }
}

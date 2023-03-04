using Microsoft.EntityFrameworkCore;

namespace L01_2020SF601.Models
{
    public class blogContext : DbContext
    {
        public blogContext(DbContextOptions<blogContext> dbContext):base(dbContext) { 
        }


        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Publicacion> publicaciones { get; set;}
        public DbSet<Comentario> comentarios { get; set; }

    }
}

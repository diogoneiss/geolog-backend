using GeoLogBackend.Dominio;
using Microsoft.EntityFrameworkCore;

namespace GeoLogBackend.GeoLogBackend.Infraestrutura
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}

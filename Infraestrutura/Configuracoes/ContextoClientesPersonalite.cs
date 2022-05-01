using Entidades.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Configuracoes
{
    public class ContextoClientesPersonalite : DbContext
    {
        public ContextoClientesPersonalite(DbContextOptions<ContextoClientesPersonalite> opcoes) : base(opcoes)
        {
            this.Database.EnsureCreated();
            this.Database.Migrate();
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>().Navigation(cliente => cliente.Endereco)
            .AutoInclude();
        }
    }
}

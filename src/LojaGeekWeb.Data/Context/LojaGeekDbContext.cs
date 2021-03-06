using LojaGeek.App.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LojaGeekWeb.Data.Context
{
    public class LojaGeekDbContext : DbContext
    {
        public LojaGeekDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LojaGeekDbContext).Assembly);

            //Impedindo a realização do delete cascade pela classes pai
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
    }
}

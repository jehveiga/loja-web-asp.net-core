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

        // O Método abaixo é chamado na criação do banco de dados, trazendo as configurações mapeadas ex: Fluent Api   
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurando o mapeamento dos Mappings usando a herança 'IEntityTypeConfiguration' das classes configuradas no DbSet 
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LojaGeekDbContext).Assembly);

            //Impedindo a realização do delete cascade pela classes pai, exemplo: Deletando Fornecedor
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
    }
}

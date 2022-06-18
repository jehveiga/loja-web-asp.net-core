using AppLojaWebMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaGeek.Data.Context
{
    internal class LojaGeekDbContext : DbContext
    {
        public LojaGeekDbContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
    }
}

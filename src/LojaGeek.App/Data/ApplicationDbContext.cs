using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LojaGeek.App.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ViewModels.FornecedorViewModel> FornecedorViewModel { get; set; }
        public DbSet<ViewModels.ProdutoViewModel> ProdutoViewModel { get; set; }
        public DbSet<ViewModels.EnderecoViewModel> EnderecoViewModel { get; set; }
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using LojaGeek.App.ViewModels;

namespace LojaGeek.App.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<LojaGeek.App.ViewModels.FornecedorViewModel> FornecedorViewModel { get; set; }
        public DbSet<LojaGeek.App.ViewModels.ProdutoViewModel> ProdutoViewModel { get; set; }
    }
}

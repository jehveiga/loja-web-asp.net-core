using LojaGeek.App.Models;
using LojaGeek.Business.Interfaces;
using LojaGeekWeb.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaGeekWeb.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(LojaGeekDbContext context) : base(context)
        {
        }

        public async Task<Produto> ObterProdutosFornecedor(Guid id)
        {
            return await Db.Produtos
                .AsNoTracking() // Desativando o Change Tracking do EntityFramework para ter uma performace melhor
                .Include(f => f.Fornecedor) //Incluindo os dados do fornecedor com INNER JOIN para trazer na consulta a relação entre o produto : fornecedor
                .FirstOrDefaultAsync(p => p.Id == id); //Trazendo os dados do produto que foi passado pelo id como parametro
        }

        public async Task<IEnumerable<Produto>> ObterProdutosFornecedores()
        {
            return await Db.Produtos
                .AsNoTracking() // Desativando o Change Tracking do EntityFramework para ter uma performace melhor
                .Include(f => f.Fornecedor) //Incluindo os dados do fornecedor com INNER JOIN para trazer na consulta a relação entre o produto : fornecedor
                .OrderBy(p => p.Nome) // Ordenando por nome dos produtos 
                .ToListAsync(); // Executando consulta trazendo a lista de produtos
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId)
        {
            return await Buscar(p => p.FornecedorId == fornecedorId); //Usando o método Buscar() da classe generica Repository, trazendo os Produto pelo fornecedorId
        }
    }
}

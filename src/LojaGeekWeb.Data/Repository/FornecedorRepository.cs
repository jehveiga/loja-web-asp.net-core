using LojaGeek.App.Models;
using LojaGeek.Business.Interfaces;
using LojaGeekWeb.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LojaGeekWeb.Data.Repository
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(LojaGeekDbContext context) : base(context)
        {
        }

        public async Task<Fornecedor> ObterFornecedorEndereco(Guid id)
        {
            return await Db.Fornecedores
                .AsNoTracking() // Desativando o Change Tracking do EntityFramework para ter uma performace melhor
                .Include(e => e.Endereco) //Incluindo os dados do endereco com INNER JOIN para trazer na consulta a relação entre o fornecedor : endereco
                .FirstOrDefaultAsync(c => c.Id == id); //Trazendo os dados do fornecedor que foi passado pelo id como parametro
        }

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id)
        {
            return await Db.Fornecedores
                .AsNoTracking() // Desativando o Change Tracking do EntityFramework para ter uma performace melhor
                .Include(p => p.Produtos) //Incluindo os dados do endereco com INNER JOIN para trazer na consulta a relação entre o fornecedor : produtos
                .Include(e => e.Endereco) //Incluindo os dados do endereco com INNER JOIN para trazer na consulta a relação entre o fornecedor : endereco
                .FirstOrDefaultAsync(c => c.Id == id); //Trazendo os dados do fornecedor que foi passado pelo id como parametro
        }
    }
}

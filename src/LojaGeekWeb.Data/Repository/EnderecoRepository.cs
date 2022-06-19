using LojaGeek.App.Models;
using LojaGeek.Business.Interfaces;
using LojaGeekWeb.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LojaGeekWeb.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(LojaGeekDbContext context) : base(context)
        {
        }

        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
        {
            return await Db.Enderecos
                .AsNoTracking() // Desativando o Change Tracking do EntityFramework para ter uma performace melhor
                .FirstOrDefaultAsync(f => f.FornecedorId == fornecedorId); //Trazendo os dados do endereco que foi passado pelo id como parametro
        }
    }
}

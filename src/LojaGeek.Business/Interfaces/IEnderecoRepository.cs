using LojaGeek.App.Models;
using System;
using System.Threading.Tasks;

namespace LojaGeek.Business.Interfaces
{
    public interface IEnderecoRepository : IRepository<Endereco> //extend especializando para classe endereço
    {
        Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId); // Obtem o endereco do fornecedor com o id passado pelo parametro
    }
}

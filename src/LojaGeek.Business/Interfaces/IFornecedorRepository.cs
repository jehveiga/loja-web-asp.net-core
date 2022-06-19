using LojaGeek.App.Models;
using System;
using System.Threading.Tasks;

namespace LojaGeek.Business.Interfaces
{
    public interface IFornecedorRepository : IRepository<Fornecedor>
    {
        Task<Fornecedor> ObterFornecedorEndereco(Guid id); // Obtem um fornecedor e o endereco passando o id
        Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id); // Obtem tanto fornecedor a lista de produtos e o endereco do fornecedor passando o id

    }
}

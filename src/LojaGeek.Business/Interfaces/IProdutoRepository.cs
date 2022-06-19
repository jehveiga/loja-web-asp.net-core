using LojaGeek.App.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LojaGeek.Business.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId); // Metodo que retorna uma lista de produtos relacionado ao fornecedor passado pelo parametro
        Task<IEnumerable<Produto>> ObterProdutosFornecedores(); // Retorna uma lista de produtos e os fonecedores daquele produto
        Task<Produto> ObterProdutosFornecedor(Guid id); // Retorna o produto e o fornecedor que foi passado por parametro
    }
}

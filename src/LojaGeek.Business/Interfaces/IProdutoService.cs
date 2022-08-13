using LojaGeek.App.Models;
using System;
using System.Threading.Tasks;

namespace LojaGeek.Business.Interfaces
{
    public interface IProdutoService : IDisposable
    {
        Task Adicionar(Produto produto);

        Task Atualizar(Produto produto);

        Task Remover(Guid id);
    }
}

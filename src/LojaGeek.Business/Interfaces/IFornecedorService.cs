using LojaGeek.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaGeek.Business.Interfaces
{
    public interface IFornecedorService
    {
        Task Adicionar(Fornecedor fornecedor);

        Task Atualizar(Fornecedor fornecedor);

        Task Remover(Guid id);

        Task AtualizarEndereco(Endereco endereco);

    }
}

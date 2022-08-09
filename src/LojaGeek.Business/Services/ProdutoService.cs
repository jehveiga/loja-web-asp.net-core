using LojaGeek.App.Models;
using LojaGeek.Business.Interfaces;
using LojaGeek.Business.Models.Validations;
using System;
using System.Threading.Tasks;

namespace LojaGeek.Business.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {

        public async Task Adicionar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

        }

        public async Task Atualizar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;
        }

        public async Task Remover(Guid id)
        {
            throw new NotImplementedException();
        }

    }


}

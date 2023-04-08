using LojaGeek.App.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LojaGeek.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity // Declaração de interface do tipo repositório genérico, obrigatório ser filha de Entity
    {
        Task Adicionar(TEntity entity); // Adiciona uma entidade desde que herde de Entity, retorno void
        Task<TEntity> ObterPorId(Guid id); // Obtem um <TEntity> pelo id, retorno um objeto Entity
        Task<List<TEntity>> ObterTodos(); // retorna uma lista de <TEntity>, retorno uma List de obj Entity
        Task Atualizar(TEntity entity); // atualiza um objeto <TEntity> passando um objeto do mesmo tipo, retorno void
        Task Remover(Guid id); // Remove um objeto passando o id como filtro, retorno void
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate); // Metodo para buscar qualquer entidade por qualquer parametro
                                                                                      // , retorno uma List Enumerable de obj Entity
        Task<int> SaveChanges(); // Abstração de SaveChanges, retorna um tipo int que são a quantidade de linhas afetadas

    }
}

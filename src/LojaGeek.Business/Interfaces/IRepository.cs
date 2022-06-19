using LojaGeek.App.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LojaGeek.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity // Declaração de interface do tipo repositório genérico
    {
        Task Adicionar(TEntity entity); // Adiciona uma entidade desde que herde de Entity
        Task<TEntity> ObterPorId(Guid id); // Obtem um <TEntity> pelo id
        Task<List<TEntity>> ObterTodos(); // retorna uma lista de <TEntity> 
        Task Atualizar(TEntity entity); // atualiza um objeto <TEntity> passando um objeto do mesmo tipo 
        Task Remover(Guid id); // Remove um objeto passando o id como filtro
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate); // Metodo para buscar qualquer entidade por qualquer parametro
        Task<int> SaveChanges(); // Abstração de SaveChanges, retorna um tipo int que são a quantidade de linhas afetadas

    }
}

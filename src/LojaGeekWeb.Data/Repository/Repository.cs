using LojaGeek.App.Models;
using LojaGeek.Business.Interfaces;
using LojaGeekWeb.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LojaGeekWeb.Data.Repository
{
    // Implementado a classe generica abstrata herdando da interface generica e especificando que a classe Repository é filha de Entity
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly LojaGeekDbContext Db; // Sendo protect dar acesso para herança de Repository

        protected readonly DbSet<TEntity> DbSet;

        protected Repository(LojaGeekDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>(); // Atalho para usar o DbSet para ficar menos verboso a chamada nos metodos a serem usados
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync(); // AsNotracking ajuda com a performance de busca no banco desabilitando o Change Tracking do C#
        }

        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }
        public virtual async Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remover(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();

        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}

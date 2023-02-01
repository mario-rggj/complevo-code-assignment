using System.Linq.Expressions;
using Complevo.Domain.Infrastructure.Repositories;
using Complevo.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Complevo.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
  protected readonly ApiContext Context;

  public Repository(ApiContext context)
  {
    Context = context;
  }

  public async Task<IEnumerable<TEntity>> GetAll(int pageIndex, int pageSize)
  {
    return await Context.Set<TEntity>().Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
  }

  public async Task<TEntity> Get(int id)
  {
    return (await Context.Set<TEntity>().FindAsync(id))!;
  }

  public async Task<bool> Exist(int id)
  {
    return (await Get(id)) is not null;
  }

  public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
  {
    return await Context.Set<TEntity>().Where(predicate).ToListAsync();
  }

  public void Add(TEntity entity)
  {
    Context.Set<TEntity>().Add(entity);
  }

  public void AddRange(IEnumerable<TEntity> entities)
  {
    Context.Set<TEntity>().AddRange(entities);
  }

  public void Remove(TEntity entity)
  {
    Context.Set<TEntity>().Remove(entity);
  }

  public void RemoveRange(IEnumerable<TEntity> entities)
  {
    Context.Set<TEntity>().RemoveRange(entities);
  }

  public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
  {
    return Context.Set<TEntity>().Entry(entity);
  }
}
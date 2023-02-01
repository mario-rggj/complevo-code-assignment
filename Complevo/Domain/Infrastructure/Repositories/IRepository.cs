using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Complevo.Domain.Infrastructure.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
  Task<IEnumerable<TEntity>> GetAll(int pageIndex, int pageSize);
  Task<TEntity> Get(int id);
  Task<bool> Exist(int id);
  Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);

  void Add(TEntity entity);
  void AddRange(IEnumerable<TEntity> entities);

  void Remove(TEntity entity);
  void RemoveRange(IEnumerable<TEntity> entities);
  EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
}
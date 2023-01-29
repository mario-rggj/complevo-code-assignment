using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Complevo.Domain.Infrastructure.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
  Task<ActionResult<IEnumerable<TEntity>>> GetAll();
  Task<ActionResult<TEntity>> Get(int id);
  Task<ActionResult<IEnumerable<TEntity>>> Find(Expression<Func<TEntity, bool>> predicate);

  void Add(TEntity entity);
  void AddRange(IEnumerable<TEntity> entities);

  void Remove(TEntity entity);
  void RemoveRange(IEnumerable<TEntity> entities);
  EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
}
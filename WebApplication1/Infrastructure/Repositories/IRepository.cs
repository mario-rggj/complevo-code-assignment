using System.Linq.Expressions;

namespace WebApplication1.Infrastructure.Repositories;

public interface IRepository<TEntity> where TEntity: class
{
    IEnumerable<TEntity> GetAll();
    TEntity Get(int id);
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);

    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
}
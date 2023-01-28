using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using WebApplication1.Domain.Infrastructure.Repositories;
using WebApplication1.Infrastructure.Context;

namespace WebApplication1.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly ApiContext Context;

    public Repository(ApiContext context)
    {
        Context = context;
    }
    
    public async Task<ActionResult<IEnumerable<TEntity>>> GetAll()
    {
        return await Context.Set<TEntity>().ToListAsync();
    }

    public async Task<ActionResult<TEntity>> Get(int id)
    {
        return await Context.Set<TEntity>().FindAsync(id);
    }

    public async Task<ActionResult<IEnumerable<TEntity>>> Find(Expression<Func<TEntity, bool>> predicate)
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
}
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Sneaker.Domain.IRepository;
using Sneaker.Infrastructure.Context;

namespace Sneaker.Infrastructure.Repository;

public class RepositoryBase<T>:IRepositoryBase<T> where T : class
{
    public SneakerContext Context { get; set; }

    public RepositoryBase(SneakerContext context)
    {
        Context = context;
    }
    
    public IQueryable<T> FindAll()
    {
       return Context.Set<T>().AsNoTracking();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
    {
        return Context.Set<T>().Where(expression).AsNoTracking();
    }

    public void Create(T entity)
    {
        Context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        Context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        Context.Set<T>().Remove(entity);
    }
}
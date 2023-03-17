using WebAssembly.Server.Services.Contexts;
using WebAssembly.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.Services.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal ApplicationContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(ApplicationContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }
        public virtual IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }
        public virtual TEntity Get(Guid id)
        {
            return dbSet.Find(id);
        }
        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }
        public virtual void Delete(Guid id)
        {
            Delete(dbSet.Find(id));
        }
        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}

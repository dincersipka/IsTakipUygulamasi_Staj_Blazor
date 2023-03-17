namespace WebAssembly.Server.Services.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(Guid id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(Guid entity);
    }
}

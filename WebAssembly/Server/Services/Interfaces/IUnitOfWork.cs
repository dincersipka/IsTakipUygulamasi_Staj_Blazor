namespace WebAssembly.Server.Services.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IMessageRepository messages { get; }
        IUserRepository users { get; }
        IRoleRepository roles { get; }
        IDepartmentRepository departments { get; }
        IStatisticRepository statistics { get; }
        int Complete();
    }
}

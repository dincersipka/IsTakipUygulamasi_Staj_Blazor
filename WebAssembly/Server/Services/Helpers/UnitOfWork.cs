using WebAssembly.Server.Services.Contexts;
using WebAssembly.Server.Services.Interfaces;

namespace Blazor.Server.Services.Helpers
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public IMessageRepository messages { get; }
        public IUserRepository users { get; }
        public IDepartmentRepository departments { get; }
        public IRoleRepository roles { get; }
        public IStatisticRepository statistics { get; }

        public UnitOfWork(ApplicationContext applicationContext, IMessageRepository messageRepository, IUserRepository userRepository, IDepartmentRepository departments, IRoleRepository roles, IStatisticRepository statistics)
        {
            this._context = applicationContext;
            this.messages = messageRepository;
            this.users = userRepository;
            this.departments = departments;
            this.roles = roles;
            this.statistics = statistics;
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}

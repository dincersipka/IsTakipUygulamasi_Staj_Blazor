using WebAssembly.Server.Services.Contexts;
using WebAssembly.Server.Services.Interfaces;
using WebAssembly.Shared.Models;

namespace Blazor.Server.Services.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context) { }
    }
}

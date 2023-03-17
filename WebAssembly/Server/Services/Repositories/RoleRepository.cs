using WebAssembly.Server.Services.Contexts;
using WebAssembly.Server.Services.Interfaces;
using WebAssembly.Shared.Models;

namespace Blazor.Server.Services.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationContext context) : base(context) { }
    }
}

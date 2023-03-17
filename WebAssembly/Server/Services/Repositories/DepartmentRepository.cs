using WebAssembly.Server.Services.Contexts;
using WebAssembly.Server.Services.Interfaces;
using WebAssembly.Shared.Models;

namespace Blazor.Server.Services.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationContext context) : base(context) { }
    }
}

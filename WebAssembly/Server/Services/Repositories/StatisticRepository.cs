using WebAssembly.Server.Services.Contexts;
using WebAssembly.Server.Services.Interfaces;
using WebAssembly.Shared.Models;

namespace Blazor.Server.Services.Repositories
{
    public class StatisticRepository : GenericRepository<Statistic>, IStatisticRepository
    {
        public StatisticRepository(ApplicationContext context) : base(context) { }
    }
}

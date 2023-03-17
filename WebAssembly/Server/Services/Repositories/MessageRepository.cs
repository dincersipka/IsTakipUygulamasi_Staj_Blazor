using WebAssembly.Server.Services.Contexts;
using WebAssembly.Server.Services.Interfaces;
using WebAssembly.Shared.Models;

namespace Blazor.Server.Services.Repositories
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(ApplicationContext context) : base(context) { }
    }
}

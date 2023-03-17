using Microsoft.AspNetCore.Mvc;
using WebAssembly.Server.Services.Interfaces;
using WebAssembly.Shared.Models;
using WebAssembly.Shared.ViewModels;

namespace WebAssembly.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public MessagesController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        [HttpGet]
        public IEnumerable<Message> GetMessages(Guid id)
        {
            var query = (from m in unitOfWork.messages.GetAll()
                         join u in unitOfWork.users.GetAll() on m.senderId  equals u.id
                         where !m.isDeleted && m.receiverId == id
                         select new MessageView 
                         {
                             id = m.id,
                             sendDate = m.sendDate,
                             message = m.message,
                             senderName = $"{u.name} {u.surname}",
                             senderId = m.senderId,
                             receiverId = m.receiverId
                         }).OrderByDescending(x => x.sendDate).Take(10);

            return query;
        }

        [HttpPost]
        public ActionResult PostMessage(MessageView messageView)
        {
            unitOfWork.messages.Insert(messageView);
            unitOfWork.Complete();
            return Ok();
        }
    }
}

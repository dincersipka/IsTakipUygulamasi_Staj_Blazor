using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebAssembly.Server.Services.Interfaces;
using WebAssembly.Shared.Models;
using WebAssembly.Server.Services.Helpers;

namespace WebAssembly.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public UsersController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            var query = (from u in unitOfWork.users.GetAll()
                         join r in unitOfWork.roles.GetAll() on u.roleId equals r.id
                         join d in unitOfWork.departments.GetAll() on u.departmentId equals d.id
                         where !u.isDeleted
                         select u).ToList();

            return query;
        }

        // GET: api/Users
        [HttpGet("{id}")]
        public User GetUsers(Guid id)
        {
            var query = (from u in unitOfWork.users.GetAll()
                         join r in unitOfWork.roles.GetAll() on u.roleId equals r.id
                         join d in unitOfWork.departments.GetAll() on u.departmentId equals d.id
                         where !u.isDeleted && u.id == id
                         select u).ToList().FirstOrDefault();

            return query;
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public ActionResult<UserSession> Login([FromBody] UserLogin userLogin)
        {
            var jwtAuth = new JwtAuthentication(unitOfWork);
            var userSession = jwtAuth.GenerateJwtToken(userLogin.mail, userLogin.password);

            if (userSession is null)
            {
                return Unauthorized();
            }
            else
            {
                return userSession;
            }
        }
    }
}

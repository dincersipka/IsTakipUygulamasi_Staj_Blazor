using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebAssembly.Server.Services.Interfaces;
using WebAssembly.Shared.Models;
using WebAssembly.Server.Services.Helpers;

namespace WebAssembly.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public DepartmentsController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        
        [HttpGet]
        public IEnumerable<Department> GetUsers()
        {
            var query = (from d in unitOfWork.departments.GetAll()
                         where !d.isDeleted
                         select d).ToList();

            return query;
        }
    }
}

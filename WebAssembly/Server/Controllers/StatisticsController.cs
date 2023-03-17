using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAssembly.Server.Services.Interfaces;
using WebAssembly.Shared.Models;
using WebAssembly.Shared.ViewModels;

namespace WebAssembly.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public StatisticsController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        [Route("[action]")]
        [HttpGet]
        public IEnumerable<Statistic> Company()
        {
            var query = (from s in unitOfWork.statistics.GetAll()
                         where !s.isDeleted && s.departmentId == 1
                         select s).OrderBy(x => x.recordDate).Take(10);

            return query;
        }

        [Route("[action]/{id}")]
        [HttpGet("{id}")]
        public IEnumerable<StatisticView> Department(int id)
        {
            var query = (from s in unitOfWork.statistics.GetAll()
                         join d in unitOfWork.departments.GetAll() on s.departmentId equals d.id
                         where !s.isDeleted && s.departmentId == id
                         select new StatisticView
                         {
                             id = s.id,
                             oee = s.oee,
                             performance = s.performance,
                             availability = s.availability,
                             quality = s.quality,
                             recordDate = s.recordDate,
                             departmentId = s.departmentId,
                             isDeleted = s.isDeleted,
                             departmentName = d.name
                         }).OrderBy(x => x.recordDate);

            return query;
        }

        [Route("[action]")]
        [HttpGet]
        public StatisticView[] LastDepartmentStatistics()
        {
            var departments = (from d in unitOfWork.departments.GetAll()
                               where !d.isDeleted && d.id != 1
                               select d).ToArray();

            StatisticView[] statistics = new StatisticView[departments.Count()];

            for(int i = 0; i < statistics.Length; i++)
            {
                var statistic = (from s in unitOfWork.statistics.GetAll()
                                 join d in unitOfWork.departments.GetAll() on s.departmentId equals d.id
                                 where !s.isDeleted && s.departmentId == departments[i].id
                                 select new StatisticView
                                 {
                                     id = s.id,
                                     oee = s.oee,
                                     performance = s.performance,
                                     availability = s.availability,
                                     quality = s.quality,
                                     recordDate = s.recordDate,
                                     departmentId = s.departmentId,
                                     isDeleted = s.isDeleted,
                                     departmentName = d.name
                                 }).OrderBy(x => x.recordDate).Last();

                statistics[i] = statistic;

            }

            return statistics;
        }

        [HttpPost]
        public ActionResult PostStatistic(Statistic statistic)
        {
            unitOfWork.statistics.Insert(statistic);
            unitOfWork.Complete();
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using PMS.API.Models;
using PMS.Domain.ProjectAggregate;
using PMS.Domain.TaskAggregate;
using System.Threading.Tasks;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PMSController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITaskRepository _taskRepository;

        public PMSController(IProjectRepository projectRepository, ITaskRepository taskRepository)
        {
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
        }

        [HttpPost]
        public IActionResult Add([FromBody]ProjectRequest projectRequest)
        {
            var projectEntity = new Project()
            {
                Code = projectRequest.Code,
                Name = projectRequest.Name,
                StartDate = projectRequest.StartDate,
                FinishDate = projectRequest.FinishDate
            };

            _projectRepository.Add(projectEntity);

            return Ok();

        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody]ProjectRequest projectRequest)
        {
            var projectEntity = await _projectRepository.FindByIdAsync(projectRequest.Id);

            projectEntity.Code = projectRequest.Code;
            projectEntity.Name = projectRequest.Name;
            projectEntity.StartDate = projectRequest.StartDate;
            projectEntity.FinishDate = projectRequest.FinishDate;


            _projectRepository.Update(projectEntity);

            return Ok();

        }
    }
}
using Microsoft.AspNetCore.Mvc;
using PMS.API.Models;
using PMS.Domain.ProjectAggregate;
using PMS.Domain.TaskAggregate;

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
        public IActionResult AddProjects([FromBody]ProjectRequest projectRequest)
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
    }
}
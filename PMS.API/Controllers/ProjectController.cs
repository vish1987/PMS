using Microsoft.AspNetCore.Mvc;
using PMS.API.Models;
using PMS.Domain.ProjectAggregate;
using PMS.Domain.TaskAggregate;
using System.Threading.Tasks;

namespace PMS.API.Controllers
{
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [Route("project/add")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]ProjectRequest projectRequest)
        {
            var projectEntity = new Project()
            {
                Code = projectRequest.Code,
                Name = projectRequest.Name,
                StartDate = projectRequest.StartDate,
                FinishDate = projectRequest.FinishDate,
                ParentId = projectRequest.ParentId
            };

            if (projectRequest.ParentId == -1)
                await _projectRepository.Add(projectEntity);

            await _projectRepository.AddSubProject(projectEntity);

            return Ok();

        }

        [Route("project/update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody]ProjectRequest projectRequest)
        {
            if (!projectRequest.ProjectId.HasValue)
                return BadRequest("Plase provide project id to update valid record");

            var projectEntity = await _projectRepository.FindByIdAsync(projectRequest.ProjectId.Value);

            projectEntity.Code = projectRequest.Code;
            projectEntity.Name = projectRequest.Name;
            projectEntity.StartDate = projectRequest.StartDate;
            projectEntity.FinishDate = projectRequest.FinishDate;


            await _projectRepository.Update(projectEntity);

            return Ok();
        }

        [Route("project/delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _projectRepository.Delete(id);

            return Ok();
        }
    }
}
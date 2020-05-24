using Microsoft.AspNetCore.Mvc;
using PMS.API.Models;
using PMS.Domain.TaskAggregate;
using System.Threading.Tasks;
using Task = PMS.Domain.TaskAggregate.Task;

namespace PMS.API.Controllers
{
    [ApiController]
    public class TaskController : ControllerBase
    {
        ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [Route("task/add")]
        [HttpPost]
        public IActionResult Add([FromBody]TaskRequest taskRequest)
        {
            var taskEntity = new Task()
            {
                Name = taskRequest.Name,
                StartDate = taskRequest.StartDate,
                FinishDate = taskRequest.FinishDate,
                Description = taskRequest.Description,
                ProjectId = taskRequest.ProjectId,
                State = taskRequest.State
            };

            _taskRepository.Add(taskEntity);

            return Ok();

        }

        [Route("task/update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody]TaskRequest taskRequest)
        {
            if (!taskRequest.TaskId.HasValue)
                return BadRequest("Plase provide project id to update valid record");

            var taskEntity = await _taskRepository.FindByIdAsync(taskRequest.TaskId.Value);

            taskEntity.Name = taskRequest.Name;
            taskEntity.Description = taskRequest.Description;
            taskEntity.StartDate = taskRequest.StartDate;
            taskEntity.FinishDate = taskRequest.FinishDate;
            taskEntity.State = taskRequest.State;
            taskRequest.ProjectId = taskRequest.ProjectId;


            _taskRepository.Update(taskEntity);

            return Ok();
        }

        [Route("task/delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _taskRepository.Delete(id);

            return Ok();
        }
    }
}
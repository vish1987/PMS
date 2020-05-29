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

        [Route("tasks/add")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]TaskRequest taskRequest)
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

            if (taskRequest.ParentId == -1)
                await _taskRepository.Add(taskEntity);
            else
            {
                taskEntity.ParentId = taskRequest.ParentId;
                await _taskRepository.AddSubTask(taskEntity);
            }
            return Ok();
        }

        [Route("tasks/update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody]TaskRequest taskRequest)
        {
            if (!taskRequest.TaskId.HasValue)
                return BadRequest("Please provide task id to update valid record");

            var taskEntity = await _taskRepository.FindByIdAsync(taskRequest.TaskId.Value);

            taskEntity.Name = taskRequest.Name;
            taskEntity.Description = taskRequest.Description;
            taskEntity.StartDate = taskRequest.StartDate;
            taskEntity.FinishDate = taskRequest.FinishDate;
            taskEntity.State = taskRequest.State;
            taskRequest.ProjectId = taskRequest.ProjectId;


            await _taskRepository.Update(taskEntity);

            return Ok();
        }

        [Route("tasks/delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _taskRepository.Delete(id);

            return Ok();
        }
    }
}
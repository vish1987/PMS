using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PMS.API.Models.Task;
using PMS.Domain.TaskAggregate;
using System.Threading.Tasks;
using Task = PMS.Domain.TaskAggregate.Task;

namespace PMS.API.Controllers
{
    [ApiController]
    public class TaskController : ControllerBase
    {
        ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskController(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        [Route("tasks/add")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]AddTaskRequest addTaskRequest)
        {
            var taskEntity = _mapper.Map<Task>(addTaskRequest);

            await _taskRepository.AddSubTask(taskEntity);

            return Ok();
        }

        [Route("tasks/addsubtask")]
        [HttpPost]
        public async Task<IActionResult> AddSubTask([FromBody]AddSubTaskRequest addSubTaskRequest)
        {
            var taskEntity = _mapper.Map<Task>(addSubTaskRequest);

            await _taskRepository.AddSubTask(taskEntity);

            return Ok();
        }

        [Route("tasks/update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody]UpdateTaskRequest updateTaskRequest)
        {
            Task taskEntity = await _taskRepository.FindByIdAsync(updateTaskRequest.TaskId);

            taskEntity = _mapper.Map<Task>(updateTaskRequest);

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
﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PMS.API.Models.Task;
using PMS.Domain.TaskAggregate;
using Swashbuckle.Swagger.Annotations;
using System.Collections.Generic;
using System.Net;
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskEntity = _mapper.Map<Task>(addTaskRequest);

            await _taskRepository.Add(taskEntity);

            return Ok();
        }

        [Route("tasks/addsubtask")]
        [HttpPost]
        public async Task<IActionResult> AddSubTask([FromBody]AddSubTaskRequest addSubTaskRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskEntity = _mapper.Map<Task>(addSubTaskRequest);

            await _taskRepository.AddSubTask(taskEntity);

            return Ok();
        }

        [Route("tasks/update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody]UpdateTaskRequest updateTaskRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Task taskEntity = await _taskRepository.FindByIdAsync(updateTaskRequest.TaskId.Value);

            taskEntity.Name = updateTaskRequest.Name;
            taskEntity.Description = updateTaskRequest.Description;
            taskEntity.StartDate = updateTaskRequest.StartDate.Value;
            taskEntity.FinishDate = updateTaskRequest.FinishDate.Value;
            taskEntity.State = updateTaskRequest.State.Value;

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

        [Route("tasks/getall")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IList<GetAllTaskResponse>))]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskRepository.GetAll();

            var reponse = _mapper.Map<IList<Task>, IList<GetAllTaskResponse>>(tasks);

            return Ok(reponse);
        }
    }
}
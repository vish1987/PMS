using AutoMapper;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using PMS.API.Models.Project;
using PMS.Domain.ProjectAggregate;
using PMS.Domain.TaskAggregate;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PMS.API.Controllers
{
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public ProjectController(IProjectRepository projectRepository, ITaskRepository taskRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        [Route("projects/add")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]AddProjectRequest addRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var projectEntity = _mapper.Map<Project>(addRequest);

            await _projectRepository.Add(projectEntity);

            return Ok();
        }

        [Route("projects/addsubproject")]
        [HttpPost]
        public async Task<IActionResult> AddSubProject([FromBody]AddSubProjectRequest addSubProjectRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var projectEntity = _mapper.Map<Project>(addSubProjectRequest);

            await _projectRepository.AddSubProject(projectEntity);

            return Ok();
        }

        [Route("projects/getall")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IList<GetAllProjectResponse>))]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _projectRepository.GetAll();

            var overallResponse = new List<GetAllProjectResponse>();

            foreach (var project in projects)
            {
                var projectResponse = _mapper.Map<GetAllProjectResponse>(project);

                List<int> projectsIds = GetProjectIds(project);

                var tasks = await _taskRepository.FindByProjectIdsAsync(projectsIds);

                projectResponse.State = project.CalcualteState(tasks).ToString();

                overallResponse.Add(projectResponse);
            }

            return Ok(overallResponse);
        }

        private static List<int> GetProjectIds(Project project)
        {
            var projectsIds = new List<int> { project.Id };

            var subprojectIds = project.SubProjects?.Select(x => x.Id).ToList();

            if (subprojectIds != null)
                projectsIds.AddRange(subprojectIds);

            return projectsIds;
        }

        [Route("projects/update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody]UpdateProjectRequest updateProjectRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var projectEntity = await _projectRepository.FindByIdAsync(updateProjectRequest.ProjectId.Value);

            projectEntity.Code = updateProjectRequest.Code;
            projectEntity.Name = updateProjectRequest.Name;
            projectEntity.StartDate = updateProjectRequest.StartDate.Value;
            projectEntity.FinishDate = updateProjectRequest.FinishDate.Value;

            await _projectRepository.Update(projectEntity);

            return Ok();
        }

        [Route("projects/delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _projectRepository.Delete(id);

            return Ok();
        }

        [Route("projects/report")]
        [HttpGet]
        public async Task<IActionResult> CreateReport(string fromDate, string toDate)
        {
            var from = DateTime.Parse(fromDate);
            var to = DateTime.Parse(toDate);

            var projects = await _projectRepository.FindByDatesAndTaskStatus(from, to, Domain.StateType.Planned);

            using var workbook = new XLWorkbook();
            CreateProjectsSheet(projects, workbook);

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "pms.xlsx");
        }

        private static void CreateProjectsSheet(List<Project> projects, XLWorkbook workbook)
        {
            var worksheet = workbook.Worksheets.Add("Projects");
            var currentRow = 1;
            worksheet.Cell(currentRow, 1).Value = "Id";
            worksheet.Cell(currentRow, 2).Value = "Project Code";
            worksheet.Cell(currentRow, 3).Value = "Project Name";
            worksheet.Cell(currentRow, 4).Value = "Parent Project";
            worksheet.Cell(currentRow, 5).Value = "Project Start Date";
            worksheet.Cell(currentRow, 6).Value = "Project Finish Date";

            worksheet.Cell(currentRow, 7).Value = "Task Name";
            worksheet.Cell(currentRow, 8).Value = "Parent Task";
            worksheet.Cell(currentRow, 9).Value = "Task Description ";
            worksheet.Cell(currentRow, 10).Value = "Task Start Date ";
            worksheet.Cell(currentRow, 11).Value = "Task Finish Date ";
            worksheet.Cell(currentRow, 12).Value = "Task Status ";

            foreach (var project in projects)
            {
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = project.Id;
                worksheet.Cell(currentRow, 2).Value = project.Code;
                worksheet.Cell(currentRow, 3).Value = project.Name;
                worksheet.Cell(currentRow, 4).Value = project.ParentProject?.Name;
                worksheet.Cell(currentRow, 5).Value = project.StartDate;
                worksheet.Cell(currentRow, 6).Value = project.FinishDate;

                foreach (var task in project.Tasks)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 7).Value = task.Name;
                    worksheet.Cell(currentRow, 8).Value = task.ParentTask?.Name;
                    worksheet.Cell(currentRow, 9).Value = task.Description;
                    worksheet.Cell(currentRow, 10).Value = task.StartDate;
                    worksheet.Cell(currentRow, 11).Value = task.FinishDate;
                    worksheet.Cell(currentRow, 12).Value = task.State.ToString();
                }
            }
        }
    }
}
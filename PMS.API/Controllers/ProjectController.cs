using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using PMS.API.Models;
using PMS.Domain.ProjectAggregate;
using System;
using System.Collections.Generic;
using System.IO;
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

        [Route("projects/add")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]ProjectRequest projectRequest)
        {
            var projectEntity = new Project()
            {
                Code = projectRequest.Code,
                Name = projectRequest.Name,
                StartDate = projectRequest.StartDate,
                FinishDate = projectRequest.FinishDate
            };

            if (projectRequest.ParentId == -1)
                await _projectRepository.Add(projectEntity);
            else
            {
                projectEntity.ParentId = projectRequest.ParentId;
                await _projectRepository.AddSubProject(projectEntity);
            }
            return Ok();

        }

        [Route("projects/update")]
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
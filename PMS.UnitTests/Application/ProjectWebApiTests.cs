using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;    //added this package for to mock objects for unit testing
using PMS.API;
using PMS.API.Controllers;
using PMS.API.Models.Project;
using PMS.Domain.ProjectAggregate;
using PMS.Domain.TaskAggregate;
using System.Collections.Generic;
using Xunit;

namespace PMS.UnitTests.Application
{

    public class ProjectWebApiTests
    {
        private Mock<IProjectRepository> _projectRepositoryMock { get; set; }
        private Mock<ITaskRepository> _taskRepositoryMock { get; set; }
        IMapper mapper;

        public ProjectWebApiTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });

            mapper = mockMapper.CreateMapper();

            _projectRepositoryMock = new Mock<IProjectRepository>();
            _taskRepositoryMock = new Mock<ITaskRepository>();
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_GetAll_Projects_success()
        {
            _projectRepositoryMock.Setup(proj => proj.GetAll()).ReturnsAsync(GetProjects());

            _taskRepositoryMock.Setup(task => task.FindByProjectIdsAsync(It.IsAny<IList<int>>())).ReturnsAsync(GetTasks());

            var projectController = new ProjectController(_projectRepositoryMock.Object, _taskRepositoryMock.Object, mapper);

            var response = await projectController.GetAll() as OkObjectResult; ;

            var result = Assert.IsAssignableFrom<IList<GetAllProjectResponse>>(response.Value);

            Assert.Equal(3, result.Count);
        }

        private List<Project> GetProjects()
        {
            var projects = new List<Project>();

            for (int i = 1; i <= 3; i++)
            {
                var project = new Project
                {
                    Id = 1,
                    Code = "proj" + i,
                    Name = i + "Machisma"
                };

                projects.Add(project);
            }

            return projects;
        }

        private List<Task> GetTasks()
        {
            var tasks = new List<Task>();

            for (int i = 1; i <= 3; i++)
            {
                var task = new Task
                {
                    Id = 1,
                    Description = "task" + i,
                    Name = i + "Machisma"
                };

                tasks.Add(task);
            }

            return tasks;
        }
    }
}

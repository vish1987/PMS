using AutoMapper;
using PMS.API.Models;
using PMS.API.Models.Project;
using PMS.API.Models.Task;
using PMS.Domain.ProjectAggregate;
using PMS.Domain.TaskAggregate;

namespace PMS.API
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<AddProjectRequest, Project>();

            CreateMap<AddSubProjectRequest, Project>();

            CreateMap<UpdateProjectRequest, Project>();

            CreateMap<Project, GetAllProjectResponse>();

            CreateMap<Project, ParentProject>();

            CreateMap<AddTaskRequest, Task>();

            CreateMap<AddSubTaskRequest, Task>();

            CreateMap<UpdateTaskRequest, Task>();

            CreateMap<Task, GetAllTaskResponse>();

            CreateMap<Task, ParentTask>();

        }
    }
}

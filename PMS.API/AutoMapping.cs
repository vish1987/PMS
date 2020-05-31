using AutoMapper;
using PMS.API.Models.Project;
using PMS.Domain.ProjectAggregate;

namespace PMS.API
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<AddProejctRequest, Project>();

            CreateMap<AddSubProjectRequest, Project>();

            CreateMap<UpdateProjectRequest, Project>();

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.ProjectAggregate
{
    public interface IProjectRepository
    {
        Project Add(Project project);
        Project Update(Project project);
        Task<Project> FindByIdAsync(int id);
    }
}

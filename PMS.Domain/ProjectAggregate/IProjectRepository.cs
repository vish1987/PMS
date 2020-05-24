using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.ProjectAggregate
{
    public interface IProjectRepository
    {
        void Add(Project project);
        void Update(Project project);
        Task<Project> FindByIdAsync(int id);
    }
}

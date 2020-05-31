using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.Domain.ProjectAggregate
{
    public interface IProjectRepository
    {
        Task Add(Project project);
        Task AddSubProject(Project project);
        Task Update(Project project);
        Task Delete(int id);
        Task<Project> FindByIdAsync(int id);
        Task<List<Project>> GetAll();
        Task<List<Project>> FindByDatesAndTaskStatus(DateTime fromDate, DateTime toDate, StateType stateType);
    }
}

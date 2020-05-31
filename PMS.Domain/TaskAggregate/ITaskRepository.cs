using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.Domain.TaskAggregate
{
    public interface ITaskRepository
    {
        System.Threading.Tasks.Task Add(Task task);
        System.Threading.Tasks.Task Update(Task task);
        System.Threading.Tasks.Task Delete(int id);
        System.Threading.Tasks.Task AddSubTask(Task task);
        Task<Task> FindByIdAsync(int id);
        Task<List<Task>> FindByProjectIdsAsync(IList<int> projectIds);
    }
}

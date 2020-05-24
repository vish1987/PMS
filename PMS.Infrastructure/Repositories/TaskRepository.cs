using PMS.Domain.TaskAggregate;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Task = PMS.Domain.TaskAggregate.Task;

namespace PMS.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly PMSContext _context;

        public TaskRepository(PMSContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task Add(Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task AddSubTask(Task task)
        {
            var parentTask = _context.Tasks.Include(y => y.SubTasks).Where(x => x.Id == task.ParentId).First();
            parentTask.SubTasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task Update(Task task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task<Task> FindByIdAsync(int id)
        {
            var task = await _context.Tasks
                                .Where(x => x.Id == id)
                                .SingleOrDefaultAsync();

            return task;
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var task = new Task
            {
                Id = id
            };

            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }
    }
}

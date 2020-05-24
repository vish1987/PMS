using PMS.Domain.TaskAggregate;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PMS.Infrastructure.Repositories
{
    class TaskRepository : ITaskRepository
    {
        private readonly PMSContext _context;

        public TaskRepository(PMSContext context)
        {
            _context = context;
        }

        public void Add(Task task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public void AddSubTask(Task task)
        {
            var parentTask = _context.Tasks.Include(y => y.SubTasks).Where(x => x.Id == task.ParentId).First();
            parentTask.SubTasks.Add(task);
            _context.SaveChanges();
        }

        public void Update(Task task)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }
    }
}

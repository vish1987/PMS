using PMS.Domain.TaskAggregate;

namespace PMS.Infrastructure.Repositories
{
    class TaskRepository : ITaskRepository
    {
        private readonly PMSContext _context;

        public TaskRepository(PMSContext context)
        {
            _context = context;
        }

        public Task Add(Task task)
        {
            return _context.Tasks.Add(task).Entity;
        }

        public Task Update(Task task)
        {
            return _context.Tasks.Update(task).Entity;
        }
    }
}

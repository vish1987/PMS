using PMS.Domain.TaskAggregate;
using System;

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
            throw new NotImplementedException();
        }

        public Task Update(Task task)
        {
            throw new NotImplementedException();
        }
    }
}

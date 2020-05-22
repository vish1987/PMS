using PMS.Domain.ProjectAggregate;

namespace PMS.Infrastructure.Repositories
{
    class ProjectRepository : IProjectRepository
    {

        private readonly PMSContext _context;

        public ProjectRepository(PMSContext context)
        {
            _context = context;
        }

        public Project Add(Project project)
        {
            return _context.Projects.Add(project).Entity;
        }

        public Project Update(Project project)
        {
            return _context.Projects.Update(project).Entity;
        }
    }
}

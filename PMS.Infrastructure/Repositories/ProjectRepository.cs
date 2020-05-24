using Microsoft.EntityFrameworkCore;
using PMS.Domain.ProjectAggregate;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Infrastructure.Repositories
{
    class ProjectRepository : IProjectRepository
    {
        private readonly PMSContext _context;

        public ProjectRepository(PMSContext context)
        {
            _context = context;
        }

        public void Add(Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
        }

        public void AddSubProject(Project project)
        {
            var parentProject = _context.Projects.Include(x => x.SubProjects).Where(x => x.Id == project.ParentId).First();
            parentProject.SubProjects.Add(project);
            _context.SaveChanges();
        }

        public async Task<Project> FindByIdAsync(int id)
        {
            var project = await _context.Projects
                                .Where(x => x.Id == id)
                                .SingleOrDefaultAsync();

            return project;
        }

        public async Task<List<Project>> GetAll()
        {
            var projects = await _context.Projects.Include(x => x.Tasks).ToListAsync();

            return projects;
        }

        public void Update(Project project)
        {
            _context.Projects.Update(project);
            _context.SaveChanges();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using PMS.Domain.ProjectAggregate;
using System;
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

        public Project Add(Project project)
        {
            return _context.Projects.Add(project).Entity;
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

        public Project Update(Project project)
        {
            return _context.Projects.Update(project).Entity;
        }
    }
}

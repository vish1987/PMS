using Microsoft.EntityFrameworkCore;
using PMS.Domain.ProjectAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly PMSContext _context;

        public ProjectRepository(PMSContext context)
        {
            _context = context;
        }

        public async Task Add(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Project>> GetAll()
        {
            return await _context.Projects.Include(proj => proj.SubProjects).ToListAsync();
        }

        public async Task AddSubProject(Project project)
        {
            var parentProject = _context.Projects.Include(proj => proj.SubProjects).Where(proj => proj.Id == project.ParentId).First();
            parentProject.SubProjects.Add(project);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var project = new Project
            {
                Id = id
            };

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }

        public async Task<Project> FindByIdAsync(int id)
        {
            var project = await _context.Projects
                                .Where(x => x.Id == id)
                                .SingleOrDefaultAsync();

            return project;
        }

        public async Task<List<Project>> FindByDatesAndTaskStatus(DateTime fromDate, DateTime toDate, Domain.StateType stateType)
        {
            var projects = await _context.Projects
                               .Include(y => y.Tasks)
                               .Where(t => t.Tasks.Any(s => s.State == stateType) && t.StartDate >= fromDate && t.StartDate <= toDate)
                               .ToListAsync();


            return projects;
        }

        public async Task Update(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }
    }
}

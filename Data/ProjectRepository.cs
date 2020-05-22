using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkplaceManager.Contracts;
using WorkplaceManager.Models;

namespace WorkplaceManager.Data
{
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public void CreateProject(Project project) => Create(project);

        public void DeleteProject(Project project) => Delete(project);

        public async Task<List<Project>> GetAllProjects(int? managerId)
        {
            var results = await FindByCondition(p => p.ManagerId == managerId);
            var projects = results.Include(p => p.Manager).ToList();
            return projects;
        }

        public async Task<Project> GetProjectById(int projectId)
        {
            var result = await FindByCondition(p => p.ProjectId == projectId);
            var project = result.Include(p => p.Manager).SingleOrDefault();
            return project;
        }

        public void UpdateProject(Project project) => Update(project);
    }
}

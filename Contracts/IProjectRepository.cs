using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkplaceManager.Models;

namespace WorkplaceManager.Contracts
{
    public interface IProjectRepository : IRepositoryBase<Project>
    {
        void CreateProject(Project project);
        void UpdateProject(Project project);
        void DeleteProject(Project project);
        Task<Project> GetProjectById(int projectId);
        Task<List<Project>> GetAllProjects(int? managerId);
    }
}

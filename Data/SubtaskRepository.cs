using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkplaceManager.Contracts;
using WorkplaceManager.Models;

namespace WorkplaceManager.Data
{
    public class SubtaskRepository : RepositoryBase<Subtask>, ISubtaskRepository
    {
        public SubtaskRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public void CreateSubtask(Subtask subtask) => Create(subtask);

        public void DeleteSubtask(Subtask subtask) => Delete(subtask);

        public async Task<List<Subtask>> GetAllSubtasks(string jobId)
        {
            var results = await FindByCondition(s => s.JobId == jobId);
            var subtasks = results.Include(s => s.Job).ToList();
            return subtasks;
        }

        public async Task<Subtask> GetSubtaskById(string subtaskId)
        {
            var result = await FindByCondition(s => s.SubtaskId == subtaskId);
            var subtask = result.Include(s => s.Job).SingleOrDefault();
            return subtask;
        }

        public void UpdateSubtask(Subtask subtask) => Update(subtask);
    }
}

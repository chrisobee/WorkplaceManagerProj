using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkplaceManager.Models;

namespace WorkplaceManager.Contracts
{
    public interface ISubtaskRepository : IRepositoryBase<Subtask>
    {
        void CreateSubtask(Subtask subtask);
        void UpdateSubtask(Subtask subtask);
        void DeleteSubtask(Subtask subtask);
        Task<Subtask> GetSubtaskById(int subtaskId);
        Task<List<Subtask>> GetAllSubtasks(int jobId);
    }
}

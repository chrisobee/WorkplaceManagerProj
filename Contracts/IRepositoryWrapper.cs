using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkplaceManager.Contracts
{
    public interface IRepositoryWrapper
    {
        ISeniorManagerRepository SeniorManager { get; }
        IManagerRepository Manager { get; }
        IEmployeeRepository Employee { get; }
        IJobRepository Job { get; }
        ISubtaskRepository Subtask { get; }
        IProjectRepository Project { get; }
        IBranchRepository Branch { get; }
        IEmployeeJobRepository EmployeeJob { get; }
        Task Save();
    }
}

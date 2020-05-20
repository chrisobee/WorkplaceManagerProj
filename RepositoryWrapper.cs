using System.Threading.Tasks;
using WorkplaceManager.Contracts;
using WorkplaceManager.Data;

namespace WorkplaceManager
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ApplicationDbContext _context;
        private ISeniorManagerRepository _seniorManager;
        private IManagerRepository _manager;
        private IEmployeeRepository _employee;
        private IJobRepository _job;
        private ISubtaskRepository _subtask;
        private IProjectRepository _project;
        private IBranchRepository _branch;
        private IEmployeeJobRepository _employeeJob;

        public ISeniorManagerRepository SeniorManager
        {
            get
            {
                if (_seniorManager == null)
                {
                    _seniorManager = new SeniorManagerRepository(_context);
                }
                return _seniorManager;
            }
        }
        public IManagerRepository Manager
        {
            get
            {
                if (_manager == null)
                {
                    _manager = new ManagerRepository(_context);
                }
                return _manager;
            }
        }
        public IEmployeeRepository Employee
        {
            get
            {
                if (_employee == null)
                {
                    _employee = new EmployeeRepository(_context);
                }
                return _employee;
            }
        }
        public IJobRepository Job
        {
            get
            {
                if (_job == null)
                {
                    _job = new JobRepository(_context);
                }
                return _job;
            }
        }
        public ISubtaskRepository Subtask
        {
            get
            {
                if (_subtask == null)
                {
                    _subtask = new SubtaskRepository(_context);
                }
                return _subtask;
            }
        }
        public IProjectRepository Project
        {
            get
            {
                if (_project == null)
                {
                    _project = new ProjectRepository(_context);
                }
                return _project;
            }
        }
        public IBranchRepository Branch
        {
            get
            {
                if (_branch == null)
                {
                    _branch = new BranchRepository(_context);
                }
                return _branch;
            }
        }
        public IEmployeeJobRepository EmployeeJob
        {
            get
            {
                if (_employeeJob == null)
                {
                    _employeeJob = new EmployeeJobRepository(_context);
                }
                return _employeeJob;
            }
        }

        public RepositoryWrapper(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }
}

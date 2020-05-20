using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkplaceManager.Models;

namespace WorkplaceManager.Contracts
{
    public interface ITrelloAPI
    {
        //Board Create and Get
        Task CreateBoard(string managerName);
        Task<Branch> GetBoardById();

        //List Create and Get
        Task CreateList(string projectName, string branchId);
        Task<List<Project>> GetAllLists(string boardId);
        Task<Project> GetListById(string projectId);

        //Card Create and Get
        Task CreateCard(string projectId);
        Task<List<Job>> GetAllCards(string projectId);
        Task<Job> GetCardById(string jobId);
    }
}

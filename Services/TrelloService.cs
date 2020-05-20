using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using WorkplaceManager.Contracts;
using WorkplaceManager.Models;
using System.Net.Http;

namespace WorkplaceManager.Services
{
    public class TrelloService : ITrelloAPI
    {
        public async Task CreateBoard(string managerName)
        {
            string url = $"https://api.trello.com/1/boards/?key={APIKey.TrelloKey}&token={APIKey.TrelloToken}";
            HttpClient client = new HttpClient();
            HttpContent content = new StringContent(managerName);
            await client.PostAsync(url, content);
        }

        public Task CreateCard(string projectId)
        {
            throw new NotImplementedException();
        }

        public Task CreateList(string projectName, string branchId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Branch>> GetAllBoards()
        {
            throw new NotImplementedException();
        }

        public Task<List<Job>> GetAllCards(string projectId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Project>> GetAllLists(string boardId)
        {
            throw new NotImplementedException();
        }

        public Task<Branch> GetBoardById()
        {
            throw new NotImplementedException();
        }

        public Task<Job> GetCardById(string jobId)
        {
            throw new NotImplementedException();
        }

        public Task<Project> GetListById(string projectId)
        {
            throw new NotImplementedException();
        }
    }
}

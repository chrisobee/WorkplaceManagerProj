using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using WorkplaceManager.Contracts;
using WorkplaceManager.Models;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace WorkplaceManager.Services
{
    public class TrelloService : ITrelloAPI
    {
        public async Task CreateBoard(string managerName)
        {
            string url = $"https://api.trello.com/1/boards/?key={APIKey.TrelloKey}&token={APIKey.TrelloToken}&";
            HttpClient client = new HttpClient();
            string json = JsonConvert.SerializeObject(new {name = managerName });
            HttpContent content = new StringContent(json);
            await client.PostAsync(url, content);
        }

        public async Task CreateCard(string projectId)
        {
            string url = $"https://api.trello.com/1/cards/?key={APIKey.TrelloKey}&token={APIKey.TrelloToken}&";
            HttpClient client = new HttpClient();
            string json = JsonConvert.SerializeObject(new { idList = projectId });
            HttpContent content = new StringContent(json);
            await client.PostAsync(url, content);
        }

        public async Task CreateList(string projectName, string branchId)
        {
            string url = $"https://api.trello.com/1/lists?key={APIKey.TrelloKey}&token={APIKey.TrelloToken}&";
            HttpClient client = new HttpClient();
            string json = JsonConvert.SerializeObject(new { name = projectName, idBoard = branchId });
            HttpContent content = new StringContent(json);
            await client.PostAsync(url, content);
        }

        public async Task<List<Job>> GetAllCards(string projectId)
        {
            string url = $"https://api.trello.com/1/lists/{projectId}/cards?fields=name&key={APIKey.TrelloKey}&token={APIKey.TrelloToken}";
            List<Job> listOfJobs = new List<Job>();
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(url);
            string jsonResult = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                listOfJobs = JsonConvert.DeserializeObject<List<Job>>(jsonResult);
            }
            return listOfJobs;
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

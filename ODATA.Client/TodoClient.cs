using ODATA.Client.Models;
using Simple.OData.Client;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ODATA.Client
{
    public class TodoClient
    {
        private readonly ODataClient client;

        public TodoClient(string url)
        {
            client = new ODataClient(new ODataClientSettings(url)
            {
                IgnoreResourceNotFoundException = true,
                OnTrace = (x, y) => Console.WriteLine(string.Format(x, y)),
            });
        }

        public async Task<Todo> GetById(int id)
        {
            return await client
                .For<Todo>()
.Key(id)
                .FindEntryAsync();
        }

        public Task<IEnumerable<Todo>> GetAll()
        {
            return client
                .For<Todo>()
                .FindEntriesAsync();
        }

        public async Task<IEnumerable<Todo>> GetBy(Expression<Func<Todo, bool>> filter)
        {
            return await client
                .For<Todo>()
                .Filter(filter)
                .FindEntriesAsync();
        }

        public async Task<Todo> Create(Todo todo)
        {
            return await client
                .For<Todo>()
                .Set(todo)
                .InsertEntryAsync(false);
        }

        public async Task<Todo> Update(int id, Todo todo)
        {
            return await client
                .For<Todo>()
                .Key(id)
                .Set(todo)
                .UpdateEntryAsync(false);
        }

        public async Task Delete(int id)
        {
            await client
            .For<Todo>()
            .Key(id)
            .DeleteEntryAsync();
        }
    }
}
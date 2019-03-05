using ODATA.Client.Models;
using System;
using System.Threading.Tasks;

namespace ODATA.Client.Console
{
    class Program
    {
        private const string URL = @"https://localhost:44327/odata/";
        private static TodoClient client;

        static async Task Main(string[] args)
        {
            System.Console.WriteLine("Hello OData!");

            client = new TodoClient(URL);

            await Seed();
            await ListTodos();
            await Update();
            await ListTodos();
            await Delete();
            await ListTodos();

            System.Console.WriteLine("\nDone");
        }

        private static async Task Delete()
        {
            await client.Delete(1);
        }

        private static async Task Update()
        {
            await client.Update(1, new Todo()
            {
                Id = 1,
                Title = "Publish OData code on github",
                Done = false,
                DueDate = new DateTime(2019, 3, 31)
            });
        }

        private static async Task Seed()
        {
            await client.Create(new Todo()
            {
                Title = "Publish the code on github",
                Done = false,
                DueDate = new DateTime(2019, 03, 31)
            });
        }

        private static async Task ListTodos()
        {
            var todos = await client.GetAll();
            foreach(var p in todos)
            {
                DisplayTodo(p);
            }
        }

        private static void DisplayTodo(Todo todo)
        {
            System.Console.WriteLine($"Id: {todo.Id} - {todo.Title} Done: {todo.Done} Due on: {todo.DueDate}");
        }
    }
}

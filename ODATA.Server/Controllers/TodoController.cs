using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ODATA.Server.Models;
using System.Linq;

namespace ODATA.Server.Controllers
{
    public class TodoController : ODataController
    {
        private readonly TodoContext context;

        public TodoController(TodoContext context)
        {
            this.context = context;
        }

        [EnableQuery(PageSize = 20)]
        public IActionResult Get()
        {
            return Ok(this.context.Todos.AsQueryable());
        }

        [EnableQuery]
        public IActionResult GetById([FromODataUri] int key)
        {
            var todo = this.context.Todos.Find(key);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        public IActionResult Post([FromBody] Todo todo)
        {
            this.context.Todos.Add(todo);
            this.context.SaveChanges();
            return Created(todo);
        }

        public IActionResult Put([FromODataUri] int key, Todo todo)
        {
            if (key != todo.Id)
            {
                return BadRequest();
            }
            if (!this.context.Todos.Any(t => t.Id == todo.Id))
            {
                return NotFound();
            }
            this.context.Entry(todo).State = EntityState.Modified;
            this.context.SaveChanges();
            return Updated(todo);
        }

        public IActionResult Patch([FromODataUri] int key, Delta<Todo> todo)
        {
            var entry = this.context.Todos.Find(key);
            if (entry == null)
            {
                return NotFound();
            }
            todo.Patch(entry);
            this.context.SaveChanges();
            return Updated(entry);
        }

        public IActionResult Delete([FromODataUri] int key)
        {
            var todo = this.context.Todos.Find(key);
            if (todo == null)
            {
                return NotFound();
            }

            this.context.Remove(todo);
            this.context.SaveChanges();
            return NoContent();
        }
    }
}
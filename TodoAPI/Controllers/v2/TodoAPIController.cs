using Microsoft.AspNetCore.Mvc;
using TodoAPI.Models;

namespace TodoAPI.Controllers.v2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiVersion("2.0")]
    public class TodoAPIController : ControllerBase
    {
        private static List<TodoModel> todos = new List<TodoModel>
        {
            new TodoModel { Id = 1, Title = "Learn API Versioning in .NET", IsCompleted = true },
            new TodoModel { Id = 2, Title = "Learn & Implement API Versioning in .NET ", IsCompleted = false },
            new TodoModel { Id = 3, Title = "Enterprise Web Application with .NET", IsCompleted = false }
        };
        [HttpGet]
        public IActionResult Get() => Ok(todos);

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var todo = todos.FirstOrDefault(w => w.Id == id);
            if (todo is null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        [HttpPost]
        public IActionResult Create(TodoModel todo)
        {
            todo.Id = todos.Max(m => m.Id) + 1;
            todos.Add(todo);
            return CreatedAtAction(nameof(GetById), new { id = todo.Id, version = "2.0" }, todo);
        }
    }
}
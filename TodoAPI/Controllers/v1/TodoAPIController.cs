using Microsoft.AspNetCore.Mvc;
using TodoAPI.Models;

namespace TodoAPI.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class TodoAPIController : ControllerBase
    {
        private static List<TodoModel> todos = new List<TodoModel>
        {
            new TodoModel { Id = 1, Title = "Learn API Versioning in .NET", IsCompleted = false }
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
    }
}
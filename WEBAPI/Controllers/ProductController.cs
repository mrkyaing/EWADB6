using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEBAPI.DAO;
using WEBAPI.Dto;
using WEBAPI.Models;

namespace WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _appContext;

        public ProductController(AppDbContext appContext)
        {
            this._appContext = appContext;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll() => Ok(_appContext.Products.ToList());

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _appContext.Products.Find(id);
            return product != null ? Ok(product) : NotFound();
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.User + "," + UserRoles.Admin)]
        public IActionResult Create([FromBody] Product product)
        {
            _appContext.Products.Add(product);//collect the data into the dbContext
            _appContext.SaveChanges();//save the data into the database
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product product)
        {
            if (id != product.Id) return BadRequest();
            _appContext.Products.Update(product);
            _appContext.SaveChanges();//save the data into the database
            return NoContent();
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _appContext.Products.Find(id);
            if (product == null) return NotFound();
            _appContext.Products.Remove(product);
            _appContext.SaveChanges();//save the data into the database
            return Ok();
        }
    }
}
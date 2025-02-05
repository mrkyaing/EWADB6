using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleWebAPIWithNoSQL.Models;
using SimpleWebAPIWithNoSQL.Repositories;

namespace SimpleWebAPIWithNoSQL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductApiController(IProductService productService)=>_productService = productService;

        //get all api/products
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(products);
        }
        //get by id api/products/{id}
        [HttpGet("{productId:length(24)}")]
        public async Task<ActionResult<ProductModel>> Get(string id){
            var product=await _productService.GetProductByIdAsync(id);
            if(product is null ) return NotFound();
            return product;
        }
        //post api/products
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductModel product)
        {
            await _productService.CreateProductAsync(product);
            return Ok("Product is created successfully.");
        }

        //put api/products/{id}

        [HttpPut("{productId:length(24)}")]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody] ProductModel product)
        {
            var productToUpdate = await _productService.GetProductByIdAsync(id);
            if (productToUpdate == null)
            {
                return NotFound();
            }
            await _productService.UpdateProductAsync(id, product);
            return Ok();
        }
        //delete api/products/{id}
        [HttpDelete("{productId:length(24)}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var productToDelete = await _productService.GetProductByIdAsync(id);
            if (productToDelete == null)
            {
                return NotFound();
            }
            await _productService.DeleteProductAsync(id);
            return Ok();
        }
    }
}
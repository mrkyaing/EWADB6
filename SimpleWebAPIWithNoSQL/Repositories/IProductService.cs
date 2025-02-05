using SimpleWebAPIWithNoSQL.Models;

namespace SimpleWebAPIWithNoSQL.Repositories
{
    public interface IProductService
    {
        Task<IList<ProductModel>> GetProductsAsync();
        Task<ProductModel> GetProductByIdAsync(string id);
        Task CreateProductAsync(ProductModel product);
        Task UpdateProductAsync(string id, ProductModel product);
        Task DeleteProductAsync(string id);
    }
}
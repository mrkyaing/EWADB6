using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SimpleWebAPIWithNoSQL.Config;
using SimpleWebAPIWithNoSQL.Models;

namespace SimpleWebAPIWithNoSQL.Repositories
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<ProductModel> _productCollection;
        public ProductService(IOptions<DbSetting> dbSettings)
        {
            var client = new MongoClient(dbSettings.Value.ConnectionString);
            var database = client.GetDatabase(dbSettings.Value.DatabaseName);
            _productCollection = database.GetCollection<ProductModel>(dbSettings.Value.CollectionName);
        }
        public async Task CreateProductAsync(ProductModel product)
        {
            await _productCollection.InsertOneAsync(product);
        }

        public async Task DeleteProductAsync(string id)
        {
            await _productCollection.DeleteOneAsync(product => product.Id == id);
        }

        public Task<ProductModel> GetProductByIdAsync(string id)
        {
            return _productCollection.Find(product => product.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IList<ProductModel>> GetProductsAsync()
        {
            return await _productCollection.Find(product => true).ToListAsync();
        }

        public async Task UpdateProductAsync(string id, ProductModel product)
        {
            await _productCollection.ReplaceOneAsync(product => product.Id == id, product);
        }
    }
}
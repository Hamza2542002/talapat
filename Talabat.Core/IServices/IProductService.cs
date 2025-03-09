using Talabat.Core.Entities;
using Talabat.Core.Specifications.ProductSpecs;

namespace Talabat.Core.IServices
{
    public interface IProductService
    {
        Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecsParams productParamsModel);
        Task<int> GetCountAsync(ProductSpecsParams productParamsModel); 
        Task<Product?> GetProductAsync(int id);
        Task<IReadOnlyList<Brand>> GetBrandsAsync();
        Task<IReadOnlyList<Category>> GetCategoriesAsync();
    }
}

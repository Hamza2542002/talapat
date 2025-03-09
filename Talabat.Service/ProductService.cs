using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.IServices;
using Talabat.Core.Specifications.ProductSpecs;

namespace Talabat.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecsParams productParamsModel)
        {
            var products =
                await _unitOfWork.GetRepository<Product>().GetAllWithSpecsAsync(new ProductSpecifications(productParamsModel));

            return products;
        }
        public async Task<int> GetCountAsync(ProductSpecsParams productParamsModel)
        {
            return await _unitOfWork
                .GetRepository<Product>().GetCountAsync(new ProductCountSpecs(productParamsModel));
        }
        public async Task<Product?> GetProductAsync(int id)
        {
            var product = await _unitOfWork
                .GetRepository<Product>().GetWithSpecsAsync(new ProductSpecifications(id));

            return product;
        }

        public async Task<IReadOnlyList<Brand>> GetBrandsAsync()
        {
            return await _unitOfWork
                .GetRepository<Brand>().GetAllAsync();
        }

        public async Task<IReadOnlyList<Category>> GetCategoriesAsync()
        {
            return await _unitOfWork
                .GetRepository<Category>().GetAllAsync();
        }
    }
}

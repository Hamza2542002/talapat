using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpecs
{
    public class ProductCountSpecs : BaseSpecifications<Product>
    {
        public ProductCountSpecs(ProductSpecsParams productParamsModel)
            :base(
                    P =>
                     (productParamsModel.BrandId == null ? true : productParamsModel.BrandId == P.BrandId) &&
                     (productParamsModel.CategoryId == null ? true : productParamsModel.CategoryId == P.CategoryId) &&
                     (string.IsNullOrEmpty(productParamsModel.Search) ? true : P.Name.Contains(productParamsModel.Search))
             )
        {
            
        }
    }
}

using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpecs
{
    public class ProductSpecifications : BaseSpecifications<Product>
    {
        public ProductSpecifications(ProductSpecsParams productParamsModel)
            :base(
                    P => 
                     (productParamsModel.BrandId == null ? true : productParamsModel.BrandId == P.BrandId) &&
                     (productParamsModel.CategoryId == null ? true : productParamsModel.CategoryId == P.CategoryId) &&
                     (string.IsNullOrEmpty(productParamsModel.Search) ? true : P.Name.Contains(productParamsModel.Search))
                 )
        {
            IncludeCategoryAndBrand();
            if (!string.IsNullOrEmpty(productParamsModel.Sort))
            {
                switch (productParamsModel.Sort)
                {
                    case "priceAsc":
                        OrderBy = P => P.Price;
                        break;

                    case "priceDesc":
                        OrderByDesc = P => P.Price;
                        break;

                    case "nameAsc":
                        OrderBy = P => P.Name;
                        break;

                    case "nameDesc":
                        OrderByDesc = P => P.Name;
                        break;

                }
            }

            ApplyPagginaion(productParamsModel);

        }

        private void ApplyPagginaion(ProductSpecsParams productParamsModel)
        {
            IsPaginationEnabled = true;
            PageIndex = productParamsModel.PageIndex;
            PageSize = productParamsModel.PageSize;
        }

        public ProductSpecifications(int id)
            :base(P => P.Id == id)
        {
            IncludeCategoryAndBrand();
        }

        private void IncludeCategoryAndBrand()
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }
    }
}

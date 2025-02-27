namespace Talabat.Core.Specifications.ProductSpecs
{
    public class ProductSpecsParams
    {
        public int PageIndex { get; set; } = 1;
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public string? Search { get; set; }

        private int pageSize = 5;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > 5 || value <= 0) ? 5 : value; }
        }
        public ProductSpecsParams()
        {
            
        }
    }
}

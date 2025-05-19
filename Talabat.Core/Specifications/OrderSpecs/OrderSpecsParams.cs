namespace Talabat.Core.Specifications.OrderSpecs
{
    public class OrderSpecsParams
    {
        public int PageIndex { get; set; } = 1;
        public string? Sort { get; set; }
        public string? CustomerEmail { get; set; }

        private int pageSize = 5;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > 5 || value <= 0) ? 5 : value; }
        }
    }
}

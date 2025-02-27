namespace Talabat.Core.Specifications.EmployeeSpecs
{
    public class EmployeeSpecsParams
    {
        public string? Search { get; set; }
        public int PageIndex { get; set; } = 1;
        public string? Sort { get; set; }
        public int? DepartmentId { get; set; }

        private int pageSize = 5;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value == 0 ? 5 : value; }
        }

    }
}

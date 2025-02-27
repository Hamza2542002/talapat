using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.EmployeeSpecs
{
    public class EmployeeCountSpecs : BaseSpecifications<Employee>
    {
        public EmployeeCountSpecs(EmployeeSpecsParams specsParams)
            : base(
                    E => (string.IsNullOrEmpty(specsParams.Search) ? true : E.Name.Contains(specsParams.Search)) &&
                    (specsParams.DepartmentId == null ? true : E.DepartmentId == specsParams.DepartmentId)
                 )
        {
            
        }
    }
}

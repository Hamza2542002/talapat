using System.Linq.Expressions;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.EmployeeSpecs
{
    public class EmployeeSpecifications : BaseSpecifications<Employee>
    {
        public EmployeeSpecifications(EmployeeSpecsParams specsParams)
            :base(
                    E => (string.IsNullOrEmpty(specsParams.Search) ? true : E.Name.Contains(specsParams.Search)) &&
                    (specsParams.DepartmentId == null ? true : E.DepartmentId == specsParams.DepartmentId)
                 )
        {
            Includes.Add(E => E.Department);

            if (!string.IsNullOrEmpty(specsParams.Sort))
            {
                switch (specsParams.Sort)
                {
                    case "nameAsc" : OrderBy = E => E.Name; break;
                    case "nameDesc": OrderByDesc = E => E.Name; break;
                    case "ageAsc": OrderBy = E => E.Age; break;
                    case "ageDesc": OrderByDesc = E => E.Age; break;
                    default:
                        OrderBy = E => E.Id;
                        break;
                }
            }

            ApplyPagination(specsParams);
        }

        private void ApplyPagination(EmployeeSpecsParams specsParams)
        {
            IsPaginationEnabled = true;
            PageIndex =  specsParams.PageIndex;
            PageSize = specsParams.PageSize;
        }

        public EmployeeSpecifications(int id)
            :base(E => E.Id == id)
        {
            Includes.Add(E => E.Department);
        }
    }
}

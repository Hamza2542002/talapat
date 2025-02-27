using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.IRepositories;
using Talabat.Core.Specifications.EmployeeSpecs;
using Talabat.Helpers;

namespace Talabat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmplyeeController : ControllerBase
    {
        private readonly IGenericRepository<Employee> _employeeRepository;

        public EmplyeeController(IGenericRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees([FromQuery]EmployeeSpecsParams specsParams)
        {
            var employees = await _employeeRepository
                .GetAllWithSpecsAsync(new EmployeeSpecifications(specsParams));

            var response = new PaginationResponse<Employee>
            {
                Data = employees,
                PageIndex = specsParams.PageIndex,
                PageSize = specsParams.PageSize,
                Count = _employeeRepository.GetCountAsync(new EmployeeCountSpecs(specsParams)).Result
            };
            
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employees = await _employeeRepository.GetAllWithSpecsAsync(new EmployeeSpecifications(id));

            return Ok(employees);
        }
    }
}

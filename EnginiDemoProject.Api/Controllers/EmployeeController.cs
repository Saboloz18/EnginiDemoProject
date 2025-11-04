using EnginiDemoProject.Application.Services.Employees;
using EnginiDemoProject.Domain.Employees;
using Microsoft.AspNetCore.Mvc;

namespace EnginiDemoProject.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        /// <summary>
        /// Retrieves an employee and their entire subordinate hierarchy.
        /// </summary>
        /// <remarks>
        /// This endpoint performs a recursive query to fetch the specified employee
        /// and all of their direct and indirect subordinates in a nested structure.
        /// <br/><br/>
        /// **Example Request:**
        ///
        ///     GET /employees/1
        ///
        /// </remarks>
        /// <param name="id">The unique identifier of the employee to retrieve.</param>
        /// <response code="200">Returns the employee's hierarchical data.</response>
        /// <response code="404">If the employee with the specified ID is not found.</response>
        /// <response code="500">If an unexpected server error occurs.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            var employee = await _employeeService.GetEmployeeWithSubordinatesAsync(id);

            return Ok(employee);
        }
    }
}

using EnginiDemoProject.Domain.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnginiDemoProject.Application.Services.Employees
{
    public interface IEmployeeService
    {
        public Task<Employee> GetEmployeeWithSubordinatesAsync(int employeeId);
    }
}

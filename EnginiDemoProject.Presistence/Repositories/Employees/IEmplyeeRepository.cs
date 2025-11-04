using EnginiDemoProject.Domain.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnginiDemoProject.Presistence.Repositories.Employees
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployeeHierarchyAsync(int employeeId);
    }
}

using EnginiDemoProject.Application.Exceptions;
using EnginiDemoProject.Domain.Employees;
using EnginiDemoProject.Presistence.Repositories.Employees;


namespace EnginiDemoProject.Application.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> GetEmployeeWithSubordinatesAsync(int employeeId)
        {

            var employeeHierarchyList = (await _employeeRepository.GetEmployeeHierarchyAsync(employeeId)).ToList();

            if (!employeeHierarchyList.Any())
            {
                throw new NotFoundException($"User with Id: {employeeId} not found");
            }

            var employeeMap = employeeHierarchyList.ToDictionary(e => e.Id);
            Employee root = new();

            foreach (var employee in employeeHierarchyList)
            {
                if (employee.Id == employeeId)
                {
                    root = employee;
                }
                if (employee.ManagerId.HasValue && employeeMap.TryGetValue(employee.ManagerId.Value, out var manager))
                {
                    manager.Subordinates.Add(employee);
                }
            }

            return root;
        }
    }
}

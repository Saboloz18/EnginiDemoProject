using EnginiDemoProject.Domain.Employees;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnginiDemoProject.Presistence.Repositories.Employees
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EnginiDemoProjectDbContext _context;

        public EmployeeRepository(EnginiDemoProjectDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetEmployeeHierarchyAsync(int employeeId)
        {
            const string rawSqlQuery = @"
            WITH EmployeeHierarchyCTE AS (
                SELECT Id, Name, ManagerId FROM Employees WHERE Id = @Id
                UNION ALL
                SELECT e.Id, e.Name, e.ManagerId
                FROM Employees e
                INNER JOIN EmployeeHierarchyCTE eh ON e.ManagerId = eh.Id
            )
            SELECT * FROM EmployeeHierarchyCTE";


            var parameter = new SqlParameter("@Id", employeeId);

            return await _context.Employees
                .FromSqlRaw(rawSqlQuery, parameter)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}

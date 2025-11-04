using EnginiDemoProject.Domain.Employees;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnginiDemoProject.Presistence.Seed
{
    public static class Seed
    {
        public static async Task SeedSampleDataAsync(EnginiDemoProjectDbContext context)
        {
            if (!await context.Employees.AnyAsync())
            {
                var employees = new List<Employee>
                {
                new() { Name = "Employee 1", ManagerId = null },
                new() { Name = "Employee 2", ManagerId = 1 },
                new() { Name = "Employee 3", ManagerId = 1 },
                new() { Name = "Employee 4", ManagerId = 2 },
                new() { Name = "Employee 5", ManagerId = 2 },
                new() { Name = "Employee 6", ManagerId = 4 }
                };

                await context.Employees.AddRangeAsync(employees);
                await context.SaveChangesAsync();
            }
        }
    }
}

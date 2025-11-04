using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EnginiDemoProject.Domain.Employees
{
    public class Employee 
    {
        public int Id { get; set; }
        public int? ManagerId { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<Employee> Subordinates { get; set; } = new();
    }
}

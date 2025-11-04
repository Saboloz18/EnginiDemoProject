using EnginiDemoProject.Domain.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnginiDemoProject.Presistence.EntityConfigurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name)
                .IsRequired() 
                .HasMaxLength(100); 
            builder.Property(e => e.ManagerId)
                .IsRequired(false);
            builder.Ignore(e => e.Subordinates);

        }
    }
}

using Employees.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Data.EntityConfiguration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(60);


            builder.Property(e => e.Salary)
                .IsRequired()
                .HasColumnType("DECIMAL")
                .HasPrecision(15, 2);

            builder.Property(e => e.Address)
                .IsRequired(false)
                .HasMaxLength(255);

            builder.HasOne(e => e.Manager)
                .WithMany(m => m.Employees)
                .HasForeignKey(e => e.ManagerId)
                .IsRequired(false);
                

            
        }
    }
}

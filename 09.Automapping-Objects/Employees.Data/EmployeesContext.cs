﻿
namespace Employees.Data
{
    using Employees.Data.EntityConfiguration;
    using Microsoft.EntityFrameworkCore;

    using Models;
    public class EmployeesContext : DbContext
    {
        public EmployeesContext() { }
        public EmployeesContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        }
    }
}
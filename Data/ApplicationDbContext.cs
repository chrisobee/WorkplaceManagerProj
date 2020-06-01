using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkplaceManager.Models;

namespace WorkplaceManager.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<SeniorManager> SeniorManagers { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<EmployeeJob> EmployeeJobs { get; set; }
        public DbSet<Subtask> Subtasks { get; set; }
        public DbSet<QualityOfWork> QualityOfWorks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole
                    {
                        Name = "Senior Manager",
                        NormalizedName = "SENIOR MANAGER",
                        Id = "3d0149f5-a07c-49f3-a71b-64339d768a3c"
                    },
                    new IdentityRole
                    {
                        Name = "Manager",
                        NormalizedName = "MANAGER",
                        Id = "1044e8d0-e0d0-4ec9-822e-e9c12f94f8bd"
                    },
                    new IdentityRole
                    {
                        Name = "Employee",
                        NormalizedName = "EMPLOYEE",
                        Id = "1717911b-db92-44c5-b150-e4ab5e8664a0"
                    }
                    );
        }
    }
}

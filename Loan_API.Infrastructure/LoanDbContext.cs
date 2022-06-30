using Loan_API.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_API.Infrastructure
{
    public class LoanDbContext : DbContext
    {
        public LoanDbContext(DbContextOptions<LoanDbContext> options) :
            base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Loan> Loans { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<LoanStatus> LoanStatuses { get; set; }
        public DbSet<LoanType> LoanTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, RoleName = "Accountant" },
                new Role { Id = 2, RoleName = "User" }
            );
            modelBuilder.Entity<Currency>().HasData(
                new Currency { Id = 1, CurrencyName = "GEL" },
                new Currency { Id = 2, CurrencyName = "USD" },
                new Currency { Id = 3, CurrencyName = "EUR" }
            );

            modelBuilder.Entity<LoanType>().HasData(
                new LoanType() { Id = 1, LoanTypeName = "Quick loan" },
                new LoanType() { Id = 2, LoanTypeName = "Auto loan" },
                new LoanType() { Id = 3, LoanTypeName = "Installment" }
                );
            modelBuilder.Entity<LoanStatus>().HasData(
                new LoanStatus { Id = 1, LoanStatusName = "In process" },
                new LoanStatus { Id = 2, LoanStatusName = "Approved" },
                new LoanStatus { Id = 3, LoanStatusName = "Rejected" }
                );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "Thomas", LastName = "Hardy", UserName = "admin@gmail.com", Password = BCryptNet.HashPassword("admin1234"), Age = 20, Salary = 1000, RoleId = 1},
                new User { Id = 2, FirstName = "Christina", LastName = "Berglund", UserName = "user2@gmail.com", Password = BCryptNet.HashPassword("admin1234"), Age = 25, Salary = 1000, IsBlocked = true },
                new User { Id = 3, FirstName = "Ana", LastName = "Trujillo", UserName = "user3@gmail.com", Password = BCryptNet.HashPassword("admin1234"), Age = 30, Salary = 1000, },
                new User { Id = 4, FirstName = "Maria", LastName = "Anders", UserName = "user4@gmail.com", Password = BCryptNet.HashPassword("admin1234"), Age = 35, Salary = 1000, }
                );

            modelBuilder.Entity<Loan>().HasData(
                new Loan { Id = 1, UserId = 2, Amount = 2000, LoanPeriodTime = new DateTime(2023, 12, 31, 5, 10, 20), CurrencyId = 1, LoanStatusId = 1, LoanTypeId = 1 },
                new Loan { Id = 2, UserId = 3, Amount = 3000, LoanPeriodTime = new DateTime(2023, 12, 31, 5, 10, 20), CurrencyId = 2, LoanStatusId = 1, LoanTypeId = 2 },
                new Loan { Id = 3, UserId = 4, Amount = 4000, LoanPeriodTime = new DateTime(2023, 12, 31, 5, 10, 20), CurrencyId = 3, LoanStatusId = 3, LoanTypeId = 3 }
                );

        }
    }
}





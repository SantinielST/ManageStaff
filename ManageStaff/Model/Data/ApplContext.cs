using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageStaff.Model;
using Microsoft.EntityFrameworkCore;

namespace ManageStaff.Data
{
    public class ApplContext : DbContext
    {
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Department> Departments { get; set; }

        public ApplContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ManageSaffDBAppDB;Trusted_Connection=True;");
        }
    }
}

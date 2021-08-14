using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Bài_tập_Quản_lý_Project.Models
{
    public class QLProject_Context :DbContext
    {
        public QLProject_Context() : base("Model_QLProject")
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<AssignTask> AssignTasks { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace bb23028_MD1
{
    
    public class UniversityContext : DbContext //creates the database
    {
        public UniversityContext() {
            _connectionString = "";
        }
        private string _connectionString;

        public UniversityContext(string cs) {
            _connectionString = cs;
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Assignement> Assignements { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string cs = ConfigurationManager.ConnectionStrings["MyUniversityConn"].ConnectionString;
            optionsBuilder.UseSqlServer(_connectionString);
        }

    }
}

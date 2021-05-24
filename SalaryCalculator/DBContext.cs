using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SalaryCalculator
{
    class DBContext : DbContext
    {
        public DbSet<Worker> Staff { get; set; }
        public DbSet<AccessLevel> AccessLevels { get; set; }
        public DbSet<Position> Positions { get; set; }

        public DBContext() : base("DefaultConnection") { }
    }
}

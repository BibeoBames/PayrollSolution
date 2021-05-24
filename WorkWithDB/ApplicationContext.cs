using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WorkWithDB
{
    class ApplicationContext : DbContext
    {
        public DbSet<Worker> Staff { get; set; }

        public ApplicationContext() : base("DefaultConnection") { }
    }
}

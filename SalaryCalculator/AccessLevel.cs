using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalaryCalculator
{
    [Table("AccessLevels")]
    public class AccessLevel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public AccessLevel() { }

        public AccessLevel(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

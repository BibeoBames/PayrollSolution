using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalaryCalculator
{
    [Table("Positions")]
    public class Position
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double SeniorityBonus { get; set; }
        public double MaxSeniorityBonus { get; set; }
        public double? ManagementBonus { get; set; }
    }
}

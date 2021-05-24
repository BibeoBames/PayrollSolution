using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalaryCalculator
{
    [Table("Staff")]
    public class Worker
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string HiringDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PositionID { get; set; }
        public int? SupervisorID { get; set; }
        public int AccessLevelID { get; set; }
        public double BaseSalary { get; set; }

        public Worker() { }

        public Worker(string fullName, 
            string hiringDate, 
            string email, 
            string password, 
            int positionID, 
            int? supervisorID, 
            int accessLevelID, 
            double baseSalary)
        {
            FullName = fullName;
            HiringDate = hiringDate;
            Email = email;
            Password = password;
            PositionID = positionID;
            SupervisorID = supervisorID;
            AccessLevelID = accessLevelID;
            BaseSalary = baseSalary;
        }

    }
}

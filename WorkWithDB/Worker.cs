using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkWithDB
{
    [Table("Staff")]
    class Worker
    {
        public int ID { get; set; }

        public string Fullname { get; set; }
        public string HiringDate { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int PositionID { get; set; }
        public int? SupervisorID { get; set; }
        public int AccessLevelID { get; set; }
        public float BaseSalary { get; set; }

        public Worker() { }

        public Worker(string fullname, string hiringDate, string login, string password, int positionID, int supervisorID, int accessLevelID, float baseSalary)
        {
            Fullname = fullname;
            HiringDate = hiringDate;
            Login = login;
            Password = password;
            PositionID = positionID;
            SupervisorID = supervisorID;
            AccessLevelID = accessLevelID;
            BaseSalary = baseSalary;
        }

        //нужно для дебага
        public override string ToString()
        {
            string finalString = "";
            finalString += "ID: " + ID + "\n";
            finalString += "Ф.И.О: " + Fullname + "\n";
            finalString += "Дата трудоустройства: " + HiringDate + "\n";
            finalString += "Логин: " + Login + "\n";
            finalString += "Пароль: " + Password + "\n";
            finalString += "Должность ID: " + PositionID + "\n";
            finalString += "Уровень доступа: " + AccessLevelID + "\n";
            finalString += "Начальная Зарплата: " + BaseSalary + "\n";
            if (SupervisorID != null)
            {
                finalString += "Руководтель: " + SupervisorID + "\n";
            }
            else
            {
                finalString += "Руководтель не назначен \n";
            }
            return finalString;
        }
    }
}

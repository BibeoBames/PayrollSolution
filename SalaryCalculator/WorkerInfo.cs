using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryCalculator
{
    //отдельный класс для представления в Listview а не для работы с базой
    public class WorkerInfo
    {
        public Worker BaseWorker { get; private set; }

        //Большенство переменных не отлсчаются от основного класса
        public int ID { get => BaseWorker.ID; }
        public string FullName { get => BaseWorker.FullName;}
        public string HiringDate { get => BaseWorker.HiringDate;}
        public string Email { get => BaseWorker.Email; }

        //переменные с уже конечными данными
        public string Position { get; private set; }
        public string SupervisorFullName { get; private set; }
        public string AccessLevel { get; private set; }
        public int SubordinatesAmount { get; private set; }
        public double FinalSalary { get; private set; }

        public WorkerInfo(Worker worker)
        {
            BaseWorker = worker;

            using (DBContext db = new DBContext())
            {
                //получение названия должности с базы
                Position basePosition = db.Positions.Where(X => X.ID == BaseWorker.PositionID).FirstOrDefault();
                Position = basePosition.Name;

                //получение имени руководителя
                if (BaseWorker.SupervisorID != null)
                {
                    Worker boss = db.Staff.Where(X => X.ID == BaseWorker.SupervisorID).FirstOrDefault();
                    SupervisorFullName = boss.FullName;
                }
                else
                {
                    SupervisorFullName = "Руководтель не назначен";
                }

                //access level
                AccessLevel = db.AccessLevels.Where(x => x.ID == BaseWorker.AccessLevelID).FirstOrDefault().Name;

                //количество подчиненных
                SubordinatesAmount = 0;
                if (Position != "Employee")
                {
                    SubordinatesAmount = db.Staff.Where(x => x.SupervisorID == BaseWorker.ID).Count();
                }

                //рассчитываем бонус за стаж
                DateTime properHiringDate = Convert.ToDateTime(BaseWorker.HiringDate);
                double totalYearBonus = 0;
                if (DateTime.Now > properHiringDate)
                {
                    double intervalInDays = DateTime.Now.Subtract(properHiringDate).TotalDays;
                    double intervalInYears = Math.Floor(intervalInDays / 365);
                    totalYearBonus = intervalInYears * basePosition.SeniorityBonus;
                    if (totalYearBonus > basePosition.MaxSeniorityBonus)
                    {
                        totalYearBonus = basePosition.MaxSeniorityBonus;
                    }
                }

                //рассчитываем бонус за подчиненных
                double totalManagementsBonus = 0;
                if (basePosition.ManagementBonus != null)
                {
                    totalManagementsBonus = SubordinatesAmount * (double)basePosition.ManagementBonus;
                }

                //рассчет конечной зп
                double totalBonus = totalManagementsBonus + totalYearBonus;
                FinalSalary = BaseWorker.BaseSalary / 100.0 * totalBonus + BaseWorker.BaseSalary;
            }
        }

    }
}

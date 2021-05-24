using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SalaryCalculator
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            string emailInput = EmailTextBox.Text.Trim();
            string passInput = PasswordTextBox.Text.Trim();

            if (emailInput == "" || passInput == "")
            {
                MessageBox.Show("Заполнены не все поля");
                return;
            }

            if (emailInput.Length <= 6 || !emailInput.Contains('@') || !emailInput.Contains('.'))
            {
                MessageBox.Show("Почта указана не верно");
                EmailTextBox.Text = "";
                return;
            }

            using (DBContext db = new DBContext())
            {
                Worker user = db.Staff.Where(x => x.Email == emailInput && x.Password == passInput).FirstOrDefault();
                if (user == null)
                {
                    MessageBox.Show("Неверные данные повторите попытку");
                }
                else
                {
                    MainWindow mw = new MainWindow(user);
                    mw.Show();
                    Close();
                }
            }


            /*
            using (DBContext db = new DBContext())
            {
                foreach (Worker worker in db.Staff)
                {
                    string finalString = "";
                    finalString += "ID: " + worker.ID + "\n";
                    finalString += "Ф.И.О: " + worker.FullName + "\n";
                    finalString += "Дата трудоустройства: " + worker.HiringDate + "\n";
                    finalString += "Почта: " + worker.Email + "\n";
                    finalString += "Пароль: " + worker.Password + "\n";
                    Position pos = db.Positions.Where(X => X.ID == worker.PositionID).FirstOrDefault();
                    finalString += "Должность: " + pos.Name + "\n";
                    if (worker.SupervisorID != null)
                    {
                        Worker boss = db.Staff.Where(X => X.ID == worker.SupervisorID).FirstOrDefault();
                        finalString += "Руководтель: " + boss.FullName + "\n";
                    }
                    else
                    {
                        finalString += "Руководтель не назначен \n";
                    }
                    AccessLevel al = db.AccessLevels.Where(X => X.ID == worker.AccessLevelID).FirstOrDefault();
                    finalString += "Уровень доступа: " + al.Name + "\n";
                    finalString += "Начальная Зарплата: " + worker.BaseSalary + "\n";

                    Debug.WriteLine(finalString);
                }
            }
            */
        }
    }
}

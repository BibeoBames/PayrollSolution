using System;
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
    /// Interaction logic for AddWorkerWindow.xaml
    /// </summary>
    public partial class AddWorkerWindow : Window
    {
        public List<string> PositionList { get; private set; } = new List<string>();
        public List<string> AccessList { get; private set; } = new List<string>();

        public AddWorkerWindow()
        {
            InitializeComponent();

            //заполняем комбобоксы
            using (DBContext db = new DBContext())
            {
                foreach (Position pos in db.Positions)
                {
                    PositionList.Add(pos.Name);
                }

                foreach (AccessLevel al in db.AccessLevels)
                {
                    AccessList.Add(al.Name);
                }
            }
            PositionComboBox.SelectedIndex = 0;
            AccessComboBox.SelectedIndex = 0;
            HiringDatePicker.SelectedDate = DateTime.Now;

            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (DBContext db = new DBContext())
            {
                //проверка имени
                string fullNameInput = FullnameTextBox.Text.Trim();
                if (fullNameInput == "" ||
                    !fullNameInput.Contains(' ') ||
                    fullNameInput.Length <= 5)
                {
                    MessageBox.Show("Некоректное имя новго сотрудника");
                    return;
                }

                //дата вступления в должность
                DateTime date = (DateTime)HiringDatePicker.SelectedDate;
                string hiringDateInput = date.ToString("yyyy-MM-dd");

                //проверка почты
                string emailInput = EmailTextBox.Text.Trim();
                if (emailInput.Length <= 6 || !emailInput.Contains('@') || !emailInput.Contains('.'))
                {
                    MessageBox.Show("Почта указана не верно");
                    return;
                }

                //проверка пароля
                string passwordInput = PasswordTextBox.Text.Trim();
                if (passwordInput.Length <= 6 ||            //должен быть больше 6 символов
                    !(passwordInput.Any(char.IsLetter) &&   //должен содержать хотябы одну букву
                    passwordInput.Any(char.IsDigit)) ||     //или цифру 
                    passwordInput.Any(char.IsPunctuation) ||//не должен содержать специальных имволов 
                    passwordInput.Any(char.IsWhiteSpace))
                {
                    MessageBox.Show("Неподходящий пароль: \n" +
                        "он должен быть не меньше 7 символов, \n" +
                        "должен содержать хотябы одну букву или одну цифру, \n" +
                        "не должен содержать специальных имволов");
                    return;
                }

                //должность
                string positionName = PositionComboBox.Text;
                int positionIDInput = db.Positions.Where(x => x.Name == positionName).FirstOrDefault().ID;

                //руководитель (опциональное поле)
                string managerName = ManagerTextBox.Text.Trim();
                int? managerIDInput;
                if (managerName == "")
                {
                    managerIDInput = null;
                }
                else
                {
                    Worker manager = db.Staff.Where(x => x.FullName == managerName).FirstOrDefault();
                    if (manager == null)
                    {
                        MessageBox.Show("Менеджер с таким именем не найден");
                        return;
                    }
                    Position managerPosition = db.Positions.Where(x => x.ID == manager.PositionID).FirstOrDefault();
                    if (managerPosition.Name == "Employee")
                    {
                        MessageBox.Show("Сотрудник указанный руководителем \n" +
                            "не может управлять персоналом");
                        return;
                    }
                    managerIDInput = manager.ID;
                }

                //доступ
                string accessName = AccessComboBox.Text;
                int accessLvlIDInput = db.AccessLevels.Where(x => x.Name == accessName).FirstOrDefault().ID;

                //зарплата
                string salaryString = SalaryTextBox.Text.Trim();
                double salaryInput;
                bool isDouble;
                isDouble = double.TryParse(salaryString, out salaryInput);
                if (!isDouble)
                {
                    MessageBox.Show("Зароботная плата указана не верно");
                    return;
                }

                //добаление нового сотрудника в базу
                Worker newWorker = new Worker(fullNameInput,
                    hiringDateInput,
                    emailInput,
                    passwordInput,
                    positionIDInput,
                    managerIDInput,
                    accessLvlIDInput,
                    salaryInput);
                db.Staff.Add(newWorker);
                db.SaveChanges();
                MessageBox.Show("Сотрудник добавлен в базу");
                Close();
            }
        }
    }
}

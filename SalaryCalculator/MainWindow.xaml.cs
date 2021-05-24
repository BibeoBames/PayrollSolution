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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SalaryCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Worker user;
        private AccessLevel userAL;
        public List<WorkerInfo> WorkerInfoList { get; private set; } = new List<WorkerInfo>();


        public MainWindow(Worker user)
        {
            InitializeComponent();
            DataContext = this;
            this.user = user;
            using (DBContext db = new DBContext())
                userAL = db.AccessLevels.Where(x => x.ID == user.AccessLevelID).FirstOrDefault();
            SetList();
            MainListView.SelectedItem = WorkerInfoList.First();
            if (userAL.Name == "Total")
            {
                AddButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
                TotalTextBlock.IsEnabled = true;
            }
        }

        public void SetList()
        {
            WorkerInfoList.Clear();
            //если полный доступ
            if (userAL.Name == "Total")
            {
                //выводим абсолютно всех сотрудников
                using (DBContext db = new DBContext())
                {
                    foreach (Worker worker in db.Staff)
                    {
                        WorkerInfoList.Add(new WorkerInfo(worker));
                    }
                }
                //считаем Суммарный объем выплат
                double totalCost = 0;
                foreach (WorkerInfo wi in WorkerInfoList)
                {
                    totalCost += wi.FinalSalary;
                }
                TotalTextBlock.Text = "Суммарный объем выплат: " + totalCost;
            }
            else
            {
                //если доступ базовй
                //выводим только сотрудника и подчиненных
                WorkerInfoList.Add(new WorkerInfo(user));
                List<Worker> subordinates = new List<Worker>();
                using (DBContext db = new DBContext())
                {
                    subordinates.AddRange(db.Staff.Where(x => x.SupervisorID == user.ID));
                }
                foreach (Worker sub in subordinates)
                {
                    WorkerInfoList.Add(new WorkerInfo(sub));
                }    
            }

            MainListView.Items.Refresh();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            SetList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddWorkerWindow aww = new AddWorkerWindow();
            aww.Show();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            WorkerInfo workerInfo = (WorkerInfo)MainListView.SelectedItem;

            if (user.ID == workerInfo.ID)
            {
                MessageBox.Show("Нельзя удалить себя");
                return;
            }

            using (DBContext db = new DBContext())
            {
                Worker workerToDelete = db.Staff.Where(x => x.ID == workerInfo.ID).FirstOrDefault();
                db.Staff.Remove(workerToDelete);
                db.SaveChanges();
                SetList();
                MessageBox.Show("Сотрудник удален");
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }
    }
}

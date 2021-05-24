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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WorkWithDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext db;
        public MainWindow()
        {
            InitializeComponent();
            db = new ApplicationContext();
        }

        private void ButtonOnclick(object sender, RoutedEventArgs e)
        {
            /*
            string fullname = "Борис Александрович Буряк";
            string hiringDate = "2015-01-01 11:11:11.111";
            string login = "Borya74";
            string password = "zxcvbn89";

            Worker worker = new Worker(fullname,
                hiringDate,
                login,
                password,
                1,2,1,
                40000);
            db.Staff.Add(worker);
            db.SaveChanges();
            */
            foreach (Worker worker in db.Staff)
            {
                Debug.WriteLine(worker.ToString());
            }
        }
    }
}

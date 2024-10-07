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
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.Common;
using BelAirWays.View;
using BelAirWays.ViewModel;
using BelAirWays.DBConnection;

namespace BelAirWays
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //UserContext db;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainVM(this);
            DBConnection.DBConnection.GetInstance();
        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
using BelAirWays.ViewModel;
using BelAirWays;
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

namespace BelAirWays.View
{
    /// <summary>    /// Логика взаимодействия для AdminView.xaml
    /// </summary>
    public partial class AdminView : Window
    {
        public AdminView(MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = new AdminVM(this, mainWindow);
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

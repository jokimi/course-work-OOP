using BelAirWays.ViewModel;
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

namespace BelAirWays.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddFlightsForm.xaml
    /// </summary>
    public partial class AddFlightsForm : Page
    {
        private DatePicker datePicker;
        public AddFlightsForm()
        {
            InitializeComponent();
            DataContext = new FlightsVM();
            datePicker = new DatePicker();
        }
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePicker.SelectedDate.HasValue)
            {
                string formattedDate = datePicker.SelectedDate.Value.ToString("yyyy-MM-dd");
                // Используйте formattedDate в нужных вам целях
            }
        }
    }
}
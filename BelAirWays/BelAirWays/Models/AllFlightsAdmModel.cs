using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BelAirWays.Models
{
    class AllFlightsAdmModel : INotifyPropertyChanged
    {

        private ListView _flightList;

        public BindingList<AllFlightsAdmModel> allFlights = new BindingList<AllFlightsAdmModel>();

        private int _idFlight;
        public int IdFlight
        {
            get { return _idFlight; }
            set
            {
                _idFlight = value;
                OnPropertyChanged("IdFlight");

            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");

            }
        }
        private string _surName;
        public string SurName
        {
            get { return _surName; }
            set
            {
                _surName = value;
                OnPropertyChanged("SurName");

            }
        }

        private string _Document;
        public string Document
        {
            get { return _Document; }
            set
            {
                _Document = value;
                OnPropertyChanged("SurName");

            }
        }

        private string _DATE_FROM;
        public string DATE_FROM
        {
            get { return _DATE_FROM; }
            set
            {
                _DATE_FROM = value;
                OnPropertyChanged("DATE_FROM");

            }
        }
        private string _DATE_TO;
        public string DATE_TO
        {
            get { return _DATE_TO; }
            set
            {
                _DATE_TO = value;
                OnPropertyChanged("DATE_TO");

            }
        }

        private string _townFrom;
        public string townFrom
        {
            get { return _townFrom; }
            set
            {
                _townFrom = value;
                OnPropertyChanged("townFrom");
            }
        }
        private string _townTo;
        public string townTo
        {
            get { return _townTo; }
            set
            {
                _townTo = value;
                OnPropertyChanged("townTo");
            }
        }

        private int _count_of_seats;
        public int count_of_seats
        {
            get { return _count_of_seats; }
            set
            {
                _count_of_seats = value;
                OnPropertyChanged("count_of_seats");
            }
        }
        private int _price;
        public int price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged("price");
            }
        }
        private string _company;
        public string company
        {
            get { return _company; }
            set
            {
                _company = value;
                OnPropertyChanged("company");
            }
        }
        private string _clas;
        public string clas
        {
            get { return _clas; }
            set
            {
                _clas = value;
                OnPropertyChanged("clas");
            }
        }

        public void Delete(ListView flightList)
        {
            FontFamily fontFam = new FontFamily("Trebuchet MS");
            string hexColor1 = "#d7ecfd";
            Color color1 = (Color)ColorConverter.ConvertFromString(hexColor1);
            SolidColorBrush backgroundBrush = new SolidColorBrush(color1);
            if (flightList.SelectedIndex != -1)
            {
                BindingList<AllFlightsAdmModel> t = new BindingList<AllFlightsAdmModel>();
                foreach (var x in allFlights)
                {
                    t.Add(x);
                }
                int i = flightList.SelectedIndex;


                SqlCommand sqlCommand = new SqlCommand();

                sqlCommand.CommandText = $"delete from tickets where {allFlights[i].IdFlight} = tickets.id_flight";
                sqlCommand.Connection = DBConnection.DBConnection.SqlConnection;
                sqlCommand.ExecuteNonQuery();


                sqlCommand.CommandText = $"delete from flights where {allFlights[i].IdFlight} = flights.id_flight";
                sqlCommand.Connection = DBConnection.DBConnection.SqlConnection;
                sqlCommand.ExecuteNonQuery();

                allFlights.RemoveAt(i);
            }
            else
            {
                Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                mbox.Background = backgroundBrush;
                mbox.Caption = "Ошибка удаления";
                mbox.Text = "Выберите рейс для удаления!";
                mbox.FontFamily = fontFam;
                mbox.ShowDialog();
            }
        }
        public void ShowAllFlights(ListView flightlist)
        {
            _flightList = flightlist;
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"select flights.id_flight, date_from, date_to,s1.town[от куда], s2.town[куда],company_name ,count_of_seats-count_of_customers, class, case class when 'Бизнес' 
then cast(route.length_of_route*company.cost_of_1km + company.cost_of_business as int) else cast(route.length_of_route*company.cost_of_1km + company.cost_of_econom as int)
end Стоимость from   flights inner join route 
on flights.route = route.id_route inner join airport s1  on s1.nameAirport = route.id_airport_from  inner join  airport s2 on s2.nameAirport = route.id_airport_to
inner join company on flights.company = company.company_name  inner join  aircrafts on flights.aircraft = aircrafts.name_aircraft  ";
            sqlCommand.Connection = DBConnection.DBConnection.SqlConnection;
            SqlDataReader reader = sqlCommand.ExecuteReader();

            allFlights.Clear();

            foreach (var i in reader)
            {
                allFlights.Add(new AllFlightsAdmModel(Convert.ToInt32(reader.GetInt32(0)), Convert.ToString(reader.GetDateTime(1).Date).Substring(0, 10), Convert.ToString(reader.GetDateTime(2).Date).Substring(0, 10), Convert.ToString(reader.GetSqlString(3)), Convert.ToString(reader.GetSqlString(4)), Convert.ToString(reader.GetSqlString(5)), Convert.ToInt32(reader.GetInt32(6)), Convert.ToString(reader.GetSqlString(7)), Convert.ToInt32(reader.GetInt32(8))));
            }
            flightlist.ItemsSource = allFlights;
            reader.Close();
        }


        public AllFlightsAdmModel(ListView flightList)
        {

        }
        public AllFlightsAdmModel()
        {

        }
         public AllFlightsAdmModel(int id_flight,string dateFrom, string dateTo, string TownFrom, string TownTo, string companyName, int countSeats, string Clas, int Price)
        {
            IdFlight = id_flight;
            DATE_FROM = dateFrom;
            DATE_TO = dateTo;
            townFrom = TownFrom;
            townTo = TownTo;
            company = companyName;
            count_of_seats = countSeats;
            clas = Clas;
            price = Price;
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}

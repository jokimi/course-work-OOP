using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BelAirWays.Models
{
    class HistoryModel : INotifyPropertyChanged
    {
        public static int UserId;
        private ListView _flightList;

        public BindingList<HistoryModel> allHistory = new BindingList<HistoryModel>();
   

        private int _idUser;
        public int IdUser
        {
            get { return _idUser; }
            set
            {
                _idUser = value;
                OnPropertyChanged("IdUser");

            }
        }

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

        private string _date;
        public string Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged("Date");

            }
        }
        private string _townFrom;
        public string TownFrom
        {
            get { return _townFrom; }
            set
            {
                _townFrom = value;
                OnPropertyChanged("TownFrom");

            }
        }
        private string _townTo;
        public string TownTo
        {
            get { return _townTo; }
            set
            {
                _townTo = value;
                OnPropertyChanged("TownTo");

            }
        }
        private string _clas;
        public string Clas
        {
            get { return _clas; }
            set
            {
                _clas = value;
                OnPropertyChanged("Clas");

            }
        }
        private string _company;
        public string Company
        {
            get { return _company; }
            set
            {
                _company = value;
                OnPropertyChanged("Company");

            }
        }


        public void InsertAllHistory()
        {
            allHistory.Clear();
            

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"select DISTINCT history.userId,history.flightId,history.airportFrom,history.airportTo,history.date,flights.company,flights.class from history inner join flights on history.flightId = flights.id_flight, tickets where history.flightId = tickets.id_flight";
            sqlCommand.Connection = DBConnection.DBConnection.SqlConnection;
            SqlDataReader reader = sqlCommand.ExecuteReader();

            foreach (var i in reader)
            {
                if(reader.GetInt32(0) == UserId)
                {
                    allHistory.Add(new HistoryModel(IdUser = reader.GetInt32(0), IdFlight = reader.GetInt32(1), TownFrom = reader.GetString(2), TownTo = reader.GetString(3), Date = Convert.ToString(reader.GetDateTime(4).Date).Substring(0, 10), Company = reader.GetString(5), Clas = reader.GetString(6)));
                }
            }
            reader.Close();
            _flightList.ItemsSource = allHistory;
        }

        public void ShowAllHistory()
        {

        }
        public HistoryModel(int userId, int idFlight, string townFrom, string townTo, string date, string company,  string clas)
        {
            IdUser = userId;
            IdFlight = idFlight;
            Date = date;
            TownFrom = townFrom;
            TownTo = townTo;
            Company = company;
            Clas = clas;
        }
        public HistoryModel()
        {

        }
        public HistoryModel(ListView flightList)
        {
            _flightList = flightList;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace BelAirWays.Models
{
    class RouteModel : INotifyPropertyChanged
    {
        public static ObservableCollection<AirportModel> AirportsFrom { get; set; } = new ObservableCollection<AirportModel>();
        public static ObservableCollection<AirportModel> AirportsTo { get; set; } = new ObservableCollection<AirportModel>();
        public List<int> id_flights = new List<int>();

        private AirportModel _selectedAirportTo;
        public AirportModel SelectedAirportTo
        {
            get
            {
                return _selectedAirportTo;
            }
            set
            {
                _selectedAirportTo = value;
                OnPropertyChanged("SelectedAirportTo");
            }
        }


        private AirportModel _selectedAirportFrom;
        public AirportModel SelectedAirportFrom
        {
            get
            {
                return _selectedAirportFrom;
            }
            set
            {
                _selectedAirportFrom = value;
                OnPropertyChanged("SelectedAirportFrom");
            }
        }

        private string _airport_From_To;
        public string airport_From_To
        {
            get { return _airport_From_To; }
            set
            {
                _airport_From_To = value;
                OnPropertyChanged("airport_name_from");
            }
        }

        private int _IdRoute;
        public int IdRoute
        {
            get { return _IdRoute; }
            set
            {
                _IdRoute = value;
                OnPropertyChanged("IdRoute");
            }
        }

        private string _length_of_route;
        public string length_of_route
        {
            get { return _length_of_route; }
            set
            {
                _length_of_route = value;
                OnPropertyChanged("length_of_route");
            }
        }

        public void DeleteAirportFrom()
        {
            FontFamily fontFam = new FontFamily("Trebuchet MS");
            string hexColor1 = "#d7ecfd";
            Color color1 = (Color)ColorConverter.ConvertFromString(hexColor1);
            SolidColorBrush backgroundBrush = new SolidColorBrush(color1);
            if (SelectedAirportFrom != null)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = $"select id_airport_from,id_flight from route  inner join flights on flights.route = route.id_route where id_airport_from = @airportFrom or id_airport_to = @airportFrom";
                    sqlCommand.Connection = DBConnection.DBConnection.SqlConnection;

                    SqlParameter airportFromParam = new SqlParameter("@airportFrom", SelectedAirportFrom.nameAirport);
                    sqlCommand.Parameters.Add(airportFromParam);
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    foreach (var x in reader)
                    {
                        id_flights.Add(reader.GetInt32(1));
                    }
                    reader.Close();

                    for (int i = 0; i < id_flights.Count; i++)
                    {
                        sqlCommand.CommandText = $"delete tickets where id_flight = {id_flights[i]}";
                        sqlCommand.ExecuteNonQuery();
                    }

                    for (int i = 0; i < id_flights.Count; i++)
                    {
                        sqlCommand.CommandText = $"delete flights where id_flight = {id_flights[i]}";
                        sqlCommand.ExecuteNonQuery();
                    }

                    sqlCommand.CommandText = $"delete route where id_airport_from = @airportFrom2";
                    SqlParameter airportFrom2Param = new SqlParameter("@airportFrom2", SelectedAirportFrom.nameAirport);
                    sqlCommand.Parameters.Add(airportFrom2Param);
                    sqlCommand.ExecuteNonQuery();

                    sqlCommand.CommandText = $"delete route where id_airport_to = @airportTo2";
                    SqlParameter airportToParam = new SqlParameter("@airportTo2", SelectedAirportFrom.nameAirport);
                    sqlCommand.Parameters.Add(airportToParam);
                    sqlCommand.ExecuteNonQuery();

                    sqlCommand.CommandText = $"delete airport where nameAirport = @airportt";
                    SqlParameter airportParam = new SqlParameter("@airportt", SelectedAirportFrom.nameAirport);
                    sqlCommand.Parameters.Add(airportParam);
                    sqlCommand.ExecuteNonQuery();

                    TakeAllAirports();

                    Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                    mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                    mbox.Background = backgroundBrush;
                    mbox.Caption = "Удаление аэропорта";
                    mbox.Text = "Аэропорт успешно удалён!";
                    mbox.FontFamily = fontFam;
                    mbox.ShowDialog();
                }
                catch
                {
                    Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                    mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                    mbox.Background = backgroundBrush;
                    mbox.Caption = "Ошибка удаления";
                    mbox.Text = "Данный аэропорт нельзя удалить, т. к. он используется!";
                    mbox.FontFamily = fontFam;
                    mbox.ShowDialog();
                }
            }
            else
            {
                Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                mbox.Background = backgroundBrush;
                mbox.Caption = "Ошибка удаления";
                mbox.Text = "Выберите аэропорт!";
                mbox.FontFamily = fontFam;
                mbox.ShowDialog();
            }
        }

        public void TakeAllAirports()
        {
            AirportsFrom.Clear();
            AirportsTo.Clear();

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"select * from airport";
            sqlCommand.Connection = DBConnection.DBConnection.SqlConnection;
            SqlDataReader reader = sqlCommand.ExecuteReader();

            foreach(var x in reader)
            {
                AirportsFrom.Add(new AirportModel { nameAirport = reader.GetString(0), country = reader.GetString(1), town = reader.GetString(2) });
                AirportsTo.Add(new AirportModel { nameAirport = reader.GetString(0), country = reader.GetString(1), town = reader.GetString(2) });
            }
            reader.Close();
        }

        public void Add()
        {
            FontFamily fontFam = new FontFamily("Trebuchet MS");
            string hexColor1 = "#d7ecfd";
            Color color1 = (Color)ColorConverter.ConvertFromString(hexColor1);
            SolidColorBrush backgroundBrush = new SolidColorBrush(color1);
            bool flag = true;
            Regex length = new Regex(@"^(?!0\d)\d*$");

            if (SelectedAirportFrom != null && SelectedAirportTo != null)
            {
                if (SelectedAirportFrom.nameAirport == SelectedAirportTo.nameAirport)
                {
                    Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                    mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                    mbox.Background = backgroundBrush;
                    mbox.Caption = "Ошибка добавления";
                    mbox.Text = "Вы указали 2 одинаковых аэропорта!";
                    mbox.FontFamily = fontFam;
                    mbox.ShowDialog();
                }
                else
                {
                    int len;
                    if (Int32.TryParse(length_of_route, out len))
                    {
                        if (!length.IsMatch(length_of_route))
                        {
                            Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                            mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                            mbox.Background = backgroundBrush;
                            mbox.Caption = "Ошибка добавления";
                            mbox.Text = "Некорректное значение длины маршрута!";
                            mbox.FontFamily = fontFam;
                            mbox.ShowDialog();
                            return;
                        }

                        if (len < 100)
                        {
                            Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                            mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                            mbox.Background = backgroundBrush;
                            mbox.Caption = "Ошибка добавления";
                            mbox.Text = "Слишком короткий маршрут!";
                            mbox.FontFamily = fontFam;
                            mbox.ShowDialog();
                            flag = false;
                        }

                        SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.CommandText = "select * from route";
                        sqlCommand.Connection = DBConnection.DBConnection.SqlConnection;
                        SqlDataReader reader = sqlCommand.ExecuteReader();

                        foreach (var i in reader)
                        {
                            if (reader.GetSqlString(1) == SelectedAirportFrom.nameAirport && reader.GetSqlString(2) == SelectedAirportTo.nameAirport)
                            {
                                Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                                mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                                mbox.Background = backgroundBrush;
                                mbox.Caption = "Ошибка добавления";
                                mbox.Text = "Данный маршрут уже существует!";
                                mbox.FontFamily = fontFam;
                                mbox.ShowDialog();
                                flag = false;
                                reader.Close();
                                break;
                            }
                        }
                        if (flag)
                        {
                            reader.Close();
                            sqlCommand.CommandText = $"insert into route (id_airport_from, id_airport_to, length_of_route ) values (@nameAirportFrom, @nameAirportTo,'{length_of_route}')";
                            sqlCommand.Connection = DBConnection.DBConnection.SqlConnection;
                            SqlParameter nameAirportFromParam = new SqlParameter("@nameAirportFrom", SelectedAirportFrom.nameAirport);
                            sqlCommand.Parameters.Add(nameAirportFromParam);
                            SqlParameter nameAirportToParam = new SqlParameter("@nameAirportTo", SelectedAirportTo.nameAirport);
                            sqlCommand.Parameters.Add(nameAirportToParam);
                            sqlCommand.ExecuteNonQuery();

                            FlightsModel.Routs.Clear();
                            sqlCommand.CommandText = "select * from route";
                            sqlCommand.Connection = DBConnection.DBConnection.SqlConnection;
                            SqlDataReader reader2 = sqlCommand.ExecuteReader();
                            foreach (var i in reader2)
                            {
                                FlightsModel.Routs.Add(new RouteModel { IdRoute = reader2.GetInt32(0), airport_From_To = $"{reader2.GetString(1)} - {reader2.GetString(2)}", length_of_route = Convert.ToString(reader2.GetInt32(3)) });
                            }
                            reader2.Close();
                            Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                            mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                            mbox.Background = backgroundBrush;
                            mbox.Caption = "Добавление маршрута";
                            mbox.Text = "Маршрут успешно добавлен!";
                            mbox.FontFamily = fontFam;
                            mbox.ShowDialog();
                        }
                    }
                    else
                    {
                        Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                        mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                        mbox.Background = backgroundBrush;
                        mbox.Caption = "Ошибка добавления";
                        mbox.Text = "Некорректный маршрут!";
                        mbox.FontFamily = fontFam;
                        mbox.ShowDialog();
                    }
                }
            }
            else
            {
                Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                mbox.Background = backgroundBrush;
                mbox.Caption = "Ошибка добавления";
                mbox.Text = "Заполните все поля!";
                mbox.FontFamily = fontFam;
                mbox.ShowDialog();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
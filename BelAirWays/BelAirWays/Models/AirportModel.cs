using System.ComponentModel;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace BelAirWays.Models
{
    class AirportModel : INotifyPropertyChanged
    {
        private string _nameAirport;
        public string nameAirport
        {
            get { return _nameAirport; }
            set
            {
                _nameAirport = value;
                OnPropertyChanged("nameAirport");
            }
        }

        private string _country;
        public string country
        {
            get { return _country; }
            set
            {
                _country = value;
                OnPropertyChanged("country");
            }
        }

        private string _town;
        public string town
        {
            get { return _town; }
            set
            {
                _town = value;
                OnPropertyChanged("town");
            }
        }

        public void Add()
        {
            FontFamily fontFam = new FontFamily("Trebuchet MS");
            string hexColor1 = "#d7ecfd";
            Color color1 = (Color)ColorConverter.ConvertFromString(hexColor1);
            SolidColorBrush backgroundBrush = new SolidColorBrush(color1);
            bool flag = true;
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "select * from airport";
            sqlCommand.Connection = DBConnection.DBConnection.SqlConnection;
            SqlDataReader reader = sqlCommand.ExecuteReader();

            Regex nameRegex = new Regex(@"^[a-zA-Zа-яА-Я\s-]+$");
            Regex countryRegex = new Regex(@"^[a-zA-Zа-яА-Я\s-]+$");
            Regex townRegex = new Regex(@"^[a-zA-Zа-яА-Я\s-]+$");

            foreach (var i in reader)
            {
                if (reader.GetSqlString(0) == nameAirport)
                {
                    Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                    mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                    mbox.Background = backgroundBrush;
                    mbox.Caption = "Ошибка добавления";
                    mbox.Text = "Данный аэропорт уже существует!";
                    mbox.FontFamily = fontFam;
                    mbox.ShowDialog();
                    flag = false;
                    reader.Close();
                    break;
                }
            }
            if (flag)
            {
                if (nameAirport != null  && country != null  && town != null )
                {
                    if (!nameRegex.IsMatch(nameAirport))
                    {
                        Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                        mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                        mbox.Background = backgroundBrush;
                        mbox.Caption = "Ошибка добавления";
                        mbox.Text = "Некорректное название аэропорта!";
                        mbox.FontFamily = fontFam;
                        mbox.ShowDialog();
                        reader.Close();
                        return;
                    }
                    if (!countryRegex.IsMatch(country))
                    {
                        Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                        mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                        mbox.Background = backgroundBrush;
                        mbox.Caption = "Ошибка добавления";
                        mbox.Text = "Некорректное название страны!";
                        mbox.FontFamily = fontFam;
                        mbox.ShowDialog();
                        reader.Close();
                        return;
                    }
                    if (!townRegex.IsMatch(town))
                    {
                        Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                        mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                        mbox.Background = backgroundBrush;
                        mbox.Caption = "Ошибка добавления";
                        mbox.Text = "Некорректное название города!";
                        mbox.FontFamily = fontFam;
                        mbox.ShowDialog();
                        reader.Close();
                        return;
                    }
                    if (nameAirport != "" && country != "" && town != "")
                    {
                        reader.Close();
                        sqlCommand.CommandText = $"insert into airport (nameAirport, country, town ) values(@nameAirport,@country,@town)";
                        SqlParameter nameParam = new SqlParameter("@nameAirport", nameAirport);
                        sqlCommand.Parameters.Add(nameParam);
                        SqlParameter countryParap = new SqlParameter("@country", country);
                        sqlCommand.Parameters.Add(countryParap);
                        SqlParameter townParam = new SqlParameter("@town", town);
                        sqlCommand.Parameters.Add(townParam);
                        sqlCommand.ExecuteNonQuery();
                        Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                        mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                        mbox.Background = backgroundBrush;
                        mbox.Caption = "Добавление аэропорта";
                        mbox.Text = "Аэропорт успешно добавлен!";
                        mbox.FontFamily = fontFam;
                        mbox.ShowDialog();

                        sqlCommand.CommandText = "select * from airport";
                        sqlCommand.Connection = DBConnection.DBConnection.SqlConnection;
                        SqlDataReader reader2 = sqlCommand.ExecuteReader();

                        RouteModel.AirportsFrom.Clear();
                        RouteModel.AirportsTo.Clear();
                        foreach (var x in reader2)
                        {
                            RouteModel.AirportsFrom.Add(new AirportModel { nameAirport = reader2.GetString(0), country = reader2.GetString(1), town = reader2.GetString(2) });
                            RouteModel.AirportsTo.Add(new AirportModel { nameAirport = reader2.GetString(0), country = reader2.GetString(1), town = reader2.GetString(2) });
                        }
                        reader2.Close();
                    }
                    else
                    {
                        Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                        mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                        mbox.Background = backgroundBrush;
                        mbox.Caption = "Ошибка добавления";
                        mbox.Text = "Заполнены не все поля!";
                        mbox.FontFamily = fontFam;
                        mbox.ShowDialog();
                        reader.Close();
                    }
                }
                else
                {
                    Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                    mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                    mbox.Background = backgroundBrush;
                    mbox.Caption = "Ошибка добавления";
                    mbox.Text = "Заполнены не все поля!";
                    mbox.FontFamily = fontFam;
                    mbox.ShowDialog();
                    reader.Close();
                }
            }        
            reader.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
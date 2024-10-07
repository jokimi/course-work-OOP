using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BelAirWays.Models
{
    class TicketsModel : INotifyPropertyChanged
    {
        private ListView _flightList;
        public static int IdUser;
        public  BindingList<TicketsModel> allFlights = new BindingList<TicketsModel>();
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
                OnPropertyChanged("Document");

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


        private DateTime _searchDate = DateTime.Now;
        public DateTime SearchDate
        {
            get { return _searchDate; }
            set
            {
                _searchDate = value;
                OnPropertyChanged("SearchDate");
            }
        }

        private string _town;
        public string Town
        {
            get { return _town; }
            set
            {
                _town = value;
                OnPropertyChanged("Town");
            }
        }

        private string _searchPrice;
        public string Searchprice
        {
            get { return _searchPrice; }
            set
            {
                _searchPrice = value;
                OnPropertyChanged("Searchprice");
            }
        }

        public int TicketIsValid(string name,string surname, string document, int idFlights)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "select * from tickets";
            sqlCommand.Connection = DBConnection.DBConnection.SqlConnection;
            SqlDataReader reader = sqlCommand.ExecuteReader();

            foreach(var x in reader)
            {             
                if(reader.GetInt32(2) == idFlights && reader.GetString(5) == Document)
                {
                    reader.Close();
                    return 2;
                }
                else if (reader.GetSqlString(3) == surname && reader.GetSqlString(4) == name && reader.GetInt32(2) == idFlights)
                {
                    reader.Close();
                    return 1;
                }
            }
            reader.Close();
            return 0;
        }

        public void SearchFlight(ListView flightList)
        {
            FontFamily fontFam = new FontFamily("Trebuchet MS");
            string hexColor1 = "#d7ecfd";
            Color color1 = (Color)ColorConverter.ConvertFromString(hexColor1);
            SolidColorBrush backgroundBrush = new SolidColorBrush(color1);
            BindingList<TicketsModel> heltlist = new BindingList<TicketsModel>();

            Regex townRegex = new Regex(@"^[a-zA-Zа-яА-Я\s-]+$");
            Regex costRegex = new Regex(@"^(?!0\d)\d*$");

            int valid;
            if ((Town == null || Town == "") && (Searchprice == null || Searchprice == ""))
            {
                for (int i = 0; i < allFlights.Count; i++)
                {
                    if (allFlights[i].DATE_FROM == SearchDate.ToString().Substring(0, 10))
                    {
                        heltlist.Add(allFlights[i]);
                    }
                }
            }
            else if (Town == null || Town == "")
            {
                if (Int32.TryParse(Searchprice, out valid))
                {
                    for (int i = 0; i < allFlights.Count; i++)
                    {
                        if (!costRegex.IsMatch(Searchprice))
                        {
                            Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                            mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                            mbox.Background = backgroundBrush;
                            mbox.Caption = "Ошибка поиска";
                            mbox.Text = "Некорректная цена!";
                            mbox.FontFamily = fontFam;
                            mbox.ShowDialog();
                            return;
                        }
                        if ((allFlights[i].DATE_FROM == SearchDate.ToString().Substring(0, 10)) && (allFlights[i].price == Convert.ToInt32(Searchprice)))
                        {
                            heltlist.Add(allFlights[i]);
                        }
                    }
                }
                else
                {
                    Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                    mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                    mbox.Background = backgroundBrush;
                    mbox.Caption = "Ошибка поиска";
                    mbox.Text = "Некорректная цена!";
                    mbox.FontFamily = fontFam;
                    mbox.ShowDialog();
                    return;
                }
            }
            else if (Searchprice == null || Searchprice == "")
            {
                for (int i = 0; i < allFlights.Count; i++)
                {
                    if ((allFlights[i].DATE_FROM == SearchDate.ToString().Substring(0, 10)) && ((allFlights[i].townTo).ToLower() == Town.ToLower()))
                    {
                        heltlist.Add(allFlights[i]);
                    }
                }
                if (!townRegex.IsMatch(Town))
                {
                    Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                    mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                    mbox.Background = backgroundBrush;
                    mbox.Caption = "Ошибка поиска";
                    mbox.Text = "Некорректное название города!";
                    mbox.FontFamily = fontFam;
                    mbox.ShowDialog();
                    return;
                }
            }
            else
            {
                if (townRegex.IsMatch(Town) && Int32.TryParse(Searchprice, out valid))
                {
                    if (!costRegex.IsMatch(Searchprice))
                    {
                        Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                        mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                        mbox.Background = backgroundBrush;
                        mbox.Caption = "Ошибка поиска";
                        mbox.Text = "Некорректная цена!";
                        mbox.FontFamily = fontFam;
                        mbox.ShowDialog();
                        return;
                    }
                    for (int i = 0; i < allFlights.Count; i++)
                    {
                        if ((allFlights[i].DATE_FROM == SearchDate.ToString().Substring(0, 10)) && ((allFlights[i].townTo).ToLower() == Town.ToLower()) && (allFlights[i].price == Convert.ToInt32(Searchprice)))
                        {
                            heltlist.Add(allFlights[i]);
                        }
                    }
                }
                else if (!townRegex.IsMatch(Town))
                {
                    Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                    mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                    mbox.Background = backgroundBrush;
                    mbox.Caption = "Ошибка поиска";
                    mbox.Text = "Некорректное название города!";
                    mbox.FontFamily = fontFam;
                    mbox.ShowDialog();
                    return;
                }
                else if (!costRegex.IsMatch(Searchprice))
                {
                    Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                    mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                    mbox.Background = backgroundBrush;
                    mbox.Caption = "Ошибка поиска";
                    mbox.Text = "Некорректная цена!";
                    mbox.FontFamily = fontFam;
                    mbox.ShowDialog();
                    return;
                }
                else
                {
                    Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                    mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                    mbox.Background = backgroundBrush;
                    mbox.Caption = "Ошибка поиска";
                    mbox.Text = "Некорректная цена!";
                    mbox.FontFamily = fontFam;
                    mbox.ShowDialog();
                    return;
                }
            }
            if (heltlist.Count == 0)
            {
                Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                mbox.Background = backgroundBrush;
                mbox.Caption = "Нет результатов";
                mbox.Text = "Не найдено ни одного рейса.";
                mbox.FontFamily = fontFam;
                mbox.ShowDialog();
                return;
            }
            else
            {
                // Вывод результатов поиска в flightList
                flightList.ItemsSource = heltlist;
            }
        }

        public void BuyTicket()
        {
            FontFamily fontFam = new FontFamily("Trebuchet MS");
            string hexColor1 = "#d7ecfd";
            Color color1 = (Color)ColorConverter.ConvertFromString(hexColor1);
            SolidColorBrush backgroundBrush = new SolidColorBrush(color1);

            Regex nameRegex = new Regex(@"^[a-zA-Zа-яА-Я\s-]+$");
            Regex surnameRegex = new Regex(@"^[a-zA-Zа-яА-Я\s-]+$");
            Regex passportRegex = new Regex(@"^[A-Z]{2}\d{7}$");

            if (_flightList.SelectedIndex != -1)
            {
                BindingList<TicketsModel> t = new BindingList<TicketsModel>();
                foreach (var x in allFlights)
                {
                    t.Add(x);
                }
                int i = _flightList.SelectedIndex;

                if (Name == null || SurName == null || Document == null)
                {
                    Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                    mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                    mbox.Background = backgroundBrush;
                    mbox.Caption = "Ошибка бронирования";
                    mbox.Text = "Заполните все поля!";
                    mbox.FontFamily = fontFam;
                    mbox.ShowDialog();
                }
                else if (!nameRegex.IsMatch(Name))
                {
                    Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                    mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                    mbox.Background = backgroundBrush;
                    mbox.Caption = "Ошибка бронирования";
                    mbox.Text = "Некорректное имя!";
                    mbox.FontFamily = fontFam;
                    mbox.ShowDialog();
                }
                else if (!surnameRegex.IsMatch(SurName))
                {
                    Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                    mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                    mbox.Background = backgroundBrush;
                    mbox.Caption = "Ошибка бронирования";
                    mbox.Text = "Некорректная фамилия!";
                    mbox.FontFamily = fontFam;
                    mbox.ShowDialog();
                }
                else if (!passportRegex.IsMatch(Document))
                {
                    Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                    mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                    mbox.Background = backgroundBrush;
                    mbox.Caption = "Ошибка бронирования";
                    mbox.Text = "Некорректный номер паспорта!";
                    mbox.FontFamily = fontFam;
                    mbox.ShowDialog();
                }
                else
                {
                    int number = TicketIsValid(Name, SurName, Document, allFlights[i].IdFlight);
                    if (allFlights[i].count_of_seats != 0)
                    {
                        switch (number)
                        {
                            case 0:
                            {
                                SqlCommand sqlCommand = new SqlCommand();
                                sqlCommand.CommandText = $"insert into tickets values({IdUser},{allFlights[i].IdFlight}, @SurName, @Name, @Document,'{price}')";
                                sqlCommand.Connection = DBConnection.DBConnection.SqlConnection;

                                SqlParameter surNameParam = new SqlParameter("@SurName", SurName);
                                sqlCommand.Parameters.Add(surNameParam);

                                    SqlParameter nameParam = new SqlParameter("@Name", Name);
                                    sqlCommand.Parameters.Add(nameParam);

                                    SqlParameter documentParam = new SqlParameter("@Document", Document);
                                    sqlCommand.Parameters.Add(documentParam);

                                    sqlCommand.ExecuteNonQuery();

                                    sqlCommand.CommandText = $"insert into history (userId, flightId, airportFrom, airportTo, date) values('{IdUser}','{allFlights[i].IdFlight}', @airportFrom, @airportTo,'{allFlights[i].DATE_FROM}')";
                                    sqlCommand.Connection = DBConnection.DBConnection.SqlConnection;

                                    SqlParameter townFromParam = new SqlParameter("@airportFrom", allFlights[i].townFrom);
                                    sqlCommand.Parameters.Add(townFromParam);

                                    SqlParameter townToParam = new SqlParameter("@airportTo", allFlights[i].townTo);
                                    sqlCommand.Parameters.Add(townToParam);

                                sqlCommand.ExecuteNonQuery();

                                sqlCommand.CommandText = @"select flights.id_flight, date_from, date_to,s1.town[от куда], s2.town[куда],company_name ,count_of_seats-count_of_customers, class, case class when 'Бизнес' 
                                then cast(route.length_of_route*company.cost_of_1km + company.cost_of_business as int) else cast(route.length_of_route*company.cost_of_1km + company.cost_of_econom as int)
                                end Стоимость from   flights inner join route on flights.route = route.id_route inner join airport s1  on s1.nameAirport = route.id_airport_from  inner join  airport s2 on s2.nameAirport = route.id_airport_to
                                inner join company on flights.company = company.company_name  inner join  aircrafts on flights.aircraft = aircrafts.name_aircraft";
                                    sqlCommand.Connection = DBConnection.DBConnection.SqlConnection;
                                    SqlDataReader reader = sqlCommand.ExecuteReader();
                                    foreach (var y in reader)
                                    {
                                        if (reader.GetInt32(0) == allFlights[i].IdFlight)
                                        {
                                            if (reader.GetInt32(6) == 0)
                                            {
                                                Xceed.Wpf.Toolkit.MessageBox mbox1 = new Xceed.Wpf.Toolkit.MessageBox();
                                                mbox1.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                                                mbox1.Background = backgroundBrush;
                                                mbox1.Caption = "Ошибка бронирования";
                                                mbox1.Text = "Нет свободных мест на рейс!";
                                                mbox1.FontFamily = fontFam;
                                                mbox1.ShowDialog();
                                            }
                                            else
                                            {
                                                int userPrice = reader.GetInt32(8);
                                                reader.Close();
                                                sqlCommand.CommandText = $"update flights set count_of_customers  = count_of_customers  + 1 where {allFlights[i].IdFlight} = flights.id_flight ";

                                                sqlCommand.Connection = DBConnection.DBConnection.SqlConnection;
                                                sqlCommand.ExecuteNonQuery();
                                                sqlCommand.CommandText = $"update tickets set price = {userPrice} where {allFlights[i].IdFlight}=tickets.id_flight ";
                                                sqlCommand.Connection = DBConnection.DBConnection.SqlConnection;
                                                sqlCommand.ExecuteNonQuery();
                                            }
                                            break;
                                        }
                                    }
                                    reader.Close();
                                    ShowAllFlights(_flightList);
                                    Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                                    mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                                    mbox.Background = backgroundBrush;
                                    mbox.Caption = "Билет забронирован";
                                    mbox.Text = "Билет забронирован, ждём Вас в турагенстве BelAirWays!";
                                    mbox.FontFamily = fontFam;
                                    mbox.ShowDialog();
                                    break;
                                }
                            case 1:
                                {
                                    Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                                    mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                                    mbox.Background = backgroundBrush;
                                    mbox.Caption = "Ошибка бронирования";
                                    mbox.Text = "Вы уже забронировали место на данный рейс!";
                                    mbox.FontFamily = fontFam;
                                    mbox.ShowDialog();
                                    break;
                                }
                            case 2:
                                {
                                    Xceed.Wpf.Toolkit.MessageBox mbox1 = new Xceed.Wpf.Toolkit.MessageBox();
                                    mbox1.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                                    mbox1.Background = backgroundBrush;
                                    mbox1.Caption = "Ошибка бронирования";
                                    mbox1.Text = "Этот номер паспорта уже используется!";
                                    mbox1.FontFamily = fontFam;
                                    mbox1.ShowDialog();
                                    break;
                                }
                        }
                    }
                    else
                    {
                        Xceed.Wpf.Toolkit.MessageBox mbox1 = new Xceed.Wpf.Toolkit.MessageBox();
                        mbox1.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                        mbox1.Background = backgroundBrush;
                        mbox1.Caption = "Ошибка бронирования";
                        mbox1.Text = "Нет мест на данный рейс!";
                        mbox1.FontFamily = fontFam;
                        mbox1.ShowDialog();
                    }
                }
            }
            else
            {
                Xceed.Wpf.Toolkit.MessageBox mbox1 = new Xceed.Wpf.Toolkit.MessageBox();
                mbox1.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                mbox1.Background = backgroundBrush;
                mbox1.Caption = "Ошибка бронирования";
                mbox1.Text = "Выберите рейс для покупки билета!";
                mbox1.FontFamily = fontFam;
                mbox1.ShowDialog();
            }
        }

        public void ShowAllFlights(ListView flightlist)
        {
            _flightList = flightlist;

            Town = null;
            Searchprice = null;
            SearchDate = DateTime.Now;
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
                allFlights.Add(new TicketsModel(Convert.ToInt32(reader.GetInt32(0)),Convert.ToString(reader.GetDateTime(1).Date).Substring(0, 10), Convert.ToString(reader.GetDateTime(2).Date).Substring(0, 10), Convert.ToString(reader.GetSqlString(3)), Convert.ToString(reader.GetSqlString(4)), Convert.ToString(reader.GetSqlString(5)), Convert.ToInt32(reader.GetInt32(6)), Convert.ToString(reader.GetSqlString(7)), Convert.ToInt32(reader.GetInt32(8))));
            }

            int countFlights = allFlights.Count;

            for (int i = countFlights - 1; i >= 0; i--)
            {
                if (Convert.ToDateTime(allFlights[i].DATE_FROM) < DateTime.Now)
                {
                    allFlights.RemoveAt(i);
                }
            }
            reader.Close();
            flightlist.ItemsSource = allFlights; 
        }
        public TicketsModel()
        {

        }

        public TicketsModel(int id_flight,string dateFrom, string dateTo, string TownFrom, string TownTo, string companyName, int countSeats, string Clas, int Price)
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

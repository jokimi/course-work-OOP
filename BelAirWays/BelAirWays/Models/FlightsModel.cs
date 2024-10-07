using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    class FlightsModel : INotifyPropertyChanged
    {
        public static ObservableCollection<RouteModel> Routs { get; set; } = new ObservableCollection<RouteModel>();
        public ObservableCollection<FlightsModel> Classes { get; set; } = new ObservableCollection<FlightsModel>();
        public static ObservableCollection<CompanyModel> Company { get; set; } = new ObservableCollection<CompanyModel>();
        public static ObservableCollection<AircraftModel> Aircrafts { get; set; } = new ObservableCollection<AircraftModel>();



        private RouteModel _selectedRouts;
        public RouteModel SelectedRouts
        {
            get
            {
                return _selectedRouts;
            }
            set
            {
                _selectedRouts = value;
                OnPropertyChanged("SelectedRouts");
            }
        }
        private AircraftModel _selectedAircraft;
        public AircraftModel SelectedAircraft
        {
            get
            {
                return _selectedAircraft;
            }
            set
            {
                _selectedAircraft = value;
                OnPropertyChanged("SelectedAircraft");
            }
        }
        private FlightsModel _selectedClass;
        public FlightsModel SelectedClass
        {
            get
            {
                return _selectedClass;
            }
            set
            {
                _selectedClass = value;
                OnPropertyChanged("SelectedClass");
            }
        }
        private CompanyModel _selectedCompany;
        public CompanyModel SelectedCompany
        {
            get{ return _selectedCompany; }
            set
            {
                _selectedCompany = value;
                OnPropertyChanged("SelectedCompany");
            }
        }

        private DateTime _date_from=DateTime.Now;
        public DateTime date_from
        {
            get { return _date_from; }
            set
            {
                _date_from = value;
                OnPropertyChanged("date_from");

            }
        }

        private DateTime _date_to = DateTime.Now;
        public DateTime date_to
        {
            get { return _date_to; }
            set
            {
                _date_to = value;
                OnPropertyChanged("date_to");

            }
        }

        private int _count_of_customers;
        public int count_of_customers
        {
            get { return _count_of_customers; }
            set
            {
                _count_of_customers = value;
                OnPropertyChanged("count_of_customers");
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

        public void DeleteCompany()
        {
            FontFamily fontFam = new FontFamily("Trebuchet MS");
            string hexColor1 = "#d7ecfd";
            Color color1 = (Color)ColorConverter.ConvertFromString(hexColor1);
            SolidColorBrush backgroundBrush = new SolidColorBrush(color1);
            if (SelectedCompany != null)
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = @"select company, company_name, id_flight from flights inner join company on company = company_name";
                sqlCommand.Connection = DBConnection.DBConnection.SqlConnection;
                SqlDataReader reader = sqlCommand.ExecuteReader();

                bool flag = true;

                foreach (var x in reader)
                {
                    if (reader.GetString(1) == SelectedCompany.company_name)
                    {
                        try
                        {
                            flag = false;
                            string company = reader.GetString(1);

                            int id_flight = reader.GetInt32(2);
                            reader.Close();
                            sqlCommand.CommandText = $"delete tickets where id_flight = {id_flight}";
                            sqlCommand.ExecuteNonQuery();

                            sqlCommand.CommandText = $"delete flights where company = '{company}'";
                            sqlCommand.ExecuteNonQuery();
                            sqlCommand.CommandText = $"delete company where company_name = '{company}'";
                            sqlCommand.ExecuteNonQuery();

                            Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                            mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                            mbox.Background = backgroundBrush;
                            mbox.Caption = "Удаление компании";
                            mbox.Text = "Компания успешно удалена!";
                            mbox.FontFamily = fontFam;
                            mbox.ShowDialog();

                            TakeAllRouts(); // Для обновления ComboBox
                            break;
                        }
                        catch
                        {
                            Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                            mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                            mbox.Background = backgroundBrush;
                            mbox.Caption = "Ошибка удаления";
                            mbox.Text = "Данную компанию нельзя удалить, т. к. она используется!";
                            mbox.FontFamily = fontFam;
                            mbox.ShowDialog();
                        }
                    }

                }
                reader.Close();

                if (flag)
                {
                    if (SelectedCompany != null)
                    {
                        sqlCommand.CommandText = $"delete company where company_name = @companyName";

                        SqlParameter nameParam = new SqlParameter("@companyName", SelectedCompany.company_name);
                        sqlCommand.Parameters.Add(nameParam);

                        sqlCommand.ExecuteNonQuery();
                        Routs.Remove(SelectedRouts);

                        Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                        mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                        mbox.Background = backgroundBrush;
                        mbox.Caption = "Удаление компании";
                        mbox.Text = "Компания успешно удалена!";
                        mbox.FontFamily = fontFam;
                        mbox.ShowDialog();
                    }
                }

                TakeAllRouts(); // для обновления ComboBox
            }
            else
            {
                Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                mbox.Background = backgroundBrush;
                mbox.Caption = "Ошибка удаления";
                mbox.Text = "Выберите компанию для удаления!";
                mbox.FontFamily = fontFam;
                mbox.ShowDialog();
            }

        }
        public void DeleteAircraft()
        {
            FontFamily fontFam = new FontFamily("Trebuchet MS");
            string hexColor1 = "#d7ecfd";
            Color color1 = (Color)ColorConverter.ConvertFromString(hexColor1);
            SolidColorBrush backgroundBrush = new SolidColorBrush(color1);
            if (SelectedAircraft != null)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = $"delete flights where aircraft = @aircraft";
                    sqlCommand.Connection = DBConnection.DBConnection.SqlConnection;

                    SqlParameter aircraftParam = new SqlParameter("@aircraft", SelectedAircraft.name_aircraft);
                    sqlCommand.Parameters.Add(aircraftParam);
                    sqlCommand.ExecuteNonQuery();

                    sqlCommand.CommandText = $"delete aircrafts where name_aircraft = @nameAircraft";
                    SqlParameter nameParam = new SqlParameter("@nameAircraft", SelectedAircraft.name_aircraft);
                    sqlCommand.Parameters.Add(nameParam);

                    sqlCommand.ExecuteNonQuery();

                    Aircrafts.Remove(SelectedAircraft);
                    Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                    mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                    mbox.Background = backgroundBrush;
                    mbox.Caption = "Удаление самолёта";
                    mbox.Text = "Самолёт успешно удалён!";
                    mbox.FontFamily = fontFam;
                    mbox.ShowDialog();
                }
                catch
                {
                    Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                    mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                    mbox.Background = backgroundBrush;
                    mbox.Caption = "Ошибка удаления";
                    mbox.Text = "Данный самолёт нельзя удалить, т. к. он используется!";
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
                mbox.Text = "Выберите самолёт для удаления!";
                mbox.FontFamily = fontFam;
                mbox.ShowDialog();
            }
        }
        public void DeleteRoute()
        {
            FontFamily fontFam = new FontFamily("Trebuchet MS");
            string hexColor1 = "#d7ecfd";
            Color color1 = (Color)ColorConverter.ConvertFromString(hexColor1);
            SolidColorBrush backgroundBrush = new SolidColorBrush(color1);
            if (SelectedRouts != null)
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = @"select tickets.id_flight,route,id_route from tickets inner join flights on tickets.id_flight = flights.id_flight inner join route on flights.route = route.id_route";
                sqlCommand.Connection = DBConnection.DBConnection.SqlConnection;
                SqlDataReader reader = sqlCommand.ExecuteReader();
                bool flag = true;

                foreach (var x in reader)
                {
                    if (reader.GetInt32(2) == SelectedRouts.IdRoute)
                    {
                        try
                        {
                            flag = false;
                            int id_flight = reader.GetInt32(0);
                            int route = reader.GetInt32(1);
                            int id_route = reader.GetInt32(2);
                            reader.Close();
                            sqlCommand.CommandText = $"delete tickets where id_flight = {id_flight}";
                            sqlCommand.ExecuteNonQuery();
                            sqlCommand.CommandText = $"delete flights where route = {route}";
                            sqlCommand.ExecuteNonQuery();
                            sqlCommand.CommandText = $"delete route where id_route = {id_route}";
                            sqlCommand.ExecuteNonQuery();

                            TakeAllRouts(); // Для обновления ComboBox
                            break;
                        }
                        catch
                        {
                            Xceed.Wpf.Toolkit.MessageBox mbox2 = new Xceed.Wpf.Toolkit.MessageBox();
                            mbox2.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                            mbox2.Background = backgroundBrush;
                            mbox2.Caption = "Ошибка удаления";
                            mbox2.Text = "Данный маршрут нельзя удалить, т. к. он используется!";
                            mbox2.FontFamily = fontFam;
                            mbox2.ShowDialog();
                            break;
                        }
                    }

                }
                reader.Close();
                if (flag)
                {
                    if (SelectedRouts != null)
                    {
                        try
                        {
                            sqlCommand.CommandText = $"delete flights where route = {SelectedRouts.IdRoute}";
                            sqlCommand.ExecuteNonQuery();

                            sqlCommand.CommandText = $"delete route where id_route = {SelectedRouts.IdRoute}";
                            sqlCommand.ExecuteNonQuery();
                            Routs.Remove(SelectedRouts);
                        }
                        catch
                        {
                            Xceed.Wpf.Toolkit.MessageBox mbox2 = new Xceed.Wpf.Toolkit.MessageBox();
                            mbox2.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                            mbox2.Background = backgroundBrush;
                            mbox2.Caption = "Ошибка удаления";
                            mbox2.Text = "Данный маршрут нельзя удалить, т. к. он используется!";
                            mbox2.FontFamily = fontFam;
                            mbox2.ShowDialog();
                        }
                    }
                }
                Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                mbox.Background = backgroundBrush;
                mbox.Caption = "Удаление маршрута";
                mbox.Text = "Маршрут успешно удалён!";
                mbox.FontFamily = fontFam;
                mbox.ShowDialog();
            }
            else
            {
                Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                mbox.Background = backgroundBrush;
                mbox.Caption = "Ошибка удаления";
                mbox.Text = "Выберите маршрут для удаления!";
                mbox.FontFamily = fontFam;
                mbox.ShowDialog();
            }
        }

        public void TakeAllRouts()
        {
            Classes.Clear();
            Classes.Add(new FlightsModel { clas = "Бизнес" });
            Classes.Add(new FlightsModel { clas = "Эконом" });

            Routs.Clear();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"select id_route,s1.town,s2.town,length_of_route from route   inner join airport s1  on s1.nameAirport = route.id_airport_from 
                 inner join  airport s2 on s2.nameAirport = route.id_airport_to";
            sqlCommand.Connection = DBConnection.DBConnection.SqlConnection;
            SqlDataReader reader = sqlCommand.ExecuteReader();
            foreach (var i in reader)
            {
                Routs.Add(new RouteModel { IdRoute = reader.GetInt32(0) ,airport_From_To = $"{reader.GetString(1)} - {reader.GetString(2)}", length_of_route = Convert.ToString(reader.GetInt32(3)) });
            }
            reader.Close();

            Aircrafts.Clear();
            sqlCommand.CommandText = @"select * from aircrafts";
            sqlCommand.Connection = DBConnection.DBConnection.SqlConnection;
            SqlDataReader reader2 = sqlCommand.ExecuteReader();

            foreach (var i in reader2)
            {        
                Aircrafts.Add(new AircraftModel { name_aircraft = reader2.GetString(0), count_of_seats = reader2.GetInt32(1).ToString(),  speed = reader2.GetInt32(2).ToString() });
            }
            reader2.Close();

            Company.Clear();
            sqlCommand.CommandText = @"select * from company";
            sqlCommand.Connection = DBConnection.DBConnection.SqlConnection;
            SqlDataReader reader3 = sqlCommand.ExecuteReader();

            foreach (var i in reader3)
            {               
                Company.Add(new CompanyModel { company_name = reader3.GetString(0), cost_of_1km = reader3.GetFloat(1).ToString(), cost_of_business = reader3.GetFloat(2).ToString(), cost_of_econom = reader3.GetFloat(3).ToString() });
            }
            reader3.Close();

        }

        public int Add()
        {
            FontFamily fontFam = new FontFamily("Trebuchet MS");
            string hexColor1 = "#d7ecfd";
            Color color1 = (Color)ColorConverter.ConvertFromString(hexColor1);
            SolidColorBrush backgroundBrush = new SolidColorBrush(color1);
            bool flag = true; // для проверки есть ли уже аткая запись

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "select * from flights";
            sqlCommand.Connection = DBConnection.DBConnection.SqlConnection;
            SqlDataReader reader = sqlCommand.ExecuteReader();

            if (date_from>date_to || date_from < DateTime.Now || date_to < DateTime.Now)
            {
                Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                mbox.Background = backgroundBrush;
                mbox.Caption = "Ошибка добавления";
                mbox.Text = "Неправильно указаны даты!";
                mbox.FontFamily = fontFam;
                mbox.ShowDialog();
                reader.Close();
                return 1;
            }

            if (SelectedAircraft != null && SelectedCompany != null && SelectedRouts != null && SelectedClass != null )
            {
                foreach (var i in reader)
                {
                    if (reader.GetSqlString(4) == SelectedCompany.company_name && reader.GetSqlString(5) == SelectedAircraft.name_aircraft && reader.GetDateTime(1) == date_from && reader.GetDateTime(2) == date_to && reader.GetInt32(3) == SelectedRouts.IdRoute)
                    {
                        Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                        mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                        mbox.Background = backgroundBrush;
                        mbox.Caption = "Ошибка добавления";
                        mbox.Text = "Данный рейс уже существует!";
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
                    sqlCommand.CommandText = $"insert into flights(date_from, date_to, route,company,aircraft,count_of_customers,class ) values('{date_from}','{date_to}','{SelectedRouts.IdRoute}',@company,@nameAircraft,'{0}','{SelectedClass.clas}')";

                    SqlParameter companyParam = new SqlParameter("@company", SelectedCompany.company_name);
                    sqlCommand.Parameters.Add(companyParam);

                    SqlParameter aircraftParam = new SqlParameter("@nameAircraft", SelectedAircraft.name_aircraft);
                    sqlCommand.Parameters.Add(aircraftParam);

                    sqlCommand.ExecuteNonQuery();
                    Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                    mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                    mbox.Background = backgroundBrush;
                    mbox.Caption = "Добавление рейса";
                    mbox.Text = "Рейс успешно добавлен!";
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
                mbox.Text = "Заполните все поля!";
                mbox.FontFamily = fontFam;
                mbox.ShowDialog();
            }
            reader.Close();
            return 0;
        }

        public FlightsModel(ListView flightList) { }
        public FlightsModel() { }
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

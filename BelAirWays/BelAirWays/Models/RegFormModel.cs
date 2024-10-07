  using System;
using BelAirWays;
using BelAirWays.DBConnection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using BelAirWays.View;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net;
using System.Windows.Media;
using System.Text.RegularExpressions;

namespace BelAirWays.Models
{
    class RegFormModel : INotifyPropertyChanged
    {
        private string EmailCode;
        
        public int userId { get; set; }

        private string _user_log_name;
        public string userLogName
        {
            get { return _user_log_name; }
            set
            {
                _user_log_name = value;
                OnPropertyChanged("userLogName");

            }
        }
        
        public int count_of_orders { get; set; }

        private string _user_email;
        public string userMail
        {
            get { return _user_email; }
            set
            {
                _user_email = value;
                OnPropertyChanged("userMail");
            }
        }

        public int bit { get; set; }

        private string _code;
        public string Code
        {
            get { return _code; }
            set
            {
                _code = value;
                OnPropertyChanged("Code");
            }
        }


        public string GetHashString(string s)
        {
            //переводим строку в байт-массим  
            byte[] bytes = Encoding.Unicode.GetBytes(s);

            //создаем объект для получения средст шифрования  
            MD5CryptoServiceProvider CSP =
                new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }
        static bool IsValidEmail(string email)
        {
            // Шаблон регулярного выражения для проверки почты
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            // Проверка соответствия строки шаблону
            return Regex.IsMatch(email, pattern);
        }
        public void SendCode()
        {
            FontFamily fontFam = new FontFamily("Trebuchet MS");
            string hexColor1 = "#d7ecfd";
            Color color1 = (Color)ColorConverter.ConvertFromString(hexColor1);
            SolidColorBrush backgroundBrush = new SolidColorBrush(color1);
            if (userMail != null && userMail != "")
            {
                bool flag = true;

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "select * from users";
                sqlCommand.Connection = DBConnection.DBConnection.SqlConnection;
                SqlDataReader reader = sqlCommand.ExecuteReader();

                foreach (var y in reader)
                {
                    if (userMail == reader.GetString(4))
                    {
                        flag = false;
                    }
                }

                if (flag)
                {
                    //Создание объекта для генерации чисел
                    Random rnd = new Random();
                    //Получить случайное число (в диапазоне от 0 до 10)
                    EmailCode = Convert.ToString(rnd.Next(100, 999));

                    if (IsValidEmail(userMail))
                        {
                            Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                            mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                            mbox.Background = backgroundBrush;
                            mbox.Caption = "Код подтверждения регистрации";
                            mbox.Text = $"Ваш код: {EmailCode}";
                            mbox.FontFamily = fontFam;
                            mbox.ShowDialog();
                            reader.Close();
                        }
                        // отправитель - устанавливаем адрес и отображаемое в письме имя
                        //MailAddress from = new MailAddress("jokeiminny@gmail.com", "belairways");
                        // кому отправляем
                        //MailAddress to = new MailAddress($"{userMail}");
                        // создаем объект сообщения
                        //MailMessage m = new MailMessage(from, to);
                        // тема письма
                        //m.Subject = "Код авторизации";
                        // текст письма
                        //m.Body = $"<h1>{EmailCode}</h1>";
                        //m.IsBodyHtml = true;

                        // адрес smtp-сервера и порт, с которого будем отправлять письмо
                        //SmtpClient smtp = new SmtpClient("localhost", 465);
                        // логин и пароль
                        //smtp.Credentials = new NetworkCredential("jokeiminny@gmail.com", "k76-sP8-MQi-ATg");
                        //smtp.EnableSsl = true;
                        //smtp.Send(m);
                    else
                    {
                        
                        Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                        mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                        mbox.Background = backgroundBrush;
                        mbox.Caption = "Ошибка отправки";
                        mbox.Text = "Проблемы с отправкой сообщения, проверьте введенную почту!";
                        mbox.FontFamily = fontFam;
                        mbox.ShowDialog();
                        reader.Close();
                    }
                    reader.Close();
                }
                else
                {
                    Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                    mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                    mbox.Background = backgroundBrush;
                    mbox.Caption = "Ошибка отправки";
                    mbox.Text = "Введенная почта уже занята!";
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
                mbox.Caption = "Ошибка отправки";
                mbox.Text = "Заполните поле e-mail!";
                mbox.FontFamily = fontFam;
                mbox.ShowDialog();
            }
        }
    
        public void Exit(MainWindow mainWindow, RegistrationForm regform)
        {
                regform.Hide();
                mainWindow.ShowDialog();
        }
        public void Add(string password, RegistrationForm regform, MainWindow mainWindow)
        {
            FontFamily fontFam = new FontFamily("Trebuchet MS");
            string hexColor1 = "#d7ecfd";
            Color color1 = (Color)ColorConverter.ConvertFromString(hexColor1);
            SolidColorBrush backgroundBrush = new SolidColorBrush(color1);
            if (Code == EmailCode && Code != null && Code != "")
            {

                if (userLogName == null || password == null || userMail == null || userLogName == "" && password =="" )
                {
                    Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                    mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                    mbox.Background = backgroundBrush;
                    mbox.Caption = "Ошибка регистрации";
                    mbox.Text = "Заполните все поля!";
                    mbox.FontFamily = fontFam;
                    mbox.ShowDialog();
                }
                else
                {
                    bool flag = true;
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = "select * from users";
                    sqlCommand.Connection = DBConnection.DBConnection.SqlConnection;
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    foreach (var i in reader)
                    {
                        if (reader.GetSqlString(1) == userLogName)
                        {
                            Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                            mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                            mbox.Background = backgroundBrush;
                            mbox.Caption = "Ошибка регистрации";
                            mbox.Text = "Данный логин уже занят!";
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
                        sqlCommand.CommandText = $"insert into users (userLogName,userPassword,count_of_orders,userMail,bit ) values( @userLogName, @password, {count_of_orders}, @userMail, {bit})";
                        SqlParameter userLogNameParam = new SqlParameter("@userLogName", userLogName);
                        sqlCommand.Parameters.Add(userLogNameParam);
                        SqlParameter passwordParam = new SqlParameter("@password", password);
                        sqlCommand.Parameters.Add(passwordParam);

                        SqlParameter mailParam = new SqlParameter("@userMail", userMail);
                        sqlCommand.Parameters.Add(mailParam);

                        sqlCommand.ExecuteNonQuery();
                        regform.Hide();
                        mainWindow.Show();
                    }
                    reader.Close();
                }
            }
            else
            {
                Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                mbox.Background = backgroundBrush;
                mbox.Caption = "Ошибка регистрации";
                mbox.Text = "Неверный код, попробуйте еще раз!";
                mbox.FontFamily = fontFam;
                mbox.ShowDialog();
            }  
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
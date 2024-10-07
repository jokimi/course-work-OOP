using BelAirWays.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net;
using System.Windows.Media;
//using MimeKit;
//using SendGrid;
//using SendGrid.Helpers.Mail;
//using MailKit.Net.Smtp;
using System.Diagnostics;

namespace BelAirWays.Models
{
    class LoginModel : INotifyPropertyChanged
    {
        private string _logname;
        public string LogName
        {
            get { return _logname; }
            set
            {
                _logname = value;
                OnPropertyChanged("LogName");
            }
        }

        private string _user_email;
        public string userMail
        {
            get { return _user_email; }
            set { _user_email = value; }
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

        public void ChangePassword(string password)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = $"update users set userPassword = @password where @LogName =users.userLogName";
            SqlParameter passwordParam = new SqlParameter("@password", password);
            sqlCommand.Parameters.Add(passwordParam);

            SqlParameter logNameParam = new SqlParameter("@LogName", LogName);
            sqlCommand.Parameters.Add(logNameParam);

            sqlCommand.Connection = DBConnection.DBConnection.SqlConnection;
            sqlCommand.ExecuteNonQuery();
        }

        public void Exit(MainWindow mainWindow, LoginForm loginform)
        {
            loginform.Hide();
            mainWindow.ShowDialog();

        }
        public void SendNewPassword()
        {
            FontFamily fontFam = new FontFamily("Trebuchet MS");
            string hexColor1 = "#d7ecfd";
            Color color1 = (Color)ColorConverter.ConvertFromString(hexColor1);
            SolidColorBrush backgroundBrush = new SolidColorBrush(color1);
            if (LogName != null && LogName != "")
            {
                //Создание объекта для генерации чисел
                Random rnd = new Random();
                //Получить случайное число (в диапазоне от 0 до 10)
                string randpass = Convert.ToString(rnd.Next(1000, 9999));

                bool flag = false;
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "select * from users";
                sqlCommand.Connection = DBConnection.DBConnection.SqlConnection;
                SqlDataReader reader = sqlCommand.ExecuteReader();

                foreach (var i in reader)
                {
                    if (reader.GetSqlString(1) == LogName)
                    {
                        userMail = Convert.ToString(reader.GetSqlString(4));
                        flag = true;
                    }

                }
                reader.Close();
                if (flag)
                {
                    try
                    {
                        /*// отправитель - устанавливаем адрес и отображаемое в письме имя
                        MailAddress from = new MailAddress("lizakozeka@gmail.com", "belairways");
                        // кому отправляем
                        MailAddress to = new MailAddress("jokeiminny@gmail.com", $"{LogName}");
                        // создаем объект сообщения
                        MailMessage m = new MailMessage(from, to);
                        // тема письма
                        m.Subject = "Ваш временный пароль";
                        // текст письма
                        m.Body = $"<h1>{randpass}</h1>";
                        m.IsBodyHtml = true;

                        // адрес smtp-сервера и порт, с которого будем отправлять письмо
                        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 465);
                        // логин и пароль
                        smtp.Credentials = new NetworkCredential("lizakozeka@gmail.com", "8352lobster");
                        smtp.EnableSsl = true;
                        smtp.Send(m);

                        var message = new Message(new List<string> { "jokeiminny@gmail.com" }, "Восстановление доступа", $"{randpass}");

                        var emailConfiguration = new EmailConfiguration()
                        {
                            SMTPFrom = "lizakozeka@gmail.com",
                            SMTPHost = "smtp.gmail.com",
                            SMTPLogin = "lizakozeka@gmail.com",
                            SMTPPassword = "8352lobster",
                            SMTPPort = 587
                        };

                        var _emailSender = new EmailSender(emailConfiguration);

                        _emailSender.SendEmail(message);

                        var client = new SendGridClient("MYKEY");
                        var msg = new SendGridMessage()
                        {
                            From = new EmailAddress("lizakozeka@gmail.com", "DX Team"),
                            Subject = "parol",
                            PlainTextContent = $"{randpass}",
                            HtmlContent = $"{randpass}"
                        };
                        msg.AddTo(new EmailAddress("jokeiminny@gmail.com"));

                        msg.SetClickTracking(false, false);

                        await client.SendEmailAsync(msg);*/

                        sqlCommand.CommandText = $"update users set userPassword = '{GetHashString(randpass)}' where '{LogName}'=users.userLogName";
                        sqlCommand.Connection = DBConnection.DBConnection.SqlConnection;
                        sqlCommand.ExecuteNonQuery();

                        Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                        mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                        mbox.Background = backgroundBrush;
                        mbox.Caption = "Восстановление доступа";
                        mbox.Text = $"Ваш временный пароль: {randpass}";
                        mbox.FontFamily = fontFam;
                        mbox.ShowDialog();
                    }
                    catch
                    {
                        Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                        mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                        mbox.Background = backgroundBrush;
                        mbox.Caption = "Ошибка отправки";
                        mbox.Text = "Проблемы с соединением!";
                        mbox.FontFamily = fontFam;
                        mbox.ShowDialog();
                    }
                }
                else
                {
                    Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                    mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                    mbox.Background = backgroundBrush;
                    mbox.Caption = "Ошибка отправки";
                    mbox.Text = "Пользователь с таким именем не найден!";
                    mbox.FontFamily = fontFam;
                    mbox.ShowDialog();
                }
            }
            else
            {
                Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                mbox.Background = backgroundBrush;
                mbox.Caption = "Ошибка отправки";
                mbox.Text = "Введите ваш логин!";
                mbox.FontFamily = fontFam;
                mbox.ShowDialog();
            }
        }
        public int Check(string password)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "select * from users";
            sqlCommand.Connection = DBConnection.DBConnection.SqlConnection;
            SqlDataReader reader = sqlCommand.ExecuteReader();

            foreach (var i in reader)
            {
                if (reader.GetSqlString(1) == LogName)
                {

                    if(reader.GetSqlString(2) == password)
                    {
                        if (reader.GetSqlInt32(5) == 0)
                        {
                            TicketsModel.IdUser = reader.GetInt32(0);
                            UpdatePasswordModel.IdUser = reader.GetInt32(0);
                            HistoryModel.UserId = reader.GetInt32(0);
                            reader.Close();
                            return 0;
                        }
                        else
                        {
                            reader.Close();
                            return 1;
                        }
                    }

                }

            }
            reader.Close();
            return 3;

        }
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

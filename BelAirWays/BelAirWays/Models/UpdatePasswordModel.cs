using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace BelAirWays.Models
{
    class UpdatePasswordModel
    {
        public static int IdUser;

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
            FontFamily fontFam = new FontFamily("Trebuchet MS");
            string hexColor1 = "#d7ecfd";
            Color color1 = (Color)ColorConverter.ConvertFromString(hexColor1);
            SolidColorBrush backgroundBrush = new SolidColorBrush(color1);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = $"update users set userPassword = @password where {IdUser}=users.userId";
            sqlCommand.Connection = DBConnection.DBConnection.SqlConnection;

            SqlParameter passwordParam = new SqlParameter("@password", password);
            sqlCommand.Parameters.Add(passwordParam);

            sqlCommand.ExecuteNonQuery();

            Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
            mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
            mbox.Background = backgroundBrush;
            mbox.Caption = "Изменение пароля";
            mbox.Text = "Пароль успешно изменен!";
            mbox.FontFamily = fontFam;
            mbox.ShowDialog();
        }
    }
}

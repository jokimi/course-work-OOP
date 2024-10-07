using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelAirWays.DBConnection
{
    class DBConnection
    {
        private static DBConnection instance;
        public static SqlConnection SqlConnection { get; private set; }
        private DBConnection()
        {
            SqlConnection = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=BelAirWays;Integrated Security=True");
            SqlConnection.Open();
        }
        public static DBConnection GetInstance()
        {
            if (instance == null)
                instance = new DBConnection();
            return instance;
        }
        public static void Close()
        {
            if (SqlConnection.State == System.Data.ConnectionState.Open)
                SqlConnection.Close();
        }
    }
}
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizOpdracht.Databases
{
    internal class DatabaseConnect
    {
        private static DatabaseConnect _DatabaseConnect = null;

        private readonly string mySqlCon = "server=localhost; user=root; database=quizopdracht; password=";

        private MySqlConnection mySqlConnection;

        private DatabaseConnect()
        {
            try
            {
                mySqlConnection = new MySqlConnection(mySqlCon);
                mySqlConnection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public MySqlConnection GetConnection()
        {
            return mySqlConnection;
        }

        public static DatabaseConnect GetInstance()
        {
            if (_DatabaseConnect == null)
            {
                _DatabaseConnect = new DatabaseConnect();
            }
            return _DatabaseConnect;
        }

        public void CloseConnection()
        {
            if (mySqlConnection != null && mySqlConnection.State == System.Data.ConnectionState.Open)
            {
                mySqlConnection.Close();
                Console.WriteLine("close connection");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace ShopContent_Тепляков.Classes
{
    public class Connection
    {
        private static readonly string config = "server=USER\\SQLEXPRESS;Trusted_Connection=No;DataBase=ShopContent;User=sa;pwd=sa";

        public static SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(config);
            connection.Open();
            return connection;
        }

        public static SqlDataReader Query(string SQL, out SqlConnection connection)
        {
            connection = OpenConnection();
            return new SqlCommand(SQL, connection).ExecuteReader();
        }

        public static void CloseConnection(SqlConnection connection)
        {
            connection.Close();
            SqlConnection.ClearPool(connection);
        }
    }
}

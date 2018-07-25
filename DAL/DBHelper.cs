using System;
using System.Collections.Generic;
using System.IO;
using MySql.Data.MySqlClient;

namespace DAL
{
    // Using Singleton Design Pattern
    public class DBHelper
    {
        private static MySqlConnection connection;
        public static MySqlConnection GetConnection()
        {
            if (connection == null)
            {
                connection = new MySqlConnection
                {
                    ConnectionString = "server=localhost;user id=Tom;password=nde17065;port=3306;database=shopping;SslMode=None"
                };
            }
            return connection;
        }
        public static MySqlDataReader ExecQuery(string query)
        {
            MySqlCommand command = new MySqlCommand(query, connection);
            return command.ExecuteReader();
        }
         public static MySqlConnection OpenConnection(string connectionString)
        {
            try{
                MySqlConnection connection = new MySqlConnection
                {
                    ConnectionString = connectionString
                };
                connection.Open();
                return connection;
            }catch{
                return null;
            }
        }
        public static MySqlConnection OpenConnection()
        {
            // if (connection == null)
            // {
            //     GetConnection();
            // }
            // connection.Open();
            // return connection;
            try
            {
                string connectionString;

                FileStream fileStream = File.OpenRead("ConnectionString.txt");
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    connectionString = reader.ReadLine();
                }
                fileStream.Close();

                return OpenConnection(connectionString);
            }
            catch
            {
                return null;
            }
        }
        public static void CloseConnection()
        {
            if (connection != null) connection.Close();
        }
        public static bool ExecTransaction(List<string> queries)
        {
            bool result = true;
            OpenConnection();
            MySqlCommand command = connection.CreateCommand();
            MySqlTransaction trans = connection.BeginTransaction();

            command.Connection = connection;
            command.Transaction = trans;

            try
            {
                foreach (var query in queries)
                {
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                    trans.Commit();
                }
                result = true;
            }
            catch
            {
                result = false;
                try
                {
                    trans.Rollback();
                }
                catch
                {
                }
            }
            finally
            {
                CloseConnection();
            }
            return result;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class CustomerDAL
    {
        private string query;
        private MySqlConnection connection;
        private MySqlDataReader reader;
        public static Customer GetCustomer(MySqlDataReader reader)
        {
            Customer customer = new Customer();
            customer.UserId = reader.GetInt32("UserID");
            customer.UserName = reader.GetString("UserName");
            customer.Phone = reader.GetString("UserPhone");
            customer.Password = reader.GetString("UserPass");
            customer.Address = reader.GetString("UserAddress");
            return customer;
        }
        public Customer Login(string UserName, string password)
        {
            Regex regex1 = new Regex("[a-zA-z0-9_]");
            Regex regex2 = new Regex("[a-zA-Z0-9_]");
            MatchCollection matchcollection1 = regex1.Matches(UserName);
            MatchCollection matchcollection2 = regex2.Matches(password);
            if ((matchcollection1.Count < UserName.Length) || (matchcollection2.Count < password.Length))
            {
                return null;
            }

            query = @"Select * From Users  where UserName = '" + UserName + "' and UserPass = '" + password + "';";
            Customer customer = null;
            connection = DBHelper.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(query, connection);
            using (reader = cmd.ExecuteReader())
            {
                
                if (reader.Read())
                {
                    // customer = new Customer();
                    customer = GetCustomer(reader);
                    // Console.Write(customer.UserName + customer.Password);
                }
            }
            connection.Close();
            return customer;
        }
    }

}

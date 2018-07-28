using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class Category_DAL
    {
        private MySqlConnection connection;
        private MySqlDataReader reader;
        public static Category GetCategory(MySqlDataReader reader)
        {
            Category Categorys = new Category();
            Categorys.CategoryID = reader.GetInt32("CategoryID");
            Categorys.CategoryName = reader.GetString("CategoryName");

            return Categorys;
        }
        public List<Category> GetCategory()
        {
            string query = "Select * from Category;";
            List<Category> list = new List<Category>();
            using (connection = DBHelper.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(GetCategory(reader));
                    }
                }
            }
            return list;
        }


    }

}
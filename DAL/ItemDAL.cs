using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistence;

namespace DAL {
    public class Item_DAL {
        private MySqlConnection connection;
        private MySqlDataReader reader;
        public static Items GetItem (MySqlDataReader reader) {
            Items item = new Items ();
            item.ItemId = reader.GetInt32 ("ItemID");
            item.ItemName = reader.GetString ("ItemName");
            item.ItemPrice = reader.GetInt32 ("ItemPrice");
            item.ItemAmount = reader.GetInt32 ("ItemAmount");
            item.CategoryID = reader.GetInt32 ("CategoryID");
            return item;
        }
        public List<Items> getItemByCategoryId(int CategoryID)
        {
            string query = "Select * from Items where CategoryID = "+ CategoryID+";";
            List<Items> lit = new List<Items>();
            using (connection = DBHelper.OpenConnection ()) {
                MySqlCommand cmd = new MySqlCommand (query, connection);
                using (reader = cmd.ExecuteReader ()) {
                    while (reader.Read ()) {
                        lit.Add (GetItem (reader));
                    }
                }
            }
            return lit;
        }
        public List<Items> GetItems () {
            string query = "Select * from Items;";
            List<Items> list = new List<Items> ();
            using (connection = DBHelper.OpenConnection ()) {
                MySqlCommand cmd = new MySqlCommand (query, connection);
                using (reader = cmd.ExecuteReader ()) {
                    while (reader.Read ()) {
                        list.Add (GetItem (reader));
                    }
                }
            }
            return list;
        }
        public Items getItemById (int id) {
            string query = $"SELECT * FROM Items WHERE ItemId = " + id + ";"; 
            Items item = null;
            using (connection = DBHelper.OpenConnection ()) {
                MySqlCommand cmd = new MySqlCommand (query, connection);
                using (reader = cmd.ExecuteReader ()) {
                    if (reader.Read ()) {
                        item = GetItem (reader);
                    }
                }
            }
            return item;
        }
        public List<Items> GetItemsByOrderId()
        {
            if (connection == null)
            {
                connection = DBHelper.OpenConnection();
            }
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            int order_id = 0;
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = "select OrderId from orderdetail order by OrderId desc limit 1 ;";
                    using (reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            order_id = reader.GetInt32("OrderId");
                        }
                    }
            string query = $"select items.ItemID, ItemName, OrderDetail.Amount as ItemAmount, ItemPrice, CategoryId from OrderDetail inner join Items on OrderDetail.ItemId = Items.ItemId where OrderId = "+order_id+";";
            List<Items> li = new List<Items>();
            using (connection = DBHelper.OpenConnection ()) {
                MySqlCommand cmd = new MySqlCommand (query, connection);
                using (reader = cmd.ExecuteReader ()) {
                    while (reader.Read ()) {
                        li.Add(GetItem (reader));
                    }
                }
            }
            return li;
        }
    }
}
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistence;
namespace DAL
{
    public class OrderDAL
    {
        public bool AddOrder(Orders order)
        {
            if (order == null || order.listItems == null || order.listItems.Count == 0)
            {
                return false;
            }
            bool result = true;
            MySqlConnection connection = DbConfiguration.OpenConnection();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.Connection = connection;
            //Khoa cap nhat tat ca table , bao dam tinh toan ven du lieu
            cmd.CommandText = "lock tables Users write, Orders write, Items write, OrderDetail write;";
            cmd.ExecuteNonQuery();
            MySqlTransaction trans = connection.BeginTransaction();
            cmd.Transaction = trans;
            MySqlDataReader reader;
            try
            {

                // Nhap du lieu cho bang Order
                //  cmd.CommandText = "insert into Orders(CodeOrders , UserId) values ("+order.Order_id+");";
                cmd.CommandText = "insert into Orders(UserID, OD_Status) values (@customerId, @orderStatus);";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@customerId", order.customer.UserId);
                cmd.Parameters.AddWithValue("@orderStatus", order.Status);
                cmd.ExecuteNonQuery();
                //get new Order_ID
                // cmd.CommandText = "select order_id from Orders order by order_id desc limit 1;";
                cmd.CommandText = "select LAST_INSERT_ID() as OrderID";
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    order.Order_id = reader.GetInt32("OrderID");
                }
                reader.Close();


                foreach (var item in order.listItems)
                {


                    //lấy giá trị cho item
                    cmd.CommandText = "select ItemPrice from Items where ItemID=@itemId";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@itemId", item.ItemId);
                    reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        throw new Exception("Not Exists Item");
                    }
                    item.ItemPrice = reader.GetDecimal("ItemPrice");
                    reader.Close();

                    //Nhập dữ liệu cho bảng orderdetail

                    cmd.CommandText = @"insert into OrderDetail(OrderID,ItemId,Unit_price,Amount) value (" + order.Order_id + "," + item.ItemId + "," + item.ItemPrice + "," + item.ItemAmount + ") on duplicate key update Amount = Amount +" + item.ItemAmount + ";";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "update Items set ItemAmount = ItemAmount - " + item.ItemAmount + " where ItemId = "+ item.ItemId + " ;";
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                cmd.CommandText = "unlock tables;";
                cmd.ExecuteNonQuery();
                DbConfiguration.CloseConnection();
            }

            return result;
        }
        public bool CreateOrder(Orders order)
        {
            if (order == null || order.listItems == null || order.listItems.Count == 0)
            {
                return false;
            }
            bool result = true;
            MySqlConnection connection = DbConfiguration.OpenConnection();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.Connection = connection;
            //Lock update all tables
            cmd.CommandText = "lock tables Users write, Orders write, Items write, OrderDetail write;";
            cmd.ExecuteNonQuery();
            MySqlTransaction trans = connection.BeginTransaction();
            cmd.Transaction = trans;
            MySqlDataReader reader = null;
            try
            {
                //Insert Order
                cmd.CommandText = "insert into Orders(UserID, OD_Status) values (@customerId, @orderStatus);";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@customerId", order.customer.UserId);
                cmd.Parameters.AddWithValue("@orderStatus", order.Status);
                cmd.ExecuteNonQuery();
                //get new Order_ID
                // cmd.CommandText = "select order_id from Orders order by order_id desc limit 1;";
                cmd.CommandText = "select LAST_INSERT_ID() as OrderID";
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    order.Order_id = reader.GetInt32("OrderID");
                }
                reader.Close();

                //insert Order Details table
                foreach (var item in order.listItems)
                {
                    if (item.ItemId == null || item.ItemAmount <= 0)
                    {
                        throw new Exception("Not Exists Item");
                    }
                    //get unit_price
                    cmd.CommandText = "select ItemPrice from Items where ItemID=@itemId";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@itemId", item.ItemId);
                    reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        throw new Exception("Not Exists Item");
                    }
                    item.ItemAmount = reader.GetInt16("ItemPrice"); //
                    reader.Close();

                    //insert to Order Details
                    cmd.CommandText = @"insert into OrderDetail(OrderID, ItemID, Unit_price,Amount ,OD_Status) value 
                            (" + order.Order_id + ", " + item.ItemId + ", " + item.ItemPrice + ", " + item.ItemAmount + ", " + order.Status + ");";
                    cmd.ExecuteNonQuery();

                    //update amount in Items
                    cmd.CommandText = "update Items set ItemAmount=ItemAmount-@quantity where ItemID=" + item.ItemId + ";";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@quantity", item.ItemAmount);
                    cmd.ExecuteNonQuery();
                }
                //commit transaction
                trans.Commit();
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                //unlock all tables;
                cmd.CommandText = "unlock tables;";
                cmd.ExecuteNonQuery();
                DBHelper.CloseConnection();
            }
            return result;
        }
    }

}
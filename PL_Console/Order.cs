using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using BL;
using Persistence;

namespace PL_Console
{
    class OrderInterface
    {
        public  Customer_Bl cbl = new Customer_Bl();

        public  void OrderMenu(Customer c)
        {
            //  Order o = new Order();
            short imChoose;
            string[] mainMenu1 = { "Mua Hàng", "Quay lại menu chính " };
            do
            {
                imChoose = Validate.Menu_Interface("Menu Hàng ", mainMenu1);
                switch (imChoose)
                {
                    case 1:
                        Order(c);
                        break;

                }
            } while (imChoose != mainMenu1.Length);

        }

        public static void Order(Customer c)
        {
            OrderBL orderbl = new OrderBL();
            Orders o = new Orders();
            // List<Items> lito = new List<Items>();
            CategoryBL cbl = new CategoryBL();
            List<Category> lc = new List<Category>();
            lc = cbl.GetCategory();
            foreach (var item in lc)
            {
                Console.WriteLine(item.CategoryID + " | " + item.CategoryName);
            }
            int choice;
            do
            {
                Console.Write("-chọn: ");
                choice = Convert.ToInt32(Console.ReadLine());
            } while ((choice < 1) || (choice > lc.Count));
            Item_BL ibl = new Item_BL();
            List<Items> lit = new List<Items>();
            lit = ibl.getItemByCategoryId(choice);
            while (true)
            {
                Console.WriteLine("___________________________________________________________________________");
                Console.WriteLine("|============================== Sản Phẩm =================================|");
                Console.WriteLine("|_________________________________________________________________________|");
                foreach (var item in lit)
                {
                    Console.WriteLine("|{0,-3} | {1,-43} | {2,-3}K VND| {3,-1} Amount", item.ItemId, item.ItemName, item.ItemPrice, item.ItemAmount);
                    Console.WriteLine("|------------------------------------------------------------------------|");

                }
                Console.Write("1.Chọn ID sản phẩm bạn muốn mua :");


                int product;
                // lito = new List<Items>();
                string choice1 = "";
                Items io = null;
                while (true)
                {
                    product = Validate.InputInt(Console.ReadLine());
                    int count = 0;
                    foreach (var iteml in lit)
                    {
                        if (product == iteml.ItemId)
                        {
                            count++;
                        }
                    }
                    if (count == 0)
                    {
                        Console.Write("Không có sản phẩm này trong danh mục bạn chọn , mời nhập lại: ");
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                io = ibl.getItemById(product);
                int quantity;
                Console.Write(" Nhập số lượng: ");
                while (true)
                {
                    quantity = Validate.InputInt(Console.ReadLine());
                    if (quantity <= io.ItemAmount)
                    {
                        break;
                    }
                    Console.Write("Số lượng không đúng, mời nhập lại: ");
                }

                io.ItemAmount = quantity;
                
                Console.WriteLine("Bạn có muốn tiếp tục thêm sản phẩm không? (C/K) ");
                choice1 = Console.ReadLine().ToUpper();
                if (choice1 == "C")
                {

                    continue;
                }
                else if (choice1 == "K")
                {

                    break;
                }
            }
           

            Console.WriteLine("Bạn có muốn hoàn tất Order không(C/K) ? ");
            string choice2 = Console.ReadLine().ToUpper();
            switch (choice2)
            {
                case "C":
                 Customer_Bl cbl1 = new Customer_Bl();
                   
                    // o.listItems = lito;
                    o.customer = c;
                    o.Status = 1;
                    Console.WriteLine(" Order " + (orderbl.AddOrder(o) ? "Hoàn tất!" : "Không hoàn tất!"));
                    break;
                case "K":
                    Console.WriteLine("Nhấn phím bất kỳ để hủy Order: ");
                    Console.ReadKey();
                    break;

            }

           
           



        }


    }
}
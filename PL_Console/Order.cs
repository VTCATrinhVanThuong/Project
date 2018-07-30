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
        public static Item_BL itembl = new Item_BL();
        public static List<Items> listitem = new List<Items>();


        public Customer_Bl cbl = new Customer_Bl();

        public void OrderMenu(Customer c)
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
            string choice1 = "";
            OrderBL orderbl = new OrderBL();
            Orders o = new Orders();
            List<Items> lito = new List<Items>();
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
                Console.WriteLine("___________________________________________________________________________________________________________");
                Console.WriteLine("|========================================== Sản Phẩm ======================================================|");
                Console.WriteLine("|__________________________________________________________________________________________________________|");
                Console.WriteLine("|ID  |                 Tên Sản Phẩm                        |        Giá Sản Phẩm      |    Số Lượng        |");
                foreach (var item in lit)
                {
                    Console.WriteLine("|{0,-3} | {1,-51} | {2,21} VND| {3,-14} ", item.ItemId, item.ItemName, item.ItemPrice, item.ItemAmount);
                    Console.WriteLine("|----------------------------------------------------------------------------------------------------------|");

                }
                Console.Write("1.Chọn ID sản phẩm bạn muốn mua :");


                int product;


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
                int dem = 0;
                int index = 0;
                foreach (var item in lito)
                {
                    if (item.ItemId == product)
                    {
                        dem++;
                        break;
                    }
                    index++;
                }
                if (io.ItemAmount != 0 && dem == 0)
                {
                    lito.Add(io);
                }
                else if (dem > 0)
                {
                    lito[index].ItemAmount += quantity;
                }
                while (true)
                {
                    Console.WriteLine("Bạn có muốn tiếp tục thêm sản phẩm không? (C/K) ");
                    choice1 = Console.ReadLine().ToUpper();
                    switch (choice1)
                    {
                        case "C":
                            break;
                        case "K":
                            break;
                        default:
                            continue;
                    }
                    break;
                }
                if (choice1 == "C")
                {

                    continue;
                }
                else if (choice1 == "K")
                {

                    break;
                }

            }
            // listitem = lito;
            Console.WriteLine("____________________________________________________________________________________________________");
            Console.WriteLine("|-----------------------------------------Đơn Hàng--------------------------------------------------|");
            Console.WriteLine("| Tên Sản Phẩm                                   |   Số Lượng   | Giá Sản Phẩm      |  Thành Tiền   |");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
            decimal totalprice = 0;
            // Console.WriteLine(o.listItems.Count);
            foreach (var item in lito)
            {
                decimal a = 0;
                a = item.ItemPrice * item.ItemAmount;
                totalprice += a;

                Console.WriteLine("|{0,-48}|{1,-14}|{2,15} VND|{3,11} VND|", item.ItemName, item.ItemAmount, pricevalid(item.ItemPrice), a);
            }
            Console.WriteLine("_____________________________________________________________________________________________________");
            Console.WriteLine("Tổng tiền:                                                                              {0} VND", pricevalid(totalprice));

            while (true)
            {
                Console.Write("Bạn có muốn thanh toán đơn hàng? (C/K)?");
                choice1 = Console.ReadLine().ToUpper();
                switch (choice1)
                {
                    case "C":
                        break;
                    case "K":
                        return;
                    default:
                        continue;
                }
                break;
            }
            Console.Write("Nhập số tiền khách trả(đồng): ");
            decimal cusmoney;
            while (true)
            {
                try
                {
                    cusmoney = Convert.ToDecimal(Console.ReadLine());
                    if (cusmoney < 0)
                    {
                        Console.Write("Số tiền trả không thể nhỏ hơn 0, mời bạn nhập lại(đồng): ");
                        continue;
                    }
                    else if (cusmoney > 999999999)
                    {
                        Console.Write("Số tiền quá lớn, mời nhập lại(đồng): ");
                        continue;
                    }
                    else if (cusmoney < totalprice)
                    {
                        Console.Write("Số tiền trả không đủ, mời nhập lại(đồng): ");
                   continue;
                    }
                }
                catch (System.Exception)
                {
                    Console.Write("Dữ liệu nhập vào không đúng hoặc số tiền quá lớn, mời bạn nhập lại(đồng): ");
                    continue;
                }
                break;
            }
            if ((cusmoney - totalprice) > 0)
            {
                Console.WriteLine("Tiền trả lại: {0} VND", pricevalid(cusmoney - totalprice));
            }
            Customer_Bl cbl1 = new Customer_Bl();
            o.listItems = lito;
            // Console.WriteLine(o.listItems.ToString()); 
            o.customer = c;
            o.Status = 1;
            Console.WriteLine(" Tạo Order " + (orderbl.AddOrder(o) ? "Hoàn tất!" : "Không hoàn tất!") + " Ấn phím bất kì để thoát!");
            // listitem = o.listItems;
            Console.ReadKey();


            // List<Items> newlit = ibl.GetItemsByOrderId();



        }
        public static string pricevalid(decimal pricez)
        {
            string prices = pricez.ToString();
            string price = "";
            int balance = (prices.Length - 1) % 3;
            for (int i = 0; i < prices.Length; i++)
            {
                // if (i == 0)
                // {
                //     price = price + prices[i];
                // }
                // else
                if (i == prices.Length - 1)
                {
                    price = price + prices[i];
                }
                else if ((i - balance) % 3 == 0)
                {
                    price = price + prices[i] + ".";
                }
                else
                {
                    price = price + prices[i].ToString();
                }
            }
            // price = price + "đ";
            return price;
        }


    }
}
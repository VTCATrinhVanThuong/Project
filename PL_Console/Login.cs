using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using BL;
using Persistence;

namespace PL_Console
{
    class UserInterface
    {
        public  Customer c = new Customer();
        public  Customer_Bl cbl = new Customer_Bl();
        public  void LoginConsole()
        {
            //  Order o = new Order();
            short imChoose;
            string[] mainMenu = { "Đăng Nhập", "Thoát" };
            do
            {
                imChoose = Validate.Menu_Interface(" Chào mừng đến với hệ thống mua hàng online ", mainMenu);
                switch (imChoose)
                {
                    case 1:
                        Login();
                        break;
                    case 2:
                        Environment.Exit(0);
                        break;
                }
            } while (imChoose != mainMenu.Length);

        }

        public  void Login()
        {
            Customer result = null;
            while (true)
            {
                Console.Write("- Nhập Tên người dùng       : ");
                c.UserName = Console.ReadLine();
                Console.Write("- Nhập Mật Khẩu             : ");
                c.Password = Validate.hidenpassword();
                result = cbl.login(c.UserName, c.Password);
                if (result != null)
                {
                    Console.WriteLine("Đăng nhập thành công!!!");
                    OrderInterface od = new OrderInterface();
                    od.OrderMenu(result);
                    break;
                }
                else
                {
                    Console.WriteLine("Nhập sai tài khoản vui lòng nhập lại: ");
                    continue;
                }
            }
            
            // od.OrderMenu(result);


        }

    }
}
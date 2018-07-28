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
        public static Customer_Bl cbl = new Customer_Bl();

        public static void OrderMenu(Customer c)
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
            while (true)
            {
                // Console.WriteLine("==============================================");
                // Console.WriteLine("============ Danh Mục Sản Phẩm ===============");
                // Console.WriteLine("==============================================");
                
            }



        }


    }
}
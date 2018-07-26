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
        public static void InterfaceShop()
        {
            while (true)
            {
                Console.Clear();

                Menu_Interface();
                LoginUser login = new LoginUser();
                int number;
                while (true)
                {
                    bool isINT = Int32.TryParse(Console.ReadLine(), out number);
                    if (!isINT)
                    {
                        Console.WriteLine("Giá trị sai vui lòng nhập lại.");
                        Console.Write("# Chon : ");
                    }
                    else if (number < 0 || number > 2)
                    {
                        Console.WriteLine("Giá trị sai vui lòng nhập lại 1 hoặc 2. ");
                        Console.Write("#Chọn : ");
                    }
                    else
                    {
                        break;
                    }
                }
                switch (number)
                {
                    case 1:
                        Console.Clear();
                        login.Login();
                        Console.Clear();
                        break;
                    case 2:
                        Environment.Exit(0);
                        Console.Clear();
                        return;
                }
            }
        }

        public static void Menu_Interface()
        {
            string[] menu = { "Đăng Nhập.", "Thoát", "#Chọn: " };
            Console.WriteLine("\n=============================================");
            Console.WriteLine("------------ Mua hàng trực tuyến ------------");
            Console.WriteLine("=============================================");
            for (int i = 0; i < 3; i++)
            {
                if (i != 2)
                {
                    Console.WriteLine($"{i + 1}. {menu[i]}");
                }
                else
                {
                    Console.Write($"{menu[i]}");
                }
            }
        }

        public class LoginUser
        {
            // private static string UserName;
            // private static string password;

            public void Login()
            {
                Console.Clear();
                Console.WriteLine("\n=============================================================");
                Console.WriteLine("------------------------  Đăng Nhập -------------------------");
                Console.WriteLine("=============================================================");
                Customer_Bl ad = new Customer_Bl();
                string UserName;
                string password;
                Console.Write("- Nhập Tên người dùng       : ");
                UserName = Console.ReadLine();
                Console.Write("- Nhập Mật Khẩu             : ");
                password = hidenpassword();
                while (CheckNotSpecialCharacters(UserName, password) == false)
                {
                    while (true)
                    {
                        Console.Write("Tên người dùng và mật khẩu không được chứa kí tự đặc biệt, bạn có muốn nhập lại? (C/K)");
                        string choice = Console.ReadLine().ToUpper();
                        switch (choice)
                        {
                            case "C":
                                Login();
                                return;
                            case "K":
                                InterfaceShop();
                                return;
                            default:
                                continue;
                        }
                    }
                }
                while (ad.Login(UserName, password) == null)
                {
                    while (true)
                    {
                        Console.Write("Tên người dùng và mật khẩu không đúng, bạn có muốn nhập lại? (C/K)");
                        string choice = Console.ReadLine().ToUpper();
                        switch (choice)
                        {
                            case "C":
                                Login();
                                return;
                            case "K":
                                InterfaceShop();
                                return;
                            default:
                                continue;
                        }
                    }
                }
            }

            public static string hidenpassword()
            {
                StringBuilder sb = new StringBuilder();
                while (true)
                {
                    ConsoleKeyInfo cki = Console.ReadKey(true);
                    if (cki.Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine();
                        break;
                    }

                    if (cki.Key == ConsoleKey.Backspace)
                    {
                        if (sb.Length > 0)
                        {
                            Console.Write("\b\0\b");
                            sb.Length--;
                        }

                        continue;
                    }

                    Console.Write('*');
                    sb.Append(cki.KeyChar);
                }
                return sb.ToString();
            }

            private static bool CheckNotSpecialCharacters(string UserName, string password)
            {
                Regex regex1 = new Regex("[a-zA-z0-9]");
                Regex regex2 = new Regex("[a-zA-Z0-9]");
                MatchCollection matchcollection1 = regex1.Matches(UserName);
                MatchCollection matchcollection2 = regex2.Matches(password);
                if ((matchcollection1.Count < UserName.Length) || (matchcollection2.Count < password.Length))
                {
                    return false;
                }
                return true;
            }

            // private static void CheckLoginSuccessOrFailure(int count)
            // {
            //     if (count != 1)
            //     {
            //         Console.Clear();
            //         Console.WriteLine("-------------------------------------------------------------");
            //         Console.WriteLine("   Tên người dùng hoặc mật khẩu của bạn chưa chính xác.");
            //         Console.WriteLine("-------------------------------------------------------------");
            //         while (true)
            //         {
            //             Console.WriteLine("1. Thử lại.");
            //             Console.WriteLine("2. Thoát");
            //             Console.Write("#Chọn : ");
            //             int number;
            //             while (true)
            //             {
            //                 bool isINT = Int32.TryParse(Console.ReadLine(), out number);
            //                 if (!isINT)
            //                 {
            //                     Console.WriteLine("Giá trị sai vui lòng nhập lại");
            //                     Console.Write("#Chọn : ");
            //                 }
            //                 else if (number < 0 || number > 2)
            //                 {
            //                     Console.WriteLine("Giá trị sai vui lòng nhập lại 1 - 2. ");
            //                     Console.Write("#Chọn : ");
            //                 }
            //                 else
            //                 {
            //                     break;
            //                 }
            //             }
            //             switch (number)
            //             {
            //                 case 1:
            //                     Console.Clear();
            //                     Menu_Interface();
            //                     break;
            //                 case 2:
            //                     Console.Clear();
            //                     InterfaceShop();
            //                     break;
            //             }
            //             if (number == 1)
            //             {
            //                 break;
            //             }
            //         }
            //     }
            // else
            // {
            // ShopInterface shop = new ShopInterface();
            // shop.Shop ();
            // Console.WriteLine ("-------------------------------------------------------------");
            // return;
            // }
            // }
            // public static Customer GetCustomer()
            // {
            //     Customer_Bl cus = new Customer_Bl();
            //     Customer customer = cus.Login(UserName, password);
            //     return customer;
            // }
        }
    }
}
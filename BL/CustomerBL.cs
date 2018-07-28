using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DAL;
using Persistence;

namespace BL
{
    public class Customer_Bl
    {
        public CustomerDAL idal = new CustomerDAL();
        // public Customer Login(String UserName, String password)
        // {
        //     Regex regex1 = new Regex("[a-zA-z0-9]");
        //     Regex regex2 = new Regex("[a-zA-Z0-9]");
        //     MatchCollection matchcollection1 = regex1.Matches(UserName);
        //     MatchCollection matchcollection2 = regex2.Matches(password);
        //     if ((matchcollection1.Count < UserName.Length) || (matchcollection2.Count < password.Length))
        //     {
        //         return null;
        //     }
        //     return idal.Login(UserName, password);
        // }
        public Customer login(string username, string password)
        {
            return idal.Login(username,password);
        }
    }


}
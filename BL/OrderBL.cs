using System;
using System.Collections.Generic;
using Persistence;
using DAL;

namespace BL
{
    public class OrderBL
    {
        private OrderDAL odl = new OrderDAL();
        public bool AddOrder(Orders order)
        {
            bool result = odl.AddOrder(order);
            return result;
        }
        // public  bool CreateOder(Orders order)
        // {
        //     return odl.CreateOrder(order);
        // }
    }
}
using System;
using System.Collections.Generic;
namespace Persistence
{

    public class Orders
    {

        public int? Order_id{get;set;}
        public Customer customer{get; set;}
        public int Amount {get; set;}
        public int Status{get;set;}

        public override int GetHashCode()
        {
            return Order_id.GetHashCode();
        }
        public List<Items> listItems{get;set;}
    }
}
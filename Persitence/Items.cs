using System;
namespace Persistence
{
    public class Items
    {
        public int? ItemId{get; set;}
        public string ItemName{get; set;}
        public decimal ItemPrice{get; set;}
        public int ItemAmount{get; set;}

        public int CategoryID{get;set;}
        // public Category category{get;set;}
        public override bool Equals(object obj){
            if(obj is Items){
                return ((Items)obj).ItemId.Equals(ItemId);
            }
            return false;
        }

        public override int GetHashCode(){
            return ItemId.GetHashCode();
        }
    }
}
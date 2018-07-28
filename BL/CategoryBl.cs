using System;
using Persistence;
using DAL;
using System.Collections.Generic;

namespace BL
{
    public class CategoryBL
    {
        Category_DAL cdal = new Category_DAL();
        public List<Category> GetCategory()
        {
            return cdal.GetCategory();
        }
    }
}
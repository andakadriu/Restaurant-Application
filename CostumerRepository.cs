using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantApp.Models;


namespace RestaurantApp.Repositories
{
    public class CostumerRepository
    {
        private RestaurantDBEntities objRestaurantDbEntities;
        public CostumerRepository()
        {

            objRestaurantDbEntities = new RestaurantDBEntities();

        }
        public IEnumerable<SelectListItem> GetAllCostumers()
        {
            var objSelectListItems = new List<SelectListItem>();
            objSelectListItems = (from obj in objRestaurantDbEntities.Customers
                                  select new SelectListItem()
                                  {
                                      Text = obj.CustomerName,
                                      Value = obj.CustomerId.ToString(),
                                      Selected = true

                                  }
                                 ).ToList();
            return objSelectListItems;
        }
    }
}
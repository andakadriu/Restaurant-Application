using webAppRestaurantDemoApp.Repositories;
using webAppRestaurantDemoApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using webAppRestaurantDemoApp.Models;
using RestaurantApp.Repositories;

namespace webAppRestaurantDemoApp.Controllers
{
    public class HomeController : Controller
    {
        private RestaurantDBEntities objRestaurantDbEntities;
        public HomeController()
        {

            objRestaurantDbEntities = new RestaurantDBEntities();
        }
        // GET: Home
        public ActionResult Index()
        {
            CostumerRepository objCustomerRepository = new CostumerRepository();
            ItemRepository objItemRepository = new ItemRepository();
            PaymentTypeRepository objPaymentTypeRepository = new PaymentTypeRepository();

            var objMultipleModels = new Tuple<IEnumerable<SelectListItem>, IEnumerable<SelectListItem>, IEnumerable<SelectListItem>>
           (objCustomerRepository.GetAllCostumers(), objItemRepository.GetAllItems(), objPaymentTypeRepository.GetAllPaymentType());
            return View(objMultipleModels);
        }

        [HttpGet]
        public JsonResult getItemUnitPrice(int itemId)
        {
            decimal UnitPrice = objRestaurantDbEntities.Items.Single(model => model.ItemId == itemId).ItemPrice;
            return Json(UnitPrice, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Index(OrderViewModel objOrderViewModel)
        {
            OrderRepository objOrderRepository = new OrderRepository();
            objOrderRepository.AddOrder(objOrderViewModel);
            return Json("Your order has been Successfully Placed", JsonRequestBehavior.AllowGet);
        }
    }
}
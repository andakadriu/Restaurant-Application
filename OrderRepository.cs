using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantApp.Repositories
{
    public class OrderRepository
    {
        private RestaurantDBEntities objRestaurantDbEntities;
        public OrderRepository()
        {

            objRestaurantDbEntities = new RestaurantDBEntities();

        }
        public bool AddOrder(OrderViewModel objOrderViewModel)
        {
            Order objOrder = new Order();
            objOrder.CostumerId = objOrderViewModel.CostumerId;
            objOrder.FinalTotal = objOrderViewModel.FinalTotal;
            objOrder.OrderDate = DateTime.Now;
            objOrder.OrderNumber = String.Format("{0:ddmmmyyyyhhmmss}", DateTime.Now);
            objOrder.PaymentTypeId = objOrderViewModel.PaymentTypeId;
            objRestaurantDbEntities.Orders.Add(objOrder);
            objRestaurantDbEntities.SaveChanges();
            int OrderId = objOrder.OrderId;
            foreach (var item in objOrderViewModel.ListOfOrderDetailViewModel)
            {
                OrderDetail objOrderDetail = new OrderDetail();
                objOrderDetail.OrderId = OrderId;
                objOrderDetail.Discount = item.Discount;
                objOrderDetail.ItemId = item.ItemId;
                objOrderDetail.Total = item.Total;
                objOrderDetail.UnitPrice = item.UnitPrice;
                objOrderDetail.Quantity = item.Quantity;
                objRestaurantDbEntities.OrderDetails.Add(objOrderDetail);
                objRestaurantDbEntities.SaveChanges();

                Transaction objTransaction = new Transaction();
                objTransaction.ItemId = item.ItemId;
                objTransaction.Quantity = (-1) * item.Quantity;
                objTransaction.TransactionDate = DateTime.Now;
                objTransaction.TypeId = 2;
                objRestaurantDbEntities.Transactions.Add(objTransaction);
                objRestaurantDbEntities.SaveChanges();
            }
            return true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class OrdersData : DataBase<YoRentEntities, Order>
    {
        /// <summary>
        /// set the order return date to now and the car available to true
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public bool ReturnCar(int orderId)
        {
            return Update(_order => _order.Id == orderId,
                _order =>
                {
                    _order.returndate = DateTime.Now;
                    _order.CarsForRent.available = true;
                    return true;
                });
        }
        
        /// <summary>
        /// set the car available to false
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public bool TakeCar(int orderId)
        {
            return Update(order => order.Id == orderId, order =>
            {
                order.CarsForRent.available = false;
                return true;
            });
        }

        //function to be called before the delete
        protected override void Delete(Order item)
        {
           
        }

        //filter order by the search
        protected override Expression<Func<Order, bool>> MySearch(string[] search)
        {
            return order => search.Any(s =>
            (order.User != null &&
            (order.User.idcard.Contains(s) ||
            order.User.username.Contains(s) ||
            order.User.fullname.Contains(s)))
            ||
            (order.carid != null &&
            order.CarsForRent.cartype != null &&
            (order.CarsForRent.CarType1.model.Contains(s) ||
            order.CarsForRent.CarType1.manufacturer.Contains(s) ||
             order.CarsForRent.registrationplate.Contains(s)
            )));
        }

        //the parameters to update
        protected override bool Update(Order order, Order updatedOrder)
        {
            order.carid = updatedOrder.carid;
            order.enddate = updatedOrder.enddate;
            order.startdate = updatedOrder.startdate;
            order.userid = updatedOrder.userid;
            order.returndate = updatedOrder.returndate;
            return true;
        }
    }
}

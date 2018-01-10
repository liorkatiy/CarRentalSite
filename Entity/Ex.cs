using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    //extension methods
    static class Ex
    {
        /// <summary>
        /// filter all the cars that arent available
        /// </summary>
        internal static IQueryable<CarsForRent> IsDateOk
            (this IQueryable<CarsForRent> cars, DateTime start, DateTime end)
        {
            return cars.Where(car =>
            car.Orders.All(order =>
            order.returndate != null ||
            !(start <= order.enddate
            && order.startdate <= end)));
        }

        /// <summary>
        /// select only 1 cartype 
        /// </summary>
        /// <param name="cars"></param>
        /// <returns></returns>
        internal static IQueryable<CarsForRent> DistinctCar
            (this IQueryable<CarsForRent> cars)
        {
            return cars.GroupBy(car =>
            car.cartype).Select(car =>
            car.FirstOrDefault());
        }

        //return generic array with take amount of items
        internal static T[] SearchResult<T>(this IQueryable<CarsForRent> cars, int skip, int take,Func<CarsForRent,T> select)
        {
            return 
                cars.OrderBy(car => car.CarType1.manufacturer).
                    Skip(skip * take).
                    Take(take).
                    Select(select).ToArray();
        }
    }
}

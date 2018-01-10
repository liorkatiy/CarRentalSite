using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class CarsData : DataBase<YoRentEntities, CarsForRent>
    {
        /// <summary>
        /// Take ISearch And Returns Array Of Generic Object"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="search"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        public T[] GetCarsByDate<T>(ISearch search,Func<CarsForRent,T> select) 
        {
            var cars = db.CarsForRents.Where(car => car.branch == search.Branch);

            if (search.IsManual != null)
                cars = cars.Where(x => x.CarType1.ismanual == search.IsManual);
            if (search.Model != null)
                cars = cars.Where(x => x.CarType1.model.Contains(search.Model));
            if (search.Manufacturer != null)
                cars = cars.Where(x => x.CarType1.manufacturer.Contains(search.Manufacturer));
            if (search.Year != null)
                cars = cars.Where(x => x.CarType1.year >= search.Year);

            cars = cars
                .IsDateOk(search.Start, search.End)
                .DistinctCar();

            var f = cars.ToArray();
            return cars.SearchResult(search.Skip, search.Take,select);
        }

        /// <summary>
        /// check if the car is truly available between the given dates
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="regPlate"></param>
        /// <returns></returns>
        public bool IsDateValid(DateTime start, DateTime end, string regPlate)
        {
            var car = db.CarsForRents.FirstOrDefault(_car => _car.registrationplate == regPlate);
            if (car != null)
            {
                return car.Orders.All(
                    order =>
                     order.returndate != null ||
                     !(start <= order.enddate
                     && order.startdate <= end));
            }
            return false;
        }

        //search to filter cars in the get from search method
        protected override Expression<Func<CarsForRent, bool>> MySearch(string[] search)
        {
            return car =>
                search.Any(s =>
                    car.CarType1.manufacturer.Contains(s) ||
                    car.CarType1.model.Contains(s) ||
                    car.CarType1.year.ToString().Contains(s) ||
                    car.registrationplate.Contains(s) ||
                    car.Branch1.name.Contains(s));
        }

        //method to be called before the delete
        protected override void Delete(CarsForRent item)
        {
            foreach (var order in item.Orders)
            {
                order.carid = null;
            } 
        }

        //what parameters to update
        protected override bool Update(CarsForRent item, CarsForRent updatedItem)
        {
            item.available = updatedItem.available;
            item.branch = updatedItem.branch;
            item.cartype = updatedItem.cartype;
            item.km = updatedItem.km;
            return true;
        }
    }
}

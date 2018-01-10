using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.IO;

namespace Entity
{
    public class CarTypeData : DataBase<YoRentEntities, CarType>
    {
        //what parameters to update
        protected override bool Update(CarType item, CarType updatedItem)
        {
            item.dailycost = updatedItem.dailycost;
            item.latefee = updatedItem.latefee;
            item.model = updatedItem.model;
            item.manufacturer = updatedItem.manufacturer;
            item.year = updatedItem.year;
            item.ismanual = updatedItem.ismanual;
            item.picture = updatedItem.picture;
            return true;
        }

        //filter cartype in the getfromsearch method
        protected override Expression<Func<CarType, bool>> MySearch(string[] search)
        {
            return carType => 
                search.Any(s => 
                    carType.model.Contains(s) || 
                    carType.manufacturer.Contains(s) || 
                    carType.year.ToString().Contains(s));
        }

        //function to b called before delete
        protected override void Delete(CarType item)
        {
            foreach (var car in item.CarsForRents)
            {
                car.cartype = null;
            } 
        }

        /// <summary>
        /// check if the directory have pictures that are not in the database
        /// </summary>
        /// <param name="path"></param>
        /// <param name="pictures"></param>
        public void RemovePictures(string path)
        {
            string[] pictures = db.CarTypes
                .Select(carType => carType.picture).ToArray();
            foreach (var filePath in Directory.EnumerateFiles(path))
            {
                var fileName = Path.GetFileName(filePath);
                bool delete = !pictures.Any(picture => picture == fileName);
                if (delete)
                    File.Delete(filePath);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    //class to control branches database
    public class BranchesData : DataBase<YoRentEntities, Branch>
    {
        //make sure all the cars with foregin key that point on this car will b null
        protected override void Delete(Branch branch)
        {
          foreach(var car in branch.CarsForRents)
            {
                car.branch = null;
            }
        }

        //check if the search contains any branch name
        protected override Expression<Func<Branch, bool>> MySearch(string[] search)
        {
            return branch => search.Any(s => branch.name.Contains(s));
        }

        //what part of the branch to update
        protected override bool Update(Branch branch, Branch updatedbranch)
        {
            branch.name = updatedbranch.name;
            branch.longitude = updatedbranch.longitude;
            branch.latitude = updatedbranch.latitude;
            return true;
        }
    }
}

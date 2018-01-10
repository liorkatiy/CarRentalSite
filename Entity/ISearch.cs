using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    //the search for car parameters
    public interface ISearch
    {
        int Skip { get; }
        int Take { get; }
        DateTime Start { get; }
        DateTime End { get; }
        bool? IsManual { get; }
        string Manufacturer { get; }
        int? Year { get; }
        string Model { get; }
        int Branch { get; }
    }
}

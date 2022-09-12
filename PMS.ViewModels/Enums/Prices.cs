using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.ViewModels.Enums
{
    public enum Prices: int
    {
        BasePriceAbove10Below50km = 1000,
        PriceForEachKilometerAbove10km = 10,
        BasePriceAbove50Below100km = 5000,
        PriceForEachKilometerAbove50km = 8,
        BasePriceAbove100km = 10000,
        PriceForEachKilometerAbove100km = 7,
        PriceForPiano = 5000
    }


}

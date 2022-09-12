using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.ViewModels.Enums
{
    public enum Prices: int
    {
        BasePriceAboveFirstBelowSecondDist = 1000,      
        PriceForEachKmAboveFirstDist = 10,              
        BasePriceAboveSecondBelowThirdDist = 5000,      
        PriceForEachKmAboveSecondDist = 8,              
        BasePriceAboveThirdDist = 10000, 
        PriceForEachKmAboveThirdDist = 7,                
        PriceForPiano = 5000
    }


}

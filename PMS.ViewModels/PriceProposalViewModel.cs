using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.ViewModels
{
    public class PriceProposalViewModel
    {
        public int id { get; set; }
        public int distance { get; set; }
        public int livingArea { get; set; }
        public int atticArea { get; set; }
        public bool hasPiano { get; set; }
        public int price { get; set; }
    }

    public class CalculatePriceProposalViewModel
    {
        public int id { get; set; }
        public int distance { get; set; }
        public int livingArea { get; set; }
        public int atticArea { get; set; }
        public bool hasPiano { get; set; }
        public string addressFrom { get; set; }
        public string addressTo { get; set; }
    }
}

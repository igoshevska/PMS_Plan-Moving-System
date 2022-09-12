using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.ViewModels
{
    public class ProposalViewModel
    {
        public int id { get; set; }
        public string proposalNumber { get; set; }
        public string addressFrom { get; set; }
        public string addressTo { get; set; }
        public OrderViewModel order { get; set; }
        public PriceProposalViewModel priceProposal { get; set; }
        public UserViewModel user { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain
{
    public class Proposal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ProposalNumber { get; set; }
        public string AddressFrom { get; set; }
        public string AddressTo { get; set; }
        public int? OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        public int PriceProposalId { get; set; }
        [ForeignKey("PriceProposalId")]
        public PriceProposal PriceProposal { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain
{
    public class PriceProposal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int Distance { get; set; }
        [Required]
        public int LivingArea { get; set; }
        [Required]
        public int AtticArea { get; set; }
        [Required]
        public bool HasPiano { get; set; }
        [Required]
        public int Price { get; set; }
    }
}

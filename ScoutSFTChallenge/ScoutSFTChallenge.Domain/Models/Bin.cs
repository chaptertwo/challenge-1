using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutSFTChallenge.Domain.Models
{
    public class Bin
    {
        public int BinId { get; set; }
        [DisplayName("Bin Name")]
        [Required]
        public string BinName { get; set; }
    }
}

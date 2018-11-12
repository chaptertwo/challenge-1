using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutSFTChallenge.Domain.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [StringLength(8, MinimumLength = 6)]
        [Required]
        public string SKU { get; set; }
        [DisplayName("Description")]
        public string ProductDescription { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int BinCount { get; set; }

        

    }
}

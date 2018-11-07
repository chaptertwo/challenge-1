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
        [Required]
        public string SKU { get; set; }
        [Required]
        [DisplayName("Description")]
        public string ProductDescription { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}

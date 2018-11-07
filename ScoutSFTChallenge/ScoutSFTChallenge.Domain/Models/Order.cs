using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutSFTChallenge.Domain.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        [Required]
        [DisplayName("Date of Order")]
        public DateTime DateOrdered { get; set; }
        [Required]
        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }
        [Required]
        [DisplayName("Address")]
        public string CustomerAddress { get; set; }
        public int OrderNumber { get; set; }
        public decimal Total { get; set; }
    }
}

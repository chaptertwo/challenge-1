using ScoutSFTChallenge.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoutSFTChallenge.UI.Models
{
    public class OrderVM
    {
        public IEnumerable<Inventory> Inventory { get; set; }
        public Order Order { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public OrderLine OrderLine { get; set; }
        public IEnumerable<OrderLine> OrderLines { get; set; }
        public List<Product> Products { get; set; }
        public Product Product { get; set; }
    }
}
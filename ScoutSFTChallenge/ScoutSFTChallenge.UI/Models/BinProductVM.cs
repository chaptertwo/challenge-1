using ScoutSFTChallenge.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoutSFTChallenge.UI.Models
{
    public class BinProductVM
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Bin> Bins { get; set; }
        public IEnumerable<Inventory> Inventory { get; set; }
        public Bin Bin { get; set; }
        
    }
}
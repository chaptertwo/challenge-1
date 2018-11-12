using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutSFTChallenge.Domain.Models
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        public int BinId { get; set; }
        public int ProductId { get; set; }
        public int InventoryQuantity { get; set; }
        public string BinName { get; set; }
        public string ProductDescription { get; set; }
        public string SKU { get; set; }
        //IEnumerable<Bin> Bins { get; set; }
        //IEnumerable<Product> Products { get; set; }
        
    }
}

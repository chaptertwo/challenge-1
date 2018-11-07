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
    }
}

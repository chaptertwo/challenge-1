using ScoutSFTChallenge.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScoutSFTChallenge.UI.Models
{
    public class InventoryVM
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Bin> Bins { get; set; }
        public IEnumerable<Inventory> Inventory { get; set; }
        public Product Product { get; set; }
        
        public int SelectBinId { get; set; }
        public IEnumerable<SelectListItem> BinList
        {
            get
            {
                return new SelectList(Bins, "BinId", "BinName");
            }
            set { }
        }
    }
}
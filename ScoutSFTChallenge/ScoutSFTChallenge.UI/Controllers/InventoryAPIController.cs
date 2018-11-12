using ScoutSFTChallenge.Domain;
using ScoutSFTChallenge.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ScoutSFTChallenge.UI.Controllers
{
    public class InventoryAPIController : ApiController
    {
        private InventoryService service;
        public InventoryAPIController(InventoryService service)
        {
            this.service = service;
        }

        [Route("inventory")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Inventory()
        {
            InventoryVM vm = new InventoryVM();
            vm.Inventory = service.GrabInventory();
            vm.Bins = service.GetAllBins();
            vm.Products = service.GetAllProducts();
            foreach(var i in vm.Inventory)
            {
                foreach(var b in vm.Bins)
                {
                    if(b.BinId == i.BinId)
                    {
                        i.BinName = b.BinName;
                    }
                }
                foreach(var p in vm.Products)
                {
                    if(p.ProductId == i.ProductId)
                    {
                        i.ProductDescription = p.ProductDescription;
                    }
                }
            }
            return Ok(vm.Inventory);
        }
    }
}

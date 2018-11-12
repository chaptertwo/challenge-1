using ScoutSFTChallenge.Domain;
using ScoutSFTChallenge.Domain.Models;
using ScoutSFTChallenge.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScoutSFTChallenge.UI.Controllers
{
    public class BinProductController : Controller
    {
        private InventoryService service;

        public BinProductController(InventoryService service)
        {
            this.service = service;
        }
        // GET: BinProduct
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BinHome()
        {
            InventoryVM vm = new InventoryVM();
            vm.Inventory = service.GrabInventory();
            vm.Bins = service.GetAllBins();
            vm.Products = service.GetAllProducts();
            foreach (var i in vm.Inventory)
            {
                foreach (var b in vm.Bins)
                {
                    if (b.BinId == i.BinId)
                    {
                        i.BinName = b.BinName;
                    }
                }
                foreach (var p in vm.Products)
                {
                    if (p.ProductId == i.ProductId)
                    {
                        i.ProductDescription = p.ProductDescription;
                        i.SKU = p.SKU;
                    }
                }
            }
            return View(vm);
        }

        public ActionResult EditBin(int id)
        {
            Bin thisBin = service.FindBinById(id);
            BinProductVM vm = new BinProductVM();
            vm.Bin = thisBin;
            vm.Products = service.GetAllProducts().ToList();
            vm.Inventory = service.GrabInventory();
            foreach(var i in vm.Inventory)
            {
                if(i.BinId == id)
                {
                    i.ProductDescription = vm.Products.Where(p => p.ProductId == i.ProductId).First().ProductDescription;
                    foreach (var p in vm.Products)
                    {
                        if (p.ProductId == i.ProductId)
                        {
                            p.BinCount = i.InventoryQuantity;
                            vm.Bin.Products.Add(p);
                        }
                    }
                }
                
            }
            return View(vm);
        }

        [HttpPost]
        public ActionResult EditBin(BinProductVM vm)
        {
            if (ModelState.IsValid)
            {
                service.UpdateBin(vm.Bin);
                foreach(var p in vm.Bin.Products)
                {
                    if(p.BinCount == 0)
                    {
                        service.RemoveProductFromBin(vm.Bin, p.ProductId);
                    }
                    service.UpdateBinQuantity(vm.Bin.BinId, p);
                }
                return RedirectToAction("BinHome");
            }
            Bin thisBin = service.FindBinById(vm.Bin.BinId);
            vm.Bin = thisBin;
            vm.Products = service.GetAllProducts();
            vm.Inventory = service.GrabInventory();
            foreach (var i in vm.Inventory)
            {
                if (i.BinId == vm.Bin.BinId)
                {
                    i.ProductDescription = vm.Products.Where(p => p.ProductId == i.ProductId).First().ProductDescription;
                }
            }
            return View(vm);
        }

        [HttpGet]
        public ActionResult DeleteBin(int id)
        {
            service.DeleteBin(id);
            return RedirectToAction("BinHome");
        }

        [HttpGet]
        public ActionResult CreateBin()
        {
            BinProductVM vm = new BinProductVM();
            vm.Bins = service.GetAllBins();
            vm.Products = service.GetAllProducts();
            vm.Inventory = service.GrabInventory();
            return View(vm);
        }

        [HttpPost]
        public ActionResult CreateBin(BinProductVM vm)
        {
            var bins = service.GetAllBins();
            if(bins.Any(b => b.BinName == vm.Bin.BinName))
            {
                ModelState.AddModelError("BinName", "A bin with this name already exists. Bin names must be unique. Please try again.");
                vm.Bins = service.GetAllBins();
                vm.Products = service.GetAllProducts();
                vm.Inventory = service.GrabInventory();
                return View(vm);
            }
            if (ModelState.IsValid)
            {
                service.SaveBin(vm.Bin);
                return RedirectToAction("BinHome");
            }
            else
            {
                vm.Bins = service.GetAllBins();
                vm.Products = service.GetAllProducts();
                vm.Inventory = service.GrabInventory();
                return View(vm);
            }
        }

        public ActionResult CreateProduct()
        {
            InventoryVM vm = new InventoryVM();
            vm.Bins = service.GetAllBins();
            vm.Products = service.GetAllProducts();
            vm.Inventory = service.GrabInventory();
            
            return View(vm);
        }

        [HttpPost]
        public ActionResult CreateProduct(InventoryVM vm)
        {
            var productSKUs = service.GetAllProducts();
            if(productSKUs.Any(a => a.SKU == vm.Product.SKU))
            {
                ModelState.AddModelError("SKU", "A product with this SKU already exists. Please try again.");
                vm.Bins = service.GetAllBins();
                vm.Products = service.GetAllProducts();
                vm.Inventory = service.GrabInventory();
                return View(vm);
            }
            if (ModelState.IsValid)
            {
                service.SaveProduct(vm.Product);
                service.UpdateInventory(vm.Product, vm.SelectBinId);
                return RedirectToAction("BinHome");
            }
            else
            {
                vm.Bins = service.GetAllBins();
                vm.Products = service.GetAllProducts();
                vm.Inventory = service.GrabInventory();
                return View(vm);
            }
        }

    }
}
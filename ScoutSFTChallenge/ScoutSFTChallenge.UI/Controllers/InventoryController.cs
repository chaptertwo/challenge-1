using ScoutSFTChallenge.Domain;
using ScoutSFTChallenge.Domain.Models;
using ScoutSFTChallenge.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ScoutSFTChallenge.UI.Controllers
{
    public class InventoryController : Controller
    {
        private InventoryService service;

        public InventoryController(InventoryService service)
        {
            this.service = service;
        }

        // GET: Inventory
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllOrders()
        {
            OrderVM vm = new OrderVM();
            vm.Inventory = service.GrabInventory();
            vm.Orders = service.GetAllOrders();
            vm.OrderLines = service.GetAllOrderLines();
            return View(vm);
        }

        public ActionResult ViewOrderLine(int id)
        {
            OrderVM vm = new OrderVM();
            vm.Order = service.FindOrderById(id);
            vm.OrderLines = service.GetAllOrderLines().Where(o => o.OrderId == vm.Order.OrderId);
            vm.Inventory = service.GrabInventory();
            var allProducts = service.GetAllProducts();
            vm.Products = new List<Product>();
            foreach (var o in vm.OrderLines)
            {
                foreach (var p in allProducts)
                {
                    if (p.ProductId == o.ProductId)
                    {
                        vm.Products.Add(p);
                    }
                }
            }
            return View(vm);
        }

        //public ActionResult EditOrderLine(int id)
        //{
        //    OrderVM vm = new OrderVM();
        //    vm.Order = service.FindOrderById(id);
        //    vm.OrderLines = service.GetAllOrderLines().Where(o => o.OrderId == vm.Order.OrderId);
        //    vm.Inventory = service.GrabInventory();
        //    var allProducts = service.GetAllProducts();
        //    vm.Products = new List<Product>();
        //    foreach (var o in vm.OrderLines)
        //    {
        //        foreach (var p in allProducts)
        //        {
        //            if (p.ProductId == o.ProductId)
        //            {
        //                vm.Products.Add(p);
        //            }
        //        }
        //    }
        //    return View(vm);
        //}

        public ActionResult CreateOrder()
        {
            OrderVM vm = new OrderVM();
            vm.Inventory = service.GrabInventory();
            vm.Orders = service.GetAllOrders();
            vm.OrderLines = service.GetAllOrderLines();
            return View(vm);
        }

        [HttpPost]
        public ActionResult CreateOrder(OrderVM vm)
        {
            var products = service.GetAllProducts();
            var orders = service.GetAllOrders();
            var inventory = service.GrabInventory();
            var productOrderQTY = vm.OrderLine.OrderQuantity;
            int totalQuantity = 0;
            foreach(var i in inventory)
            {
                if(i.ProductId == vm.OrderLine.OrderId)
                {
                    totalQuantity += i.InventoryQuantity;
                }
            }
            if(totalQuantity > vm.OrderLine.OrderQuantity)
            {
                ModelState.AddModelError("OrderQuantity", "We don't currently have enough of this item in stock.");
                vm.Inventory = service.GrabInventory();
                vm.Orders = service.GetAllOrders();
                vm.OrderLines = service.GetAllOrderLines();
                return View(vm);
            }
            if(orders.Any(a => a.OrderNumber == vm.Order.OrderNumber))
            {
                ModelState.AddModelError("OrderNumber", "A order with this number already exists. Please edit the existing order or find the most recent unused order number.");
                vm.Inventory = service.GrabInventory();
                vm.Orders = service.GetAllOrders();
                vm.OrderLines = service.GetAllOrderLines();
                return View(vm);
            }
            if (!products.Any(a => a.SKU == vm.Product.SKU))
            {
                ModelState.AddModelError("SKU", "A product with this SKU does not exist in our database. Please add the product first.");
                vm.Inventory = service.GrabInventory();
                vm.Orders = service.GetAllOrders();
                vm.OrderLines = service.GetAllOrderLines();
                return View(vm);
            }
            if (ModelState.IsValid)
            {
                //save order
                foreach(var p in products)
                {
                    if(p.SKU == vm.Product.SKU)
                    {
                        vm.OrderLine.ProductId = p.ProductId;
                    }
                }
                service.SaveOrder(vm.Order, vm.OrderLine);
                return RedirectToAction("AllOrders");
            }
            else
            {
                vm.Inventory = service.GrabInventory();
                vm.Orders = service.GetAllOrders();
                vm.OrderLines = service.GetAllOrderLines();
                return View(vm);
            }
        }
    }
}
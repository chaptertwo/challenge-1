using ScoutSFTChallenge.Domain.Interfaces;
using ScoutSFTChallenge.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutSFTChallenge.Domain
{
    public class InventoryService
    {
        private IBinRepository binRepo;
        private IInventoryRepository inventoryRepo;
        private IOrderLineRepository orderLineRepo;
        private IOrderRepository orderRepo;
        private IProductRepository productRepo;

        public InventoryService(IBinRepository binRepo, IInventoryRepository inventoryRepo, IOrderLineRepository orderLineRepo, IOrderRepository orderRepo, IProductRepository productRepo)
        {
            this.binRepo = binRepo;
            this.inventoryRepo = inventoryRepo;
            this.orderLineRepo = orderLineRepo;
            this.orderRepo = orderRepo;
            this.productRepo = productRepo;
        }

        public IEnumerable<OrderLine> GetAllOrderLines()
        {
            return orderLineRepo.All();
        }

        public Order FindOrderById(int id)
        {
            return orderRepo.FindById(id);
        }

        public OrderLine GetOrderLineById(int id)
        {
            return orderLineRepo.FindById(id);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return orderRepo.All();
        }

        public IEnumerable<Bin> GetAllBins()
        {
            var bins = binRepo.All();
            return bins;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = productRepo.All();
            return products;
        }

        public IEnumerable<Inventory> GrabInventory()
        {
            var allInventory = inventoryRepo.All();
            var products = productRepo.All();
            var bins = binRepo.All();
            foreach (var i in allInventory)
            {
                foreach(var b in bins)
                {
                    if(i.BinId == b.BinId)
                    {
                        i.BinName = b.BinName;
                    }
                }

                foreach(var p in products)
                {
                    if(p.ProductId == i.ProductId)
                    {
                        p.ProductDescription = i.ProductDescription;
                    }
                }
            }
            return allInventory;
        }

        public Bin FindBinById(int id)
        {
            return binRepo.FindById(id);
        }

        public void UpdateBin(Bin bin)
        {
            binRepo.Update(bin);
        }

        public void UpdateBinQuantity(int binId, Product binCount)
        {
            inventoryRepo.UpdateBinQuantity(binId, binCount);
        }

        public void RemoveProductFromBin(Bin bin, int productId)
        {
            inventoryRepo.RemoveProductFromBin(bin, productId);
        }

        public void DeleteBin(int id)
        {
            binRepo.Delete(id);
        }

        public void SaveProduct(Product product)
        {
            productRepo.Insert(product);
        }

        public void UpdateInventory(Product product, int selectBinId)
        {
            inventoryRepo.AddNewProduct(product, selectBinId);
        }

        public void SaveBin(Bin bin)
        {
            Bin newBin = binRepo.Insert(bin);
        }

        public void SaveOrder(Order order, OrderLine orderLine)
        {
            Order orderToReturn = orderRepo.Insert(order);
            orderLine.OrderId = orderToReturn.OrderId;
            OrderLine line = orderLineRepo.Insert(orderLine);
        }
    }
}

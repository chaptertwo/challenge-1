using ScoutSFTChallenge.Domain.Interfaces;
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
    }
}

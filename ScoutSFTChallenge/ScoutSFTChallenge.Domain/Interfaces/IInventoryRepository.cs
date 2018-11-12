using ScoutSFTChallenge.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutSFTChallenge.Domain.Interfaces
{
    public interface IInventoryRepository
    {
        IEnumerable<Inventory> All();
        void UpdateBinQuantity(int binId, Product binCount);
        void RemoveProductFromBin(Bin bin, int productId);
        void AddNewProduct(Product product, int selectBinId);
    }
}

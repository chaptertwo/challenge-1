using Dapper;
using ScoutSFTChallenge.Domain.Interfaces;
using ScoutSFTChallenge.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutSFTChallenge.Data
{
    public class InventoryRepository : IInventoryRepository
    {
        private const string CONN_STRING = "DefaultConnection";

        public IEnumerable<Inventory> All()
        {
            const string sql = "SELECT InventoryId, BinId, ProductId, InventoryQuantity FROM Inventory;";
            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Query<Inventory>(sql);
            }
        }

        public Inventory FindById(int id)
        {
            const string sql = "SELECT InventoryId, BinId, ProductId, InventoryQuantity FROM Inventory " +
                "WHERE InventoryId = @InventoryId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Query<Inventory>(sql, new { InventoryId = id }).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            const string sql = "DELETE FROM Inventory WHERE InventoryId = @InventoryId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Execute(sql, new { OrderId = id }) > 0;
            }
        }

        public Inventory Insert(Inventory inventory)
        {
            const string sql = "INSERT INTO Inventory (InventoryId, BinId, ProductId, InventoryQuantity) " +
                "VALUES (@InventoryId, @BinId, @ProductId, @InventoryQuantity); " +
                "SELECT SCOPE_IDENTITY()";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                inventory.InventoryId = conn.Query<int>(sql, inventory).First();
            }
            return inventory;
        }

        public Inventory Update(Inventory inventory)
        {
            const string sql = "UPDATE Inventory SET " +
                "InventoryId = @InventoryId, " +
                "BinId = @BinId, " +
                "ProductId = @ProductId, " +
                "InventoryQuantity = @InventoryQuantity;";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                if (conn.Execute(sql, inventory) > 0)
                {
                    return inventory;
                }
                return null;
            }
        }

        public void UpdateBinQuantity(int binId, Product product)
        {
            const string sql = "UPDATE Inventory SET " +
                "InventoryQuantity = @BinCount " +
                "WHERE BinId = @BinId AND ProductId = @ProductId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                conn.Execute(sql, new { BinId = binId, BinCount = product.BinCount, ProductId = product.ProductId });
            }
        }

        public void RemoveProductFromBin(Bin bin, int productId)
        {
            const string sql = "DELETE FROM Inventory WHERE BinId = @BinId AND ProductId = @ProductId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                conn.Execute(sql, new { BinId = bin.BinId, ProductId = productId });
            }
        }

        public void AddNewProduct(Product product, int binId)
        {
            const string sql = "INSERT INTO Inventory (InventoryQuantity, BinId, ProductId) " +
                "VALUES (@BinCount, @BinId, @ProductId); " +
                "SELECT SCOPE_IDENTITY()";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                conn.Execute(sql, new { BinId = binId, BinCount = product.BinCount, ProductId = product.ProductId });
            }
        }
    }
}

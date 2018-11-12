using Dapper;
using ScoutSFTChallenge.Domain.Interfaces;
using ScoutSFTChallenge.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutSFTChallenge.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private const string CONN_STRING = "DefaultConnection";

        public IEnumerable<Product> All()
        {
            const string sql = "SELECT ProductId, SKU, ProductDescription, Price FROM Product";
            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Query<Product>(sql);
            }
        }

        public Product FindById(int id)
        {
            const string sql = "SELECT ProductId, SKU, ProductDescription, Price FROM Product " +
                "WHERE ProductId = @ProductId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Query<Product>(sql, new { ProductId = id }).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            const string sql = "DELETE FROM Product WHERE ProductId = @ProductId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Execute(sql, new { ProductId = id }) > 0;
            }
        }

        public Product Insert(Product product)
        {
            const string sql = "INSERT INTO Product (SKU, ProductDescription, Price) " +
                "VALUES (@SKU, @ProductDescription, @Price); " +
                "SELECT SCOPE_IDENTITY()";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                product.ProductId = conn.Query<int>(sql, product).First();
            }
            return product;
        }

        public Product Update(Product product)
        {
            const string sql = "UPDATE Product SET " +
                "SKU = @SKU, " +
                "ProductDescription = @ProductDescription, " +
                "Price = @Price " +
                "WHERE ProductId = @ProductId";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                if (conn.Execute(sql, product) > 0)
                {
                    return product;
                }
                return null;
            }
        }
    }
}

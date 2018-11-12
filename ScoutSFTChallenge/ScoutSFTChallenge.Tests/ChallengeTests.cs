using NUnit.Framework;
using ScoutSFTChallenge.Data.Repositories;
using ScoutSFTChallenge.Domain.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutSFTChallenge.Tests
{
    [TestFixture]
    public class ChallengeTests
    {
        [SetUp]
        public void SetUp()
        {
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "DbReset";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cn;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        [Test]
        public void AllProducts()
        {
            var repo = new ProductRepository();
            var products = repo.All();
            Assert.IsTrue(products != null && products.Any());
        }

        [TestCase(1)]
        [TestCase(3)]
        public void FindByProductId(int productId)
        {
            var repo = new ProductRepository();
            var product = repo.FindById(productId);
            Assert.AreEqual(productId, product.ProductId);
        }

        [TestCase("64531a4s", "Exclusive Red iPhone Verizon", 649.99)]
        [TestCase("4a3e45s", "Kindle Fire Kids Edition", 89.99)]
        public void InsertProduct(string SKU, string productDescription, decimal price)
        {
            var repo = new ProductRepository();
            var product = new Product
            {
                SKU = SKU,
                ProductDescription = productDescription,
                Price = price
            };
            var result = repo.Insert(product);
            Assert.IsTrue(result.ProductId > 0);
        }

        [TestCase("4", "Holiday Sale!", 320.99)]
        public void UpdateProduct(string sku, string productDescription, decimal price)
        {
            var repo = new ProductRepository();
            var product = repo.FindById(1);
            product.ProductDescription = productDescription;
            product.SKU = sku;
            product.Price = price;
            var result = repo.Update(product);
            Assert.IsTrue(result.ProductDescription == "Holiday Sale!");
        }
    }
}

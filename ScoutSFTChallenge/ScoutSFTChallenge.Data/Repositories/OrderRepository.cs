using ScoutSFTChallenge.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ScoutSFTChallenge.Domain.Interfaces;

namespace ScoutSFTChallenge.Data
{
    public class OrderRepository : IOrderRepository
    {
        private const string CONN_STRING = "DefaultConnection";

        public IEnumerable<Order> All()
        {
            const string sql = "SELECT OrderId, DateOrdered, CustomerName, CustomerAddress, OrderNumber, Total FROM [Order];";
            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Query<Order>(sql);
            }
        }

        public Order FindById(int id)
        {
            const string sql = "SELECT OrderId, DateOrdered, CustomerName, CustomerAddress, OrderNumber, Total FROM [Order] " +
                "WHERE OrderId = @OrderId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Query<Order>(sql, new { OrderId = id }).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            const string sql = "DELETE FROM [Order] WHERE OrderId = @OrderId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Execute(sql, new { OrderId = id }) > 0;
            }
        }

        public Order Insert(Order order)
        {
            const string sql = "INSERT INTO [Order] (DateOrdered, CustomerName, CustomerAddress, OrderNumber) " +
                "VALUES (@DateOrdered, @CustomerName, @CustomerAddress, @OrderNumber); " +
                "SELECT SCOPE_IDENTITY()";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                order.OrderId = conn.Query<int>(sql, order).First();
            }
            return order;
        }

        public Order Update(Order order)
        {
            const string sql = "UPDATE [Order] SET " +
                "OrderId = @OrderId, " +
                "DateOrdered = @DateOrdered, " +
                "CustomerName = @CustomerName, " +
                "CustomerAddress = @CustomerAddress, " +
                "OrderNumber = @OrderNumber, " +
                "Total = @Total";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                if (conn.Execute(sql, order) > 0)
                {
                    return order;
                }
                return null;
            }
        }
    }
}

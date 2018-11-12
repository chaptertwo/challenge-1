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
    public class OrderLineRepository : IOrderLineRepository
    {
        private const string CONN_STRING = "DefaultConnection";

        public IEnumerable<OrderLine> All()
        {
            const string sql = "SELECT OrderLineId, OrderId, ProductId, OrderQuantity, PricePerLine FROM OrderLine;";
            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Query<OrderLine>(sql);
            }
        }

        public OrderLine FindById(int id)
        {
            const string sql = "SELECT OrderLineId, OrderId, ProductId, OrderQuantity, PricePerLine FROM OrderLine " +
                "WHERE OrderLineId = @OrderLineId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Query<OrderLine>(sql, new { OrderLineId = id }).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            const string sql = "DELETE FROM OrderLine WHERE OrderLineId = @OrderLineId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Execute(sql, new { OrderLineId = id }) > 0;
            }
        }

        public OrderLine Insert(OrderLine orderLine)
        {
            const string sql = "INSERT INTO OrderLine (OrderId, ProductId, OrderQuantity) " +
                "VALUES  (@OrderId, @ProductId, @OrderQuantity); " +
                "SELECT SCOPE_IDENTITY()";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                orderLine.OrderLineId = conn.Query<int>(sql, orderLine).First();
            }
            return orderLine;
        }

        public OrderLine Update(OrderLine orderLine)
        {
            const string sql = "UPDATE OrderLine SET " +
                "OrderLineId = @OrderLineId, " +
                "OrderId = @OrderId, " +
                "ProductId = @ProductId, " +
                "OrderQuantity = @OrderQuantity, " +
                "PricePerLine = @PricePerLine;";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                if (conn.Execute(sql, orderLine) > 0)
                {
                    return orderLine;
                }
                return null;
            }
        }
    }
}

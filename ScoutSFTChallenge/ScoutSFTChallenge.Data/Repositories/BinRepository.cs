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
    public class BinRepository : IBinRepository
    {
        private const string CONN_STRING = "DefaultConnection";

        public IEnumerable<Bin> All()
        {
            const string sql = "SELECT BinId, BinName FROM Bin;";
            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Query<Bin>(sql);
            }
        }

        public Bin FindById(int id)
        {
            const string sql = "SELECT BinId, BinName FROM Bin " +
                "WHERE BinId = @BinId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Query<Bin>(sql, new { BinId = id }).FirstOrDefault();
            }
        }

        public bool Delete(int id)
        {
            const string sql = "UPDATE Inventory SET BinId = NULL WHERE BinId = @BinId " +
                "DELETE FROM Bin WHERE BinId = @BinId;";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                return conn.Execute(sql, new { BinId = id }) > 0;
            }
        }

        public Bin Insert(Bin bin)
        {
            const string sql = "INSERT INTO Bin (BinName) " +
                "VALUES (@BinName); " +
                "SELECT SCOPE_IDENTITY()";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                bin.BinId = conn.Query<int>(sql, bin).First();
            }
            return bin;
        }

        public Bin Update(Bin bin)
        {
            const string sql = "UPDATE Bin SET " +
                "BinName = @BinName " +
                "WHERE BinId = @BinId";

            using (var conn = Database.GetOpenConnection(CONN_STRING))
            {
                if (conn.Execute(sql, bin) > 0)
                {
                    return bin;
                }
                return null;
            }
        }
    }
}

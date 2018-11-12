using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScoutSFTChallenge.Domain.Models;

namespace ScoutSFTChallenge.Domain.Interfaces
{
    public interface IOrderLineRepository
    {
        IEnumerable<OrderLine> All();
        OrderLine FindById(int id);
        OrderLine Insert(OrderLine orderLine);
    }
}

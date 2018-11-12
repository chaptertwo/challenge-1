using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScoutSFTChallenge.Domain.Models;

namespace ScoutSFTChallenge.Domain.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> All();
        Order FindById(int id);
        Order Insert(Order order);
    }
}

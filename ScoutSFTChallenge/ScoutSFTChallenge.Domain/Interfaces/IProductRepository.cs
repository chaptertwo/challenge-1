using ScoutSFTChallenge.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutSFTChallenge.Domain.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> All();
        Product Insert(Product product);
    }
}

using ScoutSFTChallenge.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutSFTChallenge.Domain.Interfaces
{
    public interface IBinRepository
    {
        IEnumerable<Bin> All();
        Bin FindById(int id);
        Bin Update(Bin bin);
        bool Delete(int id);
        Bin Insert(Bin bin);
    }
}

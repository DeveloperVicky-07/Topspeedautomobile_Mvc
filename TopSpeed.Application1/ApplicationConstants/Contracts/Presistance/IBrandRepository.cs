using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSpeed.Domain1.Models;

namespace TopSpeed.Application1.ApplicationConstants.Contracts.Presistance
{
   public interface IBrandRepository : IGenericRepository<Brand>
    {
        Task Update(Brand brand);
    }
}

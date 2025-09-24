using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSpeed.Application1.ApplicationConstants.Contracts.Presistance;
using TopSpeed.Infrastructure1.Comman;
using TopSpeed.Infrastructure1.Repositories;

namespace TopSpeed.Infrastructure1.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)

        {
            _dbContext = dbContext;
             Brand = new BrandRepository(_dbContext);
            VehicleType = new VehicleTypeRepository(_dbContext);
            Post = new  PostRepository(_dbContext);
        }



        public IBrandRepository Brand { get; private set; }

        public IVehicleTypeRepository VehicleType {  get; private set; }

        public IPostRepository Post { get; private set; }       

        public void Dispose()
        {
           _dbContext.Dispose();
        }

        public async Task SaveAsync()
        {
           await _dbContext.SaveChangesAsync(); 
        }
    }
}

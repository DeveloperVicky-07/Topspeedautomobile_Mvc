using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSpeed.Domain1.Models;


namespace TopSpeed.Infrastructure1.Comman
{
  public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Brand> Brand { get; set; }

        public DbSet<VehicleType> VehicleType { get; set; }


        public DbSet<Post> Post { get; set; }


    }
}

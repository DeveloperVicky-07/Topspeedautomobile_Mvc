﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSpeed.Domain1.Models;

namespace TopSpeed.Infrastructure1.Comman
{
    public class SeedData
    {
        public static async Task SeedDataAsync(ApplicationDbContext _dbContext)
        {
            if (! _dbContext.VehicleType.Any())
            {
                await _dbContext.VehicleType.AddRangeAsync(
                    new VehicleType
                    {
                        Name = "MotorCycle"
                    },

                   new VehicleType
                   {
                       Name = "Car"
                   },


                   new VehicleType
                   {
                       Name = "Sun"
                   },

                   new VehicleType
                   {
                       Name = "Van"
                   },


                   new VehicleType
                   {
                       Name = "Sedan"
                   },


                   new VehicleType
                   {
                       Name = "Truck"
                   }

                    );

                await _dbContext.SaveChangesAsync();


            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSpeed.Domain1.Common;

namespace TopSpeed.Domain1.Models
{
 public class VehicleType : BaseModel
    {
        [Required]
        public string Name { get; set; }
    }
}

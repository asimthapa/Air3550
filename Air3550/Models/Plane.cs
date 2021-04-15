using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air3550.Models
{
    public class Plane
    {
        [Key]
        public int Model { get; set; }
        public int Capacity { get; set; }
    }
}

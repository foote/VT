using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDF.VehicleTracker.BL.Models;

namespace WebApplication2.ViewModels
{
    public class VehicleColors
    {
        public Vehicle Vehicle { get; set; }
        public List<Color> Colors { get; set; }
    }
}

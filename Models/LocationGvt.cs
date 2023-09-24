using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoVision.Models
{
    public class LocationGvt
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Notes { get; set; } = null;
        public decimal LAT { get; set; }
        public decimal LNG { get; set; }


    }
}
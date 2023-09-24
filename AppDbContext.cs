using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoVision.Models;
using Microsoft.EntityFrameworkCore;

namespace GeoVision
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<LocationGvt> LOCATIONS_GVT => Set<LocationGvt>();
    }
}
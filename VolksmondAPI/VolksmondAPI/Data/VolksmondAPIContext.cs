using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VolksmondAPI.Models;

namespace VolksmondAPI.Data
{
    public class VolksmondAPIContext : DbContext
    {
        public VolksmondAPIContext (DbContextOptions<VolksmondAPIContext> options)
            : base(options)
        {
        }

        public DbSet<VolksmondAPI.Models.Citizen> Citizen { get; set; } = default!;
    }
}

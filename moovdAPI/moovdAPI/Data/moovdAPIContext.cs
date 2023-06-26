using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using moovdAPI.Models;

namespace moovdAPI.Data
{
    public class moovdAPIContext : DbContext
    {
        public moovdAPIContext (DbContextOptions<moovdAPIContext> options)
            : base(options)
        {
        }

        public DbSet<moovdAPI.Models.GPS> GPS { get; set; } = default!;
    }
}

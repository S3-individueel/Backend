using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VolksmondAPI.Models;

namespace VolksmondAPI.Data
{
    public class VolksmondAPIContext : IdentityDbContext<Account>
    {
        public VolksmondAPIContext (DbContextOptions<VolksmondAPIContext> options)
            : base(options)
        {
        }

        public DbSet<VolksmondAPI.Models.Account> Account { get; set; } = default!;
        public DbSet<VolksmondAPI.Models.Citizen> Citizen { get; set; } = default!;
        public DbSet<VolksmondAPI.Models.Problem> Problem{ get; set; } = default!;
        public DbSet<VolksmondAPI.Models.Referendum> Referendum { get; set; } = default!;
        public DbSet<VolksmondAPI.Models.ReferendumVote> ReferendumVote{ get; set; } = default!;
        public DbSet<VolksmondAPI.Models.Reply> Reply { get; set; } = default!;
        public DbSet<VolksmondAPI.Models.ReplyVote> ReplyVote { get; set; } = default!;
        public DbSet<VolksmondAPI.Models.Solution> Solution { get; set; } = default!;
        public DbSet<VolksmondAPI.Models.SolutionVote> SolutionVote { get; set; } = default!;
    }
}

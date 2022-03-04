using Kamen.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kamen.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }

        public DbSet<Vrsta> Vrsta { get; set; }
        public DbSet<TipApl> TipApl { get; set; }
        public DbSet<Proizvod> Proizvod { get; set; }
        public DbSet<AppUser> AppUser { get; set; }
    }
}

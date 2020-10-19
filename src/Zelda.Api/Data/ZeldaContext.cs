using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zelda.Models;

namespace Zelda.Data
{
    public class ZeldaContext : DbContext
    {
        public ZeldaContext (DbContextOptions<ZeldaContext> options)
            : base(options)
        {
        }

        public DbSet<Link> Links { get; set; }

        public DbSet<Tag> Tags { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaApi.Models
{
    public class CoronaStatsDbContext : DbContext
    {

        public CoronaStatsDbContext(DbContextOptions<CoronaStatsDbContext> options)
       : base(options)
        { }

        public DbSet<CoronaRecord> CoronaRecords { get; set; }
        public DbSet<Case> Cases { get; set; }

    }
}

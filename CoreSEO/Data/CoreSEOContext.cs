using CoreSEO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSEO.Data
{

    /// <summary>
    /// https://www.youtube.com/watch?v=YNrRamW-0Eo&list=PLwbi_A3jiptdPOdADYa02v89Fb928ix-y&index=4
    /// </summary>
    public class CoreSEOContext  : DbContext
    {
        public CoreSEOContext(DbContextOptions<CoreSEOContext> options): base(options)
        {

        }

        public DbSet<Content> Contents { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Content>().ToTable("Content");
        //}
    }
}

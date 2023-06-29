using MatchInfo.API.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace MatchInfo.API.DbContexts
{       
        /// <summary>
        /// A class for dbContext
        /// </summary>
        public class MatchInfoContext : DbContext
        {
            /// <summary>
            /// Gets or sets the dbSet matches.
            /// </summary>
            public DbSet<Match> Matches { get; set; } = null!;

            /// <summary>
            ///  Gets or sets the dbSet matchOdds.
            /// </summary>
            public DbSet<MatchOdd> MatchOdds { get; set; } = null!;

            /// <summary>
            /// Ctor for MatchInfoContext.
            /// </summary>
            /// <param name="options">The db context options.</param>
            public MatchInfoContext(DbContextOptions<MatchInfoContext> options) : base(options)
            {

            }
            
            /// <summary>
            /// Override OnConfiguring method.
            /// </summary>
            /// <param name="optionsBuilder">The builder options.</param>
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                string connectionString = new ConfigurationBuilder()
                    .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"))
                    .Build().GetConnectionString("MatchInfoDbContext");

                optionsBuilder.UseSqlServer(connectionString);
                optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
                base.OnConfiguring(optionsBuilder);
            }

        /// <summary>
        /// Override OnModelCreating method.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Match>()
                    .HasCheckConstraint("CK_Matches_Sport", "[Sport] = 1 OR [Sport] = 2")
                    .HasData(
                   new Match()
                   {
                       ID = 1,
                       Description = "OSFP-PAO",
                       MatchDateTime = new DateTime(2023,7,30,13,0,0),
                       TeamA = "OSFP",
                       TeamB = "PAO",
                       Sport = 1
                   },
                   new Match()
                   {
                       ID = 2,
                       Description = "AEK-PAO",
                       MatchDateTime = new DateTime(2023, 6, 29, 13, 0, 0),
                       TeamA = "AEK",
                       TeamB = "PAO",
                       Sport = 2
                   }
                 );

                modelBuilder.Entity<MatchOdd>()
                 .HasData(
                   new MatchOdd()
                   {
                       ID = 1,
                       MatchId = 1,
                       Specifier = "X",
                       Odd = 1.5
                   },
                   new MatchOdd()
                   {
                       ID = 2,
                       MatchId = 2,
                       Specifier = "1",
                       Odd = 2.3
                   },
                     new MatchOdd()
                     {
                         ID = 3,
                         MatchId = 2,
                         Specifier = "2",
                         Odd = 3.1
                     }
                         
                   );
                base.OnModelCreating(modelBuilder);
            }

    }
}

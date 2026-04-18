using Domain.Entities.Clinics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    /// <summary>
    /// The primary database session for the Wellness system. 
    /// Coordinates with the database to track and persist Domain Entities.
    /// </summary>
    public class WellnessDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WellnessDbContext"/> class.
        /// </summary>
        /// <param name="options">The configuration settings for this context.</param>
        public WellnessDbContext(DbContextOptions<WellnessDbContext> options) : base(options) { }

        public DbSet<Clinic> Clinics => Set<Clinic>(); //Garuanteed not null

        public DbSet<Room> Rooms => Set<Room>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Clinic>().OwnsOne(c => c.Address);

        }


    }
}

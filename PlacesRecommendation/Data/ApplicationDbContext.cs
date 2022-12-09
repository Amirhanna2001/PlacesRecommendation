using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlacesRecommendation.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlacesRecommendation.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //    modelBuilder.Entity<IdentityUser>().Property(u => u.Id).co
        //}

        public DbSet<Area> Areas { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
    }
}

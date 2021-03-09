using LegendaryDashboard.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace LegendaryDashboard.Infrastructure.DbContext
{
    public sealed class DashboardContext : Microsoft.EntityFrameworkCore.DbContext
    {
        //TODO: переделать под миграции и не умереть!!!
        public DashboardContext(DbContextOptions<DashboardContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Advert:UserAdvert (M:O)
            modelBuilder.Entity<Advert>()
                .HasMany(a => a.UsersAdverts)
                .WithOne(ua => ua.Advert)
                .HasForeignKey(ua => ua.AdvertId);

            // UserAdvert:User (O:M)
            modelBuilder.Entity<UserAdvert>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UsersAdverts)
                .HasForeignKey(ua => ua.UserId);
        }
    }
}
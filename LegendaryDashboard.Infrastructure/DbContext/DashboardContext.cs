using LegendaryDashboard.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace LegendaryDashboard.Infrastructure.DbContext
{
    public class DashboardContextFactory : IDbContextFactory<DashboardContext>
    {
        /*public DashboardContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DashboardContext>();
            optionsBuilder
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LegendaryDB;Trusted_Connection=True;")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            return new DashboardContext(optionsBuilder.Options);
        }*/

        public DashboardContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DashboardContext>();
            optionsBuilder
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LegendaryDB;Trusted_Connection=True;")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            return new DashboardContext(optionsBuilder.Options);
        }
    }
    public sealed class DashboardContext : Microsoft.EntityFrameworkCore.DbContext
    {
        
        public DashboardContext(DbContextOptions<DashboardContext> options) : base(options){}
        
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

            modelBuilder.Entity<User>()
                .HasMany(u => u.SentFeedbacks)
                .WithOne(f => f.Commentator)
                .HasForeignKey(f => f.CommentatorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.TakenFeedbacks)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.ClientCascade);
            
            

        }
    }
}
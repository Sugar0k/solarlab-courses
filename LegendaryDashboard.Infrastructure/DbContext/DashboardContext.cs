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
            //Поменял @Stesniashka 
            // Image:AdvertImage (O:O)
            modelBuilder.Entity<Image>()
                .HasOne(i => i.AdvertImage)
                .WithOne(ai => ai.Image);
            
            // Image key field 
            modelBuilder.Entity<Image>()
                .HasKey(s => s.Guid);
            
            
            // Category:Category (M:O)
            modelBuilder.Entity<Category>()
                .HasMany(c => c.ChildCategories)
                .WithOne(cc => cc.ParentCategory) 
                .HasForeignKey(cc => cc.ParentCategoryId);

            // Advert:AdvertImage (M:O)
            modelBuilder.Entity<Advert>()
                .HasMany(a => a.AdvertImages)
                .WithOne(ai => ai.Advert)
                .HasForeignKey(ai => ai.AdvertId);
            
            // Advert:UserAdvert (M:O)
            modelBuilder.Entity<Advert>()
                .HasMany(a => a.UsersAdverts)
                .WithOne(ua => ua.Advert)
                .HasForeignKey(ua => ua.AdvertId);
            //
            // AdvertConnectionType:UserAdvert (M:O)
            // modelBuilder.Entity<AdvertConnectionType>()
            //     .HasMany(act => act.UsersAdverts)
            //     .WithOne(ua => ua.Type)
            //     .HasForeignKey(ua => ua.TypeId);
            
            // UserAdvert:User (O:M)
            modelBuilder.Entity<UserAdvert>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UsersAdverts)
                .HasForeignKey(ua => ua.UserId);

            /*// User:Feedback (M:O)
            modelBuilder.Entity<User>()
                .HasMany(u => u.TakenFeedbacks)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // User:Feedback (M:O)
            modelBuilder.Entity<User>()
                .HasMany(u => u.SentFeedbacks)
                .WithOne(f => f.Commentator)
                .HasForeignKey(f => f.CommentatorId)
                .OnDelete(DeleteBehavior.Cascade);*/

        }
    }
}
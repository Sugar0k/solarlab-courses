using LegendaryDashboard.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LegendaryDashboard.Api.DbContext
{
    public class DashboardContext : Microsoft.EntityFrameworkCore.DbContext
    {
        
        public DashboardContext(DbContextOptions<DashboardContext> options)
            : base(options)
        {
        }
        
        public DbSet<Advert> Adverts { get; set; }
        public DbSet<AdvertConnectionType> AdvertConnectionTypes { get; set; }
        public DbSet<AdvertImage> AdvertImages { get; set; }
        public DbSet<Category> Categories {get; set;}
        public DbSet<Feedback> Feedbacks {get; set;}
        public DbSet<Image> Images {get; set;}
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAdvert> UserAdverts {get; set;}
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             // Role:User (M:O)
            modelBuilder.Entity<Role>()
                .HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId);
            
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

            // AdvertConnectionType:UserAdvert (M:O)
            modelBuilder.Entity<AdvertConnectionType>()
                .HasMany(act => act.UsersAdverts)
                .WithOne(ua => ua.Type)
                .HasForeignKey(ua => ua.TypeId);

            // UserAdvert:User (O:M)
            modelBuilder.Entity<UserAdvert>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UsersAdverts)
                .HasForeignKey(ua => ua.UserId);

            // User:Feedback (M:O)
            modelBuilder.Entity<User>()
                .HasMany(u => u.TakenFeedbacks)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.UserId);
            
            // User:Feedback (M:O)
            modelBuilder.Entity<User>()
                .HasMany(u => u.SentFeedbacks)
                .WithOne(f => f.Commentator)
                .HasForeignKey(f => f.CommentatorId);;

        }
    }
}
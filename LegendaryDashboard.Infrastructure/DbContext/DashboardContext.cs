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
                       
        }
    }
}
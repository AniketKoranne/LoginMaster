using System.Data.Entity;
using LoginMaster.Models;

namespace LoginMaster.App_Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=dbConnect") // Update connection string name
        {
        }

        public DbSet<User> Users { get; set; } // DbSet for your User model

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure your entity mappings here if needed
        }
    }
}

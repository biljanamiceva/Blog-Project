using BlogProject.Models;
using BlogProject.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace BlogProject
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }
        public DbSet<Category>? Categories { get; set; }

        public DbSet<Post>? Posts { get; set; }
        //Add-Migration InitialMigration -OutputDir "Data/Migrations"
        //EntityFrameworkCore\update-database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // configures one-to-many relationship
            modelBuilder.Entity<Post>()
           .HasOne<Category>(s => s.Category)
           .WithMany(g => g.Posts)
           .HasForeignKey(s => s.CurrentCategoryId);
        }
    }
}

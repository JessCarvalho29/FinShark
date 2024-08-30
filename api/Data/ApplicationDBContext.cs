using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

// Inheriting from DbContext
// Let the ApplicationDbContext knows that we are now using identity: inherit from IdentityDBContext 
// We don't need to add the AppUser to the ApplicationDbContext because the IdentityDbContext will do it for us 
// Passing the AppUser to let IdentityDbContext know that this is the user object that we are going to have and to plug this into our identity
public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    // giant object that will allow to search for individual tables (specify which table we want)
    // the base will allow us to pass up the DbContext into the DbContext
    public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }
    
    // The DbContext will create the database, through Entity Framework
    // It creates the database after it gets done searching for the table
    
    // Manipulating the entire table, the DbSet will get the table from the database and return the data in the way we want 
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Comment> Comments { get; set; }
    
    public DbSet<Portfolio> Portfolios { get; set; }
    
    // Creating roles
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Declaring the foreign keys
        builder.Entity<Portfolio>(x => x.HasKey(p => new { p.AppUserId, p.StockId }));

        // Connect the foreign keys to the table 
        builder.Entity<Portfolio>()
            .HasOne(u => u.AppUser)
            .WithMany(u => u.Portfolios)
            .HasForeignKey(p => p.AppUserId);
        
        builder.Entity<Portfolio>()
            .HasOne(u => u.Stock)
            .WithMany(u => u.Portfolios)
            .HasForeignKey(p => p.StockId);

        List<IdentityRole> roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN"

            },
            new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER"
            }
        };
        builder.Entity<IdentityRole>().HasData(roles);
    }
    
}
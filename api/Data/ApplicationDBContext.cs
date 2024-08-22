using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

// inherit from DbContext
public class ApplicationDbContext : DbContext
{
    // giant object that will allow to search for individual tables (specify which table we want)
    // the base will allow us to pass up the DbContext into the DbContext
    public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }
    
    // The DbContext will create the database, through Entity Framework
    // It creates the database after it gets done searching for the table
    
    // Manipulating the entire table, the DbSet will get the table from the database and return the data in the way we want 
    public DbSet<Stock> Stock { get; set; }
    public DbSet<Comment> Comments { get; set; }
}
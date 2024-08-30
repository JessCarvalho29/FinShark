using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

// We'll create a join table, were we'll have a many-to-many relationship. 
// The table will link the userId with the StockId
// We put the Ids and the navigational properties in the model and Entity Framework Core will do the rest

// If I call the table AppUserStock entity framework will identify that is a many-to-many relationship and take care of everything

[Table("Portfolios")]
public class Portfolio
{
    // Ids in the many-to-many relation
    public string AppUserId { get; set; }
    
    public int StockId { get; set; }
    
    // Navigational properties:
    public AppUser AppUser { get; set; }

    public Stock Stock { get; set; }
}
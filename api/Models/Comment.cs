using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

// [Table("Comments")]
public class Comment
{
    public int Id { get; set; }
    
    [Column(TypeName = "varchar(100)")]
    public string Title { get; set; } = string.Empty;
    
    [Column(TypeName = "varchar(1000)")]
    public string Content { get; set; } = string.Empty;
    
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    
    // This is the Key that will form the relationship with the database
    public int StockId { get; set; }
    
    // Navigational property: important because is what is going to allow us to be able to access this part
    // allow us to navigate within our models, our relationship
    public Stock? Stock { get; set; }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using api.Dtos.Comment;

namespace api.Models;

// [Table("Stocks")]
public class Stock
{
    public int Id { get; set; }
    // identify our stock with a string
    // using string.Empty to set an empty value to the string, instead of using null
    [Column(TypeName = "varchar(100)")]
    public string Symbol { get; set; } = string.Empty;
   
    [Column(TypeName = "varchar(100)")]
    public string CompanyName { get; set; } = string.Empty;
    
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Purchase { get; set; }
    
    [Column(TypeName = "decimal(18, 2)")]
    public decimal LastDiv { get; set; }
    
    [Column(TypeName = "varchar(100)")]
    public string Industry { get; set; } = string.Empty;
    
    public long MarketCap { get; set; }
    
    // work on your one-to-many relationship using a list
    // Once we have a stock we'll have many comments, we want to link them together by convention (search through the code and form the relationship for us), connecting the Stock with the Comment
    public List<Comment> Comments { get; set; } = [];
    
    // We have to add the list of portfolios to the Stock:
    public List<Portfolio> Portfolios { get; set; } =  new List<Portfolio>();
}
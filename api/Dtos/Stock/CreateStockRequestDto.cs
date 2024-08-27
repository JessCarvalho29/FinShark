using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Stock;

public class CreateStockRequestDto
{
    //Same data from StockDto, but the id
    
    [Required]
    [MaxLength(10, ErrorMessage = "Symbol cannot be over 10 characters.")]
    public string Symbol { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(10, ErrorMessage = "Company name cannot be over 10 characters.")]
    public string CompanyName { get; set; } = string.Empty;
    
    [Required]
    [Range(1, 1000000000000)]
    public decimal Purchase { get; set; }
    
    [Required]
    [Range(0.001, 100)]
    public decimal LastDividendYield { get; set; }
  
    [Required]
    [MaxLength(10,  ErrorMessage = "Industry name cannot be over 10 characters.")]
    public string Industry { get; set; } = string.Empty;
    
    [Range(1, 5000000000000)]
    public long MarketCap { get; set; }
}
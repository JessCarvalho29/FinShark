namespace api.Dtos.Stock;

public class StockResponseDto
{
    public int Id { get; set; }

    public string Symbol { get; set; } = string.Empty;
    
    public string CompanyName { get; set; } = string.Empty;
    
    public decimal Purchase { get; set; }

    public decimal Dividend { get; set; }

    public decimal LastDividendYield { get; set; }
  
    public string Industry { get; set; } = string.Empty;
    
    public long MarketCap { get; set; }
    
}
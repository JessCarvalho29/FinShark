namespace api.Dtos.Stock;

public class UpdateStockRequestDto
{
    //Same data from StockDto, but the id
    
    public string Symbol { get; set; } = string.Empty;
    
    public string CompanyName { get; set; } = string.Empty;
    
    public decimal Purchase { get; set; }

    public decimal Dividend { get; set; }

    public decimal LastDividendYield { get; set; }
  
    public string Industry { get; set; } = string.Empty;
    
    public long MarketCap { get; set; }
}
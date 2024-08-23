using api.Dtos.Stock;
using api.Models;

namespace api.Mappers;

// It is possible to do it with an automapper, but we're creating manually

public static class StockMappers
{
    // DTO: Data Transfer Object (Response)
    public static StockResponseDto ToStockDto (this Stock stockModel)
    {
        // Returning a new StockDto
        return new StockResponseDto
        {
            Id = stockModel.Id,
            Symbol = stockModel.Symbol,
            CompanyName = stockModel.CompanyName,
            Purchase = stockModel.Purchase,
            Dividend = stockModel.Dividend,
            LastDividendYield = stockModel.LastDiv,
            Industry = stockModel.Industry,
            MarketCap = stockModel.MarketCap
        };
    }
    
    // DTO: Data Transfer Object (Request)
    public static Stock ToStockFromCreateDto(this CreateStockRequestDto stockDto)
    {
        // We cannot pass the data in form of a DTO to the add()/POST, the data has to be in a form of a stock model
        return new Stock
        {
            Symbol = stockDto.Symbol,
            CompanyName = stockDto.CompanyName,
            Purchase = stockDto.Purchase,
            Dividend = stockDto.Dividend,
            LastDiv = stockDto.LastDividendYield,
            Industry = stockDto.Industry,
            MarketCap = stockDto.MarketCap
        };
    }

    public static Stock ToStockFromUpdateDto(this UpdateStockRequestDto stockDto, int id)
    {
        return new Stock
        {
            Id = id,
            Symbol = stockDto.Symbol,
            CompanyName = stockDto.CompanyName,
            Purchase = stockDto.Purchase,
            Dividend = stockDto.Dividend,
            LastDiv = stockDto.LastDividendYield,
            Industry = stockDto.Industry,
            MarketCap = stockDto.MarketCap
        };
    }
}
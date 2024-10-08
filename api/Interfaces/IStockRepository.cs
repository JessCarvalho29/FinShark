using api.Dtos.Stock;
using api.Helpers;
using api.Models;

namespace api.Interfaces;

// Creating an interface repository to keep all the database calls and remove it from the controller

public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync(QueryObject queryObject);
    Task<Stock?> GetByIdAsync(int id); // FirstOrDefault CAN BE NULL: That's why we need the ?
    Task<Stock?> GetBySymbolAsync(string symbol);
    Task<Stock> CreateAsync(Stock stockModel);
    Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateDto);
    Task<Stock?> DeleteAsync(int id);
    Task<bool> StockExists(int id);
    
}
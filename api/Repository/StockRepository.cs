using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class StockRepository : IStockRepository
{
    private readonly ApplicationDbContext _context;
    
    public StockRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Stock>> GetAllAsync(QueryObject queryObject)
    {
        // return await _context.Stocks.ToListAsync();
        
        // Including the comments
        // return await _context.Stocks.Include(c => c.Comments).ToListAsync();
        
        // Including AsQueryable to make possible to add a filter to the query.
        var stocks = _context.Stocks.Include(c => c.Comments).AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryObject.CompanyName))
        {
            stocks = stocks.Where(s => s.CompanyName.ToLower().Contains(queryObject.CompanyName.ToLower()));
        }
        
        if (!string.IsNullOrWhiteSpace(queryObject.Symbol))
        {
            stocks = stocks.Where(s => s.Symbol.ToLower().Contains(queryObject.Symbol.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
        {
            if (queryObject.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
            {
                stocks = queryObject.IsDescending
                    ? stocks.OrderByDescending(s => s.Symbol)
                    : stocks.OrderBy(s => s.Symbol);
            }
        }
        
        // Adding pagination
        var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;
        
        // ToList comes in the end after all sql modifications done. ToList is what generates the sql and fire the sql to the database to get data back, in form of a list
        return await stocks.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
        // return await _context.Stocks.FindAsync(id);
        return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Stock> CreateAsync(Stock stockModel)
    {
        await _context.Stocks.AddAsync(stockModel);
        await _context.SaveChangesAsync();
        return stockModel;
    }

    public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateDto)
    {
        var stockModel = await _context.Stocks.FindAsync(id);

        if (stockModel == null )
        {
            return null;
        }
        
        stockModel.Symbol = updateDto.Symbol;
        stockModel.CompanyName = updateDto.CompanyName;
        stockModel.Purchase = updateDto.Purchase;
        stockModel.LastDiv = updateDto.LastDividendYield;
        stockModel.Industry = updateDto.Industry;
        stockModel.MarketCap = updateDto.MarketCap;

        await _context.SaveChangesAsync();
        return stockModel;
    }

    public async Task<Stock?> DeleteAsync(int id)
    {
        var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        
        if (stockModel == null)
        {
            return null;
        }
        
        _context.Stocks.Remove(stockModel);
        await _context.SaveChangesAsync();
        
        return stockModel;
    }

    public async Task<bool> StockExists(int id)
    {
        return await _context.Stocks.AnyAsync(s => s.Id == id);
    }
}
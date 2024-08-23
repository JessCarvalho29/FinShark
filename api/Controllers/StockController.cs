using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

// Attributes:
[Route("api/stock")]
[ApiController]

// Inheriting from a Controller base class that will support
public class StockController : ControllerBase
{
    // Making readable only to prevent from changes
    private readonly ApplicationDbContext _context;
    
    // Bringing our connection to the database as a parameter
    public StockController(ApplicationDbContext context)
    {
        _context = context;
    }   
    
    // Creating the HTTP Get: read data from the database using a method
    // Attribute Get + interface IActionResult
    [HttpGet]
    public IActionResult GetAll()
    {
        // Getting information from our SetDB defined in ApplicationDbContext and transforming in a List
        // toList: Deferred execution - in order to create the SQL, go to the database and get what we need, we have to have the list because of deferred execution
        // ... Reade more about it ...
        // Making a select, that is a .net version of a map: return an immutable array/list (foreach)
        var stocks = _context.Stock.ToList().Select(s => s.ToStockDto());
        
        // An (Ok)ObjectResult that when executed performs content negotiation, formats the entity body, and will produce a Status200OK response if negotiation and formatting succeed
        // Returning a 200 request (success)
        return Ok(stocks);
    }
    
    // Creating an endpoint API that will only return one item, so we can have the detail from the list above
    // Specifying with record we want to return, the id. The data will be transferred into the parameter and .net will use model binding to extract the string out, turn into an int and pass it into our code
    [HttpGet("{id}")]
    // IActionResult is a wrapper that will return data from the API (line 34)
    public IActionResult GetById([FromRoute] int id)
    {
        // Find(): finds an entity with the given primary key values.
        var stock = _context.Stock.Find(id);

        if (stock == null)
        {
            // A wrapper that will provide the not found request: Creates an NotFoundResult that produces a Status404NotFound response
            return NotFound();
        }
        
        return Ok(stock.ToStockDto());
    }
    
    // Building the controller endpoint
    // DTO: Data Transfer Object (Request)
    // FromBody: the data will be sent in JSON format, will be passed in the body of the HTTP
    [HttpPost]
    public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
    {
        var stockModel = stockDto.ToStockFromCreateDto();
        _context.Stock.Add(stockModel);
        _context.SaveChanges();
        
        // 
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }
    
    // Creating an Update
    [HttpPut]
    [Route("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
    {
        // Check if the item exists
        // First or default: Returns the first element of a sequence that satisfies a specified condition or a default value if no such element is found.
        // When the item is found, Entity Framework is going to be tracking it
        // in case of tracking
        var stockModel = _context.Stock.FirstOrDefault(x => x.Id == id);
        
        // in case of not tracking
        // var stockModel = _context.Stock.AsNoTracking().FirstOrDefault(x => x.Id == id);

        if (stockModel == null )
        {
            return NotFound();
        }

        // in case of not tracking
        // var stock = updateDto.ToStockFromUpdateDto(id);
        // _context.Attach(stock).State = EntityState.Modified;
        
        // in case of not tracking
        stockModel.Symbol = updateDto.Symbol;
        stockModel.CompanyName = updateDto.CompanyName;
        stockModel.Purchase = updateDto.Purchase;
        stockModel.Dividend = updateDto.Dividend;
        stockModel.LastDiv = updateDto.LastDividendYield;
        stockModel.Industry = updateDto.Industry;
        stockModel.MarketCap = updateDto.MarketCap;

        _context.SaveChanges();
        
        // Returning the result and changing to the dto
        return Ok(stockModel.ToStockDto());

    }   
    
    
    
}
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

// Attributes:
[Route("api/stock")]
[ApiController]

// Controllers are for manipulating URL, not call databases

// Inheriting from a Controller base class that will support
public class StockController : ControllerBase
{
    // Making readable only to prevent from changes
    // private readonly ApplicationDbContext _context;
    private readonly IStockRepository _stockRepository;
    
    // Bringing our connection to the database as a parameter
    public StockController(IStockRepository stockRepository)
    {
        // _context = context;
        _stockRepository = stockRepository;
    }   
    
    // Creating the HTTP Get: read data from the database using a method
    // Attribute Get + interface IActionResult
    [HttpGet]
    // making the method asynchronous, we need to use async and specify a return type (in this case, in form of Task)
    public async Task<IActionResult>  GetAll()
    {
        // Getting information from our SetDB defined in ApplicationDbContext and transforming in a List
        // toList: Deferred execution - in order to create the SQL, go to the database and get what we need, we have to have the list because of deferred execution
        // ... Reade more about it ...
        // Making a select, that is a .net version of a map: return an immutable array/list (foreach)
        
        // We should only use async await with parts of code that execute any outside task
        // Also it's necessary to change the method previous used to the Async one: ToList -> ToListAsync
        // var stocks = await _context.Stock.ToListAsync();
        var stocks = await _stockRepository.GetAllAsync();
        var stockDto = stocks.Select(s => s.ToStockDto());
        
        // An (Ok)ObjectResult that when executed performs content negotiation, formats the entity body, and will produce a Status200OK response if negotiation and formatting succeed
        // Returning a 200 request (success)
        return Ok(stockDto);
    }
    
    // Creating an endpoint API that will only return one item, so we can have the detail from the list above
    // Specifying with record we want to return, the id. The data will be transferred into the parameter and .net will use model binding to extract the string out, turn into an int and pass it into our code
    [HttpGet("{id}")]
    // IActionResult is a wrapper that will return data from the API (line 34)
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        // Find(): finds an entity with the given primary key values.
        // var stock = await _context.Stock.FindAsync(id);
        var stock = await _stockRepository.GetByIdAsync(id);
        
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
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
    {
        var stockModel = stockDto.ToStockFromCreateDto();
        // await _context.Stock.AddAsync(stockModel);
        // await _context.SaveChangesAsync();
        await _stockRepository.CreateAsync(stockModel);
        
        // CreatedAtAction method produces a Status201Created response
        // First parameter is the GetById method, second the parameter of the method called, then the return value
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }
    
    // Creating an Update
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
    {
        // Check if the item exists
        // First or default: Returns the first element of a sequence that satisfies a specified condition or a default value if no such element is found.
        // When the item is found, Entity Framework is going to be tracking it
        // in case of tracking
        // var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
        var stockModel = await _stockRepository.UpdateAsync(id, updateDto);
        
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
        // stockModel.Symbol = updateDto.Symbol;
        // stockModel.CompanyName = updateDto.CompanyName;
        // stockModel.Purchase = updateDto.Purchase;
        // stockModel.Dividend = updateDto.Dividend;
        // stockModel.LastDiv = updateDto.LastDividendYield;
        // stockModel.Industry = updateDto.Industry;
        // stockModel.MarketCap = updateDto.MarketCap;
        //
        // await _context.SaveChangesAsync();
        
        // Returning the result and changing to the dto
        return Ok(stockModel.ToStockDto());

    }   
    
    // Delete
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        // var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
        var stockModel = await _stockRepository.DeleteAsync(id);

        if (stockModel == null)
        {
            return NotFound();
        }
        
        // Remove is not an asynchronous method, so we shouldn't add an await.
        // _context.Stock.Remove(stockModel);
        // await _context.SaveChangesAsync();
        
        // returning success without content.
        return NoContent();
    }
    
    
    
}
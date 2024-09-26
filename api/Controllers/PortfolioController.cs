using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/portfolio")]
[ApiController]

public class PortfolioController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;

    private readonly IStockRepository _stockRepository;

    private readonly IPortfolioRepository _portfolioRepository;
    
    public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository)
    {
        _userManager = userManager;
        _stockRepository = stockRepository;
        _portfolioRepository = portfolioRepository;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserPortfolio()
    {
        // User is being inherited from the controller base, so whenever we utilize an endpoint, HTTP context will be created and this User object will allow me to reach in and grab everything associated with the user and the claims
        var username = User.GetUserName();
        var appUser = await _userManager.FindByNameAsync(username);
        var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);

        return Ok(userPortfolio);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddPortfolio(string symbol)
    {
        var username = User.GetUserName();
        var appUser = await _userManager.FindByNameAsync(username);
        var stock = await _stockRepository.GetBySymbolAsync(symbol);
        
        if(stock == null) return BadRequest("Stock not found");

        // Adding the user portfolio and making sure we don't add duplicates
        var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
        
        // Validation check to see if anything in the user portfolio is equal to the symbol that we are trying to get.
        if(userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower())) return BadRequest("Cannot add same stock to portfolio");
        
        // Creating the object
        var portfolioModel = new Portfolio
        {
            StockId = stock.Id,
            AppUserId = appUser.Id
        };

        await _portfolioRepository.CreateAsync(portfolioModel);

        if (portfolioModel == null)
        {
            return StatusCode(500, "Could not create");
        } else
        {
            return Created();
        }

    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeletePortfolio(string symbol)
    {
        var username = User.GetUserName();
        var appUser = await _userManager.FindByNameAsync(username);
        
        // Getting all the stocks in the User portfolio
        var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
        
        // Compare the user portfolio with the symbol to delete
        var filteredStock = userPortfolio.Where(s => s.Symbol.ToLower() == symbol.ToLower()).ToList();

        if (filteredStock.Count() == 1)
        {
            await _portfolioRepository.DeletePortfolio(appUser, symbol);
        }
        else
        {
            return BadRequest("Stock is not in your portfolio");
        }

        return Ok();

    }
    
    
}
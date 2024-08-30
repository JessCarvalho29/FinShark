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
    
    
    
}
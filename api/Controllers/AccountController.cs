using api.Dtos.Register;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/account")]
[ApiController]

public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    
    // UserManager: Provides the APIs for managing user in a persistence store
    public AccountController(UserManager<AppUser>  userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var appUser = new AppUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Email
            };
            
            var createUser = await _userManager.CreateAsync(appUser, registerRequestDto.Password);

            if (createUser.Succeeded)
            {
                // Assigning a user role to everyone that sign in 
                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                
                if (roleResult.Succeeded)
                {
                    return Ok("User Created");
                }

                return StatusCode(500, roleResult.Errors);
            }

            return StatusCode(500, createUser.Errors);

        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
}
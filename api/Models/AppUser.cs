using Microsoft.AspNetCore.Identity;

namespace api.Models;

// Identity user will add things such as password, username, email behind the scenes (default)
public class AppUser : IdentityUser
{
    // We have to add the list of portfolios to the appUser:
    public List<Portfolio> Portfolios { get; set; } =  new List<Portfolio>();
    
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Interfaces;
using api.Models;
using Microsoft.IdentityModel.Tokens;

namespace api.Service;

// Claims x Roles

public class TokenService : ITokenService
{
    
    // Bringing IConfiguration because we'll need to pull information from the appsettings.json (where the iconfig is)
    private readonly IConfiguration _config;
    
    // SymmetricSecurityKey: encrypts 
    private readonly SymmetricSecurityKey _symmetricSecurityKey;

    public TokenService(IConfiguration config)
    {
        // bringing the config object so we can access the config
        _config = config;
        _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
        // We use encoding to transform in bytes, break it up into individual bits
        // JWT:SigningKey is important, if anybody have access to your key, they can make tokens
    }
    
    public string CreateToken(AppUser appUser)
    {
        // Including claims to our token
        // Creating claims: it'll identify the user and express what the user can or cannot do.
        // Similar to a role, but more flexible
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Email, appUser.Email),
            new Claim(JwtRegisteredClaimNames.GivenName, appUser.UserName)
        };

        // What type of encryption's it will be used: 
        var credentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);
        
        // Creating the token as an object and .dot will create the token for us
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims), // The wallet: username, email, license
            Expires = DateTime.Now.AddDays(7), // expiration date, we don't want the token to last forever
            SigningCredentials = credentials, 
            Issuer = _config["JWT:Issuer"],
            Audience = _config["JWT:Audience"]
        };
        
        // Method that will create the token:
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        // Return the token in form of a string
        return tokenHandler.WriteToken(token);

    }
    
    // Roles needs to hit the database. Claims are associated with user, do not use the database
    // Using Claim with the JWT: We are going to generate the JWT on the server, and we are going to stuff it with the claims.
    // The claims are key/values pairs that will describe what the user can do
    // Everytime we send a request all the data will be within JWT, after that, these values will be associated with the user, each time they use the endpoint we can access these through the HTTP Contex
    // Claims are like an authentication wallet
}
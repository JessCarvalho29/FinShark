using api.Models;

namespace api.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser appUser);
}

// Identity handles the validation, we need to generate the token 

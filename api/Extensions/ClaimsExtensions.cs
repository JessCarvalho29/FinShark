using System.Security.Claims;

namespace api.Extensions;

public static class ClaimsExtensions
{
    // reach into the claims through the user object
    public static string GetUserName(this ClaimsPrincipal user)
    {
        // This is how we reach into the claims:
        return user.Claims.SingleOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")).Value;
        
        // When we create the token we define a name and email, we will get this values through the http context and the claims that were given to us through the token
    }
}
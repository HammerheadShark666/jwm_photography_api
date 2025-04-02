namespace jwm_photography_api.MediatR.Authentication.Login;

public record LoginResponse(string JwtToken, string RefreshToken, UserProfile UserProfile);

public record UserProfile(string FirstName, string LastName, string Email);
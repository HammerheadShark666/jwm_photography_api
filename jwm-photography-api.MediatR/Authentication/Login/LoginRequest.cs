using MediatR;

namespace jwm_photography_api.MediatR.Authentication.Login;

public record LoginRequest(string Email, string Password, string IpAddress) : IRequest<LoginResponse>;
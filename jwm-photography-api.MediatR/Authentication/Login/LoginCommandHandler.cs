using jwm_photography_api.Data.UnitOfWork.Interfaces;
using jwm_photography_api.Helper;
using jwm_photography_api.Helpers.Exceptions;
using jwm_photography_api.MediatR.Service.Interfaces;
using MediatR;


namespace jwm_photography_api.MediatR.Authentication.Login;

public class LoginCommandHandler(IUnitOfWork unitOfWork,
                                 IRefreshTokenService refreshTokenService) : IRequestHandler<LoginRequest, LoginResponse>
{

    public async Task<LoginResponse> Handle(LoginRequest loginRequest, CancellationToken cancellationToken)
    {
        var account = await unitOfWork.Accounts.GetAsync(loginRequest.Email) ?? throw new NotFoundException(ConstantMessages.AccountNotFound);
        var refreshToken = refreshTokenService.GenerateRefreshToken(loginRequest.IpAddress, account);

        refreshTokenService.RemoveExpiredRefreshTokens(account.Id);
        await refreshTokenService.AddRefreshToken(refreshToken);

        var jwtToken = AuthenticationHelper.GenerateJwtToken(account);

        return new LoginResponse(jwtToken, refreshToken.Token, new UserProfile(account.FirstName, account.LastName, account.Email));
    }
}
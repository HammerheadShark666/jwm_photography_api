using AutoMapper;
using jwm_photography_api.Data.UnitOfWork.Interfaces;
using jwm_photography_api.Domain;
using jwm_photography_api.Helper;
using jwm_photography_api.Helpers.Exceptions;
using jwm_photography_api.MediatR.Service.Interfaces;

namespace jwm_photography_api.MediatR.Service;

public class RefreshTokenService(IMapper mapper,
                                 IUnitOfWork unitOfWork) : IRefreshTokenService
{
    public readonly IUnitOfWork _unitOfWork = unitOfWork;
    public readonly IMapper _mapper = mapper;

    public void RemoveExpiredRefreshTokens(Guid accountId)
    {
        _unitOfWork.RefreshTokens.RemoveExpired(EnvironmentVariables.JwtSettingsRefreshTokenTtl, accountId);
        _unitOfWork.Complete();
    }

    public async Task AddRefreshToken(RefreshToken refreshToken)
    {
        await _unitOfWork.RefreshTokens.AddAsync(refreshToken);
        await _unitOfWork.Complete();
    }

    public RefreshToken GenerateRefreshToken(string ipAddress, Account account)
    {
        var refreshTokenExpires = DateTime.Now.AddDays(EnvironmentVariables.JwtSettingsRefreshTokenExpiryDays);
        var refreshToken = AuthenticationHelper.GenerateRefreshToken(ipAddress, refreshTokenExpires, account);
        refreshToken.Account = account;

        return refreshToken;
    }

    public async Task<RefreshToken> GetRefreshTokenAsync(string token)
    {
        var refreshToken = await _unitOfWork.RefreshTokens.ByTokenAsync(token) ?? throw new AppException(ConstantMessages.InvalidToken);
        return refreshToken;
    }
}
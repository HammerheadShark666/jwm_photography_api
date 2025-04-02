using FluentValidation;
using jwm_photography_api.Data.UnitOfWork.Interfaces;
using jwm_photography_api.Helpers.Exceptions;
using BC = BCrypt.Net.BCrypt;

namespace jwm_photography_api.MediatR.Authentication.Login;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    private readonly IUnitOfWork _unitOfWork;

    public LoginValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(loginRequest => loginRequest.Email)
               .NotEmpty().WithMessage("Email is required.")
               .Length(8, 150).WithMessage("Email length between 8 and 150.")
               .EmailAddress().WithMessage("Invalid Email.");

        RuleFor(loginRequest => loginRequest.Password)
            .NotEmpty().WithMessage("Password is required.")
            .Length(8, 50).WithMessage("Password length between 8 and 50.");

        RuleFor(loginRequest => loginRequest).MustAsync(async (loginRequest, cancellation) =>
        {
            return await ValidLoginDetails(loginRequest);
        }).WithMessage("Invalid login");
    }

    protected async Task<bool> ValidLoginDetails(LoginRequest loginRequest)
    {
        try
        {
            var account = await _unitOfWork.Accounts.GetAsync(loginRequest.Email);
            if (account != null && account.IsAuthenticated && BC.Verify(loginRequest.Password, account.PasswordHash))
                return true;
        }
        catch (NotFoundException)
        {
            return false;
        }

        return false;
    }
}
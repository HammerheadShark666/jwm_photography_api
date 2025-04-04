using FluentValidation;
using jwm_photography_api.Data.Repository.Interfaces;

namespace jwm_photography_api.MediatR.UserGallery.AddUserGallery;

public class AddUserGalleryValidator : AbstractValidator<AddUserGalleryRequest>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUserGalleryRepository _userGalleryRepository;

    public AddUserGalleryValidator(IAccountRepository accountRepository, IUserGalleryRepository userGalleryRepository)
    {
        _accountRepository = accountRepository;
        _userGalleryRepository = userGalleryRepository;

        RuleFor(AddUserGalleryRequest => AddUserGalleryRequest.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(1, 150).WithMessage("Name length between 1 and 150.");

        RuleFor(AddUserGalleryRequest => AddUserGalleryRequest.Description)
                .Length(0, 1000).WithMessage("Description length between 0 and 1000.");

        RuleFor(AddUserGalleryRequest => AddUserGalleryRequest).MustAsync(async (AddUserGalleryRequest, cancellation) =>
        {
            return await AccountExists(AddUserGalleryRequest.AccountId);
        }).WithMessage("Account not found.");
    }

    protected async Task<bool> AccountExists(Guid id)
    {
        return await _accountRepository.ExistsAsync(id);
    }
}
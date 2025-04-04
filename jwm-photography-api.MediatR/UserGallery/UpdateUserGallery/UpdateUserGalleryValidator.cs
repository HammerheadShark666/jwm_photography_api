using FluentValidation;
using jwm_photography_api.Data.Repository.Interfaces;

namespace jwm_photography_api.MediatR.UserGallery.UpdateUserGallery;

public class UpdateUserGalleryValidator : AbstractValidator<UpdateUserGalleryRequest>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUserGalleryRepository _userGalleryRepository;

    public UpdateUserGalleryValidator(IAccountRepository accountRepository, IUserGalleryRepository userGalleryRepository)
    {
        _accountRepository = accountRepository;
        _userGalleryRepository = userGalleryRepository;

        RuleFor(UpdateUserGalleryRequest => UpdateUserGalleryRequest.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(1, 150).WithMessage("Name length between 1 and 150.");

        RuleFor(UpdateUserGalleryRequest => UpdateUserGalleryRequest.Description)
                .Length(0, 1000).WithMessage("Description length between 0 and 1000.");

        RuleFor(UpdateUserGalleryRequest => UpdateUserGalleryRequest).MustAsync(async (UpdateUserGalleryRequest, cancellation) =>
        {
            return await AccountExists(UpdateUserGalleryRequest.AccountId);
        }).WithMessage("Account not found.");

        RuleFor(UpdateUserGalleryRequest => UpdateUserGalleryRequest).MustAsync(async (UpdateUserGalleryRequest, cancellation) =>
        {
            return await GalleryExists(UpdateUserGalleryRequest.AccountId, UpdateUserGalleryRequest.GalleryId);
        }).WithMessage("User gallery not found.");

        RuleFor(UpdateUserGalleryRequest => UpdateUserGalleryRequest).MustAsync(async (UpdateUserGalleryRequest, cancellation) =>
        {
            return await GalleryNameExists(UpdateUserGalleryRequest.AccountId, UpdateUserGalleryRequest.Name);
        }).WithMessage("User gallery name already exists.");
    }

    protected async Task<bool> AccountExists(Guid accountId)
    {
        return await _accountRepository.ExistsAsync(accountId);
    }

    protected async Task<bool> GalleryExists(Guid accountId, long galleryId)
    {
        return await _userGalleryRepository.ExistsAsync(accountId, galleryId);
    }

    protected async Task<bool> GalleryNameExists(Guid accountId, string name)
    {
        return !await _userGalleryRepository.ExistsAsync(accountId, name);
    }
}
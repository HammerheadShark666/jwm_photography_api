using FluentValidation;
using jwm_photography_api.Data.Repository.Interfaces;

namespace jwm_photography_api.MediatR.Favourite.AddFavouritePhoto;

public class AddFavouritePhotoValidator : AbstractValidator<AddFavouritePhotoRequest>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IPhotoRepository _photoRepository;
    private readonly IFavouriteRepository _favouriteRepository;

    public AddFavouritePhotoValidator(IAccountRepository accountRepository, IPhotoRepository photoRepository, IFavouriteRepository favouriteRepository)
    {
        _accountRepository = accountRepository;
        _photoRepository = photoRepository;
        _favouriteRepository = favouriteRepository;

        RuleFor(AddFavouritePhotoRequest => AddFavouritePhotoRequest).MustAsync(async (AddFavouritePhotoRequest, cancellation) =>
        {
            return await PhotoExists(AddFavouritePhotoRequest.PhotoId);
        }).WithMessage("Account not found.");

        RuleFor(AddFavouritePhotoRequest => AddFavouritePhotoRequest).MustAsync(async (AddFavouritePhotoRequest, cancellation) =>
        {
            return await AccountExists(AddFavouritePhotoRequest.UserId);
        }).WithMessage("Photo not found.");

        RuleFor(AddFavouritePhotoRequest => AddFavouritePhotoRequest).MustAsync(async (AddFavouritePhotoRequest, cancellation) =>
        {
            return await PhotoAlreadyFavourite(AddFavouritePhotoRequest.UserId, AddFavouritePhotoRequest.PhotoId);
        }).WithMessage("Photo already a favourite.");
    }

    protected async Task<bool> AccountExists(Guid id)
    {
        return await _accountRepository.ExistsAsync(id);
    }

    protected async Task<bool> PhotoExists(int id)
    {
        return await _photoRepository.ExistsAsync(id);
    }

    protected async Task<bool> PhotoAlreadyFavourite(Guid userId, int photoId)
    {
        return await _favouriteRepository.ExistsAsync(userId, photoId);
    }
}
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

        //RuleFor(AddFavouritePhotoRequest => AddFavouritePhotoRequest).MustAsync(async (AddFavouritePhotoRequest, cancellation) =>
        //{
        //    return await PhotoExists(AddFavouritePhotoRequest.PhotoId);
        //}).WithMessage("Photo not found.");

        //RuleFor(AddFavouritePhotoRequest => AddFavouritePhotoRequest).MustAsync(async (AddFavouritePhotoRequest, cancellation) =>
        //{
        //    return await AccountExists(AddFavouritePhotoRequest.AccountId);
        //}).WithMessage("Account not found.");

        //RuleFor(AddFavouritePhotoRequest => AddFavouritePhotoRequest).MustAsync(async (AddFavouritePhotoRequest, cancellation) =>
        //{
        //    return await PhotoAlreadyFavourite(AddFavouritePhotoRequest.AccountId, AddFavouritePhotoRequest.PhotoId);
        //}).WithMessage("Photo already a favourite.");
    }

    protected async Task<bool> AccountExists(Guid id)
    {
        return await _accountRepository.ExistsAsync(id);
    }

    //protected async Task<bool> PhotoExists(int id)
    //{
    //    return await _photoRepository.ExistsAsync(id);
    //}

    //protected async Task<bool> PhotoAlreadyFavourite(Guid userId, int photoId)
    //{
    //    return await _favouriteRepository.ExistsAsync(userId, photoId);
    //}
}
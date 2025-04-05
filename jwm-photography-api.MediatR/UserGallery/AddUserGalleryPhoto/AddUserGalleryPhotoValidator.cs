using FluentValidation;
using jwm_photography_api.Data.Repository.Interfaces;

namespace jwm_photography_api.MediatR.UserGallery.AddUserGalleryPhoto;

public class AddUserGalleryPhotoValidator : AbstractValidator<AddUserGalleryPhotoRequest>
{
    private readonly IPhotoRepository _photoRepository;
    private readonly IUserGalleryRepository _userGalleryRepository;
    private readonly IUserGalleryPhotoRepository _userGalleryPhotoRepository;

    public AddUserGalleryPhotoValidator(IPhotoRepository photoRepository, IUserGalleryRepository userGalleryRepository, IUserGalleryPhotoRepository userGalleryPhotoRepository)
    {
        _photoRepository = photoRepository;
        _userGalleryRepository = userGalleryRepository;
        _userGalleryPhotoRepository = userGalleryPhotoRepository;

        RuleFor(AddUserGalleryPhotoRequest => AddUserGalleryPhotoRequest).MustAsync(async (AddUserGalleryPhotoRequest, cancellation) =>
        {
            return await UserGalleryExists(AddUserGalleryPhotoRequest.AccountId, AddUserGalleryPhotoRequest.UserGalleryId);
        }).WithMessage("User gallery not found.");

        RuleFor(AddUserGalleryPhotoRequest => AddUserGalleryPhotoRequest).MustAsync(async (AddUserGalleryPhotoRequest, cancellation) =>
        {
            return await PhotoExists(AddUserGalleryPhotoRequest.PhotoId);
        }).WithMessage("Photo not found.");

        RuleFor(AddUserGalleryPhotoRequest => AddUserGalleryPhotoRequest).MustAsync(async (AddUserGalleryPhotoRequest, cancellation) =>
        {
            return await UserGalleryPhotoExists(AddUserGalleryPhotoRequest.UserGalleryId, AddUserGalleryPhotoRequest.PhotoId);
        }).WithMessage("Photo is already in the user gallery.");
        _userGalleryPhotoRepository = userGalleryPhotoRepository;
    }

    protected async Task<bool> PhotoExists(long photoId)
    {
        return await _photoRepository.ExistsAsync(photoId);
    }

    protected async Task<bool> UserGalleryExists(Guid accountId, long userGalleryId)
    {
        return await _userGalleryRepository.ExistsAsync(accountId, userGalleryId);
    }

    protected async Task<bool> UserGalleryPhotoExists(long userGalleryId, long photoId)
    {
        return !await _userGalleryPhotoRepository.ExistsAsync(userGalleryId, photoId);
    }
}
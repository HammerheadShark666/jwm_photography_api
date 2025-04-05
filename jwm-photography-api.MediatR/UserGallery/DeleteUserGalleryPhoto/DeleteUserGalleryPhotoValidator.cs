using FluentValidation;
using jwm_photography_api.Data.Repository.Interfaces;

namespace jwm_photography_api.MediatR.UserGallery.DeleteUserGalleryPhoto;

public class DeleteUserGalleryPhotoValidator : AbstractValidator<DeleteUserGalleryPhotoRequest>
{
    private readonly IUserGalleryPhotoRepository _userGalleryPhotoRepository;

    public DeleteUserGalleryPhotoValidator(IUserGalleryPhotoRepository userGalleryPhotoRepository)
    {
        _userGalleryPhotoRepository = userGalleryPhotoRepository;

        RuleFor(DeleteUserGalleryPhotoRequest => DeleteUserGalleryPhotoRequest).MustAsync(async (DeleteUserGalleryPhotoRequest, cancellation) =>
        {
            return await UserGalleryPhotoExists(DeleteUserGalleryPhotoRequest.AccountId, DeleteUserGalleryPhotoRequest.UserGalleryId, DeleteUserGalleryPhotoRequest.PhotoId);
        }).WithMessage("User gallery photo not found.");
    }

    protected async Task<bool> UserGalleryPhotoExists(Guid accountId, long userGalleryId, long photoId)
    {
        return await _userGalleryPhotoRepository.ExistsAsync(accountId, userGalleryId, photoId);
    }
}
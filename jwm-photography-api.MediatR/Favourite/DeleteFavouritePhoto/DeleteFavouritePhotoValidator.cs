using FluentValidation;
using jwm_photography_api.Data.Repository.Interfaces;
using jwm_photography_api.MediatR.Favourite.DeleteFavouritePhoto;

namespace jwm_photography_api.MediatR.Favourite.AddFavouritePhoto;

public class DeleteFavouritePhotoValidator : AbstractValidator<DeleteFavouritePhotoRequest>
{
    private readonly IFavouriteRepository _favouriteRepository;

    public DeleteFavouritePhotoValidator(IFavouriteRepository favouriteRepository)
    {
        _favouriteRepository = favouriteRepository;

        RuleFor(DeleteFavouritePhotoRequest => DeleteFavouritePhotoRequest).MustAsync(async (DeleteFavouritePhotoRequest, cancellation) =>
        {
            return await PhotoAlreadyFavourite(DeleteFavouritePhotoRequest.UserId, DeleteFavouritePhotoRequest.PhotoId);
        }).WithMessage("Photo is not a favourite.");
    }

    protected async Task<bool> PhotoAlreadyFavourite(Guid userId, int photoId)
    {
        return !await _favouriteRepository.ExistsAsync(userId, photoId);
    }
}
using jwm_photography_api.Data.UnitOfWork.Interfaces;
using MediatR;

namespace jwm_photography_api.MediatR.Favourite.DeleteFavouritePhoto;

public class DeleteFavouritePhotoCommandHandler(IUnitOfWork unitOfWork)
                                                    : IRequestHandler<DeleteFavouritePhotoRequest, DeleteFavouritePhotoResponse>
{

    public async Task<DeleteFavouritePhotoResponse> Handle(DeleteFavouritePhotoRequest deleteFavouritePhotoRequest, CancellationToken cancellationToken)
    {
        var favouritePhoto = await unitOfWork.Favourites.GetFavouritePhotoAsync(deleteFavouritePhotoRequest.UserId, deleteFavouritePhotoRequest.PhotoId) ?? throw new Exception("Favourite photo not found.");
        unitOfWork.Favourites.Delete(favouritePhoto);
        await unitOfWork.Complete();

        return new DeleteFavouritePhotoResponse();
    }
}
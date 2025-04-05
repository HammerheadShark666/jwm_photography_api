using jwm_photography_api.Data.UnitOfWork.Interfaces;
using jwm_photography_api.Helpers.Exceptions;
using MediatR;

namespace jwm_photography_api.MediatR.UserGallery.DeleteUserGalleryPhoto;

public class DeleteUserGalleryPhotoCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserGalleryPhotoRequest, DeleteUserGalleryPhotoResponse>
{
    public async Task<DeleteUserGalleryPhotoResponse> Handle(DeleteUserGalleryPhotoRequest deleteUserGalleryRequest, CancellationToken cancellationToken)
    {
        var userGallery = await unitOfWork.UserGalleryPhotos.GetGalleryPhotoAsync(deleteUserGalleryRequest.AccountId, deleteUserGalleryRequest.UserGalleryId, deleteUserGalleryRequest.PhotoId) ?? throw new NotFoundException("User gallery not found.");
        unitOfWork.UserGalleryPhotos.Delete(userGallery);
        await unitOfWork.Complete();

        return new DeleteUserGalleryPhotoResponse();
    }
}
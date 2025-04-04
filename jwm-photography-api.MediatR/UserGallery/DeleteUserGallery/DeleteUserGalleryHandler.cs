using jwm_photography_api.Data.UnitOfWork.Interfaces;
using jwm_photography_api.Helpers.Exceptions;
using MediatR;

namespace jwm_photography_api.MediatR.UserGallery.DeleteUserGallery;

public class DeleteUserGalleryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserGalleryRequest, DeleteUserGalleryResponse>
{
    public async Task<DeleteUserGalleryResponse> Handle(DeleteUserGalleryRequest deleteUserGalleryRequest, CancellationToken cancellationToken)
    {
        var userGallery = await unitOfWork.UserGalleries.GetAsync(deleteUserGalleryRequest.AccountId, deleteUserGalleryRequest.GalleryId) ?? throw new NotFoundException("User gallery not found.");
        unitOfWork.UserGalleries.Delete(userGallery);
        await unitOfWork.Complete();

        return new DeleteUserGalleryResponse();
    }
}
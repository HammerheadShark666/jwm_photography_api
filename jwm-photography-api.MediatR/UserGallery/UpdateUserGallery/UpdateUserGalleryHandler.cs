using jwm_photography_api.Data.UnitOfWork.Interfaces;
using jwm_photography_api.Helpers.Exceptions;
using MediatR;

namespace jwm_photography_api.MediatR.UserGallery.UpdateUserGallery;

public class AddUserGalleryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateUserGalleryRequest, UpdateUserGalleryResponse>
{
    public async Task<UpdateUserGalleryResponse> Handle(UpdateUserGalleryRequest updateUserGalleryRequest, CancellationToken cancellationToken)
    {
        var userGallery = (await unitOfWork.UserGalleries.GetAsync(updateUserGalleryRequest.AccountId, updateUserGalleryRequest.GalleryId) ?? throw new NotFoundException("User gallery not found.")) ?? throw new NotFoundException("User gallery not found.");
        userGallery.Name = updateUserGalleryRequest.Name;
        userGallery.Description = updateUserGalleryRequest.Description;

        unitOfWork.UserGalleries.Update(userGallery);
        await unitOfWork.Complete();

        return new UpdateUserGalleryResponse();
    }
}
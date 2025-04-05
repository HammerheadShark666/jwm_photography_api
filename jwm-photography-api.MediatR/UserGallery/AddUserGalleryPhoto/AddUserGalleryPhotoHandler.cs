using AutoMapper;
using jwm_photography_api.Data.UnitOfWork.Interfaces;
using MediatR;

namespace jwm_photography_api.MediatR.UserGallery.AddUserGalleryPhoto;

public class AddUserGalleryPhotoCommandHandler(IUnitOfWork unitOfWork,
                                          IMapper mapper) : IRequestHandler<AddUserGalleryPhotoRequest, AddUserGalleryPhotoResponse>
{

    public async Task<AddUserGalleryPhotoResponse> Handle(AddUserGalleryPhotoRequest addUserGalleryPhotoRequest, CancellationToken cancellationToken)
    {
        var userGalleryPhoto = mapper.Map<Domain.UserGalleryPhoto>(addUserGalleryPhotoRequest);
        await unitOfWork.UserGalleryPhotos.AddAsync(userGalleryPhoto);
        await unitOfWork.Complete();

        //TODO : Reorder other photos in the user gallery

        return new AddUserGalleryPhotoResponse();
    }
}
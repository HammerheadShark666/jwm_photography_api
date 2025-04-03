using AutoMapper;
using jwm_photography_api.Data.UnitOfWork.Interfaces;
using MediatR;

namespace jwm_photography_api.MediatR.UserGallery.AddUserGallery;

public class AddUserGalleryCommandHandler(IUnitOfWork unitOfWork,
                                          IMapper mapper) : IRequestHandler<AddUserGalleryRequest, AddUserGalleryResponse>
{

    public async Task<AddUserGalleryResponse> Handle(AddUserGalleryRequest addUserGalleryRequest, CancellationToken cancellationToken)
    {
        var userGallery = mapper.Map<Domain.UserGallery>(addUserGalleryRequest);
        await unitOfWork.UserGalleries.AddAsync(userGallery);
        await unitOfWork.Complete();

        return new AddUserGalleryResponse();
    }
}
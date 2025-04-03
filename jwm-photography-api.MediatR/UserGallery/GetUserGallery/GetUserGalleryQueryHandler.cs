using AutoMapper;
using jwm_photography_api.Data.UnitOfWork.Interfaces;
using jwm_photography_api.Helpers.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace jwm_photography_api.MediatR.UserGallery.GetUserGallery;

public class GetUserGalleryQueryHandler(IUnitOfWork unitOfWork,
                                        ILogger<GetUserGalleryQueryHandler> logger,
                                        IMapper mapper) : IRequestHandler<GetUserGalleryRequest, GetUserGalleryResponse>
{
    public async Task<GetUserGalleryResponse> Handle(GetUserGalleryRequest getUserGalleryRequest, CancellationToken cancellationToken)
    {
        var userGallery = await unitOfWork.UserGalleries.GetFullGalleryAsync(getUserGalleryRequest.AccountId, getUserGalleryRequest.UserGalleryId);
        if (userGallery == null)
        {
            logger.LogError("User Gallery not found.");
            throw new NotFoundException("User Gallery not found.");
        }

        return mapper.Map<GetUserGalleryResponse>(userGallery);
    }
}
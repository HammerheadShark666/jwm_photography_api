using AutoMapper;
using jwm_photography_api.Data.UnitOfWork.Interfaces;
using jwm_photography_api.Helpers.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace jwm_photography_api.MediatR.Gallery.GetGallery;

public class GetGalleryQueryHandler(IUnitOfWork unitOfWork,
                                    ILogger<GetGalleryQueryHandler> logger,
                                    IMapper mapper) : IRequestHandler<GetGalleryRequest, GetGalleryResponse>
{
    public async Task<GetGalleryResponse> Handle(GetGalleryRequest getGalleryRequest, CancellationToken cancellationToken)
    {
        var gallery = await unitOfWork.Galleries.GetGalleryAsync(getGalleryRequest.Id);
        if (gallery == null)
        {
            logger.LogError("Gallery not found.");
            throw new NotFoundException("Gallery not found.");
        }

        return mapper.Map<GetGalleryResponse>(gallery);
    }
}
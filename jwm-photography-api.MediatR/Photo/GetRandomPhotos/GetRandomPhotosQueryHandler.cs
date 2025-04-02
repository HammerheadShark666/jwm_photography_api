using AutoMapper;
using jwm_photography_api.Data.UnitOfWork.Interfaces;
using MediatR;

namespace jwm_photography_api.MediatR.Photo.GetRandomPhotos;

public class GetRandomPhotosQueryHandler(IUnitOfWork unitOfWork,
                                         IMapper mapper) : IRequestHandler<GetRandomPhotosRequest, GetRandomPhotosResponse>
{
    public async Task<GetRandomPhotosResponse> Handle(GetRandomPhotosRequest getRandomPhotosRequest, CancellationToken cancellationToken)
    {
        var photos = await unitOfWork.Photos.GetRandomPhotosAsync(getRandomPhotosRequest.NumberOfLandscape,
                                                                  getRandomPhotosRequest.NumberOfPortrait,
                                                                  getRandomPhotosRequest.NumberOfSqure);

        return mapper.Map<GetRandomPhotosResponse>(photos);
    }
}
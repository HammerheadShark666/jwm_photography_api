using MediatR;

namespace jwm_photography_api.MediatR.Photo.GetRandomPhotos;

public record GetRandomPhotosRequest(int NumberOfLandscape, int NumberOfPortrait, int NumberOfSqure) : IRequest<GetRandomPhotosResponse>;
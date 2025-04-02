using FluentValidation;
using jwm_photography_api.Data.UnitOfWork.Interfaces;
using jwm_photography_api.Helpers.Exceptions;
using jwm_photography_api.MediatR.Gallery.GetGallery;

namespace jwm_photography_api.MediatR.Gallert.GetGallery;

public class GetGalleryValidator : AbstractValidator<GetGalleryRequest>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetGalleryValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(getGalleryRequest => getGalleryRequest).MustAsync(async (getGalleryRequest, cancellation) =>
        {
            return await GalleryExists(getGalleryRequest);
        }).WithMessage("Gallery Not Found.");
    }

    protected async Task<bool> GalleryExists(GetGalleryRequest getGalleryRequest)
    {
        try
        {
            var galleryExists = await _unitOfWork.Galleries.ExistsAsync(getGalleryRequest.Id);
            if (galleryExists)
                return true;
        }
        catch (NotFoundException)
        {
            return false;
        }

        return false;
    }
}
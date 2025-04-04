using FluentValidation;
using jwm_photography_api.Data.UnitOfWork.Interfaces;
using jwm_photography_api.Helpers.Exceptions;

namespace jwm_photography_api.MediatR.UserGallery.GetUserGallery;

public class GetUserGalleryValidator : AbstractValidator<GetUserGalleryRequest>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserGalleryValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(getUserGalleryRequest => getUserGalleryRequest).MustAsync(async (getUserGalleryRequest, cancellation) =>
        {
            return await UserGalleryExists(getUserGalleryRequest);
        }).WithMessage("User Gallery Not Found.");
    }

    protected async Task<bool> UserGalleryExists(GetUserGalleryRequest getUserGalleryRequest)
    {
        try
        {
            var userGalleryExists = await _unitOfWork.UserGalleries.ExistsAsync(getUserGalleryRequest.AccountId, getUserGalleryRequest.UserGalleryId);
            if (userGalleryExists)
                return true;
        }
        catch (NotFoundException)
        {
            return false;
        }

        return false;
    }
}
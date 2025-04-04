using FluentValidation;
using jwm_photography_api.Data.Repository.Interfaces;

namespace jwm_photography_api.MediatR.UserGallery.DeleteUserGallery;

public class DeleteUserGalleryValidator : AbstractValidator<DeleteUserGalleryRequest>
{
    private readonly IUserGalleryRepository _userGalleryRepository;

    public DeleteUserGalleryValidator(IUserGalleryRepository userGalleryRepository)
    {
        _userGalleryRepository = userGalleryRepository;

        RuleFor(DeleteUserGalleryRequest => DeleteUserGalleryRequest).MustAsync(async (DeleteUserGalleryRequest, cancellation) =>
        {
            return await UserGalleryExists(DeleteUserGalleryRequest.AccountId, DeleteUserGalleryRequest.GalleryId);
        }).WithMessage("User gallery not found.");
    }

    protected async Task<bool> UserGalleryExists(Guid accountId, long id)
    {
        return await _userGalleryRepository.ExistsAsync(accountId, id);
    }
}
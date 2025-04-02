using AutoMapper;
using jwm_photography_api.Data.UnitOfWork.Interfaces;
using jwm_photography_api.Helpers.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace jwm_photography_api.MediatR.Category.GetCategories;

public class GetCategoriesQueryHandler(IUnitOfWork unitOfWork,
                                       ILogger<GetCategoriesQueryHandler> logger,
                                       IMapper mapper) : IRequestHandler<GetCategoriesRequest, GetCategoriesResponse>
{
    public async Task<GetCategoriesResponse> Handle(GetCategoriesRequest getCategoriesRequest, CancellationToken cancellationToken)
    {
        var categories = await unitOfWork.Categories.AllSortedAsync();
        if (categories.Count == 0)
        {
            logger.LogError("Categories not found.");
            throw new NotFoundException("Categories not found.");
        }

        return mapper.Map<GetCategoriesResponse>(categories);
    }
}
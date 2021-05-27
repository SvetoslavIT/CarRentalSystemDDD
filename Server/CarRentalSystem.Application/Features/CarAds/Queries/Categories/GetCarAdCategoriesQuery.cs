namespace CarRentalSystem.Application.Features.CarAds.Queries.Categories
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;

    public class GetCarAdCategoriesQuery : IRequest<IEnumerable<GetCarAdCategoryOutputModel>>
    {
        public class GetCarAdCategoriesQueryHandler 
            : IRequestHandler<GetCarAdCategoriesQuery, IEnumerable<GetCarAdCategoryOutputModel>>
        {
            private readonly ICarAdRepository _carAdRepository;

            public GetCarAdCategoriesQueryHandler(ICarAdRepository carAdRepository) 
                => _carAdRepository = carAdRepository;

            public Task<IEnumerable<GetCarAdCategoryOutputModel>> Handle(
                GetCarAdCategoriesQuery request,
                CancellationToken cancellationToken)
                => _carAdRepository.GetCategories(cancellationToken);
        }
    }
}
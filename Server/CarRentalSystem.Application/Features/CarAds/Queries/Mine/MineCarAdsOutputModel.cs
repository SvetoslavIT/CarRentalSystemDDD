namespace CarRentalSystem.Application.Features.CarAds.Queries.Mine
{
    using System.Collections.Generic;
    using Common;

    public class MineCarAdsOutputModel : CarAdsOutputModel<MineCarAdOutputModel>
    {
        public MineCarAdsOutputModel(IEnumerable<MineCarAdOutputModel> carAds, int currentPage, int totalPages) 
            : base(carAds, currentPage, totalPages)
        {
        }
    }
}
namespace CarRentalSystem.Web.Features
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.Features;
    using CarRentalSystem.Application.Features.CarAds.Commands.ChangeAvailability;
    using CarRentalSystem.Application.Features.CarAds.Commands.Create;
    using CarRentalSystem.Application.Features.CarAds.Commands.Delete;
    using CarRentalSystem.Application.Features.CarAds.Commands.Edit;
    using CarRentalSystem.Application.Features.CarAds.Queries.Categories;
    using CarRentalSystem.Application.Features.CarAds.Queries.Details;
    using CarRentalSystem.Application.Features.CarAds.Queries.Mine;
    using CarRentalSystem.Application.Features.CarAds.Queries.Search;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    
    public class CarAdsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<SearchCarAdsOutputModel>> Search(
            [FromQuery] SearchCarAdsQuery query)
            => await Send(query);

        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<CarAdDetailsOutputModel>> Details(
            [FromRoute] CarAdDetailsQuery query)
            => await Send(query);

        [HttpGet]
        [Route(nameof(Mine))]
        [Authorize]
        public async Task<ActionResult<MineCarAdsOutputModel>> Mine(
            [FromQuery] MineCarAdQuery query)
            => await Send(query);

        [HttpGet]
        [Route(nameof(Categories))]
        public async Task<ActionResult<IEnumerable<GetCarAdCategoryOutputModel>>> Categories(
            [FromQuery] GetCarAdCategoriesQuery query)
            => await Send(query);

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CreateCarAdOutputModel>> Create(
            CreateCarAdCommand command)
            => await Send(command);

        [HttpPut]
        [Route(Id)]
        [Authorize]
        public async Task<ActionResult> Edit(
            int id, EditCarAdCommand command)
            => await Send(command.SetId(id));

        [HttpPut]
        [Route(Id + PathSeparator + nameof(ChangeAvailability))]
        [Authorize]
        public async Task<ActionResult> ChangeAvailability(
           [FromRoute] ChangeAvailabilityCommand command)
            => await Send(command);

        [HttpDelete]
        [Route(Id)]
        [Authorize]
        public async Task<ActionResult> Delete(
            [FromRoute] DeleteCarAdCommand command)
            => await Send(command);
    }
}
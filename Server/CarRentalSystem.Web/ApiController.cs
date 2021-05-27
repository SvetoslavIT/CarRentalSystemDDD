namespace CarRentalSystem.Web
{
    using System.Threading.Tasks;
    using Application.Common;
    using Common;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    [ApiController]
    [Route("[controller]")]
    public abstract class ApiController : ControllerBase
    {
        public const string Id = "{id}";
        public const string PathSeparator = "/";

        private IMediator? _mediator;

        protected IMediator Mediator
#pragma warning disable 8603
            => _mediator ??= HttpContext
                .RequestServices
                .GetService<IMediator>();
#pragma warning restore 8603

        protected Task<ActionResult<TResult>> Send<TResult>(IRequest<TResult> request)
            => Mediator.Send(request).ToActionResult();

        protected Task<ActionResult> Send(IRequest<Result> request)
            => Mediator.Send(request).ToActionResult();

        protected Task<ActionResult<TResult>> Send<TResult>(IRequest<Result<TResult>> request)
            => Mediator.Send(request).ToActionResult();
    }
}
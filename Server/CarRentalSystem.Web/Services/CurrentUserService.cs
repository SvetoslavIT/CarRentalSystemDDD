namespace CarRentalSystem.Web.Services
{
    using System.Security.Claims;
    using Application.Contracts;
    using Microsoft.AspNetCore.Http;

    public class CurrentUserService : ICurrentUser
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;

            UserId = user?.FindFirstValue(ClaimTypes.NameIdentifier);
            Role = user?.FindFirstValue(ClaimTypes.Role);
        }

        public string? UserId { get; }

        public string? Role { get; }
    }
}
namespace CarRentalSystem.Application.Contracts
{
    using Services;

    public interface ICurrentUser : IScopedService
    {
        string? UserId { get; }

        string? Role { get; }
    }
}
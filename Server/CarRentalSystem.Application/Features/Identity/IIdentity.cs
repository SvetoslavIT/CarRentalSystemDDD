namespace CarRentalSystem.Application.Features.Identity
{
    using System.Threading.Tasks;
    using Commands.LoginUser;
    using Common;
    using Services;

    public interface IIdentity : IService
    {
        Task<Result<IUser>> Register(UserInputModel userInput);

        Task<Result<LoginSuccessModel>> Login(UserInputModel userInput);

        Task<string?> GetUserName(string? userId);

        Task<Result> ChangePassword(string userId, string currentPassword, string newPassword);
    }
}
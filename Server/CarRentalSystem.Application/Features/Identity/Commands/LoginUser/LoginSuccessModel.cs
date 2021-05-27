namespace CarRentalSystem.Application.Features.Identity.Commands.LoginUser
{
    public class LoginSuccessModel
    {
        public LoginSuccessModel(string token, string userId)
        {
            Token = token;
            UserId = userId;
        }

        public string Token { get; }

        public string UserId { get; }
    }
}
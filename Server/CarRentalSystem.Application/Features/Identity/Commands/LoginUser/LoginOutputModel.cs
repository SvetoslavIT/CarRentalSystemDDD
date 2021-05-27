namespace CarRentalSystem.Application.Features.Identity.Commands.LoginUser
{
    public class LoginOutputModel
    {
        public LoginOutputModel(string token, int dealerId)
        {
            Token = token;
            DealerId = dealerId;
        }

        public string Token { get; }

        public int DealerId { get; }
    }
}
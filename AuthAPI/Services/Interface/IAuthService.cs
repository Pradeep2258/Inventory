using AuthAPI.Model;

namespace AuthAPI.Services.Interface
{
    public interface IAuthService
    {
        string GenerateJSONWebToken(UserModel userInfo);
        UserModel AuthenticateUser(UserLoginModel login);
    }
}

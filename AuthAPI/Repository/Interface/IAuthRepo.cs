using AuthAPI.Model;

namespace AuthAPI.Repository.Interface
{
    public interface IAuthRepo
    {
        string GetUserDetail(UserLoginModel login);
        UserModel getUserRoleByID(UserLoginModel login);
    }
}

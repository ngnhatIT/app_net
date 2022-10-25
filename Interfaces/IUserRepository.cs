using apiapp.Model;
using apiapp.ViewModel.Common;
using apiapp.ViewModel.User.SignIn;
using apiapp.ViewModel.User.SignUp;

namespace apiapp.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<ApiResult<UserSignInResponse>> SignIn(UserSignInRequest request);
        Task<ApiResult<string>> SingUp(UserSignUpRequest request);
    }
}
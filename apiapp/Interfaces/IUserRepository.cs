using apiapp.Model;
using apiapp.ViewModel.Common;
using apiapp.ViewModel.User;

namespace apiapp.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<ApiResult<UserResponse>> SignIn(UserRequest request);
        Task<ApiResult<string>> SignUp(UserRequest request);
    }
}
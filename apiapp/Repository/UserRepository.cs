using System.IdentityModel.Tokens.Jwt;
using apiapp.FirebaseAuthService.Payload;
using apiapp.FirebaseAuthService.Service;
using apiapp.Interfaces;
using apiapp.Model;
using apiapp.ViewModel.Common;
using apiapp.ViewModel.User;

namespace apiapp.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly IAuthenService _authenService;
        public UserRepository(IMongoContext context, IAuthenService authenService) : base(context)
        {
            _authenService = authenService;
        }

        public async Task<ApiResult<UserResponse>> SignIn(UserRequest request)
        {
            SignInUserResponse signInUserResponse = new SignInUserResponse();
            try
            {
                SignInUserRequest signInUserRequest = new SignInUserRequest()
                {
                    Email = request.Email,
                    Password = request.Password
                };
                signInUserResponse = await _authenService.SignIn(signInUserRequest);
            }
            catch (Exception e)
            {
                return new ApiErrorResult<UserResponse>(e.Message);
            }

            bool hasVerifiedEmail = CheckEmailVerified(signInUserResponse.IdToken);
            if (!hasVerifiedEmail)
            {
                return new ApiErrorResult<UserResponse>("User is not verified email, please check your mail!");
            }
            UserResponse userResponse = new UserResponse()
            {
                Email = signInUserResponse.Email,
                IdToken = signInUserResponse.IdToken
            };
            return new ApiSuccessResult<UserResponse>(userResponse, "Login success");
        }

        public Task<ApiResult<string>> SignUp(UserRequest request)
        {
            throw new NotImplementedException();
        }

        private bool CheckEmailVerified(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var claims = jwtSecurityToken.Claims.ToList();
            var emailVerified = claims.FirstOrDefault(x => x.Type == "email_verified").Value;
            bool.TryParse(emailVerified, out bool hasVerifyEmail);
            return hasVerifyEmail;
        }
    }
}
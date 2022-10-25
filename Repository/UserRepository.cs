using System.IdentityModel.Tokens.Jwt;
using apiapp.FirebaseAuthService.Payload;
using apiapp.FirebaseAuthService.Service;
using apiapp.Interfaces;
using apiapp.Model;
using apiapp.ViewModel.Common;
using apiapp.ViewModel.User.SignIn;
using apiapp.ViewModel.User.SignUp;

namespace apiapp.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly IAuthenService _authenService;
        public UserRepository(IMongoContext context, IAuthenService authenService) : base(context)
        {
            _authenService = authenService;
        }

        public async Task<ApiResult<UserSignInResponse>> SignIn(UserSignInRequest request)
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
                return new ApiErrorResult<UserSignInResponse>(e.Message);
            }

            bool hasVerifiedEmail = CheckEmailVerified(signInUserResponse.IdToken);
            if (!hasVerifiedEmail)
            {
                return new ApiErrorResult<UserSignInResponse>("User is not verified email, please check your mail!");
            }
            UserSignInResponse userResponse = new UserSignInResponse()
            {
                Email = signInUserResponse.Email,
                IdToken = signInUserResponse.IdToken
            };
            return new ApiSuccessResult<UserSignInResponse>(userResponse, "Login success");
        }

        public async Task<ApiResult<string>> SingUp(UserSignUpRequest request)
        {
            SignUpUserResponse signUpUserResponse = new SignUpUserResponse();
            VerifyEmailResponse verifyEmail = new VerifyEmailResponse();

            SignUpUserRequest userSignUpRequest = new SignUpUserRequest()
            {
                Email = request.Email,
                Password = request.Password
            };
            try
            {
                signUpUserResponse = await _authenService.SignUp(userSignUpRequest);
            }
            catch (Exception e)
            {
                return new ApiErrorResult<string>(e.Message);
            }

            VerifyEmailRequest verifyEmailRequest = new VerifyEmailRequest()
            {
                IdToken = signUpUserResponse.IdToken,
                RequestType = "VERIFY_EMAIL"
            };

            try
            {
                verifyEmail = await _authenService.VerificationEmail(verifyEmailRequest);
            }
            catch (Exception e)
            {
                return new ApiErrorResult<string>(e.Message);
            }
            return new ApiSuccessResult<string>(verifyEmail.Email, "Sign up success please check your mail !");
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
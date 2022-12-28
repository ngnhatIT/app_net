using System.IdentityModel.Tokens.Jwt;
using apiapp.FirebaseAuthService.Payload;
using apiapp.FirebaseAuthService.Service;
using apiapp.Interfaces;
using apiapp.Model;
using apiapp.ViewModel.User.SignIn;
using apiapp.ViewModel.User.SignUp;
using Microsoft.AspNetCore.Mvc;

namespace apiapp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _uow;

        private readonly IAuthenService _authenService;

        public UserController(IUserRepository userRepository, IUnitOfWork uow, IAuthenService authenService)
        {
            _userRepository = userRepository;
            _uow = uow;
            _authenService = authenService;
        }

        [HttpPost]
        [Route("SignIn")]
        public async Task<ActionResult<SignInUserResponse>> SignIn([FromBody] SignInRequest request)
        {
            SignInUserResponse signInUserResponse = new SignInUserResponse();
            try
            {
                SignInUserRequest signIn = new SignInUserRequest()
                {
                    Email = request.Email,
                    Password = request.Password
                };
                signInUserResponse = await _authenService.SignIn(signIn);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            bool hasVerifyEmail = CheckEmailVerified(signInUserResponse.IdToken);

            if (hasVerifyEmail)
            {
                SignInResponse response = new SignInResponse()
                {
                    IdToken = signInUserResponse.IdToken,
                    DisplayName = signInUserResponse.DisplayName
                };
                return Ok(response);
            }
            return BadRequest("Email not verified");
        }

        [HttpPost]
        [Route("SignUp")]
        public async Task<ActionResult<User>> SignUp([FromBody] SignUpRequest request)
        {
            SignUpUserResponse signUpUserResponse = new SignUpUserResponse();
            VerifyEmailResponse verifyEmail = new VerifyEmailResponse();
            try
            {
                SignUpUserRequest signUp = new SignUpUserRequest()
                {
                    Email = request.Email,
                    Password = request.Password
                };
                signUpUserResponse = await _authenService.SignUp(signUp);

                // VerifyEmailRequest verifyEmailRequest = new VerifyEmailRequest()
                // {
                //     IdToken = signUpUserResponse.IdToken,
                //     RequestType = "VERIFY_EMAIL"
                // };

                //verifyEmail = await _authenService.VerificationEmail(verifyEmailRequest);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            User user = new User()
            {
                Id = signUpUserResponse.LocalId,
                Email = signUpUserResponse.Email,
                IdToken = signUpUserResponse.IdToken
            };

            _userRepository.Add(user);
            bool isCommit = await _uow.Commit();
            return isCommit ? Ok(user) : BadRequest("Đăng ký tài khoản thất bại");
        }

        private bool CheckEmailVerified(string token)
        {
            if (string.IsNullOrEmpty(token)) return false;
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var claims = jwtSecurityToken.Claims.ToList();
            var emailVerified = claims.FirstOrDefault(x => x.Type == "email_verified").Value;
            bool.TryParse(emailVerified, out bool hasVerifyEmail);
            return hasVerifyEmail;
        }
    }
}
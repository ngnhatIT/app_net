using System.IdentityModel.Tokens.Jwt;
using apiapp.FirebaseAuthService.Payload;
using apiapp.FirebaseAuthService.Service;
using apiapp.Interfaces;
using apiapp.Model;
using apiapp.ViewModel;
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
        public async Task<ActionResult<SignInUserResponse>> SignIn([FromBody] SignInUserRequest request)
        {
            SignInUserResponse signInUserResponse = new SignInUserResponse();
            try
            {
                signInUserResponse = await _authenService.SignIn(request);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            bool hasVerifyEmail = CheckEmailViryfied(signInUserResponse.IdToken);

            if (hasVerifyEmail)
            {
                return Ok(signInUserResponse);
            }
            return BadRequest("Email not verified");
        }

        [HttpPost]
        [Route("SignUp")]
        public async Task<ActionResult<User>> SignUp([FromBody] SignUpUserRequest request)
        {
            SignUpUserResponse signUpUserResponse = new SignUpUserResponse();
            VerifyEmailResponse verifyEmail = new VerifyEmailResponse();
            try
            {
                signUpUserResponse = await _authenService.SignUp(request);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
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
            if (isCommit)
            {

            }
            else
            {

            }
            return Ok(user);
        }

        private bool CheckEmailViryfied(string token)
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
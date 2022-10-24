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
            return Ok(signInUserResponse);
        }

        [HttpPost]
        [Route("SignUp")]
        public async Task<ActionResult<User>> SignUp([FromBody] SignUpUserRequest request)
        {
            SignUpUserResponse signUpUserResponse = new SignUpUserResponse();
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
            VerifyEmailResponse verifyEmail = await _authenService.VerificationEmail(verifyEmailRequest);
            if (verifyEmail == null)
            {

            }

            User user = new User()
            {
                Id = signUpUserResponse.LocalId,
                Email = signUpUserResponse.Email,
                IdToken = signUpUserResponse.IdToken
            };

            _userRepository.Add(user);
            await _uow.Commit();
            return Ok(user);
        }
    }
}
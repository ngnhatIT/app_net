using System.IdentityModel.Tokens.Jwt;
using apiapp.FirebaseAuthService.Payload;
using apiapp.FirebaseAuthService.Service;
using apiapp.Interfaces;
using apiapp.Model;
using apiapp.ViewModel;
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
        public async Task<ActionResult<SignInUserResponse>> SignIn([FromBody] UserSignInRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userRepository.SignIn(request);
            if (!result.IsSuccessed)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost]
        [Route("SignUp")]
        public async Task<ActionResult<User>> SignUp([FromBody] UserSignUpRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userRepository.SingUp(request);
            if (!result.IsSuccessed)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
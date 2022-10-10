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
        public async Task<ActionResult<User>> Post([FromBody] UserViewModel viewModel)
        {
            SignUpUserRequest signUpUser = new SignUpUserRequest()
            {
                Email = viewModel.Email,
                Password = viewModel.PassWord
            };
            SignUpUserResponse signUpUserResponse = await _authenService.SignUp(signUpUser);
            await _uow.Commit();
            return Ok();
        }
    }
}
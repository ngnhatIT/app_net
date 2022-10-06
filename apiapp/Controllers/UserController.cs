using apiapp.Interfaces;
using apiapp.Model;
using apiapp.ViewModel;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;

namespace apiapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _uow;

        public UserController(IUserRepository userRepository, IUnitOfWork uow)
        {
            _userRepository = userRepository;
            _uow = uow;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] UserViewModel viewModel)
        {
            var user = new User();

            UserRecordArgs userRecordArgs = new UserRecordArgs()
            {
                Email = viewModel.Email,
                EmailVerified = false,
                DisplayName = viewModel.DisplayName
            };

            UserRecord userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(userRecordArgs);
            ActionCodeSettings actionCode = new ActionCodeSettings();
            var link = await FirebaseAuth.DefaultInstance.GenerateEmailVerificationLinkAsync(
                viewModel.Email, actionCode
            );


            _userRepository.Add(user);




            await _uow.Commit();
            return Ok();
        }
    }
}
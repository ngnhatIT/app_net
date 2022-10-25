using System.ComponentModel.DataAnnotations;

namespace apiapp.ViewModel.User.SignIn
{
    public class UserSignInRequest
    {
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
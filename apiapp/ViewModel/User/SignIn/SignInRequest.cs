using System.ComponentModel.DataAnnotations;

namespace apiapp.ViewModel.User.SignIn;

public class SignInRequest
{
    public string Email { get; set; }

    [DataType(DataType.Password)]
    public string Password { get; set; }
}
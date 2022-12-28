using System.ComponentModel.DataAnnotations;

namespace apiapp.ViewModel.User.SignUp;

public class SignUpRequest
{
    public string Email { get; set; }

    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}
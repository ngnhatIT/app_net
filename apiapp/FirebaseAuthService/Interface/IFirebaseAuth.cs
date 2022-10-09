using apiapp.FirebaseAuthService.Payload;

namespace apiapp.FirebaseAuthService.Interface;

public interface IFirebaseAuth
{
    Task<SignUpUserResponse> SignUp(SignUpUserRequest signUpUser);
}

using apiapp.FirebaseAuthService.Payload;

namespace apiapp.FirebaseAuthService.Service
{
    public interface IAuthenService
    {
        Task<SignUpUserResponse> SignUp(SignUpUserRequest signUpUser);
        Task<SignInUserResponse> SignIn(SignInUserRequest signUpUser);
        Task<VerifyEmailResponse> VerificationEmail(VerifyEmailRequest verifyEmail);
    }
}
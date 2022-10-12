using apiapp.FirebaseAuthService.Payload;

namespace apiapp.FirebaseAuthService.Service
{
    public interface IAuthenService
    {
        Task<SignUpUserResponse> SignUp(SignUpUserRequest signUpUser);
        Task<VerifyEmailResponse> VerificationEmail(VerifyEmailRequest verifyEmail);
    }
}
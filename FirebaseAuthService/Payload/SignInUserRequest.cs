namespace apiapp.FirebaseAuthService.Payload
{
    public class SignInUserRequest : BaseRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
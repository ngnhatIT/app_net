namespace apiapp.FirebaseAuthService.Payload
{
    public class SignUpUserRequest
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
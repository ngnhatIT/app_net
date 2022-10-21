namespace apiapp.FirebaseAuthService.Payload
{
    public class ChangePasswordRequest
    {
        public string IdToken { get; set; }
        public string Password { get; set; }
    }
}
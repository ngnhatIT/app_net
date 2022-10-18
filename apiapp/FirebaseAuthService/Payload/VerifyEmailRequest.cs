namespace apiapp.FirebaseAuthService.Payload
{
    public class VerifyEmailRequest
    {
        public string IdToken { get; set; }
        public string RequestType { get; set; }
    }
}
namespace apiapp.FirebaseAuthService.Payload
{
    public class SignUpUserResponse : BaseRequest
    {
        public string IdToken { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string RefreshToken { get; set; } = null!;

        public int ExpiresIn { get; set; }

        public string LocalId { get; set; } = null!;
    }
}
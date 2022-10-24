namespace apiapp.FirebaseAuthService.Payload
{
    public class SignInUserResponse : BaseResponse
    {
        public string LocalId { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string IdToken { get; set; }
        public string Registered { get; set; }
        public string RefreshToken { get; set; }
        public string ExpiresIn { get; set; }
    }
}
namespace apiapp.Model;

public class User
{
    public string Id { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public string IdToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}

using System.Text;
using apiapp.FirebaseAuthService.Payload;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace apiapp.FirebaseAuthService.Service
{
    public class AuthenService : IAuthenService, IDisposable
    {
        private const string WEB_API_KEY = "AIzaSyAbMNwuwGMmML2Ay3oDgthCHW4W1cSadK0";
        private readonly HttpClient _client;
        private static readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        public AuthenService()
        {
            _client = new HttpClient();
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public async Task<SignUpUserResponse> SignUp(SignUpUserRequest signUpUser)
        {
            return await Post<SignUpUserResponse>(RelyingSignInSignUpUrl("signUp"), signUpUser);
        }

        private async Task<TResponse> Post<TResponse>(string endpoint, object request) where TResponse : class
        {
            string responseJson = "";

            try
            {
                var content = JsonConvert.SerializeObject(request, jsonSettings);
                var payload = new StringContent(content, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(endpoint, payload);
                responseJson = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
                return JsonConvert.DeserializeObject<TResponse>(responseJson);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string RelyingSignInSignUpUrl(string endpoint)
        {
            return $"https://identitytoolkit.googleapis.com/v1/accounts:{endpoint}?key={WEB_API_KEY}";
        }
        private string SecureTokenUrl()
        {
            return $"https://securetoken.googleapis.com/v1/token?key={WEB_API_KEY}";
        }
    }
}
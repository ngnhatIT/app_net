using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiapp.FirebaseAuthService.Payload
{
    public class SignInUserResponse
    {
        public string LocalId { get; set; }
        public string DisplayName { get; set; }
        public string IdToken { get; set; }
        public string Registered { get; set; }
        public string RefreshToken { get; set; }
        public string ExpiresIn { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiapp.FirebaseAuthService.Payload
{
    public class VerifyEmailRequest
    {
        public string IdToken { get; set; }
        public string RequestType { get; set; }
    }
}
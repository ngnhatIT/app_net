using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiapp.FirebaseAuthService.ExceptionAuth;

namespace apiapp.FirebaseAuthService.Payload
{
    public class FirebaseErrorResponse
    {
        public FirebaseAuthErrorResponse Error { get; set; }
    }

    public class Error
    {
        public string Message { get; set; }
    }
}
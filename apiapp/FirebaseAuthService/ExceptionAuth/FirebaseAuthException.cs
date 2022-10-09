using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiapp.FirebaseAuthService.ExceptionAuth;

public class FirebaseAuthException : Exception
{
    public Exception OriginRequestException { get; set; }
    public FirebaseAuthErrorResponse Error { get; set; }
    public string ResponseJson { get; set; }

    public FirebaseAuthException(string message) : base(message)
    {
    }

    public FirebaseAuthException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

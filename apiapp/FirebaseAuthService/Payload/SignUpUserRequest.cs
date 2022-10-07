using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiapp.FirebaseAuthService.Payload
{
    public class SignUpUserRequest
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
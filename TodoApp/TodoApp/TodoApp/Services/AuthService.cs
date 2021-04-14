using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TodoApp.Services
{
    public class AuthService : IAuthService
    {
        public (bool IsAuthenticated, string Message) IsAuthenticated(string userName, string password)
        {
            //Validation on app
            if (string.IsNullOrWhiteSpace(userName)) return (false, "Username should not be empty!");
            if (string.IsNullOrWhiteSpace(password)) return (false, "Password should not be empty!");

            //TODO: supposed to have authentication from the server
            return (true, "Successfully Login!");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaniWaniBlack.Helper.Services
{
    public static class Strings
    {
        /* User Services */
        public const string INVALID_USER_PASS = @"Username/Password is invalid.";
        public const string USER_AUTH_SUCCESS = "Authentication Successful.";
        public const string USER_ALREADY_EXISTS = "That username already exists.";
        public const string PASSWORDS_DONT_MATCH = "Passwords do not match.";
        public const string USER_CREATED = "User successfuly created.";
        public const string USER_RESET_PASSWORD = "User reset password.";

        /* HTTP Helper */
        public const string WANIKANI_API_URL = @"https://www.wanikani.com/api/v1.4/user/";

        /* Token claims */
        public const string CLAIM_USER_ID = "user_id";
        public const string CLAIM_API_KEY = "api_key";
        public const string CLAIM_APPLICATION = "application";
    }
}
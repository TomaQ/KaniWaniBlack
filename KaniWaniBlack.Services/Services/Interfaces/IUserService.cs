using KaniWaniBlack.Data.Models;
using KaniWaniBlack.Services.Models.Authentication;
using KaniWaniBlack.Services.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaniWaniBlack.Services.Services.Interfaces
{
    public interface IUserService
    {
        User GetUserById(int id);

        User GetUserByUserName(string username);

        AuthenticationResponse CreateUser(string username, string password, string passwordConfirmation, string applicationUsed);

        AuthenticationResponse ValidateUser(string username, string password, string applicationUsed);

        AuthenticationResponse ResetPassword(string username, string password, string newPassword, string applicationUsed);

        UserProfile GetUserProfile(int userId);
    }
}
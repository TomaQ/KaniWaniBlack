using KaniWaniBlack.Data.Model;
using KaniWaniBlack.Services.Models.Authentication;
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
        AuthenticationResponse CreateUser(string username, string password, string passwordConfirmation);
        AuthenticationResponse ValidateUser(string username, string password);
    }
}

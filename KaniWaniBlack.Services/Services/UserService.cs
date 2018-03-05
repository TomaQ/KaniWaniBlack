using KaniWaniBlack.Data.DAL.Interfaces;
using KaniWaniBlack.Data.Models;
using KaniWaniBlack.Services.Models;
using KaniWaniBlack.Services.Models.Authentication;
using KaniWaniBlack.Services.Services.Interfaces;
using System;
using KaniWaniBlack.Helper.Services;

namespace KaniWaniBlack.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepo;
        private readonly ICryptoService _cryptoService;

        public UserService(IGenericRepository<User> genericUserRepo, ICryptoService cryServ)
        {
            _userRepo = genericUserRepo;
            _cryptoService = cryServ;
        }

        public User GetUserById(int id)
        {
            return _userRepo.Get(x => x.Id == id);
        }

        public User GetUserByUserName(string username)
        {
            return _userRepo.Get(x => x.Username.Equals(username, StringComparison.CurrentCultureIgnoreCase));
        }

        public AuthenticationResponse CreateUser(string username, string password, string passwordConfirmation, string applicationUsed)
        {
            var response = new AuthenticationResponse(username);
            try
            {
                if (password == passwordConfirmation)
                {
                    if (_userRepo.Get(x => x.Username.Equals(username, StringComparison.CurrentCultureIgnoreCase)) == null)
                    {
                        string salt = _cryptoService.GenerateSaltForUser();
                        string hashedPassword = _cryptoService.GeneratePasswordHashForUser(salt, password);

                        User newUser = new User
                        {
                            Username = username,
                            PasswordSalt = salt,
                            PasswordHash = hashedPassword,
                            LastApplicationUsed = applicationUsed,
                            CreatedOn = DateTime.UtcNow
                        };
                        _userRepo.Add(newUser);

                        response.Message = Strings.USER_CREATED;
                        response.Code = CodeType.Ok;

                        return response;
                    }
                    else
                    {
                        response.Message = Strings.USER_ALREADY_EXISTS;
                        return response;
                    }
                }
                else
                {
                    response.Message = Strings.PASSWORDS_DONT_MATCH;
                    return response;
                }
            }
            catch (Exception ex) //TODO: sqlexception
            {
                //Logger.HandleException(ex);
                response.Code = CodeType.Error;
                return response;
            }
        }

        public AuthenticationResponse ValidateUser(string username, string password, string applicationUsed)
        {
            var response = new AuthenticationResponse(username);
            try
            {
                User user = _userRepo.Get(x => x.Username.Equals(username, StringComparison.CurrentCultureIgnoreCase));

                if (user != null)
                {
                    string hashedPassword = _cryptoService.GeneratePasswordHashForUser(user.PasswordSalt, password);
                    if (hashedPassword == user.PasswordHash)
                    {
                        response.UserName = user.Username;

                        response.Code = CodeType.Ok;
                        response.Message = Strings.USER_AUTH_SUCCESS;

                        UpdateUserOnLoginAttempt(user, applicationUsed);
                        return response;
                    }
                    else
                    {
                        UpdateUserOnLoginAttempt(user, applicationUsed, true);
                        response.Message = Strings.INVALID_USER_PASS;
                        return response;
                    }
                }
                else
                {
                    response.Message = Strings.INVALID_USER_PASS;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }

        private void UpdateUserOnLoginAttempt(User user, string applicationUsed, bool failedLogin = false)
        {
            try
            {
                user.LastApplicationUsed = applicationUsed;
                user.ModifiedOn = DateTime.UtcNow;

                if (failedLogin)
                {
                    user.LastLoginAttempt = DateTime.UtcNow;
                    user.LoginAttempts = user.LoginAttempts != null ? user.LoginAttempts += 1 : 1;
                }
                else
                {
                    user.LastLoginDate = DateTime.UtcNow;
                    user.LoginAttempts = 0;
                }

                _userRepo.Update(user);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
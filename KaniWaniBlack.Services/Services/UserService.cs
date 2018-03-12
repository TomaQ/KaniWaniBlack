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

        //Create a user account
        public AuthenticationResponse CreateUser(string username, string password, string passwordConfirmation, string applicationUsed)
        {
            var response = new AuthenticationResponse(username);
            try
            {
                if (password == passwordConfirmation) //TODO: refactor (look at resetPassword)
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
                    }
                    else
                    {
                        response.Message = Strings.USER_ALREADY_EXISTS;
                    }
                }
                else
                {
                    response.Message = Strings.PASSWORDS_DONT_MATCH;
                }
            }
            catch (Exception ex) //TODO: sqlexception
            {
                //Logger.HandleException(ex);
                response.Code = CodeType.Error;
            }

            return response;
        }

        //Login the user and update DB for login success/fail
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
                    }
                    else
                    {
                        UpdateUserOnLoginAttempt(user, applicationUsed, true);
                        response.Message = Strings.INVALID_USER_PASS;
                    }
                }
                else
                {
                    response.Message = Strings.INVALID_USER_PASS;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message; //TODO: not this
            }

            return response;
        }

        //If the user is validated, set their password to the new one given
        public AuthenticationResponse ResetPassword(string username, string password, string newPassword, string applicationUsed)
        {
            var response = new AuthenticationResponse(username);

            try
            {
                if (ValidateUser(username, password, applicationUsed).Code == CodeType.Ok) //TODO: use token?
                {
                    string salt = _cryptoService.GenerateSaltForUser();
                    string hashedPassword = _cryptoService.GeneratePasswordHashForUser(salt, newPassword);

                    User user = new User
                    {
                        PasswordSalt = salt,
                        PasswordHash = hashedPassword,
                        LastApplicationUsed = applicationUsed,
                        ModifiedOn = DateTime.UtcNow
                    };
                    _userRepo.Update(user);

                    response.Message = Strings.USER_RESET_PASSWORD;
                    response.Code = CodeType.Ok;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        //If the user login was successful or failed, update fields in the DB
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
                //TODO:
            }
        }
    }
}
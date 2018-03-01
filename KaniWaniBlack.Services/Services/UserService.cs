﻿using KaniWaniBlack.Data.DAL.Interfaces;
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

        public AuthenticationResponse CreateUser(string username, string password, string passwordConfirmation) //TODO: not like this
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
                            CreatedOn = DateTime.UtcNow
                        };
                        _userRepo.Add(newUser);

                        response.Message = "User " + username + " created.";
                        response.Code = CodeType.Ok;

                        return response;
                    }
                    else
                    {
                        response.Message = "A user with that username already exists."; //TODO: put string in class
                        return response;
                    }
                }
                else
                {
                    response.Message = "Passwords do not match.";
                    return response;
                }
            }
            catch (Exception ex) //TODO: sqlexception
            {
                return response;
            }
        }

        public AuthenticationResponse ValidateUser(string username, string password)
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
                        //response.ApiKey = user.UserVocab; //TODO: which way to reference?
                        response.UserName = user.Username;

                        response.Code = CodeType.Ok;
                        response.Message = Strings.USER_AUTH_SUCCESS;
                        response.Status = "Passed";

                        return response;
                    }
                    else
                    {
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
                response.Status = ex.Message;
                return response;
            }
        }
    }
}
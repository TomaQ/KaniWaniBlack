﻿using KaniWaniBlack.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KaniWaniBlack.Services.Services
{
    public class CryptoService : ICryptoService
    {
        private const int HASH_ITERATIONS = 10000;
        private RandomNumberGenerator _Random;
        private int SALT_LENGTH = 50;

        public CryptoService()
        {
            _Random = RandomNumberGenerator.Create();
        }

        //Create salt for a newly created user
        public string GenerateSaltForUser()
        {
            byte[] data = new byte[SALT_LENGTH];
            _Random.GetNonZeroBytes(data);
            return Convert.ToBase64String(data);
        }

        //Creates a password hash for the user account
        public string GeneratePasswordHashForUser(string salt, string password)
        {
            try
            {
                password = salt + password;
                byte[] hashBytes = HashPassword(password);

                return Convert.ToBase64String(hashBytes);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private byte[] HashPassword(string password)
        {
            byte[] passwordBytes = new UTF8Encoding().GetBytes(password);
            byte[] hashBytes;

            using (var sha = new SHA512Managed())
            {
                hashBytes = sha.ComputeHash(passwordBytes);
            }

            return hashBytes;
        }
    }
}
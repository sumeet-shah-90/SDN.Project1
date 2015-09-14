using Project1.Common;
using Project1.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Project1.API.Utility
{
    public static class UserManager
    {
        public static string GenerateSecurityHash(ApplicationUser userToSave, string password)
        {
            string initVector = password;

            int remainingDigits = 16 - password.Length;

            if (remainingDigits > 0)
            {
                for (int i = 1; i <= remainingDigits; i++)
                {
                    initVector += "*";
                }
            }

            return RJindael.Encrypt(userToSave.JoinTS.ToBinary().ToString(), userToSave.UserID.ToString(), userToSave.PhoneNumber, "SHA1", 29, initVector, 256);
        }

        public static string GeneratePassword(ApplicationUser userToSave, string password)
        {
            string initVector = password;

            int remainingDigits = 16 - password.Length;

            if (remainingDigits > 0)
            {
                for (int i = 1; i <= remainingDigits; i++)
                {
                    initVector += "*";
                }
            }

            return RJindael.Encrypt(userToSave.SecurityHash, userToSave.UserID.ToString(), userToSave.PhoneNumber, "SHA1", 37, initVector, 256);
        }

        public static string GenerateAccessToken(ApplicationUser userToLogin)
        {
            return RJindael.Encrypt(Json.Encode(userToLogin), userToLogin.Password, userToLogin.SecurityHash, "SHA1", 47, "****************", 256);
        }
    }
}
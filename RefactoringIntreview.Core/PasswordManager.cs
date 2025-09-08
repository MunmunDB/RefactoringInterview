
using System;
using System.Security.Cryptography;

namespace RefactoringInterview.Core.Application
{
    public class PasswordManager :  IPasswordManager
    {
          

        public bool Compare(string password, string confirmPassword, out string errorMessage)
        {
            if (password != confirmPassword)
            {
                errorMessage = "Passwords do not match.";
                return false;
            }
               errorMessage = string.Empty;

            return true;
        }


        public string Encrypt(string password)
        {
           return HashPassword(password);
        }     

       

        public bool Validate(string password, out string errorMessage)
        {
            if (password?.Length < 8)
            {
                errorMessage = "Password must be at least 8 characters long.";
                return false;
            }
            errorMessage = string.Empty;
            return !(password==null);
        }
        

        private string HashPassword(string password)
        {
            using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            var salt = new byte[16];
            rng.GetBytes(salt);
            using var pbkdf2 = new System.Security.Cryptography.Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA512);
            var key = pbkdf2.GetBytes(20);
            return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(key)}";
        }
    }
}

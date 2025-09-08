using System;
using RefactoringInterview.Core.Application;
using RefactoringInterview.Core.Domain;

namespace RefactoringInterview
{
    public class ConsoleClientApplication : IClientApplication
    {
        private readonly IPasswordManager _passwordManager;
        public ConsoleClientApplication(IPasswordManager passwordManager)
        {
            _passwordManager = passwordManager;
        }

        public User? GetUserInput()
        {
            Console.WriteLine("Enter a username: ");
            var username = Console.ReadLine();
            Console.WriteLine("Enter your full name: ");
            var fullName = Console.ReadLine();
            Console.WriteLine("Enter your password: ");
            var password = Console.ReadLine();
            Console.WriteLine("Re-enter your password: ");
            var confirmPassword = Console.ReadLine();

            var errorMessage = string.Empty;

            if (! Compare(password, confirmPassword, out errorMessage) || !Validate(password, out errorMessage))
                {
                Response(errorMessage);
                return default;
            }
           

            return new User
            {
                Username = username,
                FullName = fullName,
                EncryptedPassword = password == confirmPassword ? Encrypt(password) : null
            };
        }
        string Encrypt(string password)
        {
            return _passwordManager.Encrypt(password);
        }
        bool Compare(string password, string confirmPassword, out string errorMessage)
        {
            errorMessage = string.Empty;
            return _passwordManager.Compare(password, confirmPassword, out errorMessage);
        }

        bool Validate(string password, out string errorMessage)
        {
            errorMessage = string.Empty;
            return _passwordManager.Validate(password,out errorMessage);
        }

        public void Response(string message)
        {
            Console.WriteLine(message);
        }
    }
}

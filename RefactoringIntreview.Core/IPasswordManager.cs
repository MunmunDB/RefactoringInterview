namespace RefactoringInterview.Core.Application
{
    public interface IPasswordManager
    {
        bool Validate(string password, out string errorMessage);

        bool Compare(string password, string confirmPassword, out string errorMessage);

        string Encrypt(string password);


    }
}

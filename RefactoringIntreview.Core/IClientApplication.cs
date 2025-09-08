using RefactoringInterview.Core.Domain;

namespace RefactoringInterview.Core.Application
{
    public interface IClientApplication
    {
       User GetUserInput();
       void Response(string prompt);
    }
}

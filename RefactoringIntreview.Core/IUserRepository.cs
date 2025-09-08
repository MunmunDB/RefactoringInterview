namespace RefactoringInterview.Core.Domain
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User GetUser(string username);
        IEnumerable<User> GetAllUsers();
    }
}

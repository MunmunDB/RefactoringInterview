using RefactoringInterview.Core.Domain;

namespace RefactoringInterview.Core.Application
{
    public class SecurityManager : ISecurityManager
    {
        private readonly IClientApplication _io;
        private readonly IUserRepository _userRepository;

        public SecurityManager(IClientApplication io, IUserRepository userRepository)
        {
            _io = io;
            _userRepository = userRepository;
        }

        public void CreateUser()
        {
            User user = _io.GetUserInput();
            if (user is null)
            {
                _io.Response("User creation failed due to invalid input.");
            }
            else
            {
                _userRepository.AddUser(user);
                _io.Response($"User {user.Username} created successfully.");
            }
        }
    }
}


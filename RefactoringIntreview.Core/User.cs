namespace RefactoringInterview.Core.Domain
{
    public class User
    {
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string EncryptedPassword { get; set; } = string.Empty;
        public string ValidationMessage { get; set; } = string.Empty;
    }
}

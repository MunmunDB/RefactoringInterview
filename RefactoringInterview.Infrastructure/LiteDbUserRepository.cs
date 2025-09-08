using LiteDB;
using RefactoringInterview.Core.Domain;

namespace RefactoringInterview.Infrastructure
{
    public class LiteDbUserRepository : IUserRepository
    {
        private readonly LiteDatabase _db;
        private const string CollectionName = "users";

        public LiteDbUserRepository(LiteDatabase db)
        {
            _db = db;
        }

        public void AddUser(User user)
        {
            var col = _db.GetCollection<User>(CollectionName);
            col.Upsert(user);
        }

        public User GetUser(string username)
        {
            var col = _db.GetCollection<User>(CollectionName);
            return col.FindOne(u => u.Username == username);
        }

        public IEnumerable<User> GetAllUsers()
        {
            var col = _db.GetCollection<User>(CollectionName);
            return col.FindAll().ToList();
        }
    }
}

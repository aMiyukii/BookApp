using BookApp.Core.Interfaces;
using System.Threading.Tasks;

namespace BookApp.Unittest.Fake_Repositories
{
    public class FakeUserRepository : IUserRepository
    {
        private readonly bool _loginResult;
        private readonly int _userId;

        public FakeUserRepository(bool loginResult = true, int userId = 1)
        {
            _loginResult = loginResult;
            _userId = userId;
        }

        public Task<bool> LoginAsync(string emailAddress, string password)
        {
            return Task.FromResult(_loginResult);
        }

        public Task<int> GetUserIdByEmailAsync(string emailAddress)
        {
            return Task.FromResult(_userId);
        }
    }
}
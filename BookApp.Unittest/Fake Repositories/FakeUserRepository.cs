using BookApp.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Unittest.Fake_Repositories
{
    public class FakeUserRepository : IUserRepository
    {
        private readonly List<FakeUser> _fakeUsers;

        public FakeUserRepository()
        {
            _fakeUsers = new List<FakeUser>
            {
                new FakeUser { EmailAddress = "test1@example.com", Password = "password1", UserId = 1 },
                new FakeUser { EmailAddress = "test2@example.com", Password = "password2", UserId = 2 }
            };
        }

        public Task<bool> LoginAsync(string emailAddress, string password)
        {
            var user = _fakeUsers.FirstOrDefault(u => u.EmailAddress == emailAddress && u.Password == password);
            return Task.FromResult(user != null);
        }

        public Task<int> GetUserIdByEmailAsync(string emailAddress)
        {
            var user = _fakeUsers.FirstOrDefault(u => u.EmailAddress == emailAddress);
            return Task.FromResult(user?.UserId ?? 0);
        }

        public Task<bool> CreateUserAsync(string name, string emailAddress, string password)
        {
            return Task.FromResult(true);
        }

        public Task<bool> UserExistsAsync(string emailAddress)
        {
            var userExists = _fakeUsers.Any(u => u.EmailAddress == emailAddress);
            return Task.FromResult(userExists);
        }

        private class FakeUser
        {
            public string EmailAddress { get; set; }
            public string Password { get; set; }
            public int UserId { get; set; }
        }
    }
}
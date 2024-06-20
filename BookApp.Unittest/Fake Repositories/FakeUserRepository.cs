using BookApp.Core.Interfaces;

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

        private class FakeUser
        {
            public string EmailAddress { get; set; }
            public string Password { get; set; }
            public int UserId { get; set; }
        }
    }
}
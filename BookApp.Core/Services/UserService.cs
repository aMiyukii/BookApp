using BookApp.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace BookApp.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<bool> LoginAsync(string emailAddress, string password)
        {
            if (string.IsNullOrWhiteSpace(emailAddress)) throw new ArgumentNullException(nameof(emailAddress));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password));

            return await _userRepository.LoginAsync(emailAddress, password);
        }
    }
}
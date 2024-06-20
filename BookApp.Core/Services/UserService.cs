using BookApp.Core.Interfaces;

namespace BookApp.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> LoginAsync(string emailAddress, string password)
        {
            return await _userRepository.LoginAsync(emailAddress, password);
        }

        public async Task<int> GetUserIdAsync(string emailAddress)
        {
            return await _userRepository.GetUserIdByEmailAsync(emailAddress);
        }
    }

}
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
            _userRepository = userRepository;
        }

        public async Task<bool> LoginAsync(string emailAddress, string password)
        {
            return await _userRepository.LoginAsync(emailAddress, password);
        }

        public async Task<int> GetUserIdAsync(string emailAddress)
        {
            // Implement logic to retrieve user ID by email address from database
            // Example implementation assuming UserRepository has a method for this
            return await _userRepository.GetUserIdByEmailAsync(emailAddress);
        }
    }

}
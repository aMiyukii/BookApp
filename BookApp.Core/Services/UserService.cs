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

        public async Task<bool> LoginAsync(string emailaddress, string password)
        {
            if (string.IsNullOrWhiteSpace(emailaddress)) throw new ArgumentNullException(nameof(emailaddress));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password));

            return await _userRepository.LoginAsync(emailaddress, password);
        }

        public async Task<int> GetUserIdAsync(string emailaddress)
        {
            if (string.IsNullOrWhiteSpace(emailaddress)) throw new ArgumentNullException(nameof(emailaddress));

            return await _userRepository.GetUserIdByEmailAsync(emailaddress);
        }

        public async Task<bool> CreateUserAsync(string name, string emailAddress, string password)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(emailAddress) ||
                string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Name, email address, and password must be provided.");
            }

            var userExists = await _userRepository.UserExistsAsync(emailAddress);
            if (userExists)
            {
                throw new InvalidOperationException("User with this email address already exists.");
            }

            return await _userRepository.CreateUserAsync(name, emailAddress, password);
        }

        public async Task<bool> UserExistsAsync(string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress)) throw new ArgumentNullException(nameof(emailAddress));

            return await _userRepository.UserExistsAsync(emailAddress);
        }
    }
}
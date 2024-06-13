using BookApp.Core.DTO;
using BookApp.Core.Interfaces;

namespace BookApp.Core.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

    }
}
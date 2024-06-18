using BookApp.Core.DTO;
using BookApp.Core.Interfaces;
using System;
using System.Collections.Generic;
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
    }
}
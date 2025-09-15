using WebApplication1.Models;
using WebApplication1.Repositories;
using System.Collections.Generic;

namespace WebApplication1.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Register(User user)
        {
            // Check if user already exists
            var existingUser = _userRepository.GetUserByUsername(user.Username);
            if (existingUser != null)
            {
                return false; // Username already taken
            }

            _userRepository.AddUser(user);
            return true;
        }

        public User? Login(string username, string password)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user != null && user.Password == password)
            {
                return user;
            }

            return null; // Invalid login
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }
    }
}

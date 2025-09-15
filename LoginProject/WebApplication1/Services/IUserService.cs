using WebApplication1.Models;
using System.Collections.Generic;

namespace WebApplication1.Services
{
    public interface IUserService
    {
        bool Register(User user);
        User? Login(string username, string password);
        IEnumerable<User> GetAllUsers();
    }
}

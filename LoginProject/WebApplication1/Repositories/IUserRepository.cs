using WebApplication1.Models;
using System.Collections.Generic;

namespace WebApplication1.Repositories
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User? GetUserByUsername(string username);
        IEnumerable<User> GetAllUsers();
    }
}

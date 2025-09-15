using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.Models
{
    public class UserModel
    {
        private static List<UserModel> Users = new List<UserModel>();
        private static int _nextId = 1;

        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UserModel() { }

        public UserModel(string username, string email, string password)
        {
            Id = _nextId++;
            Username = username;
            Email = email;
            Password = password;
        }

        // Adds user to the list, returns null if username exists
        public static UserModel AddUser(string username, string email, string password)
        {
            if (Users.Any(u => u.Username == username))
                return null;

            if (Users.Any(u => u.Email == email))
                return null;

                var user = new UserModel(username, email, password);
            Users.Add(user);
            return user;
        }

        // Validates credentials
        public static UserModel ValidateUser(string username, string password)
        {
            return Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}

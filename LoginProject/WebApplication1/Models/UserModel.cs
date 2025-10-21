using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BCrypt.Net; // Ensure this package is installed

namespace WebApplication1.Models
{
    public class UserModel
    {
        private static List<UserModel> Users = LoadUsers();
        private static int _nextId = 1;
        private static readonly string FilePath = @"C:\Users\camil\Desktop\ProyectoLogin\LoginProject\WebApplication1\Models\users.txt";

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UserModel() { }

        public UserModel(string nombre, string username, string email, string password)
        {
            Id = _nextId++;
            Nombre = nombre;
            Username = username;
            Email = email;
            Password = BCrypt.Net.BCrypt.HashPassword(password); // Hash the password
        }

        private static List<UserModel> LoadUsers()
        {
            var users = new List<UserModel>();
            if (File.Exists(FilePath))
            {
                var lines = File.ReadAllLines(FilePath);
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line) || !line.Contains(",")) continue;
                    var parts = line.Split(new[] { ',' }, 5); // Split into 5 parts: Id, Nombre, Username, Email, Password
                    if (parts.Length == 5)
                    {
                        if (int.TryParse(parts[0].Trim(), out int id))
                        {
                            var user = new UserModel
                            {
                                Id = id,
                                Nombre = parts[1].Trim(),
                                Username = parts[2].Trim(),
                                Email = parts[3].Trim(),
                                Password = parts[4].Trim().TrimEnd(';') // Store hashed password
                            };
                            users.Add(user);
                            _nextId = Math.Max(_nextId, id + 1); // Update _nextId
                        }
                    }
                }
            }
            return users;
        }

        private static void SaveUsers()
        {
            var lines = Users.Select(u => $"{u.Id}, {u.Nombre}, {u.Username}, {u.Email}, {u.Password};");
            File.WriteAllLines(FilePath, lines);
        }

        public static UserModel AddUser(string nombre, string username, string email, string password)
        {
            if (Users.Any(u => u.Username == username || u.Email == email))
                return null;

            var user = new UserModel(nombre, username, email, password);
            Users.Add(user);
            SaveUsers();
            return user;
        }

        public static UserModel ValidateUser(string usernameormail, string password)
        {
            var user = Users.FirstOrDefault(u => (u.Username == usernameormail || u.Email == usernameormail));
            return user != null && BCrypt.Net.BCrypt.Verify(password, user.Password) ? user : null; // Verify hashed password
        }
    }
}
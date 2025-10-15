using System;
using System.Collections.Generic;
using System.Linq;
using PharmacyManagementSystem.Data;
using PharmacyManagementSystem.Data.Models;

namespace PharmacyManagementSystem.Services
{
    public class UserService : IUserService
    {
        private readonly DatabaseContext _context;

        public UserService()
        {
            _context = new DatabaseContext();
        }

        public User Authenticate(string username, string password)
        {
            var users = _context.GetUsers();
            var user = users.FirstOrDefault(u => 
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && 
                u.Password == password && 
                u.IsActive);

            if (user != null)
            {
                user.LastLogin = DateTime.Now;
                UpdateUser(user);
            }

            return user;
        }

        public User GetUserById(int id)
        {
            var users = _context.GetUsers();
            return users.FirstOrDefault(u => u.Id == id && u.IsActive);
        }

        public List<User> GetAllUsers()
        {
            return _context.GetUsers().Where(u => u.IsActive).ToList();
        }

        public void AddUser(User user)
        {
            var users = _context.GetUsers();
            user.Id = users.Count > 0 ? users.Max(u => u.Id) + 1 : 1;
            users.Add(user);
            _context.SaveUsers(users);
        }

        public void UpdateUser(User user)
        {
            var users = _context.GetUsers();
            var existing = users.FirstOrDefault(u => u.Id == user.Id);
            if (existing != null)
            {
                var index = users.IndexOf(existing);
                users[index] = user;
                _context.SaveUsers(users);
            }
        }

        public void DeleteUser(int id)
        {
            var users = _context.GetUsers();
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.IsActive = false;
                _context.SaveUsers(users);
            }
        }

        public bool UserExists(string username)
        {
            var users = _context.GetUsers();
            return users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && u.IsActive);
        }

        public List<User> GetUsersByRole(string role)
        {
            var users = _context.GetUsers();
            return users.Where(u => u.Role == role && u.IsActive).ToList();
        }
    }
}
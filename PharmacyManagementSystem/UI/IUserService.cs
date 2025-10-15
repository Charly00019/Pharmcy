using System.Collections.Generic;
using PharmacyManagementSystem.Data.Models;

namespace PharmacyManagementSystem.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        User GetUserById(int id);
        List<User> GetAllUsers();
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
        bool UserExists(string username);
        List<User> GetUsersByRole(string role);
    }
}
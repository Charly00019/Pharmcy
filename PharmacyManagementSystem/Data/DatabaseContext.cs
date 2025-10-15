using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using PharmacyManagementSystem.Data.Models;

namespace PharmacyManagementSystem.Data
{
    public class DatabaseContext
    {
        private readonly string _dataFolder;
        private readonly string _productsFile;
        private readonly string _salesFile;
        
        private readonly string _usersFile;
        public DatabaseContext()
        {
            _dataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PharmacyManagementSystem");
            _productsFile = Path.Combine(_dataFolder, "products.json");
            _salesFile = Path.Combine(_dataFolder, "sales.json");
            _usersFile = Path.Combine(_dataFolder, "users.json");

            Directory.CreateDirectory(_dataFolder);

            if (!File.Exists(_productsFile))
                File.WriteAllText(_productsFile, "[]");
            if (!File.Exists(_salesFile))
                File.WriteAllText(_salesFile, "[]");
            if (!File.Exists(_usersFile))
            {   
                File.WriteAllText(_usersFile, "[]");
                CreateDefaultUsers();
            }
        }

        public List<Product> GetProducts()
        {
            try
            {
                var json = File.ReadAllText(_productsFile);
                return JsonConvert.DeserializeObject<List<Product>>(json) ?? new List<Product>();
            }
            catch
            {
                return new List<Product>();
            }
        }

        public void SaveProducts(List<Product> products)
        {
            var json = JsonConvert.SerializeObject(products, Formatting.Indented);
            File.WriteAllText(_productsFile, json);
        }

        // Add these methods
public List<User> GetUsers()
{
    try
    {
        var json = File.ReadAllText(_usersFile);
        return JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
    }
    catch
    {
        return new List<User>();
    }
}

public void SaveUsers(List<User> users)
{
    var json = JsonConvert.SerializeObject(users, Formatting.Indented);
    File.WriteAllText(_usersFile, json);
}

private void CreateDefaultUsers()
{
    var defaultUsers = new List<User>
    {
        new User 
        { 
            Id = 1, 
            Username = "admin", 
            Password = "admin123", 
            Role = UserRoles.Admin,
            FullName = "System Administrator",
            Email = "admin@pharmacy.com",
            Phone = "0241234567"
        },
        new User 
        { 
            Id = 2, 
            Username = "sales1", 
            Password = "sales123", 
            Role = UserRoles.Sales,
            FullName = "Sales Person One",
            Email = "sales1@pharmacy.com",
            Phone = "0241234568"
        }
    };
    SaveUsers(defaultUsers);
}

        public List<Sale> GetSales()
        {
            try
            {
                var json = File.ReadAllText(_salesFile);
                return JsonConvert.DeserializeObject<List<Sale>>(json) ?? new List<Sale>();
            }
            catch
            {
                return new List<Sale>();
            }
        }

        public void SaveSales(List<Sale> sales)
        {
            var json = JsonConvert.SerializeObject(sales, Formatting.Indented);
            File.WriteAllText(_salesFile, json);
        }

        public List<Product> GetExpiredProducts()
        {
            var products = GetProducts();
            return products.Where(p => p.IsExpired && p.IsActive).ToList();
        }

        public List<Product> GetExpiringSoonProducts()
        {
            var products = GetProducts();
            return products.Where(p => p.IsExpiringSoon && p.IsActive).ToList();
        }

        public List<Product> GetLowStockProducts()
        {
            var products = GetProducts();
            return products.Where(p => p.IsLowStock && p.IsActive).ToList();
        }
    }
}
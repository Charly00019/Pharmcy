using System.Collections.Generic;
using PharmacyManagementSystem.Data.Models;

namespace PharmacyManagementSystem.Services
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        List<Product> SearchProducts(string searchTerm);
        List<Product> GetExpiredProducts();
        List<Product> GetExpiringSoonProducts();
        List<Product> GetLowStockProducts();
    }
}
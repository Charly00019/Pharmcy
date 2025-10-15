using System.Collections.Generic;
using System.Linq;
using PharmacyManagementSystem.Data;
using PharmacyManagementSystem.Data.Models;

namespace PharmacyManagementSystem.Services
{
    public class ProductService : IProductService
    {
        private readonly DatabaseContext _context;

        public ProductService()
        {
            _context = new DatabaseContext();
        }

        public List<Product> GetAllProducts()
        {
            return _context.GetProducts().Where(p => p.IsActive).ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.GetProducts().FirstOrDefault(p => p.Id == id && p.IsActive);
        }

        public void AddProduct(Product product)
        {
            var products = _context.GetProducts();
            product.Id = products.Count > 0 ? products.Max(p => p.Id) + 1 : 1;
            
            // Ensure at least one batch exists
            if (!product.Batches.Any())
            {
                product.Batches.Add(new ProductBatch
                {
                    BatchNumber = "DEFAULT001",
                    Quantity = 0,
                    ExpiryDate = System.DateTime.Now.AddYears(2)
                });
            }
            
            products.Add(product);
            _context.SaveProducts(products);
        }

        public void UpdateProduct(Product product)
        {
            var products = _context.GetProducts();
            var existing = products.FirstOrDefault(p => p.Id == product.Id);
            if (existing != null)
            {
                var index = products.IndexOf(existing);
                products[index] = product;
                _context.SaveProducts(products);
            }
        }

        public void DeleteProduct(int id)
        {
            var products = _context.GetProducts();
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                product.IsActive = false;
                _context.SaveProducts(products);
            }
        }

        public List<Product> SearchProducts(string searchTerm)
        {
            var products = GetAllProducts();
            return products.Where(p =>
                p.Name.ToLower().Contains(searchTerm.ToLower()) ||
                p.GenericName.ToLower().Contains(searchTerm.ToLower()) ||
                p.Batches.Any(b => b.BatchNumber.ToLower().Contains(searchTerm.ToLower()))
            ).ToList();
        }

        public List<Product> GetExpiredProducts()
        {
            return _context.GetExpiredProducts();
        }

        public List<Product> GetExpiringSoonProducts()
        {
            return _context.GetExpiringSoonProducts();
        }

        public List<Product> GetLowStockProducts()
        {
            return _context.GetLowStockProducts();
        }
    }
}
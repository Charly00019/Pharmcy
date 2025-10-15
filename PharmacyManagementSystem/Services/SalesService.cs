using System;
using System.Collections.Generic;
using System.Linq;
using PharmacyManagementSystem.Data;
using PharmacyManagementSystem.Data.Models;

namespace PharmacyManagementSystem.Services
{
    public class SalesService : ISalesService
    {
        private readonly DatabaseContext _context;
        private readonly IProductService _productService;

        public SalesService()
        {
            _context = new DatabaseContext();
            _productService = new ProductService();
        }

        public void ProcessSale(Sale sale)
        {
            // Update inventory quantities using FIFO (First-In-First-Out) for batches
            foreach (var item in sale.Items)
            {
                var product = _productService.GetProductById(item.ProductId);
                if (product != null)
                {
                    DeductFromBatches(product, item.Quantity);
                    _productService.UpdateProduct(product);
                }
            }

            // Save the sale
            var sales = _context.GetSales();
            sale.Id = sales.Count > 0 ? sales.Max(s => s.Id) + 1 : 1;
            sale.SaleDate = DateTime.Now;
            sales.Add(sale);
            _context.SaveSales(sales);
        }

        private void DeductFromBatches(Product product, int quantityToDeduct)
        {
            // Get non-expired batches ordered by expiry date (FIFO)
            var validBatches = product.Batches
                .Where(b => b.IsActive && !b.IsExpired)
                .OrderBy(b => b.ExpiryDate)
                .ToList();

            int remainingQuantity = quantityToDeduct;

            foreach (var batch in validBatches)
            {
                if (remainingQuantity <= 0) break;

                if (batch.Quantity >= remainingQuantity)
                {
                    // This batch has enough quantity
                    batch.Quantity -= remainingQuantity;
                    remainingQuantity = 0;
                }
                else
                {
                    // Take all from this batch and move to next
                    remainingQuantity -= batch.Quantity;
                    batch.Quantity = 0;
                }

                // Deactivate batch if empty
                if (batch.Quantity == 0)
                {
                    batch.IsActive = false;
                }
            }

            if (remainingQuantity > 0)
            {
                throw new InvalidOperationException($"Insufficient stock for {product.Name}. Only {quantityToDeduct - remainingQuantity} units available.");
            }
        }

        public List<Sale> GetSales()
        {
            return _context.GetSales();
        }

        public List<Sale> GetSalesByDateRange(DateTime startDate, DateTime endDate)
        {
            var sales = _context.GetSales();
            return sales.Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate).ToList();
        }

        public decimal GetTotalRevenue()
        {
            var sales = _context.GetSales();
            return sales.Sum(s => s.TotalAmount);
        }

        public decimal GetDailyRevenue(DateTime date)
        {
            var sales = _context.GetSales();
            return sales.Where(s => s.SaleDate.Date == date.Date)
                       .Sum(s => s.TotalAmount);
        }

        public List<Sale> GetSalesByPaymentMethod(string paymentMethod)
        {
            var sales = _context.GetSales();
            return sales.Where(s => s.PaymentMethod.Equals(paymentMethod, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}
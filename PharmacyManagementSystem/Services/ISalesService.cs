using System;
using System.Collections.Generic;
using PharmacyManagementSystem.Data.Models;

namespace PharmacyManagementSystem.Services
{
    public interface ISalesService
    {
        void ProcessSale(Sale sale);
        List<Sale> GetSales();
        List<Sale> GetSalesByDateRange(DateTime startDate, DateTime endDate);
        decimal GetTotalRevenue();
        decimal GetDailyRevenue(DateTime date);
        List<Sale> GetSalesByPaymentMethod(string paymentMethod);
    }
}
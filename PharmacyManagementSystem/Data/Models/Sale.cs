using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PharmacyManagementSystem.Data.Models
{
    public class Sale : INotifyPropertyChanged
    {
        private int _id;
        private DateTime _saleDate;
        private decimal _totalAmount;
        private string _paymentMethod;
        private string _momoTransactionId;
        private List<SaleItem> _items;

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }

        public DateTime SaleDate
        {
            get => _saleDate;
            set { _saleDate = value; OnPropertyChanged(nameof(SaleDate)); }
        }

        public decimal TotalAmount
        {
            get => _totalAmount;
            set { _totalAmount = value; OnPropertyChanged(nameof(TotalAmount)); }
        }

        public string PaymentMethod
        {
            get => _paymentMethod;
            set { _paymentMethod = value; OnPropertyChanged(nameof(PaymentMethod)); }
        }

        public string MomoTransactionId
        {
            get => _momoTransactionId;
            set { _momoTransactionId = value; OnPropertyChanged(nameof(MomoTransactionId)); }
        }

        public List<SaleItem> Items
        {
            get => _items ??= new List<SaleItem>();
            set { _items = value; OnPropertyChanged(nameof(Items)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class SaleItem : INotifyPropertyChanged
    {
        private int _productId;
        private string _productName;
        private int _quantity;
        private decimal _unitPrice;
        private decimal _totalPrice;

        public int ProductId
        {
            get => _productId;
            set { _productId = value; OnPropertyChanged(nameof(ProductId)); }
        }

        public string ProductName
        {
            get => _productName;
            set { _productName = value; OnPropertyChanged(nameof(ProductName)); }
        }

        public int Quantity
        {
            get => _quantity;
            set { _quantity = value; OnPropertyChanged(nameof(Quantity)); CalculateTotal(); }
        }

        public decimal UnitPrice
        {
            get => _unitPrice;
            set { _unitPrice = value; OnPropertyChanged(nameof(UnitPrice)); CalculateTotal(); }
        }

        public decimal TotalPrice
        {
            get => _totalPrice;
            set { _totalPrice = value; OnPropertyChanged(nameof(TotalPrice)); }
        }

        private void CalculateTotal()
        {
            TotalPrice = Quantity * UnitPrice;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
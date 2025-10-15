using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PharmacyManagementSystem.Data.Models
{
    public class Product : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private string _genericName;
        private string _category;
        private decimal _costPrice;
        private decimal _sellingPrice;
        private string _supplier;
        private int _minStockLevel;
        private bool _isActive;
        private List<ProductBatch> _batches;

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string GenericName
        {
            get => _genericName;
            set { _genericName = value; OnPropertyChanged(nameof(GenericName)); }
        }

        public string Category
        {
            get => _category;
            set { _category = value; OnPropertyChanged(nameof(Category)); }
        }

        public decimal CostPrice
        {
            get => _costPrice;
            set { _costPrice = value; OnPropertyChanged(nameof(CostPrice)); }
        }

        public decimal SellingPrice
        {
            get => _sellingPrice;
            set { _sellingPrice = value; OnPropertyChanged(nameof(SellingPrice)); }
        }

        public string Supplier
        {
            get => _supplier;
            set { _supplier = value; OnPropertyChanged(nameof(Supplier)); }
        }

        public int MinStockLevel
        {
            get => _minStockLevel;
            set { _minStockLevel = value; OnPropertyChanged(nameof(MinStockLevel)); }
        }

        public bool IsActive
        {
            get => _isActive;
            set { _isActive = value; OnPropertyChanged(nameof(IsActive)); }
        }

        public List<ProductBatch> Batches
        {
            get => _batches ??= new List<ProductBatch>();
            set { _batches = value; OnPropertyChanged(nameof(Batches)); }
        }

        // Legacy compatibility — keep old names that UI might reference
        public decimal Price
        {
            get => SellingPrice;
            set { SellingPrice = value; OnPropertyChanged(nameof(Price)); }
        }

        public int Quantity
        {
            get => TotalQuantity;
            set
            {
                // Adjust total by modifying active batch
                if (Batches.Any())
                    Batches.First().Quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public DateTime ExpiryDate
        {
            get => NextExpiryDate ?? DateTime.Now.AddYears(1);
            set
            {
                if (Batches.Any())
                    Batches.First().ExpiryDate = value;
                OnPropertyChanged(nameof(ExpiryDate));
            }
        }

        // Derived properties
        public int TotalQuantity => Batches.Where(b => b.IsActive).Sum(b => b.Quantity);
        public decimal TotalValue => Batches.Where(b => b.IsActive).Sum(b => b.Quantity * CostPrice);
        public bool IsLowStock => TotalQuantity <= MinStockLevel;
        public bool IsExpired => Batches.Any(b => b.IsActive && b.IsExpired);
        public bool IsExpiringSoon => Batches.Any(b => b.IsActive && b.IsExpiringSoon);
        public DateTime? NextExpiryDate => Batches.Where(b => b.IsActive && !b.IsExpired)
                                                .OrderBy(b => b.ExpiryDate)
                                                .FirstOrDefault()?.ExpiryDate;

        // Methods
        public ProductBatch GetBatch(string batchNumber)
        {
            return Batches.FirstOrDefault(b => b.BatchNumber.Equals(batchNumber, StringComparison.OrdinalIgnoreCase));
        }

        public void AddBatch(ProductBatch batch)
        {
            var existing = GetBatch(batch.BatchNumber);
            if (existing != null)
            {
                existing.Quantity += batch.Quantity;
                existing.ExpiryDate = batch.ExpiryDate;
            }
            else
            {
                Batches.Add(batch);
            }

            OnPropertyChanged(nameof(TotalQuantity));
            OnPropertyChanged(nameof(TotalValue));
        }

        // Event
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Product()
        {
            _isActive = true;
            _minStockLevel = 10;
            _batches = new List<ProductBatch>();
            _category = "General";
        }
    }

    public class ProductBatch : INotifyPropertyChanged
    {
        private string _batchNumber;
        private int _quantity;
        private DateTime _expiryDate;
        private bool _isActive;

        public string BatchNumber
        {
            get => _batchNumber;
            set { _batchNumber = value; OnPropertyChanged(nameof(BatchNumber)); }
        }

        public int Quantity
        {
            get => _quantity;
            set { _quantity = value; OnPropertyChanged(nameof(Quantity)); }
        }

        public DateTime ExpiryDate
        {
            get => _expiryDate;
            set { _expiryDate = value; OnPropertyChanged(nameof(ExpiryDate)); }
        }

        public bool IsActive
        {
            get => _isActive;
            set { _isActive = value; OnPropertyChanged(nameof(IsActive)); }
        }

        // Computed states
        public bool IsExpired => ExpiryDate <= DateTime.Now;
        public bool IsExpiringSoon => ExpiryDate <= DateTime.Now.AddMonths(3) && ExpiryDate > DateTime.Now;
        public int DaysUntilExpiry => (ExpiryDate - DateTime.Now).Days;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ProductBatch()
        {
            _isActive = true;
            _expiryDate = DateTime.Now.AddYears(2);
        }

        public ProductBatch(string batchNumber, int quantity, DateTime expiryDate)
        {
            _batchNumber = batchNumber;
            _quantity = quantity;
            _expiryDate = expiryDate;
            _isActive = true;
        }
    }
}

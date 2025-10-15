using System;
using System.Linq;
using System.Windows.Forms;
using PharmacyManagementSystem.Data.Models;
using PharmacyManagementSystem.Services;

namespace PharmacyManagementSystem.UI.Forms
{
    public partial class ProductsForm : Form
    {
        private readonly IProductService _productService;

        public ProductsForm()
        {
            _productService = new ProductService();
            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            try
            {
                var products = _productService.GetAllProducts();

                // Project product view using Batches to compute quantity and next expiry
                var view = products.Select(p => new
                {
                    p.Id,
                    p.Name,
                    GenericName = (p.GenericName ?? string.Empty),
                    SellingPrice = (p.SellingPrice),         // expecting SellingPrice or CostPrice on model
                    TotalQuantity = (p.Batches != null) ? p.Batches.Sum(b => b.Quantity) : 0,
                    NextExpiry = (p.Batches != null && p.Batches.Any()) ? p.Batches.Min(b => b.ExpiryDate) : (DateTime?)null
                }).ToList();

                dataGridProducts.DataSource = view;
                UpdateCounters();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load products: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateCounters()
        {
            try
            {
                lblLowStockCount.Text = $"{_productService.GetLowStockProducts().Count} Low Stock";
                lblExpiringCount.Text = $"{_productService.GetExpiringSoonProducts().Count} Expiring Soon";
                lblExpiredCount.Text = $"{_productService.GetExpiredProducts().Count} Expired";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating counters: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var query = txtSearch.Text.Trim();
                var products = string.IsNullOrEmpty(query)
                    ? _productService.GetAllProducts()
                    : _productService.SearchProducts(query);

                var view = products.Select(p => new
                {
                    p.Id,
                    p.Name,
                    GenericName = (p.GenericName ?? string.Empty),
                    SellingPrice = (p.SellingPrice),
                    TotalQuantity = (p.Batches != null) ? p.Batches.Sum(b => b.Quantity) : 0,
                    NextExpiry = (p.Batches != null && p.Batches.Any()) ? p.Batches.Min(b => b.ExpiryDate) : (DateTime?)null
                }).ToList();

                dataGridProducts.DataSource = view;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Search failed: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblExpiredCount_Click(object sender, EventArgs e)
        {
            try
            {
                var products = _productService.GetExpiredProducts();
                var view = products.Select(p => new
                {
                    p.Id,
                    p.Name,
                    GenericName = (p.GenericName ?? string.Empty),
                    SellingPrice = (p.SellingPrice),
                    TotalQuantity = (p.Batches != null) ? p.Batches.Sum(b => b.Quantity) : 0,
                    NextExpiry = (p.Batches != null && p.Batches.Any()) ? p.Batches.Min(b => b.ExpiryDate) : (DateTime?)null
                }).ToList();
                dataGridProducts.DataSource = view;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading expired products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblLowStockCount_Click(object sender, EventArgs e)
        {
            try
            {
                var products = _productService.GetLowStockProducts();
                var view = products.Select(p => new
                {
                    p.Id,
                    p.Name,
                    GenericName = (p.GenericName ?? string.Empty),
                    SellingPrice = (p.SellingPrice),
                    TotalQuantity = (p.Batches != null) ? p.Batches.Sum(b => b.Quantity) : 0,
                    NextExpiry = (p.Batches != null && p.Batches.Any()) ? p.Batches.Min(b => b.ExpiryDate) : (DateTime?)null
                }).ToList();
                dataGridProducts.DataSource = view;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading low stock products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblExpiringCount_Click(object sender, EventArgs e)
        {
            try
            {
                var products = _productService.GetExpiringSoonProducts();
                var view = products.Select(p => new
                {
                    p.Id,
                    p.Name,
                    GenericName = (p.GenericName ?? string.Empty),
                    SellingPrice = (p.SellingPrice),
                    TotalQuantity = (p.Batches != null) ? p.Batches.Sum(b => b.Quantity) : 0,
                    NextExpiry = (p.Batches != null && p.Batches.Any()) ? p.Batches.Min(b => b.ExpiryDate) : (DateTime?)null
                }).ToList();
                dataGridProducts.DataSource = view;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading expiring products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e) => LoadProducts();

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                using var form = new ProductEditorForm();
                if (form.ShowDialog(this) == DialogResult.OK) LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            if (dataGridProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to edit.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var idObj = dataGridProducts.SelectedRows[0].Cells["Id"].Value;
                if (idObj == null) return;
                var id = Convert.ToInt32(idObj);
                var product = _productService.GetProductById(id);
                using var form = new ProductEditorForm(product);
                if (form.ShowDialog(this) == DialogResult.OK) LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (dataGridProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var idObj = dataGridProducts.SelectedRows[0].Cells["Id"].Value;
                if (idObj == null) return;
                var id = Convert.ToInt32(idObj);

                var confirm = MessageBox.Show("Are you sure you want to delete this product?",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    _productService.DeleteProduct(id);
                    LoadProducts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

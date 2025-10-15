using System;
using System.Linq;
using System.Windows.Forms;
using PharmacyManagementSystem.Data.Models;
using PharmacyManagementSystem.Services;

namespace PharmacyManagementSystem.UI.Forms
{
    public partial class ProductEditorForm : Form
    {
        private readonly IProductService _productService;
        private readonly Product _product;
        private readonly bool _isEditMode;

        public ProductEditorForm(Product product = null)
        {
            InitializeComponent();
            _productService = new ProductService();
            _product = product ?? new Product();
            _isEditMode = product != null;

            // populate UI fields if editing
            if (_isEditMode)
            {
                txtName.Text = _product.Name;
                txtGenericName.Text = _product.GenericName;
                numCostPrice.Value = Convert.ToDecimal(_product.CostPrice);
                numSellingPrice.Value = Convert.ToDecimal(_product.SellingPrice);
                txtSupplier.Text = _product.Supplier ?? string.Empty;
                numMinStockLevel.Value = _product.MinStockLevel;

                // load first batch into batch controls if exists
                var firstBatch = _product.Batches?.FirstOrDefault();
                if (firstBatch != null)
                {
                    txtBatchNumber.Text = firstBatch.BatchNumber;
                    numQuantity.Value = firstBatch.Quantity;
                    dtpExpiryDate.Value = firstBatch.ExpiryDate;
                }
                else
                {
                    txtBatchNumber.Text = $"BATCH{DateTime.Now:yyyyMMddHHmmss}";
                    numQuantity.Value = 0;
                    dtpExpiryDate.Value = DateTime.Now.AddYears(2);
                }
            }
            else
            {
                txtBatchNumber.Text = $"BATCH{DateTime.Now:yyyyMMddHHmmss}";
                dtpExpiryDate.Value = DateTime.Now.AddYears(2);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Product name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            if (numSellingPrice.Value <= 0)
            {
                MessageBox.Show("Selling price must be greater than zero.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numSellingPrice.Focus();
                return;
            }

            try
            {
                _product.Name = txtName.Text.Trim();
                _product.GenericName = txtGenericName.Text.Trim();
                _product.CostPrice = numCostPrice.Value;
                _product.SellingPrice = numSellingPrice.Value;
                _product.Supplier = txtSupplier.Text.Trim();
                _product.MinStockLevel = Convert.ToInt32(numMinStockLevel.Value);
                _product.IsActive = true;

                // create or update the first batch as the "current batch"
                var batch = new ProductBatch
                {
                    BatchNumber = txtBatchNumber.Text.Trim(),
                    Quantity = Convert.ToInt32(numQuantity.Value),
                    ExpiryDate = dtpExpiryDate.Value
                };

                if (_product.Batches == null) _product.Batches = new System.Collections.Generic.List<ProductBatch>();

                if (_isEditMode && _product.Batches.Any())
                {
                    var first = _product.Batches.First();
                    first.BatchNumber = batch.BatchNumber;
                    first.Quantity = batch.Quantity;
                    first.ExpiryDate = batch.ExpiryDate;
                }
                else
                {
                    _product.Batches.Add(batch);
                }

                if (_isEditMode)
                    _productService.UpdateProduct(_product);
                else
                    _productService.AddProduct(_product);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

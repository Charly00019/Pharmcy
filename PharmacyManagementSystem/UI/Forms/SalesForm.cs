using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PharmacyManagementSystem.Data.Models;
using PharmacyManagementSystem.Services;

namespace PharmacyManagementSystem.UI.Forms
{
    public partial class SalesForm : Form
    {
        private readonly IProductService _productService;
        private readonly ISalesService _salesService;
        private List<SaleItem> _cartItems;
        private decimal _totalAmount;
        
        // Color scheme
        private readonly Color _primaryColor = Color.FromArgb(206, 17, 38);
        private readonly Color _secondaryColor = Color.FromArgb(0, 107, 63);
        private readonly Color _accentColor = Color.FromArgb(252, 209, 22);
        private readonly Color _backgroundColor = Color.FromArgb(250, 250, 250);

        public SalesForm()
        {
            InitializeComponent();
            _productService = new ProductService();
            _salesService = new SalesService();
            _cartItems = new List<SaleItem>();
            
            ApplyTheme();
            LoadProducts();
            UpdateCartDisplay();
        }

        private void ApplyTheme()
        {
            this.BackColor = _backgroundColor;
            this.ForeColor = Color.FromArgb(51, 51, 51);
            this.Font = new Font("Segoe UI", 9, FontStyle.Regular);

            // Header styling
            lblSalesTitle.ForeColor = _primaryColor;

            // Button styling
            btnProcessSale.BackColor = _secondaryColor;
            btnProcessSale.ForeColor = Color.White;
            btnProcessSale.FlatStyle = FlatStyle.Flat;
            btnProcessSale.FlatAppearance.BorderSize = 0;

            btnAddToCart.BackColor = _primaryColor;
            btnAddToCart.ForeColor = Color.White;
            btnAddToCart.FlatStyle = FlatStyle.Flat;
            btnAddToCart.FlatAppearance.BorderSize = 0;

            btnClearCart.BackColor = Color.FromArgb(149, 165, 166);
            btnClearCart.ForeColor = Color.White;
            btnClearCart.FlatStyle = FlatStyle.Flat;
            btnClearCart.FlatAppearance.BorderSize = 0;

            // Panel styling
            panelCart.BackColor = Color.White;
            panelProducts.BackColor = Color.White;
            panelPayment.BackColor = Color.White;
        }

        private void LoadProducts()
        {
            var products = _productService.GetAllProducts();
            dataGridProducts.DataSource = products;
            FormatProductsGrid();
        }

        private void FormatProductsGrid()
        {
            dataGridProducts.AutoGenerateColumns = false;
            dataGridProducts.Columns.Clear();

            dataGridProducts.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                DataPropertyName = "Name", 
                HeaderText = "Product Name", 
                Width = 150 
            });
            dataGridProducts.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                DataPropertyName = "GenericName", 
                HeaderText = "Generic Name", 
                Width = 120 
            });
            dataGridProducts.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                DataPropertyName = "TotalQuantity",  // CHANGED: Quantity -> TotalQuantity
                HeaderText = "In Stock", 
                Width = 80 
            });
            dataGridProducts.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                DataPropertyName = "SellingPrice", 
                HeaderText = "Price (GHS)", 
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "F2" }
            });
            dataGridProducts.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                DataPropertyName = "NextExpiryDate",  // CHANGED: ExpiryDate -> NextExpiryDate
                HeaderText = "Next Expiry", 
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd" }
            });

            // Add row styling for low stock and expired items
            dataGridProducts.RowPrePaint += (s, e) =>
            {
                if (e.RowIndex >= 0 && e.RowIndex < dataGridProducts.Rows.Count)
                {
                    var product = dataGridProducts.Rows[e.RowIndex].DataBoundItem as Product;
                    if (product != null)
                    {
                        if (product.IsExpired)
                        {
                            dataGridProducts.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightPink;
                        }
                        else if (product.IsExpiringSoon)
                        {
                            dataGridProducts.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightYellow;
                        }
                        else if (product.IsLowStock)
                        {
                            dataGridProducts.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCyan;
                        }
                    }
                }
            };
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (dataGridProducts.CurrentRow?.DataBoundItem is Product selectedProduct)
            {
                // Check if product is expired
                if (selectedProduct.IsExpired)
                {
                    MessageBox.Show($"Cannot add expired product: {selectedProduct.Name}", "Expired Product", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check if product is in stock - CHANGED: Quantity -> TotalQuantity
                if (selectedProduct.TotalQuantity <= 0)
                {
                    MessageBox.Show($"Product out of stock: {selectedProduct.Name}", "Out of Stock", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Show quantity input dialog - CHANGED: Quantity -> TotalQuantity
                var quantityForm = new QuantityInputForm(selectedProduct.TotalQuantity);
                if (quantityForm.ShowDialog() == DialogResult.OK)
                {
                    int quantity = quantityForm.Quantity;
                    
                    // CHANGED: Quantity -> TotalQuantity
                    if (quantity > selectedProduct.TotalQuantity)
                    {
                        MessageBox.Show($"Only {selectedProduct.TotalQuantity} units available in stock", "Insufficient Stock", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    AddToCart(selectedProduct, quantity);
                }
            }
            else
            {
                MessageBox.Show("Please select a product to add to cart", "Select Product", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AddToCart(Product product, int quantity)
        {
            // Check if product already in cart
            var existingItem = _cartItems.FirstOrDefault(item => item.ProductId == product.Id);
            
            if (existingItem != null)
            {
                // CHANGED: Check against TotalQuantity instead of Quantity
                if (existingItem.Quantity + quantity > product.TotalQuantity)
                {
                    MessageBox.Show($"Cannot add {quantity} more units. Only {product.TotalQuantity - existingItem.Quantity} units available.", 
                        "Insufficient Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                existingItem.Quantity += quantity;
                existingItem.TotalPrice = existingItem.Quantity * existingItem.UnitPrice;
            }
            else
            {
                _cartItems.Add(new SaleItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Quantity = quantity,
                    UnitPrice = product.SellingPrice,
                    TotalPrice = product.SellingPrice * quantity
                });
            }

            UpdateCartDisplay();
        }

        private void UpdateCartDisplay()
        {
            dataGridCart.DataSource = null;
            dataGridCart.DataSource = _cartItems;

            // Format cart grid
            dataGridCart.AutoGenerateColumns = false;
            dataGridCart.Columns.Clear();

            dataGridCart.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                DataPropertyName = "ProductName", 
                HeaderText = "Product", 
                Width = 150 
            });
            dataGridCart.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                DataPropertyName = "Quantity", 
                HeaderText = "Qty", 
                Width = 60 
            });
            dataGridCart.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                DataPropertyName = "UnitPrice", 
                HeaderText = "Unit Price", 
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "F2" }
            });
            dataGridCart.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                DataPropertyName = "TotalPrice", 
                HeaderText = "Total", 
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "F2" }
            });

            // Add remove button column
            var removeColumn = new DataGridViewButtonColumn
            {
                HeaderText = "Action",
                Text = "Remove",
                UseColumnTextForButtonValue = true,
                Width = 80
            };
            dataGridCart.Columns.Add(removeColumn);

            // Calculate total
            _totalAmount = _cartItems.Sum(item => item.TotalPrice);
            lblTotalAmount.Text = $"Total: GHS {_totalAmount:F2}";

            // Update process sale button
            btnProcessSale.Enabled = _cartItems.Count > 0;
        }

        private void dataGridCart_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridCart.Columns["Action"].Index)
            {
                var item = _cartItems[e.RowIndex];
                _cartItems.RemoveAt(e.RowIndex);
                UpdateCartDisplay();
            }
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            if (_cartItems.Count > 0)
            {
                var result = MessageBox.Show("Clear all items from cart?", "Clear Cart", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    _cartItems.Clear();
                    UpdateCartDisplay();
                }
            }
        }

        private void btnProcessSale_Click(object sender, EventArgs e)
        {
            if (_cartItems.Count == 0)
            {
                MessageBox.Show("Cart is empty", "No Items", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate payment method
            if (!radioCash.Checked && !radioMomo.Checked)
            {
                MessageBox.Show("Please select a payment method", "Payment Method Required", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate MoMo transaction ID if MoMo selected
            string paymentMethod = radioCash.Checked ? "Cash" : "Mobile Money";
            string momoTransactionId = "";

            if (paymentMethod == "Mobile Money")
            {
                if (string.IsNullOrWhiteSpace(txtMomoTransactionId.Text))
                {
                    MessageBox.Show("Please enter Mobile Money transaction ID", "Transaction ID Required", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                momoTransactionId = txtMomoTransactionId.Text.Trim();
            }

            // Create and process sale
            var sale = new Sale
            {
                Items = new List<SaleItem>(_cartItems),
                TotalAmount = _totalAmount,
                PaymentMethod = paymentMethod,
                MomoTransactionId = momoTransactionId
            };

            try
            {
                _salesService.ProcessSale(sale);
                
                // Show success message
                string message = $"Sale processed successfully!\n\n" +
                               $"Total: GHS {_totalAmount:F2}\n" +
                               $"Payment Method: {paymentMethod}\n" +
                               $"Items: {_cartItems.Count}";
                
                if (paymentMethod == "Mobile Money")
                {
                    message += $"\nMoMo Transaction ID: {momoTransactionId}";
                }

                MessageBox.Show(message, "Sale Completed", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset form
                _cartItems.Clear();
                UpdateCartDisplay();
                txtMomoTransactionId.Clear();
                radioCash.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing sale: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radioMomo_CheckedChanged(object sender, EventArgs e)
        {
            txtMomoTransactionId.Enabled = radioMomo.Checked;
            if (radioMomo.Checked)
            {
                txtMomoTransactionId.BackColor = Color.LightYellow;
            }
            else
            {
                txtMomoTransactionId.BackColor = SystemColors.Window;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                LoadProducts();
            }
            else
            {
                var filteredProducts = _productService.SearchProducts(txtSearch.Text);
                dataGridProducts.DataSource = filteredProducts;
            }
        }
    }
}
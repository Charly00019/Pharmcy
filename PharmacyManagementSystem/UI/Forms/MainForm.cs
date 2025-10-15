using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PharmacyManagementSystem.Data.Models;
using PharmacyManagementSystem.Services;

namespace PharmacyManagementSystem.UI.Forms
{
    public partial class MainForm : Form
    {
        private readonly IProductService _productService;
        private readonly ISalesService _salesService;
        private User _currentUser;

        // Color scheme
        private readonly Color _primaryColor = Color.FromArgb(206, 17, 38);
        private readonly Color _secondaryColor = Color.FromArgb(0, 107, 63);
        private readonly Color _backgroundColor = Color.FromArgb(250, 250, 250);

        public MainForm()
        {
            _productService = new ProductService();
            _salesService = new SalesService();
            InitializeComponent();
            ApplyTheme();
        }

        // Public method to set the user after form creation
        public void SetUser(User user)
        {
            _currentUser = user;
            AdjustUIForRole();
            LoadDashboardData();
        }

        private void ApplyTheme()
        {
            this.BackColor = _backgroundColor;
            this.ForeColor = Color.FromArgb(51, 51, 51);
            this.Font = new Font("Segoe UI", 9, FontStyle.Regular);

            panelHeader.BackColor = _primaryColor;
            lblTitle.ForeColor = Color.White;
            lblSubtitle.ForeColor = Color.White;

            panelNav.BackColor = _secondaryColor;
            foreach (Control control in panelNav.Controls)
            {
                if (control is Button btn)
                {
                    btn.BackColor = _secondaryColor;
                    btn.ForeColor = Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 85, 50);
                    btn.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                }
            }
        }

        private void AdjustUIForRole()
        {
            if (_currentUser == null) return;

            lblTitle.Text = $"Ma'adwuma Pharmacy Manager - Welcome, {_currentUser.FullName}";

            if (_currentUser.Role == UserRoles.Sales)
            {
                btnProducts.Enabled = false;
                btnProducts.Text = "📦 Products (Admin Only)";
                btnSettings.Enabled = false;
                btnSettings.Text = "⚙️ Settings (Admin Only)";
                btnProducts.BackColor = Color.Gray;
                btnSettings.BackColor = Color.Gray;
            }
            else if (_currentUser.Role == UserRoles.Admin)
            {
                lblTitle.Text = $"Ma'adwuma Pharmacy Manager - Admin, {_currentUser.FullName}";
            }
        }

        private void LoadDashboardData()
        {
            var products = _productService.GetAllProducts();
            var lowStock = _productService.GetLowStockProducts();
            var expiringSoon = _productService.GetExpiringSoonProducts();
            var expired = _productService.GetExpiredProducts();

            lblTotalProducts.Text = products.Count.ToString();
            lblLowStock.Text = lowStock.Count.ToString();
            lblExpiringSoon.Text = expiringSoon.Count.ToString();
            lblExpired.Text = expired.Count.ToString();

            lblLowStock.ForeColor = lowStock.Count > 0 ? Color.Red : _backgroundColor;
            lblExpiringSoon.ForeColor = expiringSoon.Count > 0 ? Color.Orange : _backgroundColor;
            lblExpired.ForeColor = expired.Count > 0 ? Color.Red : _backgroundColor;
        }

        private void CheckAlerts()
        {
            var expired = _productService.GetExpiredProducts();
            var lowStock = _productService.GetLowStockProducts();

            string alertMessage = "";

            if (expired.Any())
                alertMessage += $"⚠️ {expired.Count} products have expired!\n";
            if (lowStock.Any())
                alertMessage += $"📦 {lowStock.Count} products are low in stock!\n";

            if (!string.IsNullOrEmpty(alertMessage))
            {
                MessageBox.Show(alertMessage, "Inventory Alerts",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // 🔹 EVENT HANDLERS (Added to match Designer file)
        private void MainForm_Load(object sender, EventArgs e) => CheckAlerts();

        private void btnProducts_Click(object sender, EventArgs e)
        {
            var productForm = new ProductsForm();
            productForm.ShowDialog();
            LoadDashboardData();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            var salesForm = new SalesForm();
            salesForm.ShowDialog();
            LoadDashboardData();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Reports feature coming soon!", "Information",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            if (_currentUser?.Role != UserRoles.Admin)
            {
                MessageBox.Show("Only administrators can access system settings.", "Access Denied",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Settings feature coming soon!", "Information",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                Application.Exit();
        }
    }
}

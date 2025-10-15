using System;
using System.Drawing;
using System.Windows.Forms;
using PharmacyManagementSystem.Data.Models;
using PharmacyManagementSystem.Services;

namespace PharmacyManagementSystem.UI.Forms
{
    public partial class LoginForm : Form
    {
        private readonly IUserService _userService;
        
        // Controls
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnCancel;
        private Label lblTitle;
        private Label lblUsername;
        private Label lblPassword;
        private CheckBox chkRemember;

        public User LoggedInUser { get; private set; }

        // Blue color scheme
        private readonly Color _primaryBlue = Color.FromArgb(0, 102, 204);

        public LoginForm()
        {
            _userService = new UserService();
            InitializeComponent();
            ApplyTheme();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form settings
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Text = "Pharmacy Management System - Login";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            CreateControls();
            LayoutControls();

            this.ResumeLayout(false);
        }

        private void CreateControls()
        {
            // Title Label
            lblTitle = new Label();
            lblTitle.Text = "Pharmacy Management System";
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.ForeColor = _primaryBlue;

            // Username
            lblUsername = new Label();
            lblUsername.Text = "Username";
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            txtUsername = new TextBox();
            txtUsername.Width = 200;
            txtUsername.BorderStyle = BorderStyle.FixedSingle;

            // Password
            lblPassword = new Label();
            lblPassword.Text = "Password";
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            txtPassword = new TextBox();
            txtPassword.Width = 200;
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.UseSystemPasswordChar = true;

            // Remember me
            chkRemember = new CheckBox();
            chkRemember.Text = "Remember me";
            chkRemember.AutoSize = true;

            // Buttons
            btnLogin = new Button();
            btnLogin.Text = "Login";
            btnLogin.Size = new Size(100, 35);
            btnLogin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLogin.Click += btnLogin_Click;

            btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.Size = new Size(80, 35);
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            btnCancel.Click += btnCancel_Click;
        }

        private void LayoutControls()
        {
            // Main layout using TableLayoutPanel
            TableLayoutPanel mainTable = new TableLayoutPanel();
            mainTable.Dock = DockStyle.Fill;
            mainTable.RowCount = 6;
            mainTable.ColumnCount = 2;
            mainTable.Padding = new Padding(30);
            mainTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
            mainTable.BackColor = Color.White;

            // Set row styles
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));  // Title
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));  // Username
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));  // Password
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));  // Remember me
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));  // Spacer
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));  // Buttons

            // Set column styles
            mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100)); // Labels
            mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));  // Controls

            // Row 0: Title (spans both columns)
            mainTable.Controls.Add(lblTitle, 0, 0);
            mainTable.SetColumnSpan(lblTitle, 2);

            // Row 1: Username
            mainTable.Controls.Add(lblUsername, 0, 1);
            mainTable.Controls.Add(txtUsername, 1, 1);

            // Row 2: Password
            mainTable.Controls.Add(lblPassword, 0, 2);
            mainTable.Controls.Add(txtPassword, 1, 2);

            // Row 3: Remember me
            mainTable.Controls.Add(chkRemember, 1, 3);

            // Row 5: Action Buttons
            Panel buttonPanel = new Panel();
            buttonPanel.Dock = DockStyle.Fill;
            buttonPanel.BackColor = Color.Transparent;
            buttonPanel.Controls.Add(btnLogin);
            buttonPanel.Controls.Add(btnCancel);

            btnLogin.Location = new Point(100, 10);
            btnCancel.Location = new Point(210, 10);

            mainTable.Controls.Add(buttonPanel, 0, 5);
            mainTable.SetColumnSpan(buttonPanel, 2);

            // Add main table to form
            this.Controls.Add(mainTable);
        }

        private void ApplyTheme()
        {
            this.BackColor = Color.White;
            this.ForeColor = Color.FromArgb(51, 51, 51);
            this.Font = new Font("Segoe UI", 9, FontStyle.Regular);

            // Button styling
            btnLogin.BackColor = _primaryBlue;
            btnLogin.ForeColor = Color.White;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;

            btnCancel.BackColor = Color.FromArgb(240, 240, 240);
            btnCancel.ForeColor = Color.Black;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 1;
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Login Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var user = _userService.Authenticate(username, password);
            if (user != null)
            {
                LoggedInUser = user;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtUsername.Focus();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Enter key to login
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                btnLogin.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
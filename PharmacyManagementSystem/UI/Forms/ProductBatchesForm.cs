using System;
using System.Drawing;
using System.Windows.Forms;
using PharmacyManagementSystem.Data.Models;

namespace PharmacyManagementSystem.UI.Forms
{
    public partial class ProductBatchForm : Form
    {
        public ProductBatch Batch { get; private set; }
        
        // Controls
        private Label lblTitle;
        private Label lblBatchNumber;
        private TextBox txtBatchNumber;
        private Label lblQuantity;
        private NumericUpDown numQuantity;
        private Label lblExpiryDate;
        private DateTimePicker dtpExpiryDate;
        private Button btnSave;
        private Button btnCancel;

        // Blue color scheme
        private readonly Color _primaryBlue = Color.FromArgb(0, 102, 204);

        public ProductBatchForm(ProductBatch existingBatch = null)
        {
            Batch = existingBatch ?? new ProductBatch();
            InitializeComponent();
            ApplyTheme();
            LoadBatchData();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form settings
            this.ClientSize = new System.Drawing.Size(400, 250);
            this.Text = "Add Batch";
            this.StartPosition = FormStartPosition.CenterParent;
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
            lblTitle.Text = "Product Batch";
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.ForeColor = _primaryBlue;

            // Batch Number
            lblBatchNumber = new Label();
            lblBatchNumber.Text = "Batch Number*";
            lblBatchNumber.AutoSize = true;
            lblBatchNumber.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            txtBatchNumber = new TextBox();
            txtBatchNumber.Width = 200;
            txtBatchNumber.MaxLength = 50;
            txtBatchNumber.BorderStyle = BorderStyle.FixedSingle;

            // Quantity
            lblQuantity = new Label();
            lblQuantity.Text = "Quantity*";
            lblQuantity.AutoSize = true;
            lblQuantity.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            numQuantity = new NumericUpDown();
            numQuantity.Width = 120;
            numQuantity.Minimum = 1;
            numQuantity.Maximum = 100000;
            numQuantity.BorderStyle = BorderStyle.FixedSingle;

            // Expiry Date
            lblExpiryDate = new Label();
            lblExpiryDate.Text = "Expiry Date*";
            lblExpiryDate.AutoSize = true;
            lblExpiryDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            dtpExpiryDate = new DateTimePicker();
            dtpExpiryDate.Width = 150;
            dtpExpiryDate.Format = DateTimePickerFormat.Short;
            dtpExpiryDate.MinDate = DateTime.Now.AddDays(1);

            // Action Buttons
            btnSave = new Button();
            btnSave.Text = "Save Batch";
            btnSave.Size = new Size(100, 35);
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.Click += btnSave_Click;

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
            mainTable.RowCount = 5;
            mainTable.ColumnCount = 2;
            mainTable.Padding = new Padding(25);
            mainTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
            mainTable.BackColor = Color.White;

            // Set row styles
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));  // Title
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));  // Batch Number
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));  // Quantity
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));  // Expiry Date
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));  // Buttons

            // Set column styles
            mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120)); // Labels
            mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));  // Controls

            // Row 0: Title (spans both columns)
            mainTable.Controls.Add(lblTitle, 0, 0);
            mainTable.SetColumnSpan(lblTitle, 2);

            // Row 1: Batch Number
            mainTable.Controls.Add(lblBatchNumber, 0, 1);
            mainTable.Controls.Add(txtBatchNumber, 1, 1);

            // Row 2: Quantity
            mainTable.Controls.Add(lblQuantity, 0, 2);
            mainTable.Controls.Add(numQuantity, 1, 2);

            // Row 3: Expiry Date
            mainTable.Controls.Add(lblExpiryDate, 0, 3);
            mainTable.Controls.Add(dtpExpiryDate, 1, 3);

            // Row 4: Action Buttons
            Panel buttonPanel = new Panel();
            buttonPanel.Dock = DockStyle.Fill;
            buttonPanel.BackColor = Color.Transparent;
            buttonPanel.Controls.Add(btnSave);
            buttonPanel.Controls.Add(btnCancel);

            btnSave.Location = new Point(100, 15);
            btnCancel.Location = new Point(210, 15);

            mainTable.Controls.Add(buttonPanel, 0, 4);
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
            btnSave.BackColor = _primaryBlue;
            btnSave.ForeColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;

            btnCancel.BackColor = Color.FromArgb(240, 240, 240);
            btnCancel.ForeColor = Color.Black;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 1;
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);
        }

        private void LoadBatchData()
        {
            txtBatchNumber.Text = Batch.BatchNumber;
            numQuantity.Value = Batch.Quantity;
            dtpExpiryDate.Value = Batch.ExpiryDate;

            if (!string.IsNullOrEmpty(Batch.BatchNumber))
            {
                lblTitle.Text = "Edit Batch";
                this.Text = "Edit Batch";
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtBatchNumber.Text))
            {
                MessageBox.Show("Batch number is required", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBatchNumber.Focus();
                return false;
            }

            if (numQuantity.Value <= 0)
            {
                MessageBox.Show("Quantity must be greater than 0", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                numQuantity.Focus();
                return false;
            }

            if (dtpExpiryDate.Value <= DateTime.Now)
            {
                MessageBox.Show("Expiry date must be in the future", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpExpiryDate.Focus();
                return false;
            }

            return true;
        }

        private void SaveBatch()
        {
            Batch.BatchNumber = txtBatchNumber.Text.Trim();
            Batch.Quantity = (int)numQuantity.Value;
            Batch.ExpiryDate = dtpExpiryDate.Value;
            Batch.IsActive = true;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                SaveBatch();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
namespace PharmacyManagementSystem.UI.Forms
{
    partial class SalesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Text = "Point of Sale";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Create all controls
            CreateControls();
            
            // Apply layout
            LayoutControls();
        }

        private void CreateControls()
        {
            // Title Label
            lblSalesTitle = new Label();
            lblSalesTitle.Text = "Point of Sale";
            lblSalesTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblSalesTitle.AutoSize = true;

            // Search TextBox
            txtSearch = new TextBox();
            txtSearch.PlaceholderText = "Search products by name, generic name, or batch number...";
            txtSearch.Width = 300;
            txtSearch.TextChanged += txtSearch_TextChanged;

            // Products DataGridView
            dataGridProducts = new DataGridView();
            dataGridProducts.ReadOnly = true;
            dataGridProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridProducts.AllowUserToAddRows = false;
            dataGridProducts.AllowUserToDeleteRows = false;
            dataGridProducts.RowHeadersVisible = false;

            // Products Panel
            panelProducts = new Panel();
            panelProducts.BorderStyle = BorderStyle.FixedSingle;
            panelProducts.Controls.Add(dataGridProducts);

            // Add to Cart Button
            btnAddToCart = new Button();
            btnAddToCart.Text = "Add to Cart";
            btnAddToCart.Size = new System.Drawing.Size(120, 35);
            btnAddToCart.Click += btnAddToCart_Click;

            // Cart DataGridView
            dataGridCart = new DataGridView();
            dataGridCart.ReadOnly = true;
            dataGridCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridCart.AllowUserToAddRows = false;
            dataGridCart.AllowUserToDeleteRows = false;
            dataGridCart.RowHeadersVisible = false;
            dataGridCart.CellClick += dataGridCart_CellClick;

            // Cart Panel
            panelCart = new Panel();
            panelCart.BorderStyle = BorderStyle.FixedSingle;
            panelCart.Controls.Add(dataGridCart);

            // Total Amount Label
            lblTotalAmount = new Label();
            lblTotalAmount.Text = "Total: GHS 0.00";
            lblTotalAmount.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblTotalAmount.AutoSize = true;

            // Clear Cart Button
            btnClearCart = new Button();
            btnClearCart.Text = "Clear Cart";
            btnClearCart.Size = new System.Drawing.Size(100, 35);
            btnClearCart.Click += btnClearCart_Click;

            // Process Sale Button
            btnProcessSale = new Button();
            btnProcessSale.Text = "Process Sale";
            btnProcessSale.Size = new System.Drawing.Size(120, 40);
            btnProcessSale.Enabled = false;
            btnProcessSale.Click += btnProcessSale_Click;

            // Payment Method Controls
            label1 = new Label();
            label1.Text = "Payment Method:";
            label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label1.AutoSize = true;

            radioCash = new RadioButton();
            radioCash.Text = "Cash";
            radioCash.Checked = true;
            radioCash.CheckedChanged += radioMomo_CheckedChanged;

            radioMomo = new RadioButton();
            radioMomo.Text = "Mobile Money (MoMo)";
            radioMomo.CheckedChanged += radioMomo_CheckedChanged;

            txtMomoTransactionId = new TextBox();
            txtMomoTransactionId.PlaceholderText = "Enter MoMo Transaction ID";
            txtMomoTransactionId.Enabled = false;
            txtMomoTransactionId.Width = 250;

            // Payment Panel
            panelPayment = new Panel();
            panelPayment.BorderStyle = BorderStyle.FixedSingle;
        }

        private void LayoutControls()
        {
            // Main Table Layout
            TableLayoutPanel mainTable = new TableLayoutPanel();
            mainTable.Dock = DockStyle.Fill;
            mainTable.RowCount = 4;
            mainTable.ColumnCount = 2;
            mainTable.Padding = new Padding(10);
            mainTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;

            // Set row styles
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));  // Title and search
            mainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50));   // Products
            mainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 30));   // Cart
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 120)); // Payment and actions

            // Set column styles
            mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70)); // Left column
            mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30)); // Right column

            // Row 0: Title and Search (spans both columns)
            Panel headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Fill;
            headerPanel.Controls.Add(lblSalesTitle);
            headerPanel.Controls.Add(txtSearch);
            
            lblSalesTitle.Location = new System.Drawing.Point(10, 15);
            txtSearch.Location = new System.Drawing.Point(250, 18);
            txtSearch.Size = new System.Drawing.Size(400, 25);

            mainTable.Controls.Add(headerPanel, 0, 0);
            mainTable.SetColumnSpan(headerPanel, 2);

            // Row 1: Products and Add to Cart button
            panelProducts.Dock = DockStyle.Fill;
            dataGridProducts.Dock = DockStyle.Fill;
            
            btnAddToCart.Location = new System.Drawing.Point(10, 10);
            
            Panel productsRightPanel = new Panel();
            productsRightPanel.Dock = DockStyle.Fill;
            productsRightPanel.Controls.Add(btnAddToCart);

            mainTable.Controls.Add(panelProducts, 0, 1);
            mainTable.Controls.Add(productsRightPanel, 1, 1);

            // Row 2: Cart (spans both columns)
            panelCart.Dock = DockStyle.Fill;
            dataGridCart.Dock = DockStyle.Fill;
            
            mainTable.Controls.Add(panelCart, 0, 2);
            mainTable.SetColumnSpan(panelCart, 2);

            // Row 3: Payment and Action buttons
            // Payment Panel Layout
            panelPayment.Dock = DockStyle.Fill;
            panelPayment.Padding = new Padding(10);
            
            // Arrange payment controls
            label1.Location = new System.Drawing.Point(10, 15);
            radioCash.Location = new System.Drawing.Point(10, 45);
            radioMomo.Location = new System.Drawing.Point(10, 70);
            txtMomoTransactionId.Location = new System.Drawing.Point(180, 68);
            
            panelPayment.Controls.Add(label1);
            panelPayment.Controls.Add(radioCash);
            panelPayment.Controls.Add(radioMomo);
            panelPayment.Controls.Add(txtMomoTransactionId);

            // Action Panel Layout
            Panel actionPanel = new Panel();
            actionPanel.Dock = DockStyle.Fill;
            actionPanel.Padding = new Padding(10);
            
            lblTotalAmount.Location = new System.Drawing.Point(10, 15);
            btnClearCart.Location = new System.Drawing.Point(10, 60);
            btnProcessSale.Location = new System.Drawing.Point(120, 55);
            
            actionPanel.Controls.Add(lblTotalAmount);
            actionPanel.Controls.Add(btnClearCart);
            actionPanel.Controls.Add(btnProcessSale);

            mainTable.Controls.Add(panelPayment, 0, 3);
            mainTable.Controls.Add(actionPanel, 1, 3);

            // Add main table to form
            this.Controls.Add(mainTable);
        }

        // Declare all controls as fields
        private Label lblSalesTitle;
        private TextBox txtSearch;
        private Panel panelProducts;
        private DataGridView dataGridProducts;
        private Button btnAddToCart;
        private Panel panelCart;
        private DataGridView dataGridCart;
        private Label lblTotalAmount;
        private Button btnClearCart;
        private Button btnProcessSale;
        private Panel panelPayment;
        private Label label1;
        private RadioButton radioCash;
        private RadioButton radioMomo;
        private TextBox txtMomoTransactionId;

        #endregion
    }
}
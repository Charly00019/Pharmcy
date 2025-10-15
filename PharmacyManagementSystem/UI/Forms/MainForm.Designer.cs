namespace PharmacyManagementSystem.UI.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel panelNav;
        private Button btnProducts;
        private Button btnSales;
        private Button btnReports;
        private Button btnSettings;
        private Panel panelDashboard;
        private Label lblTotalProducts;
        private Label lblLowStock;
        private Label lblExpiringSoon;
        private Label lblExpired;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Text = "Ma'adwuma Pharmacy Manager";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            CreateHeader();
            CreateNavigation();
            CreateDashboard();
        }

        private void CreateHeader()
        {
            panelHeader = new Panel();
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 100;
            panelHeader.BackColor = Color.FromArgb(206, 17, 38);

            lblTitle = new Label();
            lblTitle.Text = "Ma'adwuma Pharmacy Manager";
            lblTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(20, 20);

            lblSubtitle = new Label();
            lblSubtitle.Text = "Small Business Pharmacy Management System";
            lblSubtitle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblSubtitle.ForeColor = Color.White;
            lblSubtitle.AutoSize = true;
            lblSubtitle.Location = new Point(22, 55);

            panelHeader.Controls.Add(lblTitle);
            panelHeader.Controls.Add(lblSubtitle);
            this.Controls.Add(panelHeader);
        }

        private void CreateNavigation()
        {
            panelNav = new Panel();
            panelNav.Dock = DockStyle.Left;
            panelNav.Width = 200;
            panelNav.BackColor = Color.FromArgb(0, 107, 63);

            btnProducts = CreateNavButton("📦 Products", 20);
            btnSales = CreateNavButton("💰 Sales", 75);
            btnReports = CreateNavButton("📊 Reports", 130);
            btnSettings = CreateNavButton("⚙️ Settings", 185);

            btnProducts.Click += btnProducts_Click;
            btnSales.Click += btnSales_Click;
            btnReports.Click += btnReports_Click;
            btnSettings.Click += btnSettings_Click;

            panelNav.Controls.AddRange(new Control[] { btnProducts, btnSales, btnReports, btnSettings });
            this.Controls.Add(panelNav);
        }

        private Button CreateNavButton(string text, int top)
        {
            var button = new Button();
            button.Text = text;
            button.Width = 180;
            button.Height = 45;
            button.Location = new Point(10, top);
            button.TextAlign = ContentAlignment.MiddleLeft;
            button.Padding = new Padding(15, 0, 0, 0);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            return button;
        }

        private void CreateDashboard()
        {
            panelDashboard = new Panel();
            panelDashboard.Dock = DockStyle.Fill;
            panelDashboard.BackColor = Color.FromArgb(250, 250, 250);
            panelDashboard.Padding = new Padding(20);

            int cardWidth = 200;
            int cardHeight = 120;
            int spacing = 20;
            int startX = 20;
            int startY = 20;

            var totalProductsCard = CreateDashboardCard("Total Products", "0", startX, startY, cardWidth, cardHeight, Color.FromArgb(52, 152, 219));
            startX += cardWidth + spacing;

            var lowStockCard = CreateDashboardCard("Low Stock", "0", startX, startY, cardWidth, cardHeight, Color.FromArgb(231, 76, 60));
            startX += cardWidth + spacing;

            var expiringSoonCard = CreateDashboardCard("Expiring Soon", "0", startX, startY, cardWidth, cardHeight, Color.FromArgb(243, 156, 18));
            startX += cardWidth + spacing;

            var expiredCard = CreateDashboardCard("Expired", "0", startX, startY, cardWidth, cardHeight, Color.FromArgb(192, 57, 43));

            // Get reference to the value labels
            lblTotalProducts = totalProductsCard.Controls.OfType<Label>().First(l => l.Font.Size > 12);
            lblLowStock = lowStockCard.Controls.OfType<Label>().First(l => l.Font.Size > 12);
            lblExpiringSoon = expiringSoonCard.Controls.OfType<Label>().First(l => l.Font.Size > 12);
            lblExpired = expiredCard.Controls.OfType<Label>().First(l => l.Font.Size > 12);

            panelDashboard.Controls.AddRange(new Control[] { 
                totalProductsCard, lowStockCard, expiringSoonCard, expiredCard 
            });
            this.Controls.Add(panelDashboard);
        }

        private Panel CreateDashboardCard(string title, string value, int x, int y, int width, int height, Color color)
        {
            var panel = new Panel();
            panel.Location = new Point(x, y);
            panel.Size = new Size(width, height);
            panel.BackColor = color;
            panel.Padding = new Padding(15);

            var titleLabel = new Label();
            titleLabel.Text = title;
            titleLabel.ForeColor = Color.White;
            titleLabel.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(15, 15);

            var valueLabel = new Label();
            valueLabel.Text = value;
            valueLabel.ForeColor = Color.White;
            valueLabel.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            valueLabel.AutoSize = true;
            valueLabel.Location = new Point(15, 45);

            panel.Controls.Add(titleLabel);
            panel.Controls.Add(valueLabel);

            return panel;
        }
    }
}
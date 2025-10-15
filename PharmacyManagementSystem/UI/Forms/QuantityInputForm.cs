using System;
using System.Drawing;
using System.Windows.Forms;

namespace PharmacyManagementSystem.UI.Forms
{
    public partial class QuantityInputForm : Form
    {
        public int Quantity { get; private set; }
        private readonly int _maxQuantity;
        private Label lblQuantity;
        private Label lblInfo;

        public QuantityInputForm(int maxQuantity, string title = "Enter Quantity", string infoText = "")
        {
            _maxQuantity = maxQuantity;
            Quantity = 1;
            this.Text = title;
            InitializeComponent(infoText);
        }

        private void InitializeComponent(string infoText)
        {
            this.SuspendLayout();

            // Form settings
            this.ClientSize = new System.Drawing.Size(350, 180);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Info label
            if (!string.IsNullOrEmpty(infoText))
            {
                lblInfo = new Label();
                lblInfo.Text = infoText;
                lblInfo.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
                lblInfo.AutoSize = true;
                lblInfo.Location = new Point(20, 20);
                this.Controls.Add(lblInfo);
            }

            // Title label
            var lblTitle = new Label();
            lblTitle.Text = $"Enter Quantity to Add (Max: {_maxQuantity})";
            lblTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(20, 50);

            // Quantity display
            lblQuantity = new Label();
            lblQuantity.Text = Quantity.ToString();
            lblQuantity.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblQuantity.AutoSize = true;
            lblQuantity.Location = new Point(160, 80);

            // Buttons
            var btnMinus = new Button();
            btnMinus.Text = "-10";
            btnMinus.Size = new Size(50, 35);
            btnMinus.Location = new Point(80, 75);
            btnMinus.Click += (s, e) => ChangeQuantity(-10);

            var btnMinusSingle = new Button();
            btnMinusSingle.Text = "-1";
            btnMinusSingle.Size = new Size(50, 35);
            btnMinusSingle.Location = new Point(130, 75);
            btnMinusSingle.Click += (s, e) => ChangeQuantity(-1);

            var btnPlusSingle = new Button();
            btnPlusSingle.Text = "+1";
            btnPlusSingle.Size = new Size(50, 35);
            btnPlusSingle.Location = new Point(200, 75);
            btnPlusSingle.Click += (s, e) => ChangeQuantity(1);

            var btnPlus = new Button();
            btnPlus.Text = "+10";
            btnPlus.Size = new Size(50, 35);
            btnPlus.Location = new Point(250, 75);
            btnPlus.Click += (s, e) => ChangeQuantity(10);

            // OK and Cancel buttons
            var btnOK = new Button();
            btnOK.Text = "OK";
            btnOK.Size = new Size(80, 30);
            btnOK.Location = new Point(80, 130);
            btnOK.DialogResult = DialogResult.OK;
            btnOK.BackColor = Color.FromArgb(0, 102, 204);
            btnOK.ForeColor = Color.White;
            btnOK.FlatStyle = FlatStyle.Flat;

            var btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.Size = new Size(80, 30);
            btnCancel.Location = new Point(180, 130);
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.BackColor = Color.FromArgb(240, 240, 240);
            btnCancel.ForeColor = Color.Black;
            btnCancel.FlatStyle = FlatStyle.Flat;

            this.Controls.AddRange(new Control[] {
                lblTitle, lblQuantity, btnMinus, btnMinusSingle, btnPlusSingle, btnPlus, btnOK, btnCancel
            });

            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;

            this.ResumeLayout(false);
        }

        private void ChangeQuantity(int change)
        {
            Quantity = Math.Max(1, Math.Min(_maxQuantity, Quantity + change));
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            if (lblQuantity != null)
            {
                lblQuantity.Text = Quantity.ToString();
            }
        }
    }
}
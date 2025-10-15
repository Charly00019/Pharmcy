using System;
using System.Drawing;
using System.Windows.Forms;

namespace PharmacyManagementSystem.UI.Forms
{
    public partial class ExpiryDateForm : Form
    {
        public DateTime SelectedDate { get; private set; }
        private DateTimePicker dtpExpiryDate;
        private Button btnOK;
        private Button btnCancel;

        public ExpiryDateForm(DateTime currentDate)
        {
            SelectedDate = currentDate;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form settings
            this.ClientSize = new System.Drawing.Size(300, 150);
            this.Text = "Update Expiry Date";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Expiry Date Picker
            dtpExpiryDate = new DateTimePicker();
            dtpExpiryDate.Location = new Point(20, 30);
            dtpExpiryDate.Size = new Size(260, 25);
            dtpExpiryDate.Format = DateTimePickerFormat.Short;
            dtpExpiryDate.Value = SelectedDate;
            dtpExpiryDate.MinDate = DateTime.Now.AddDays(1); // Can't set past dates

            // OK Button
            btnOK = new Button();
            btnOK.Text = "Update";
            btnOK.Size = new Size(80, 30);
            btnOK.Location = new Point(60, 80);
            btnOK.BackColor = Color.FromArgb(0, 102, 204);
            btnOK.ForeColor = Color.White;
            btnOK.FlatStyle = FlatStyle.Flat;
            btnOK.DialogResult = DialogResult.OK;
            btnOK.Click += (s, e) => { SelectedDate = dtpExpiryDate.Value; };

            // Cancel Button
            btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.Size = new Size(80, 30);
            btnCancel.Location = new Point(160, 80);
            btnCancel.BackColor = Color.FromArgb(240, 240, 240);
            btnCancel.ForeColor = Color.Black;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.DialogResult = DialogResult.Cancel;

            // Add controls
            this.Controls.Add(dtpExpiryDate);
            this.Controls.Add(btnOK);
            this.Controls.Add(btnCancel);

            this.ResumeLayout(false);
        }
    }
}
namespace PharmacyManagementSystem.UI.Forms
{
    partial class ProductEditorForm
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

        private void InitializeComponent()
        {
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtGenericName = new System.Windows.Forms.TextBox();
            this.numCostPrice = new System.Windows.Forms.NumericUpDown();
            this.numSellingPrice = new System.Windows.Forms.NumericUpDown();
            this.txtSupplier = new System.Windows.Forms.TextBox();
            this.numMinStockLevel = new System.Windows.Forms.NumericUpDown();
            this.txtBatchNumber = new System.Windows.Forms.TextBox();
            this.numQuantity = new System.Windows.Forms.NumericUpDown();
            this.dtpExpiryDate = new System.Windows.Forms.DateTimePicker();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            this.lblName = new System.Windows.Forms.Label();
            this.lblGeneric = new System.Windows.Forms.Label();
            this.lblCost = new System.Windows.Forms.Label();
            this.lblSell = new System.Windows.Forms.Label();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.lblMinStock = new System.Windows.Forms.Label();
            this.lblBatch = new System.Windows.Forms.Label();
            this.lblQty = new System.Windows.Forms.Label();
            this.lblExpiry = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.numCostPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSellingPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinStockLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();

            this.SuspendLayout();

            // txtName
            this.txtName.Location = new System.Drawing.Point(150, 20);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(250, 23);

            // txtGenericName
            this.txtGenericName.Location = new System.Drawing.Point(150, 60);
            this.txtGenericName.Name = "txtGenericName";
            this.txtGenericName.Size = new System.Drawing.Size(250, 23);

            // numCostPrice
            this.numCostPrice.Location = new System.Drawing.Point(150, 100);
            this.numCostPrice.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.numCostPrice.DecimalPlaces = 2;

            // numSellingPrice
            this.numSellingPrice.Location = new System.Drawing.Point(150, 140);
            this.numSellingPrice.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.numSellingPrice.DecimalPlaces = 2;

            // txtSupplier
            this.txtSupplier.Location = new System.Drawing.Point(150, 180);
            this.txtSupplier.Name = "txtSupplier";
            this.txtSupplier.Size = new System.Drawing.Size(250, 23);

            // numMinStockLevel
            this.numMinStockLevel.Location = new System.Drawing.Point(150, 220);
            this.numMinStockLevel.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });

            // txtBatchNumber
            this.txtBatchNumber.Location = new System.Drawing.Point(150, 260);
            this.txtBatchNumber.Size = new System.Drawing.Size(250, 23);

            // numQuantity
            this.numQuantity.Location = new System.Drawing.Point(150, 300);
            this.numQuantity.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });

            // dtpExpiryDate
            this.dtpExpiryDate.Location = new System.Drawing.Point(150, 340);
            this.dtpExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            // btnSave
            this.btnSave.Location = new System.Drawing.Point(150, 390);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(300, 390);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // Labels
            this.lblName.Text = "Product Name:";
            this.lblName.Location = new System.Drawing.Point(30, 20);
            this.lblGeneric.Text = "Generic Name:";
            this.lblGeneric.Location = new System.Drawing.Point(30, 60);
            this.lblCost.Text = "Cost Price:";
            this.lblCost.Location = new System.Drawing.Point(30, 100);
            this.lblSell.Text = "Selling Price:";
            this.lblSell.Location = new System.Drawing.Point(30, 140);
            this.lblSupplier.Text = "Supplier:";
            this.lblSupplier.Location = new System.Drawing.Point(30, 180);
            this.lblMinStock.Text = "Min Stock Level:";
            this.lblMinStock.Location = new System.Drawing.Point(30, 220);
            this.lblBatch.Text = "Batch Number:";
            this.lblBatch.Location = new System.Drawing.Point(30, 260);
            this.lblQty.Text = "Quantity:";
            this.lblQty.Location = new System.Drawing.Point(30, 300);
            this.lblExpiry.Text = "Expiry Date:";
            this.lblExpiry.Location = new System.Drawing.Point(30, 340);

            // ProductEditorForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 450);
            this.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                lblName, txtName,
                lblGeneric, txtGenericName,
                lblCost, numCostPrice,
                lblSell, numSellingPrice,
                lblSupplier, txtSupplier,
                lblMinStock, numMinStockLevel,
                lblBatch, txtBatchNumber,
                lblQty, numQuantity,
                lblExpiry, dtpExpiryDate,
                btnSave, btnCancel
            });
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Product Editor";

            ((System.ComponentModel.ISupportInitialize)(this.numCostPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSellingPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinStockLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtGenericName;
        private System.Windows.Forms.NumericUpDown numCostPrice;
        private System.Windows.Forms.NumericUpDown numSellingPrice;
        private System.Windows.Forms.TextBox txtSupplier;
        private System.Windows.Forms.NumericUpDown numMinStockLevel;
        private System.Windows.Forms.TextBox txtBatchNumber;
        private System.Windows.Forms.NumericUpDown numQuantity;
        private System.Windows.Forms.DateTimePicker dtpExpiryDate;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblGeneric;
        private System.Windows.Forms.Label lblCost;
        private System.Windows.Forms.Label lblSell;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.Label lblMinStock;
        private System.Windows.Forms.Label lblBatch;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.Label lblExpiry;
    }
}

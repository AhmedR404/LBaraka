namespace AlBarakaPOS.Controler
{
    partial class OrderTypeButton
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnDineIn;
        private Button btnTakeAway;
        private Button btnDelivery;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnDineIn = new System.Windows.Forms.Button();
            this.btnTakeAway = new System.Windows.Forms.Button();
            this.btnDelivery = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.btnDineIn, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnTakeAway, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnDelivery, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(344, 52);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnDineIn
            // 
            this.btnDineIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDineIn.FlatAppearance.BorderSize = 1;
            this.btnDineIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDineIn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDineIn.Location = new System.Drawing.Point(2, 6);
            this.btnDineIn.Margin = new System.Windows.Forms.Padding(2, 6, 4, 6);
            this.btnDineIn.Name = "btnDineIn";
            this.btnDineIn.Size = new System.Drawing.Size(108, 40);
            this.btnDineIn.TabIndex = 0;
            this.btnDineIn.Text = "داخلي";
            this.btnDineIn.UseVisualStyleBackColor = true;
            // 
            // btnTakeAway
            // 
            this.btnTakeAway.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTakeAway.FlatAppearance.BorderSize = 1;
            this.btnTakeAway.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTakeAway.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnTakeAway.Location = new System.Drawing.Point(116, 6);
            this.btnTakeAway.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.btnTakeAway.Name = "btnTakeAway";
            this.btnTakeAway.Size = new System.Drawing.Size(108, 40);
            this.btnTakeAway.TabIndex = 1;
            this.btnTakeAway.Text = "تيك أواي";
            this.btnTakeAway.UseVisualStyleBackColor = true;
            // 
            // btnDelivery
            // 
            this.btnDelivery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelivery.FlatAppearance.BorderSize = 1;
            this.btnDelivery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelivery.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDelivery.Location = new System.Drawing.Point(230, 6);
            this.btnDelivery.Margin = new System.Windows.Forms.Padding(4, 6, 2, 6);
            this.btnDelivery.Name = "btnDelivery";
            this.btnDelivery.Size = new System.Drawing.Size(112, 40);
            this.btnDelivery.TabIndex = 2;
            this.btnDelivery.Text = "توصيل";
            this.btnDelivery.UseVisualStyleBackColor = true;
            // 
            // UserControllerButtons
            // 
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(6, 14, 28);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UserControllerButtons";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(344, 52);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }

    //partial class UserControllerButtons
    //{
    //    /// <summary> 
    //    /// Required designer variable.
    //    /// </summary>
    //    private System.ComponentModel.IContainer components = null;

    //    /// <summary> 
    //    /// Clean up any resources being used.
    //    /// </summary>
    //    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    //    protected override void Dispose(bool disposing)
    //    {
    //        if (disposing && (components != null))
    //        {
    //            components.Dispose();
    //        }
    //        base.Dispose(disposing);
    //    }

    //    #region Component Designer generated code

    //    /// <summary> 
    //    /// Required method for Designer support - do not modify 
    //    /// the contents of this method with the code editor.
    //    /// </summary>
    //    private void InitializeComponent()
    //    {
    //        components = new System.ComponentModel.Container();
    //        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
    //    }

    //    #endregion
    //}
}

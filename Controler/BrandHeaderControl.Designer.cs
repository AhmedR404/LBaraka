namespace AlBarakaPOS.Controler
{
    partial class BrandHeaderControl
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblBrand;
        private Label lblSubtitle;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblBrand = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblBrand
            // 
            this.lblBrand.BackColor = System.Drawing.Color.Transparent;
            this.lblBrand.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblBrand.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(213)))), ((int)(((byte)(146)))));
            this.lblBrand.Location = new System.Drawing.Point(8, 6);
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Size = new System.Drawing.Size(300, 52);
            this.lblBrand.TabIndex = 0;
            this.lblBrand.Text = "E L B A R A K A";
            this.lblBrand.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(132)))), ((int)(((byte)(84)))));
            this.lblSubtitle.Location = new System.Drawing.Point(146, 58);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblSubtitle.Size = new System.Drawing.Size(162, 34);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "نظام الكاشير";
            this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BrandHeaderControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(4)))), ((int)(((byte)(15)))));
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblBrand);
            this.Name = "BrandHeaderControl";
            this.Size = new System.Drawing.Size(320, 100);
            this.ResumeLayout(false);

        }
    }
}
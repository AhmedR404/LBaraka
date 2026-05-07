namespace AlBarakaPOS.Controler
{
    partial class ARLabel
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtValue;
        private Label lblHint;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtValue = new System.Windows.Forms.TextBox();
            this.lblHint = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtValue
            // 
            this.txtValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(40)))));
            this.txtValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtValue.ForeColor = System.Drawing.Color.White;
            this.txtValue.Location = new System.Drawing.Point(12, 9);
            this.txtValue.Name = "txtValue";
            this.txtValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtValue.Size = new System.Drawing.Size(250, 27);
            this.txtValue.TabIndex = 0;
            this.txtValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblHint
            // 
            this.lblHint.BackColor = System.Drawing.Color.Transparent;
            this.lblHint.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(123)))), ((int)(((byte)(81)))));
            this.lblHint.Location = new System.Drawing.Point(12, 0);
            this.lblHint.Name = "lblHint";
            this.lblHint.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblHint.Size = new System.Drawing.Size(250, 38);
            this.lblHint.TabIndex = 1;
            this.lblHint.Text = "اسم العميل";
            this.lblHint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ARLabel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.txtValue);
            this.Name = "ARLabel";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(274, 38);
            this.ResumeLayout(false);
            this.PerformLayout();


            this.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtValue.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.lblHint.Cursor = System.Windows.Forms.Cursors.IBeam;
        }
    }
    //partial class ARLabel
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

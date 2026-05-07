
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlBarakaPOS.Controler
{
    public partial class UCMain : UserControl
    {
        public UCMain()
        {
            InitializeComponent();
            groupBox4.Hide();
            groupBox1.AutoSize = true;
            groupBox1.Update();
        }

        private void InSidebtn_Click(object sender, EventArgs e)
        {
            Dlivarybtn.IsActive = false;
            tikawaybtn.IsActive = false;
            InSidebtn.IsActive = true;


            groupBox2.Show();
            groupBox4.Hide();                
            groupBox1.AutoSize = true;
            groupBox1.Update();
        }

        private void tikawaybtn_Click(object sender, EventArgs e)
        {
            Dlivarybtn.IsActive = false;
            tikawaybtn.IsActive = true;
            InSidebtn.IsActive = false;


            groupBox2.Hide();
            groupBox4.Hide();
            groupBox1.AutoSize = true;
            groupBox1.Update();
        }

        private void Dlivarybtn_Click(object sender, EventArgs e)
        {
            Dlivarybtn.IsActive = true;
            tikawaybtn.IsActive = false;
            InSidebtn.IsActive = false;


            groupBox2.Hide();
            groupBox4.Show();
            groupBox1.AutoSize = true;
            groupBox1.Update();
        }

        private void productButtonControl1_Click(object sender, EventArgs e)
        {
            decimal price = decimal.Parse(productButtonControl1.PriceText.Replace("ج", ""));
            invoicePanelControl1.AddOrIncrementItem(productButtonControl1.ProductName,  price);
        }
    }
}

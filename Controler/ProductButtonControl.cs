using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

//namespace AlBarakaPOS.Controler
//{
//    public partial class ProductButtonControl : UserControl
//    {
//        public ProductButtonControl()
//        {
//            InitializeComponent();
//        }
//    }
//}

namespace AlBarakaPOS.Controler
{
    [DefaultEvent("Click")]
    public partial class ProductButtonControl : UserControl
    {
        private bool _hover;
        private bool _pressed;

        [Category("Product")]
        public string ProductName
        {
            get => lblName.Text;
            set => lblName.Text = value;
        }

        [Category("Product")]
        public string PriceText
        {
            get => lblPrice.Text;
            set => lblPrice.Text = value;
        }

        public ProductButtonControl()
        {
            InitializeComponent();
            Cursor = Cursors.Hand;
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.OptimizedDoubleBuffer, true);

            HookClickForwarding(this);
            HookClickForwarding(lblName);
            HookClickForwarding(lblPrice);
        }

        private void HookClickForwarding(Control c)
        {
            c.MouseEnter += (_, __) => { _hover = true; Invalidate(); };
            c.MouseLeave += (_, __) => { _hover = false; _pressed = false; Invalidate(); };
            c.MouseDown += (_, e) => { if (e.Button == MouseButtons.Left) { _pressed = true; Invalidate(); } };
            c.MouseUp += (_, __) => { _pressed = false; Invalidate(); OnClick(EventArgs.Empty); };
            //c.Click += (_, __) => OnClick(EventArgs.Empty); 
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int offset = _pressed ? 1 : 0;
            var rect = new Rectangle(2 + offset, 2 + offset, Width - 5, Height - 5);

            using var path = CreateRoundRect(rect, 24);

            var c1 = ColorTranslator.FromHtml(_hover ? "#0C1427" : "#0A1020");
            var c2 = ColorTranslator.FromHtml(_hover ? "#15213A" : "#111A2C");
            var border = ColorTranslator.FromHtml(_hover ? "#2B3A59" : "#1E2943");

            using (var bg = new LinearGradientBrush(rect, c1, c2, LinearGradientMode.Horizontal))
                e.Graphics.FillPath(bg, path);

            using (var p = new Pen(border, 1.2f))
                e.Graphics.DrawPath(p, path);
        }

        private static GraphicsPath CreateRoundRect(Rectangle r, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
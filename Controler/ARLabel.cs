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

namespace AlBarakaPOS.Controler
{
    [ToolboxItem(true)]
    [DefaultProperty("HintText")]
    public partial class ARLabel : UserControl
    {
        private int _cornerRadius = 10;
        private Color _startColor = Color.FromArgb(14, 22, 40);   // left
        private Color _endColor = Color.FromArgb(16, 23, 39);     // right
        private Color _borderColor = Color.FromArgb(24, 37, 60);
        private Color _hintColor = Color.FromArgb(143, 123, 81);  // hint like image

        [Category("ARLabel")]
        public string HintText
        {
            get => lblHint.Text;
            set => lblHint.Text = value;
        }

        [Category("ARLabel")]
        public Color HintColor
        {
            get => _hintColor;
            set
            {
                _hintColor = value;
                lblHint.ForeColor = _hintColor;
                Invalidate();
            }
        }

        [Category("ARLabel")]
        public int CornerRadius
        {
            get => _cornerRadius;
            set
            {
                _cornerRadius = Math.Max(1, value);
                Invalidate();
                ApplyRoundedRegion();
            }
        }

        [Category("ARLabel")]
        public Color StartColor
        {
            get => _startColor;
            set { _startColor = value; Invalidate(); }
        }

        [Category("ARLabel")]
        public Color EndColor
        {
            get => _endColor;
            set { _endColor = value; Invalidate(); }
        }

        [Category("ARLabel")]
        public Color BorderColorEx
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }

        [Category("ARLabel")]
        public override string Text
        {
            get => txtValue.Text;
            set
            {
                txtValue.Text = value;
                UpdateHintState();
            }
        }
        public ARLabel()
        {
            InitializeComponent();

            DoubleBuffered = true;
            RightToLeft = RightToLeft.Yes;

            // IBeam cursor everywhere
            Cursor = Cursors.IBeam;
            txtValue.Cursor = Cursors.IBeam;
            lblHint.Cursor = Cursors.IBeam;

            txtValue.ForeColor = Color.White;
            txtValue.BackColor = Color.FromArgb(15, 23, 40);
            lblHint.ForeColor = _hintColor;

            txtValue.TextChanged += (_, _) =>
            {
                UpdateHintState();
                OnTextChanged(EventArgs.Empty);
            };

            txtValue.GotFocus += (_, _) => UpdateHintState();
            txtValue.LostFocus += (_, _) => UpdateHintState();

            // Clicking hint/empty area focuses textbox
            lblHint.Click += (_, _) =>
            {
                txtValue.Focus();
                txtValue.SelectionStart = txtValue.TextLength;
            };

            this.Click += (_, _) =>
            {
                txtValue.Focus();
                txtValue.SelectionStart = txtValue.TextLength;
            };

            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);

            UpdateHintState();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyRoundedRegion();

            int h = txtValue.PreferredHeight;
            txtValue.SetBounds(12, (Height - h) / 2, Math.Max(20, Width - 24), h);
            lblHint.SetBounds(12, 0, Math.Max(20, Width - 24), Height);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

            using GraphicsPath path = BuildRoundPath(rect, _cornerRadius);
            using LinearGradientBrush brush = new LinearGradientBrush(rect, _startColor, _endColor, LinearGradientMode.Horizontal);
            using Pen pen = new Pen(_borderColor, 1f);

            e.Graphics.FillPath(brush, path);
            e.Graphics.DrawPath(pen, path);
        }

        private void UpdateHintState()
        {
            // Show hint only when empty
            lblHint.Visible = string.IsNullOrWhiteSpace(txtValue.Text);
        }

        private void ApplyRoundedRegion()
        {
            if (Width <= 0 || Height <= 0) return;
            using GraphicsPath path = BuildRoundPath(new Rectangle(0, 0, Width, Height), _cornerRadius);
            Region = new Region(path);
        }

        private static GraphicsPath BuildRoundPath(Rectangle r, int radius)
        {
            int rr = Math.Min(radius, Math.Min(r.Width, r.Height) / 2);
            int d = rr * 2;

            GraphicsPath path = new GraphicsPath();
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
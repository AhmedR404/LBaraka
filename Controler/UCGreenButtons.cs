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
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    public partial class UCGreenButtons : Button
    {
        private int _cornerRadius = 12;
        private Color _baseColor = Color.FromArgb(20, 88, 55);       // #145837
        private Color _hoverColor = Color.FromArgb(26, 102, 63);
        private Color _borderColor = Color.FromArgb(20, 88, 55);
        private bool _hovered;

        [Category("Appearance")]
        public int CornerRadius
        {
            get => _cornerRadius;
            set
            {
                _cornerRadius = Math.Max(1, value);
                ApplyRegion();
                Invalidate();
            }
        }

        [Category("Appearance")]
        public Color BaseColor
        {
            get => _baseColor;
            set { _baseColor = value; Invalidate(); }
        }

        [Category("Appearance")]
        public Color HoverColor
        {
            get => _hoverColor;
            set { _hoverColor = value; Invalidate(); }
        }

        [Category("Appearance")]
        public Color BorderColorEx
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }

        public UCGreenButtons()
        {
            Text = "تاكيد الطلب";
            Size = new Size(294, 60);
            Font = new Font("Segoe UI", 16f, FontStyle.Bold);
            ForeColor = Color.FromArgb(180, 220, 200);               // muted mint text
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Cursor = Cursors.Hand;
            RightToLeft = RightToLeft.Yes;

            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            ApplyRegion();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyRegion();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            _hovered = true;
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _hovered = false;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using GraphicsPath path = BuildRoundedPath(rect, CornerRadius);
            using SolidBrush bg = new SolidBrush(_hovered ? HoverColor : BaseColor);
            using Pen border = new Pen(BorderColorEx, 1f);

            pevent.Graphics.FillPath(bg, path);
            pevent.Graphics.DrawPath(border, path);

            TextRenderer.DrawText(
                pevent.Graphics,
                Text,
                Font,
                rect,
                ForeColor,
                TextFormatFlags.HorizontalCenter |
                TextFormatFlags.VerticalCenter |
                TextFormatFlags.RightToLeft);
        }

        private void ApplyRegion()
        {
            if (Width <= 0 || Height <= 0) return;
            using GraphicsPath path = BuildRoundedPath(new Rectangle(0, 0, Width, Height), CornerRadius);
            Region = new Region(path);
        }

        private static GraphicsPath BuildRoundedPath(Rectangle r, int radius)
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

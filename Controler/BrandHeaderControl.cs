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
    public partial class BrandHeaderControl : UserControl
    {
        private bool _dragging;
        private Point _dragOffset;

        public BrandHeaderControl()
        {
            InitializeComponent();

            // Make whole control draggable, including labels
            HookDrag(this);
            HookDrag(lblBrand);
            HookDrag(lblSubtitle);
        }

        private void HookDrag(Control c)
        {
            c.MouseDown += Drag_MouseDown;
            c.MouseMove += Drag_MouseMove;
            c.MouseUp += Drag_MouseUp;
        }

        private void Drag_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            _dragging = true;
            _dragOffset = e.Location;
        }

        private void Drag_MouseMove(object? sender, MouseEventArgs e)
        {
            if (!_dragging) return;
            if (Parent == null) return;

            var src = sender as Control;
            if (src == null) return;

            Point p = Parent.PointToClient(src.PointToScreen(e.Location));
            Left = p.X - _dragOffset.X;
            Top = p.Y - _dragOffset.Y;
        }

        private void Drag_MouseUp(object? sender, MouseEventArgs e)
        {
            _dragging = false;
        }
    }
}

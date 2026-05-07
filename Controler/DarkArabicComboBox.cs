using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlBarakaPOS.Controler
{
    [ToolboxItem(true)]
    [DefaultEvent(nameof(SelectedIndexChanged))]
    public partial class DarkArabicComboBox : UserControl
    {
        private readonly Label _textLabel;
        private readonly Panel _arrowPanel;
        private readonly ListBox _listBox;
        private readonly Form _dropDownForm;

        private readonly List<string> _items = new();
        private int _selectedIndex = -1;

        public event EventHandler? SelectedIndexChanged;

        public DarkArabicComboBox()
        {
            DoubleBuffered = true;
            Size = new Size(320, 50);
            Font = new Font("Segoe UI", 14f, FontStyle.Regular);
            BackColor = Color.Transparent;
            RightToLeft = RightToLeft.No;
            Cursor = Cursors.Hand;

            _textLabel = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = Color.FromArgb(255, 204, 84),
                BackColor = Color.Transparent,
                Padding = new Padding(0, 0, 14, 0),
                Text = "اختر عامل التوصيل"
            };

            _arrowPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 44,
                BackColor = Color.Transparent
            };

            _arrowPanel.Paint += (_, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using var pen = new Pen(Color.WhiteSmoke, 2.2f);
                float cx = _arrowPanel.Width / 2f;
                float cy = _arrowPanel.Height / 2f;

                e.Graphics.DrawLines(pen, new[]
                {
                    new PointF(cx - 6, cy - 2),
                    new PointF(cx,     cy + 4),
                    new PointF(cx + 6, cy - 2)
                });
            };

            Controls.Add(_textLabel);
            Controls.Add(_arrowPanel);

            _listBox = new ListBox
            {
                BorderStyle = BorderStyle.None,
                DrawMode = DrawMode.OwnerDrawFixed,
                ItemHeight = 34,
                Font = new Font("Segoe UI", 12f, FontStyle.Regular),
                BackColor = Color.FromArgb(10, 17, 32),
                ForeColor = Color.WhiteSmoke,
                RightToLeft = RightToLeft.No
            };
            _listBox.DrawItem += ListBox_DrawItem;
            _listBox.Click += (_, __) => CommitSelection();
            _listBox.MouseDoubleClick += (_, __) => CommitSelection();

            _dropDownForm = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.Manual,
                ShowInTaskbar = false,
                TopMost = true,
                BackColor = Color.FromArgb(10, 17, 32),
                Padding = new Padding(1)
            };
            _dropDownForm.Controls.Add(_listBox);
            _listBox.Dock = DockStyle.Fill;
            _dropDownForm.Deactivate += (_, __) => HideDropDown();

            Click += (_, __) => ToggleDropDown();
            _textLabel.Click += (_, __) => ToggleDropDown();
            _arrowPanel.Click += (_, __) => ToggleDropDown();

            Resize += (_, __) => Invalidate();
        }

        [Category("Appearance")]
        [DefaultValue("اختر عامل التوصيل")]
        public string Placeholder { get; set; } = "اختر عامل التوصيل";

        [Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IReadOnlyList<string> Items => _items.AsReadOnly();

        [Category("Data")]
        [DefaultValue(-1)]
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (value < -1 || value >= _items.Count) return;
                _selectedIndex = value;
                _textLabel.Text = _selectedIndex >= 0 ? _items[_selectedIndex] : Placeholder;
                SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
                Invalidate();
            }
        }

        [Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string? SelectedItem => _selectedIndex >= 0 ? _items[_selectedIndex] : null;

        public void SetItems(IEnumerable<string> items)
        {
            _items.Clear();
            _items.AddRange(items);

            _listBox.BeginUpdate();
            _listBox.Items.Clear();
            foreach (var item in _items)
                _listBox.Items.Add(item);
            _listBox.EndUpdate();

            SelectedIndex = -1;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;

            using var path = RoundedRect(rect, 9);
            using var bg = new SolidBrush(Color.FromArgb(11, 18, 34));
            using var border = new Pen(Color.FromArgb(220, 232, 245), 1.4f);

            e.Graphics.FillPath(bg, path);
            e.Graphics.DrawPath(border, path);
        }

        private void ToggleDropDown()
        {
            if (_dropDownForm.Visible) HideDropDown();
            else ShowDropDown();
        }

        private void ShowDropDown()
        {
            if (_items.Count == 0 || FindForm() == null) return;

            Point screenPoint = Parent.PointToScreen(Location);
            int height = Math.Min(220, _items.Count * _listBox.ItemHeight + 6);

            _dropDownForm.Size = new Size(Width, height);
            _dropDownForm.Location = new Point(screenPoint.X, screenPoint.Y + Height + 4);

            _listBox.SelectedIndex = _selectedIndex;
            _dropDownForm.Show();
            _dropDownForm.BringToFront();
        }

        private void HideDropDown()
        {
            if (_dropDownForm.Visible) _dropDownForm.Hide();
        }

        private void CommitSelection()
        {
            if (_listBox.SelectedIndex >= 0)
                SelectedIndex = _listBox.SelectedIndex;

            HideDropDown();
        }

        private void ListBox_DrawItem(object? sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            bool selected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            Color bgColor = selected ? Color.FromArgb(32, 52, 92) : Color.FromArgb(10, 17, 32);

            using var bg = new SolidBrush(bgColor);
            using var fg = new SolidBrush(Color.WhiteSmoke);

            e.Graphics.FillRectangle(bg, e.Bounds);

            var textRect = new Rectangle(e.Bounds.X + 10, e.Bounds.Y, e.Bounds.Width - 20, e.Bounds.Height);
            using var sf = new StringFormat
            {
                Alignment = StringAlignment.Far,
                LineAlignment = StringAlignment.Center
            };

            e.Graphics.DrawString(_items[e.Index], _listBox.Font, fg, textRect, sf);
        }

        private static GraphicsPath RoundedRect(Rectangle rect, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            return path;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dropDownForm.Dispose();
                _listBox.Dispose();
                _textLabel.Dispose();
                _arrowPanel.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
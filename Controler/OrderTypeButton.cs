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
    public partial class OrderTypeButton : Button
    {
        private int _cornerRadius = 10;
        private bool _isActive = true;

        [Category("Appearance")]
        [Description("Rounded corner radius.")]
        public int CornerRadius
        {
            get => _cornerRadius;
            set
            {
                _cornerRadius = Math.Max(1, value);
                ApplyRoundedRegion();
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("Active style (gold) or normal style (dark).")]
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                ApplyVisualStyle();
                Invalidate();
            }
        }

        public OrderTypeButton()
        {
            AutoSize = false;
            Size = new Size(110, 40);
            Text = "داخلي";
            Font = new Font("Segoe UI", 12f, FontStyle.Bold);
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 1;
            RightToLeft = RightToLeft.Yes;
            Cursor = Cursors.Hand;

            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);

            ApplyVisualStyle();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            ApplyRoundedRegion();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyRoundedRegion();
        }

        private void ApplyVisualStyle()
        {
            if (_isActive)
            {
                BackColor = ColorTranslator.FromHtml("#6B5320");
                ForeColor = ColorTranslator.FromHtml("#E8BD63");
                FlatAppearance.BorderColor = ColorTranslator.FromHtml("#6B5320");
            }
            else
            {
                BackColor = ColorTranslator.FromHtml("#071022");
                ForeColor = ColorTranslator.FromHtml("#EDEFF6");
                FlatAppearance.BorderColor = ColorTranslator.FromHtml("#27324A");
            }
        }

        private void ApplyRoundedRegion()
        {
            if (Width <= 0 || Height <= 0) return;

            int r = Math.Min(_cornerRadius, Math.Min(Width, Height) / 2);
            int d = r * 2;

            Rectangle rect = new Rectangle(0, 0, Width, Height);
            using GraphicsPath path = new GraphicsPath();

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            Region = new Region(path);
        }
    }

}


namespace WinFormsOrderTypeDemo
{



    //public partial class UserControllerButtons : UserControl
    //{
    //    public enum OrderType
    //    {
    //        DineIn,
    //        TakeAway,
    //        Delivery
    //    }

    //    private OrderType _selectedType = OrderType.DineIn;

    //    public OrderType SelectedType
    //    {
    //        get => _selectedType;
    //        set
    //        {
    //            _selectedType = value;
    //            ApplySelectionStyle();
    //        }
    //    }

    //    public event EventHandler<OrderType>? SelectionChanged;

    //    public UserControllerButtons()
    //    {
    //        InitializeComponent();

    //        // RTL for Arabic layout
    //        RightToLeft = RightToLeft.Yes;

    //        // Button click wiring
    //        btnDineIn.Click += (_, _) => SetSelection(OrderType.DineIn);
    //        btnTakeAway.Click += (_, _) => SetSelection(OrderType.TakeAway);
    //        btnDelivery.Click += (_, _) => SetSelection(OrderType.Delivery);

    //        // Rounded corners when control loads/resizes
    //        Load += (_, _) => ApplyRoundedCorners();
    //        Resize += (_, _) => ApplyRoundedCorners();

    //        ApplySelectionStyle();
    //    }

    //    private void SetSelection(OrderType type)
    //    {
    //        if (_selectedType == type) return;
    //        _selectedType = type;
    //        ApplySelectionStyle();
    //        SelectionChanged?.Invoke(this, _selectedType);
    //    }

    //    private void ApplySelectionStyle()
    //    {
    //        StyleButton(btnDineIn, _selectedType == OrderType.DineIn);
    //        StyleButton(btnTakeAway, _selectedType == OrderType.TakeAway);
    //        StyleButton(btnDelivery, _selectedType == OrderType.Delivery);
    //    }

    //    private static void StyleButton(Button button, bool active)
    //    {
    //        if (active)
    //        {
    //            button.BackColor = ColorTranslator.FromHtml("#6B5320");
    //            button.ForeColor = ColorTranslator.FromHtml("#E8BD63");
    //            button.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#6B5320");
    //        }
    //        else
    //        {
    //            button.BackColor = ColorTranslator.FromHtml("#071022");
    //            button.ForeColor = ColorTranslator.FromHtml("#EDEFF6");
    //            button.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#27324A");
    //        }
    //    }

    //    private void ApplyRoundedCorners()
    //    {
    //        SetRoundRegion(btnDineIn, 10);
    //        SetRoundRegion(btnTakeAway, 10);
    //        SetRoundRegion(btnDelivery, 10);
    //    }

    //    private static void SetRoundRegion(Control c, int radius)
    //    {
    //        int d = radius * 2;
    //        Rectangle r = new Rectangle(0, 0, c.Width, c.Height);

    //        using GraphicsPath path = new GraphicsPath();
    //        path.AddArc(r.X, r.Y, d, d, 180, 90);
    //        path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
    //        path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
    //        path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
    //        path.CloseFigure();

    //        c.Region = new Region(path);
    //    }
    //}
}
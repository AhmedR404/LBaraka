using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace AlBarakaPOS.Controler
{
    [ToolboxItem(true)]
    [DefaultEvent(nameof(TotalsChanged))]
    public partial class InvoicePanelControl : UserControl
    {
        private sealed class InvoiceItem
        {
            public string Name { get; set; } = "";
            public decimal Price { get; set; }
            public int Qty { get; set; } = 1;
            public decimal LineTotal => Price * Qty;
        }

        private readonly List<InvoiceItem> _items = new();

        private readonly FlowLayoutPanel _itemsHost;
        private readonly Label _emptyLabel;

        private readonly Label _lblSubtotalValue;
        private readonly TextBox _txtDiscount;
        private readonly TextBox _txtService;
        private readonly Label _lblRestaurantTotalValue;
        private readonly Label _lblCustomerTotalValue;

        public event EventHandler? TotalsChanged;

        public InvoicePanelControl()
        {
            RightToLeft = RightToLeft.No;
            Font = new Font("Segoe UI", 11f, FontStyle.Regular);
            BackColor = Color.FromArgb(3, 10, 23);
            Size = new Size(350, 350);

            var outer = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                BackColor = Color.FromArgb(2, 9, 19)
            };
            Controls.Add(outer);

            var main = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                BackColor = Color.FromArgb(2, 10, 24)
            };
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 68));
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 32));
            outer.Controls.Add(main);

            var itemsContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(2, 10, 24),
                Padding = new Padding(8)
            };

            _itemsHost = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = Color.FromArgb(2, 10, 24)
            };

            _emptyLabel = new Label
            {
                Dock = DockStyle.Fill,
                Text = "اختر منتجا من القائمه",
                ForeColor = Color.FromArgb(158, 133, 93),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 24f, FontStyle.Regular),
                BackColor = Color.Transparent
            };

            itemsContainer.Controls.Add(_itemsHost);
            itemsContainer.Controls.Add(_emptyLabel);
            main.Controls.Add(itemsContainer, 0, 0);

            var summary = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 5,
                BackColor = Color.FromArgb(2, 10, 24),
                Padding = new Padding(0, 8, 0, 0)
            };
            summary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45)); // value
            summary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55)); // label
            for (int i = 0; i < 5; i++) summary.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));

            _lblSubtotalValue = CreateValueLabel();
            _txtDiscount = CreateInput();
            _txtService = CreateInput();
            _lblRestaurantTotalValue = CreateValueLabel(Color.FromArgb(255, 207, 78), true);
            _lblCustomerTotalValue = CreateValueLabel(Color.FromArgb(120, 182, 255), false);

            _txtDiscount.TextChanged += (_, __) => RefreshTotals();
            _txtService.TextChanged += (_, __) => RefreshTotals();
            _txtDiscount.Text = "0";
            _txtService.Text = "0";

            AddRow(summary, 0, _lblSubtotalValue, CreateLabel("المجموع", Color.FromArgb(255, 226, 120)));
            AddRow(summary, 1, _txtDiscount, CreateLabel("خصم", Color.FromArgb(255, 245, 214)));
            AddRow(summary, 2, _txtService, CreateLabel("خدمة", Color.FromArgb(255, 245, 214)));
            AddRow(summary, 3, _lblRestaurantTotalValue, CreateLabel("إجمالي المطعم", Color.FromArgb(255, 207, 78), true));
            AddRow(summary, 4, _lblCustomerTotalValue, CreateLabel("إجمالي العميل", Color.FromArgb(120, 182, 255)));

            main.Controls.Add(summary, 0, 1);

            RefreshUI();
        }

        [Browsable(false)]
        public decimal Subtotal => _items.Sum(x => x.LineTotal);

        [Browsable(false)]
        public decimal Discount => ParseMoney(_txtDiscount.Text);

        [Browsable(false)]
        public decimal Service => ParseMoney(_txtService.Text);

        [Browsable(false)]
        public decimal RestaurantTotal => Math.Max(0m, Subtotal - Discount + Service);

        [Browsable(false)]
        public decimal CustomerTotal => RestaurantTotal;

        public void AddOrIncrementItem(string name, decimal price)
        {
            var existing = _items.FirstOrDefault(x => x.Name == name && x.Price == price);
            if (existing != null) existing.Qty++;
            else _items.Add(new InvoiceItem { Name = name, Price = price, Qty = 1 });

            RefreshUI();
        }

        public void RemoveItem(string name, decimal price)
        {
            var item = _items.FirstOrDefault(x => x.Name == name && x.Price == price);
            if (item == null) return;
            _items.Remove(item);
            RefreshUI();
        }

        public void ClearInvoice()
        {
            _items.Clear();
            RefreshUI();
        }

        private void RefreshUI()
        {
            _itemsHost.SuspendLayout();
            _itemsHost.Controls.Clear();

            foreach (var item in _items)
            {
                _itemsHost.Controls.Add(CreateItemRow(item));
            }

            _itemsHost.ResumeLayout();

            _emptyLabel.Visible = _items.Count == 0;
            RefreshTotals();
        }

        private void RefreshTotals()
        {
            _lblSubtotalValue.Text = FormatMoney(Subtotal);
            _lblRestaurantTotalValue.Text = FormatMoney(RestaurantTotal);
            _lblCustomerTotalValue.Text = FormatMoney(CustomerTotal);
            TotalsChanged?.Invoke(this, EventArgs.Empty);
        }

        private Control CreateItemRow(InvoiceItem item)
        {
            var row = new Panel
            {
                Width = Math.Max(280, _itemsHost.ClientSize.Width - 24),
                Height = 90,
                Margin = new Padding(0, 0, 0, 8),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(4, 13, 31)
            };

            var btnDelete = new LinkLabel
            {
                Text = "حذف",
                LinkColor = Color.FromArgb(255, 88, 99),
                ActiveLinkColor = Color.FromArgb(255, 125, 135),
                VisitedLinkColor = Color.FromArgb(255, 88, 99),
                AutoSize = true,
                Location = new Point(10, 10)
            };
            btnDelete.Click += (_, __) =>
            {
                _items.Remove(item);
                RefreshUI();
            };

            var lblName = new Label
            {
                Text = item.Name,
                ForeColor = Color.FromArgb(255, 240, 212),
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(row.Width - 160, 8)
            };

            var lblPrice = new Label
            {
                Text = $"{item.Qty} x {item.Price:0.00} ج",
                ForeColor = Color.FromArgb(255, 214, 130),
                Font = new Font("Segoe UI", 13f, FontStyle.Regular),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(row.Width - 180, 48)
            };

            var btnPlus = new Button
            {
                Text = "+",
                Width = 30,
                Height = 30,
                BackColor = Color.FromArgb(41, 56, 85),
                ForeColor = Color.FromArgb(255, 228, 145),
                FlatStyle = FlatStyle.Flat,
                Location = new Point(10, 46)
            };
            btnPlus.FlatAppearance.BorderSize = 0;
            btnPlus.Click += (_, __) =>
            {
                item.Qty++;
                RefreshUI();
            };

            var btnMinus = new Button
            {
                Text = "-",
                Width = 30,
                Height = 30,
                BackColor = Color.FromArgb(41, 56, 85),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(46, 46)
            };
            btnMinus.FlatAppearance.BorderSize = 0;
            btnMinus.Click += (_, __) =>
            {
                item.Qty = Math.Max(1, item.Qty - 1);
                RefreshUI();
            };

            row.Controls.Add(btnDelete);
            row.Controls.Add(lblName);
            row.Controls.Add(lblPrice);
            row.Controls.Add(btnPlus);
            row.Controls.Add(btnMinus);

            return row;
        }

        private static void AddRow(TableLayoutPanel table, int rowIndex, Control leftValue, Control rightLabel)
        {
            leftValue.Dock = DockStyle.Fill;
            rightLabel.Dock = DockStyle.Fill;
            table.Controls.Add(leftValue, 0, rowIndex);
            table.Controls.Add(rightLabel, 1, rowIndex);
        }

        private static Label CreateLabel(string text, Color color, bool bold = false)
        {
            return new Label
            {
                Text = text,
                ForeColor = color,
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Segoe UI", bold ? 15f : 14f, bold ? FontStyle.Bold : FontStyle.Regular),
                Margin = new Padding(0)
            };
        }

        private static Label CreateValueLabel(Color? color = null, bool bold = false)
        {
            return new Label
            {
                Text = "ج 0.00",
                ForeColor = color ?? Color.FromArgb(255, 234, 174),
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", bold ? 15f : 14f, bold ? FontStyle.Bold : FontStyle.Regular),
                Margin = new Padding(0)
            };
        }

        private static TextBox CreateInput()
        {
            return new TextBox
            {
                Text = "0",
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(16, 26, 48),
                ForeColor = Color.FromArgb(255, 233, 174),
                Font = new Font("Segoe UI", 13f, FontStyle.Regular),
                TextAlign = HorizontalAlignment.Center,
                Margin = new Padding(0)
            };
        }

        private static decimal ParseMoney(string text)
        {
            if (decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out var v)) return v;
            if (decimal.TryParse(text, out v)) return v;
            return 0m;
        }

        private static string FormatMoney(decimal v) => $"ج {v:0.00}";
    }
}

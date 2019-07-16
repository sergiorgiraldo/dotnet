using System;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Ruler
{
    public sealed class Form1 : Form
    {
        private enum ResizeRegion
        {
            None = 0,
            N    = 1,
            NE   = 2,
            E    = 3,
            SE   = 4,
            S    = 5,
            SW   = 6,
            W    = 7,
            NW
        }

        private enum Unit
        {
            Pixels = 0,
            Cm = 1
        }

        private const int _RESIZE_BORDER_WIDTH = 5;

        private readonly ToolTip _toolTip = new ToolTip();
        private Point _offset;
        private Rectangle _mouseDownRect;
        private Point _mouseDownPoint;
        private ResizeRegion _resizeRegion = ResizeRegion.None;
        private readonly ContextMenu _menu = new ContextMenu();
        private MenuItem _verticalMenuItem;
        private MenuItem _toolTipMenuItem;
        private ColorDialog colorDialog1;
        private MenuItem _lockedMenuItem;
        private float HRes;
        private float VRes;
        private Unit unitForMeasure = Unit.Pixels;

        public Form1()
        {
            Graphics g = this.CreateGraphics();
            HRes = g.DpiX; // Horizontal Resolution
            VRes = g.DpiY; // Vertical Resolution

            SetStyle(ControlStyles.ResizeRedraw, true);
            UpdateStyles();

            var resources = new ResourceManager(typeof (Form1));
            Icon = ((Icon) (resources.GetObject("$this.Icon")));

            SetUpMenu();

            Text = "Ruler";
            BackColor = Color.White;
            ClientSize = new Size(400, 75);
            FormBorderStyle = FormBorderStyle.None;
            Opacity = 0.65;
            ContextMenu = _menu;
            Font = new Font("Tahoma", 10);

            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
        }

        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private bool IsVertical
        {
            get { return _verticalMenuItem.Checked; }
            set { _verticalMenuItem.Checked = value; }
        }

        private bool ShowToolTip
        {
            get { return _toolTipMenuItem.Checked; }
            set
            {
                _toolTipMenuItem.Checked = value;
                if (value)
                {
                    SetToolTip();
                }
            }
        }

        private bool IsLocked
        {
            get { return _lockedMenuItem.Checked; }
            set
            {
                _lockedMenuItem.Checked = value;

                if (value)
                {
                    MinimumSize = Size;
                    MaximumSize = Size;
                }
                else
                {
                    MinimumSize = Size.Empty;
                    MaximumSize = Size.Empty;
                }
            }
        }

        private void ChangeColor()
        {
            using (colorDialog1 = new ColorDialog())
            {
                colorDialog1.Color = BackColor;

                DialogResult result = colorDialog1.ShowDialog();

                if (result == DialogResult.OK)
                {
                    BackColor = colorDialog1.Color;
                }
            }
        }

        private void ChangeOrientation()
        {
            IsVertical = !IsVertical;
            int width = Width;
            Width = Height;
            Height = width;
        }

        private void SetUpMenu()
        {
            AddMenuItem("Stay On Top", Shortcut.CtrlS);
            AddMenuItem("Change Color", Shortcut.CtrlC);
            _lockedMenuItem = AddMenuItem("Lock Size", Shortcut.CtrlL);
            _verticalMenuItem = AddMenuItem("Vertical", Shortcut.CtrlV);
            _toolTipMenuItem = AddMenuItem("Tool Tip", Shortcut.CtrlT);
            MenuItem opacityMenuItem = AddMenuItem("Opacity");
            MenuItem unitMenuItem = AddMenuItem("Unit");
            AddMenuItem("-");
            AddMenuItem("About", Shortcut.CtrlA);
            AddMenuItem("-");
            AddMenuItem("Exit", Shortcut.CtrlX);
            
            var subMenuPixel = new MenuItem("Pixel");
            subMenuPixel.Click += UnitForMeasureMenuHandler;
            unitMenuItem.MenuItems.Add(subMenuPixel);
            var subMenuCentimeter = new MenuItem("Cm");
            subMenuCentimeter.Click += UnitForMeasureMenuHandler;
            unitMenuItem.MenuItems.Add(subMenuCentimeter);
            
            for (int i = 10; i <= 100; i += 10)
            {
                var subMenu = new MenuItem(i + "%");
                switch (i)
                {
                    case 10: 
                        subMenu.Shortcut = Shortcut.Ctrl1;
                        break;
                    case 20:
                        subMenu.Shortcut = Shortcut.Ctrl2;
                        break;
                    case 30:
                        subMenu.Shortcut = Shortcut.Ctrl3;
                        break;
                    case 40:
                        subMenu.Shortcut = Shortcut.Ctrl4;
                        break;
                    case 50:
                        subMenu.Shortcut = Shortcut.Ctrl5;
                        break;
                    case 60:
                        subMenu.Shortcut = Shortcut.Ctrl6;
                        break;
                    case 70:
                        subMenu.Shortcut = Shortcut.Ctrl7;
                        break;
                    case 80:
                        subMenu.Shortcut = Shortcut.Ctrl8;
                        break;
                    case 90:
                        subMenu.Shortcut = Shortcut.Ctrl9;
                        break;
                    case 100:
                        subMenu.Shortcut = Shortcut.Ctrl0;
                        break;
                }
                
                subMenu.Click += OpacityMenuHandler;
                opacityMenuItem.MenuItems.Add(subMenu);
            }
        }

        private MenuItem AddMenuItem(string text)
        {
            return AddMenuItem(text, Shortcut.None);
        }

        private MenuItem AddMenuItem(string text, Shortcut shortcut)
        {
            var mi = new MenuItem(text);
            mi.Click += MenuHandler;
            mi.Shortcut = shortcut;
            _menu.MenuItems.Add(mi);

            return mi;
        }

        private void MenuHandler(object sender, EventArgs e)
        {
            var mi = (MenuItem)sender;

            switch (mi.Text)
            {
                case "Lock Size":
                    IsLocked = !IsLocked;
                    break;

                case "Exit":
                    Close();
                    break;

                case "Tool Tip":
                    ShowToolTip = !ShowToolTip;
                    break;

                case "Change Color":
                    ChangeColor();
                    break;

                case "Vertical":
                    ChangeOrientation();
                    break;

                case "Stay On Top":
                    mi.Checked = !mi.Checked;
                    TopMost = mi.Checked;
                    break;

                case "About":
                    string message =
                        string.Format(
                            "Ruler v{0} by SERGIORGIRALDO\nAfter original idea from Jeff Key\nIcon by Kristen Magee @ www.kbecca.com",
                            Application.ProductVersion);
                    MessageBox.Show(message, "About Ruler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                default:
                    MessageBox.Show("Unknown menu item.");
                    break;
            }
        }

        private void SetToolTip()
        {
            var toolTip = string.Empty;

            if (unitForMeasure == Unit.Pixels)
                toolTip = "Width: {0} pixels\nHeight: {1} pixels";
            if (unitForMeasure == Unit.Cm)
                toolTip = "Width: {0} cm\nHeight: {1} cm";

            _toolTip.SetToolTip(this, string.Format(toolTip, GetWidth(Width), GetHeight(Height)));
        }

        private int GetWidthForTicks(int width)
        {
            if (unitForMeasure == Unit.Pixels)
                return width;
            if (unitForMeasure == Unit.Cm)
                return (Int32)Math.Ceiling((width / HRes) * 2.54) * 10;
            throw new Exception("ERRO");
        }
        
        private string GetWidth(int width)
        {
            if (unitForMeasure == Unit.Pixels)
                return width.ToString();
            if (unitForMeasure == Unit.Cm)
                return String.Format("{0:0.0}", (width / HRes) * 2.54);

            return "-1";
        }

        private string GetHeight(int height)
        {
            if (unitForMeasure == Unit.Pixels)
                return height.ToString();
            if (unitForMeasure == Unit.Cm)
                return String.Format("{0:0.0}", (height / VRes) * 2.54);

            return "-1";
        }

        private void HandleMoveResizeKeystroke(KeyEventArgs e)
        {
            {
                if (e.KeyCode == Keys.Right)
                {
                    if (e.Control)
                    {
                        if (e.Shift)
                        {
                            Width += 1;
                        }
                        else
                        {
                            Left += 1;
                        }
                    }
                    else
                    {
                        Left += 5;
                    }
                }
                else if (e.KeyCode == Keys.Left)
                {
                    if (e.Control)
                    {
                        if (e.Shift)
                        {
                            Width -= 1;
                        }
                        else
                        {
                            Left -= 1;
                        }
                    }
                    else
                    {
                        Left -= 5;
                    }
                }
                else if (e.KeyCode == Keys.Up)
                {
                    if (e.Control)
                    {
                        if (e.Shift)
                        {
                            Height -= 1;
                        }
                        else
                        {
                            Top -= 1;
                        }
                    }
                    else
                    {
                        Top -= 5;
                    }
                }
                else if (e.KeyCode == Keys.Down)
                {
                    if (e.Control)
                    {
                        if (e.Shift)
                        {
                            Height += 1;
                        }
                        else
                        {
                            Top += 1;
                        }
                    }
                    else
                    {
                        Top += 5;
                    }
                }
            }
        }

        private void HandleResize()
        {
            switch (_resizeRegion)
            {
                case ResizeRegion.E:
                    {
                        int diff = MousePosition.X - _mouseDownPoint.X;
                        Width = _mouseDownRect.Width + diff;
                        break;
                    }
                case ResizeRegion.S:
                    {
                        int diff = MousePosition.Y - _mouseDownPoint.Y;
                        Height = _mouseDownRect.Height + diff;
                        break;
                    }
                case ResizeRegion.SE:
                    {
                        Width = _mouseDownRect.Width + MousePosition.X - _mouseDownPoint.X;
                        Height = _mouseDownRect.Height + MousePosition.Y - _mouseDownPoint.Y;
                        break;
                    }
            }
        }

        private void SetResizeCursor(ResizeRegion region)
        {
            switch (region)
            {
                case ResizeRegion.N:
                case ResizeRegion.S:
                    Cursor = Cursors.SizeNS;
                    break;

                case ResizeRegion.E:
                case ResizeRegion.W:
                    Cursor = Cursors.SizeWE;
                    break;

                case ResizeRegion.NW:
                case ResizeRegion.SE:
                    Cursor = Cursors.SizeNWSE;
                    break;

                default:
                    Cursor = Cursors.SizeNESW;
                    break;
            }
        }

        private ResizeRegion GetResizeRegion(Point clientCursorPos)
        {
            if (clientCursorPos.Y <= _RESIZE_BORDER_WIDTH)
            {
                if (clientCursorPos.X <= _RESIZE_BORDER_WIDTH) return ResizeRegion.NW;
                if (clientCursorPos.X >= Width - _RESIZE_BORDER_WIDTH) return ResizeRegion.NE;
                return ResizeRegion.N;
            }
            if (clientCursorPos.Y >= Height - _RESIZE_BORDER_WIDTH)
            {
                if (clientCursorPos.X <= _RESIZE_BORDER_WIDTH) return ResizeRegion.SW;
                if (clientCursorPos.X >= Width - _RESIZE_BORDER_WIDTH) return ResizeRegion.SE;
                return ResizeRegion.S;
            }
            if (clientCursorPos.X <= _RESIZE_BORDER_WIDTH) return ResizeRegion.W;
            return ResizeRegion.E;
        }

        private void DrawRuler(Graphics g, int formWidth, int formHeight)
        {
            // Border
            g.DrawRectangle(Pens.Black, 0, 0, formWidth - 1, formHeight - 1);

            // Width
            g.DrawString(GetWidth(formWidth) + " " + unitForMeasure, Font, Brushes.Black, 10, (formHeight / 2) - (Font.Height / 2));

            // Ticks
            if (unitForMeasure == Unit.Cm)
            {
                int OneCmInPixels = (int)Math.Ceiling(HRes/2.54);
                int OneMmInPixels = (int)Math.Ceiling(HRes / 25.4) ;
                for (int i = 0; i < formWidth; i++)
                {
                    int tickHeight;
                    if (i % OneCmInPixels == 0)
                    {
                        tickHeight = 15;
                        DrawTickLabel(g, (i / OneCmInPixels).ToString() , i, formHeight, tickHeight);
                    }
                    else
                    {
                        tickHeight = 5;
                    }
                    if (i % OneMmInPixels == 0 || i % OneCmInPixels == 0)
                    {
                        DrawTick(g, i, formHeight, tickHeight);
                    }
                }
            }
            else
                for (int i = 0; i < GetWidthForTicks(formWidth); i++)
                {
                    if (i % 2 == 0)
                    {
                        int tickHeight;
                        if (i % 100 == 0)
                        {
                            tickHeight = 15;
                            DrawTickLabel(g, i.ToString(), i, formHeight, tickHeight);
                        }
                        else if (i % 10 == 0)
                        {
                            tickHeight = 10;
                        }
                        else
                        {
                            tickHeight = 5;
                        }

                        DrawTick(g, i, formHeight, tickHeight);
                    }
                }
        }

        private static void DrawTick(Graphics g, int xPos, int formHeight, int tickHeight)
        {
            // Top
            g.DrawLine(Pens.Black, xPos, 0, xPos, tickHeight);

            // Bottom
            g.DrawLine(Pens.Black, xPos, formHeight, xPos, formHeight - tickHeight);
        }

        private void DrawTickLabel(Graphics g, string text, int xPos, int formHeight, int height)
        {
            // Top
            g.DrawString(text, Font, Brushes.Black, xPos, height);

            // Bottom
            g.DrawString(text, Font, Brushes.Black, xPos, formHeight - height - Font.Height);
        }

        private void OpacityMenuHandler(object sender, EventArgs e)
        {
            var mi = (MenuItem) sender;
            Opacity = double.Parse(mi.Text.Replace("%", string.Empty))/100;
        }

        private void UnitForMeasureMenuHandler(object sender, EventArgs e)
        {
            var mi = (MenuItem)sender;

            if (mi.Text.Equals("Pixel"))
                unitForMeasure = Unit.Pixels;
            if (mi.Text.Equals("Cm"))
                unitForMeasure = Unit.Cm;

            OnResize(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _offset = new Point(MousePosition.X - Location.X, MousePosition.Y - Location.Y);
            _mouseDownPoint = MousePosition;
            _mouseDownRect = ClientRectangle;

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _resizeRegion = ResizeRegion.None;
            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_resizeRegion != ResizeRegion.None)
            {
                HandleResize();
                return;
            }

            Point clientCursorPos = PointToClient(MousePosition);
            Rectangle resizeInnerRect = ClientRectangle;
            resizeInnerRect.Inflate(-_RESIZE_BORDER_WIDTH, -_RESIZE_BORDER_WIDTH);

            bool inResizableArea = ClientRectangle.Contains(clientCursorPos) &&
                                   !resizeInnerRect.Contains(clientCursorPos);

            if (inResizableArea)
            {
                ResizeRegion resizeRegion = GetResizeRegion(clientCursorPos);
                SetResizeCursor(resizeRegion);

                if (e.Button == MouseButtons.Left)
                {
                    _resizeRegion = resizeRegion;
                    HandleResize();
                }
            }
            else
            {
                Cursor = Cursors.Default;

                if (e.Button == MouseButtons.Left)
                {
                    Location = new Point(MousePosition.X - _offset.X, MousePosition.Y - _offset.Y);
                }
            }

            base.OnMouseMove(e);
        }

        protected override void OnResize(EventArgs e)
        {
            if (ShowToolTip)
            {
                SetToolTip();
            }

            base.OnResize(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                    HandleMoveResizeKeystroke(e);
                    break;

                case Keys.Space:
                    ChangeOrientation();
                    break;
            }

            base.OnKeyDown(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            int height = Height;
            int width = Width;

            if (IsVertical)
            {
                graphics.RotateTransform(90);
                graphics.TranslateTransform(0, -Width + 1);
                height = Width;
                width = Height;
            }

            DrawRuler(graphics, width, height);

            base.OnPaint(e);
        }
    }
}
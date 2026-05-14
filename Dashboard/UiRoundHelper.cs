using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace AlagaTrackFrontEnd
{
    internal static class UiRoundHelper
    {
        internal static bool IsInDesignMode(Control control)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return true;

            while (control != null)
            {
                if (control.Site != null && control.Site.DesignMode) return true;
                control = control.Parent;
            }

            return false;
        }

        internal static void ApplyRoundedTheme(Control root)
        {
            foreach (Control control in root.Controls)
            {
                if (control is Panel)
                {
                    SetRound(control, 18);
                }
                else if (control is Button)
                {
                    SetRound(control, 14);
                }
                else if (control is TextBox)
                {
                    SetRound(control, 12);
                }

                if (control.HasChildren)
                {
                    ApplyRoundedTheme(control);
                }
            }
        }

        internal static void ConfigureTableBehavior(Control root)
        {
            foreach (DataGridView grid in FindAllControls<DataGridView>(root))
            {
                grid.BorderStyle = BorderStyle.None;
                grid.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                grid.MultiSelect = false;
                grid.StandardTab = false;
                grid.EnableHeadersVisualStyles = false;

                // Keep text readable even when custom themes/style overrides apply.
                grid.DefaultCellStyle.BackColor = Color.White;
                grid.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
                grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
                grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 64, 175);
                grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
                grid.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
                grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(55, 65, 81);

                grid.KeyDown -= Grid_KeyDownTopBottom;
                grid.KeyDown += Grid_KeyDownTopBottom;
            }
        }

        internal static void DrawCardShadow(Panel panel, PaintEventArgs e)
        {
            if (panel == null || e == null || panel.Width < 24 || panel.Height < 24) return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.Clear(panel.Parent != null ? panel.Parent.BackColor : SystemColors.Control);

            // Draw a subtle offset shadow first, then draw the card body on top.
            Rectangle shadowRect = new Rectangle(6, 6, panel.Width - 10, panel.Height - 10);
            Rectangle cardRect = new Rectangle(0, 0, panel.Width - 10, panel.Height - 10);

            using (GraphicsPath shadowPath = BuildRoundedPath(shadowRect, 16))
            using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(45, 0, 0, 0)))
            {
                e.Graphics.FillPath(shadowBrush, shadowPath);
            }

            using (GraphicsPath cardPath = BuildRoundedPath(cardRect, 16))
            using (SolidBrush cardBrush = new SolidBrush(Color.WhiteSmoke))
            using (Pen borderPen = new Pen(Color.FromArgb(225, 225, 225), 1))
            {
                e.Graphics.FillPath(cardBrush, cardPath);
                e.Graphics.DrawPath(borderPen, cardPath);
            }
        }

        private static void Grid_KeyDownTopBottom(object sender, KeyEventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            if (grid == null || grid.CurrentCell == null || e.KeyCode != Keys.Enter) return;

            e.Handled = true;
            e.SuppressKeyPress = true;

            int rowDelta = e.Shift ? -1 : 1;
            int nextRowIndex = grid.CurrentCell.RowIndex + rowDelta;
            if (nextRowIndex < 0 || nextRowIndex >= grid.Rows.Count) return;

            int currentColumn = grid.CurrentCell.ColumnIndex;
            grid.CurrentCell = grid.Rows[nextRowIndex].Cells[currentColumn];
        }

        private static System.Collections.Generic.IEnumerable<T> FindAllControls<T>(Control parent) where T : Control
        {
            foreach (Control control in parent.Controls)
            {
                if (control is T tControl) yield return tControl;

                foreach (T child in FindAllControls<T>(control))
                {
                    yield return child;
                }
            }
        }

        private static void SetRound(Control control, int radius)
        {
            if (control.Width <= 0 || control.Height <= 0) return;

            using (GraphicsPath path = new GraphicsPath())
            {
                int d = radius * 2;
                Rectangle r = new Rectangle(0, 0, control.Width, control.Height);
                path.AddArc(r.X, r.Y, d, d, 180, 90);
                path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
                path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
                path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
                path.CloseFigure();
                control.Region = new Region(path);
            }
        }

        private static GraphicsPath BuildRoundedPath(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;

            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
}

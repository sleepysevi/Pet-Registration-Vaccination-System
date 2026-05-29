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

        internal static void ApplyDataViewChrome(Form form)
        {
            form.BackColor = UiTheme.AppBackground;

            foreach (Panel card in FindAllControls<Panel>(form).Where(p =>
                p.Name.StartsWith("card", System.StringComparison.OrdinalIgnoreCase)))
                card.BackColor = UiTheme.Surface;

            foreach (Panel bar in FindAllControls<Panel>(form).Where(p =>
                p.Name.Equals("actionBar", System.StringComparison.OrdinalIgnoreCase)))
                bar.BackColor = UiTheme.Surface;

            foreach (Panel table in FindAllControls<Panel>(form).Where(p =>
                p.Name.Equals("tablePanel", System.StringComparison.OrdinalIgnoreCase)))
                table.BackColor = UiTheme.Surface;

            foreach (Button btn in FindAllControls<Button>(form))
            {
                if (!btn.Name.StartsWith("btn", System.StringComparison.OrdinalIgnoreCase)) continue;
                if (btn.Name.Contains("Add", System.StringComparison.OrdinalIgnoreCase)
                    || btn.Name.Contains("Report", System.StringComparison.OrdinalIgnoreCase))
                    btn.BackColor = UiTheme.ActionSuccess;
                else if (btn.Name.Contains("Delete", System.StringComparison.OrdinalIgnoreCase)
                    || btn.Name.Contains("Close", System.StringComparison.OrdinalIgnoreCase)
                    || btn.Name.Contains("Reset", System.StringComparison.OrdinalIgnoreCase))
                    btn.BackColor = UiTheme.ActionDanger;
                else if (btn.Name.Contains("Refresh", System.StringComparison.OrdinalIgnoreCase))
                    btn.BackColor = UiTheme.ActionNeutral;
                StyleActionButton(btn);
            }

            foreach (TextBox search in FindAllControls<TextBox>(form).Where(t =>
                t.Name.Contains("Search", System.StringComparison.OrdinalIgnoreCase)))
                DecorateSearchBox(search);

            ConfigureTableBehavior(form);
        }

        internal static void StyleActionButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btn.ForeColor = UiTheme.TextOnPrimary;
            btn.Height = UiTheme.ActionButtonHeight;
            btn.Padding = new Padding(12, 0, 12, 0);
            btn.Cursor = Cursors.Hand;
            SetRound(btn, UiTheme.ButtonRadius);
        }

        internal static void ConfigureTableBehavior(Control root)
        {
            foreach (DataGridView grid in FindAllControls<DataGridView>(root))
            {
                grid.BorderStyle = BorderStyle.None;
                grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                grid.GridColor = UiTheme.TableGridLine;
                grid.BackgroundColor = UiTheme.Surface;
                grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                grid.MultiSelect = false;
                grid.EnableHeadersVisualStyles = false;
                grid.RowHeadersVisible = false;
                grid.AllowUserToResizeRows = false;
                grid.AllowUserToAddRows = false;
                grid.RowTemplate.Height = 36;
                grid.ColumnHeadersHeight = 40;
                grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

                grid.DefaultCellStyle.BackColor = UiTheme.Surface;
                grid.DefaultCellStyle.ForeColor = UiTheme.TextPrimary;
                grid.DefaultCellStyle.SelectionBackColor = UiTheme.TableSelectionBg;
                grid.DefaultCellStyle.SelectionForeColor = UiTheme.TableSelectionFg;
                grid.DefaultCellStyle.Padding = new Padding(8, 4, 8, 4);
                grid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                grid.AlternatingRowsDefaultCellStyle.BackColor = UiTheme.TableRowAlt;
                grid.AlternatingRowsDefaultCellStyle.ForeColor = UiTheme.TextPrimary;
                grid.AlternatingRowsDefaultCellStyle.SelectionBackColor = UiTheme.TableSelectionBg;
                grid.AlternatingRowsDefaultCellStyle.SelectionForeColor = UiTheme.TableSelectionFg;
                grid.AlternatingRowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                grid.ColumnHeadersDefaultCellStyle.BackColor = UiTheme.TableHeaderBg;
                grid.ColumnHeadersDefaultCellStyle.ForeColor = UiTheme.TextPrimary;
                grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                grid.KeyDown -= Grid_KeyDownTopBottom;
                grid.KeyDown += Grid_KeyDownTopBottom;
            }
        }

        internal static void DecorateSearchBox(TextBox box)
        {
            if (box?.Parent == null || box.Parent.Tag as string == "search-wrap") return;

            var parent = box.Parent;
            int idx = parent.Controls.GetChildIndex(box);
            var loc = box.Location;
            var size = box.Size;
            string placeholder = box.Text;
            var font = box.Font;
            var name = box.Name;
            var tab = box.TabIndex;

            var wrap = new Panel
            {
                Name = name + "Wrap",
                Tag = "search-wrap",
                BackColor = UiTheme.InputFill,
                Location = loc,
                Size = new Size(size.Width, Math.Max(size.Height + 4, UiTheme.ActionButtonHeight)),
                TabIndex = tab
            };

            box.Parent = wrap;
            box.BorderStyle = BorderStyle.None;
            box.BackColor = UiTheme.InputFill;
            box.ForeColor = UiTheme.TextPrimary;
            box.Font = font ?? new Font("Segoe UI", 9.75F);
            box.Location = new Point(34, (wrap.Height - size.Height) / 2);
            box.Width = wrap.Width - 42;
            box.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            wrap.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var r = wrap.ClientRectangle;
                r.Width -= 1;
                r.Height -= 1;
                using var path = BuildRoundedPath(r, UiTheme.ButtonRadius);
                using var border = new Pen(UiTheme.Border, 1f);
                e.Graphics.DrawPath(border, path);
                using var iconPen = new Pen(UiTheme.TextSecondary, 2f);
                int cx = 16, cy = wrap.Height / 2;
                e.Graphics.DrawEllipse(iconPen, cx - 6, cy - 6, 10, 10);
                e.Graphics.DrawLine(iconPen, cx + 4, cy + 4, cx + 9, cy + 9);
            };

            if (!string.IsNullOrEmpty(placeholder) && placeholder.Contains("Search", System.StringComparison.OrdinalIgnoreCase))
            {
                box.ForeColor = Color.Gray;
                box.GotFocus += (_, _) =>
                {
                    if (box.ForeColor == Color.Gray) { box.Text = string.Empty; box.ForeColor = UiTheme.TextPrimary; }
                };
                box.LostFocus += (_, _) =>
                {
                    if (string.IsNullOrWhiteSpace(box.Text)) { box.Text = placeholder; box.ForeColor = Color.Gray; }
                };
            }

            parent.Controls.Add(wrap);
            parent.Controls.SetChildIndex(wrap, idx);
            SetRound(wrap, UiTheme.ButtonRadius);
        }

        internal static void DrawFlatCard(Panel panel, PaintEventArgs e)
        {
            if (panel == null || e == null || panel.Width < 16 || panel.Height < 16) return;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.Clear(panel.Parent?.BackColor ?? UiTheme.AppBackground);
            Rectangle cardRect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
            using var path = BuildRoundedPath(cardRect, UiTheme.CardRadius);
            using var fill = new SolidBrush(UiTheme.Surface);
            using var border = new Pen(UiTheme.Border, 1f);
            e.Graphics.FillPath(fill, path);
            e.Graphics.DrawPath(border, path);
        }

        internal static void DrawCardShadow(Panel panel, PaintEventArgs e) => DrawFlatCard(panel, e);

        internal static GraphicsPath BuildRoundedPath(Rectangle bounds, int radius)
        {
            var path = new GraphicsPath();
            int d = Math.Min(radius * 2, Math.Min(bounds.Width, bounds.Height));
            if (d <= 0) return path;
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private static void Grid_KeyDownTopBottom(object sender, KeyEventArgs e)
        {
            if (sender is not DataGridView grid || grid.CurrentCell == null || e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            e.SuppressKeyPress = true;
            int next = grid.CurrentCell.RowIndex + (e.Shift ? -1 : 1);
            if (next < 0 || next >= grid.Rows.Count) return;
            grid.CurrentCell = grid.Rows[next].Cells[grid.CurrentCell.ColumnIndex];
        }

        private static System.Collections.Generic.IEnumerable<T> FindAllControls<T>(Control parent) where T : Control
        {
            foreach (Control c in parent.Controls)
            {
                if (c is T t) yield return t;
                foreach (T child in FindAllControls<T>(c)) yield return child;
            }
        }

        private static void SetRound(Control control, int radius)
        {
            if (control.Width <= 0 || control.Height <= 0) return;
            using var path = BuildRoundedPath(new Rectangle(0, 0, control.Width, control.Height), radius);
            control.Region = new Region(path);
        }
    }
}

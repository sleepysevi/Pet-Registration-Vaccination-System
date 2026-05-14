using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace MainPages
{
    public partial class AboutPage : Form
    {
        public AboutPage()
        {
            InitializeComponent();

            this.Paint += AboutPage_Paint;
            // hook paint event for VersionDash
            VersionDash.Paint += VersionDash_Paint;
            DescriptionDash.Paint += DescriptionDash_Paint;
            KeyFeatures.Paint += KeyFeatures_Paint;
            BuiltBy.Paint += BuiltBy_Paint;
        }


        private void AboutPage_Paint(object sender, PaintEventArgs e)
        {
            DrawShadow(e.Graphics, VersionDash);
            DrawShadow(e.Graphics, DescriptionDash);
            DrawShadow(e.Graphics, KeyFeatures);
            DrawShadow(e.Graphics, BuiltBy);
        }
        private void VersionDash_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = VersionDash.ClientRectangle;

            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect,
                Color.FromArgb(248, 250, 252), // top
                Color.FromArgb(195, 221, 255), // bottom
                LinearGradientMode.Vertical))
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    int radius = 20;
                    int d = radius * 2;

                    path.StartFigure();
                    path.AddArc(rect.X, rect.Y, d, d, 180, 90);
                    path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
                    path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
                    path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
                    path.CloseFigure();

                    // rounded corners
                    VersionDash.Region = new Region(path);

                    // fill gradient
                    g.FillPath(brush, path);
                }
            }
        }
        private void DescriptionDash_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = DescriptionDash.ClientRectangle;

            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect,
                Color.FromArgb(248, 250, 252), // top
                Color.FromArgb(140, 255, 243), // bottom
                LinearGradientMode.Vertical))
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    int radius = 20;
                    int d = radius * 2;

                    path.StartFigure();
                    path.AddArc(rect.X, rect.Y, d, d, 180, 90);
                    path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
                    path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
                    path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
                    path.CloseFigure();

                    // rounded corners
                    DescriptionDash.Region = new Region(path);

                    // fill gradient
                    g.FillPath(brush, path);
                }
            }
        }

        private void KeyFeatures_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = KeyFeatures.ClientRectangle;

            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect,
                Color.FromArgb(248, 250, 252), // top
                Color.FromArgb(238, 238, 166), // bottom
                LinearGradientMode.Vertical))
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    int radius = 20;
                    int d = radius * 2;

                    path.StartFigure();
                    path.AddArc(rect.X, rect.Y, d, d, 180, 90);
                    path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
                    path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
                    path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
                    path.CloseFigure();

                    // rounded corners
                    KeyFeatures.Region = new Region(path);

                    // fill gradient
                    g.FillPath(brush, path);
                }
            }
        }
        private void BuiltBy_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = BuiltBy.ClientRectangle;

            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect,
                Color.FromArgb(248, 250, 252), // top
                Color.FromArgb(238, 238, 166), // bottom
                LinearGradientMode.Vertical))
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    int radius = 20;
                    int d = radius * 2;

                    path.StartFigure();
                    path.AddArc(rect.X, rect.Y, d, d, 180, 90);
                    path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
                    path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
                    path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
                    path.CloseFigure();

                    // rounded corners
                    BuiltBy.Region = new Region(path);

                    // fill gradient
                    g.FillPath(brush, path);
                }
            }
        }

        private void DrawShadow(Graphics g, Panel panel)
        {
            Rectangle r = panel.Bounds;

            int blur = 10;
            int offsetY = 5;
            int baseAlpha = 20;

            for (int i = 0; i < blur; i++)
            {
                int alpha = (int)(baseAlpha * (1f - i / (float)blur));

                using (SolidBrush b = new SolidBrush(Color.FromArgb(alpha, 0, 0, 0)))
                {
                    Rectangle shadow = new Rectangle(
                        r.X,
                        r.Y + offsetY + i,
                        r.Width,
                        r.Height
                    );

                    using (GraphicsPath path = RoundedRect(shadow, 20))
                    {
                        g.FillPath(b, path);
                    }
                }
            }
        }
        // ================= ROUNDED RECT =================
        private GraphicsPath RoundedRect(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            return path;
        }

        private void KeyFeaturesText_Click(object sender, EventArgs e)
        {

        }
    }
}
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using AlagaTrackFrontEnd;

namespace MainPages
{
    public partial class AboutPage : Form
    {
        public AboutPage()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.None;
            BackColor = UiTheme.AppBackground;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

            VersionDash.Paint += (_, e) => PaintCard(e, VersionDash, UiTheme.Surface);
            DescriptionDash.Paint += (_, e) => PaintCard(e, DescriptionDash, UiTheme.SurfaceMuted);
            KeyFeatures.Paint += (_, e) => PaintCard(e, KeyFeatures, UiTheme.Surface);
            BuiltBy.Paint += (_, e) => PaintCard(e, BuiltBy, UiTheme.Primary);

            Load += AboutPage_Load;
        }

        private void AboutPage_Load(object? sender, EventArgs e)
        {
            if (!TopLevel)
                pictureBox2.Visible = false;
        }

        private static void PaintCard(PaintEventArgs e, Panel panel, Color fill)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = panel.ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;
            using var path = UiRoundHelper.BuildRoundedPath(rect, UiTheme.CardRadius);
            using var brush = new SolidBrush(fill);
            using var pen = new Pen(fill == UiTheme.Primary ? UiTheme.PrimaryHover : UiTheme.Border, 1f);
            e.Graphics.FillPath(brush, path);
            e.Graphics.DrawPath(pen, path);
        }

        private void KeyFeaturesText_Click(object sender, EventArgs e) { }
    }
}

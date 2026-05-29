using System.Drawing;
using System.Windows.Forms;

namespace AlagaTrackFrontEnd
{
    public static class UiTheme
    {
        public static readonly Color Primary = Color.FromArgb(32, 47, 124);
        public static readonly Color PrimaryHover = Color.FromArgb(45, 62, 145);
        public static readonly Color Accent = Color.FromArgb(234, 179, 8);

        public static readonly Color AppBackground = Color.FromArgb(245, 247, 250);
        public static readonly Color Surface = Color.White;
        public static readonly Color SurfaceMuted = Color.FromArgb(248, 250, 252);
        public static readonly Color InputFill = Color.FromArgb(243, 244, 246);
        public static readonly Color Border = Color.FromArgb(209, 213, 219);

        public static readonly Color TextPrimary = Color.FromArgb(30, 41, 59);
        public static readonly Color TextSecondary = Color.FromArgb(71, 85, 105);
        public static readonly Color TextOnPrimary = Color.White;

        public static readonly Color ActionSuccess = Color.FromArgb(31, 183, 143);
        public static readonly Color ActionNeutral = Color.FromArgb(99, 118, 141);
        public static readonly Color ActionDanger = Color.FromArgb(227, 74, 74);

        public static readonly Color TableHeaderBg = Color.FromArgb(241, 245, 249);
        public static readonly Color TableRowAlt = Color.FromArgb(248, 250, 252);
        public static readonly Color TableGridLine = Color.FromArgb(226, 232, 240);
        public static readonly Color TableSelectionBg = Color.FromArgb(219, 234, 254);
        public static readonly Color TableSelectionFg = Primary;

        public const int CardRadius = 12;
        public const int InputRadius = 8;
        public const int ButtonRadius = 6;
        public const int ActionButtonHeight = 32;
    }
}

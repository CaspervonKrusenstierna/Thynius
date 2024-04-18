
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace ThyniusWeb.Server.Common
{
    public static class Utilities
    {
        public static List<string> _BackgroundColours = new List<string> { "339966", "3366CC", "CC33FF", "FF5050" };
        public static MemoryStream GenerateInitialsImage(string firstName, string lastName)
        {
            var avatarString = string.Format("{0}{1}", firstName[0], lastName[0]).ToUpper();

            var randomIndex = new Random().Next(0, _BackgroundColours.Count - 1);
            var bgColour = _BackgroundColours[randomIndex];
           
            var bmp = new Bitmap(192, 192);
            var sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            var font = new Font("Arial", 72, FontStyle.Bold, GraphicsUnit.Pixel);
            var graphics = Graphics.FromImage(bmp);

            graphics.Clear((Color)new ColorConverter().ConvertFromString("#" + bgColour));
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.DrawString(avatarString, font, new SolidBrush(Color.WhiteSmoke), new RectangleF(0, 0, 192, 192), sf);

            graphics.Flush();

            var ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Png);

            return ms;
        }
    }
}

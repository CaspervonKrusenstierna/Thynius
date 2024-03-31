using Newtonsoft.Json;
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Text;
using static ThemisWeb.Server.Common.DataClasses;

namespace ThemisWeb.Server.Common
{
    public class Utilities
    {
        private List<string> _BackgroundColours = new List<string> { "339966", "3366CC", "CC33FF", "FF5050" };
        public MemoryStream GenerateInitialsImage(string firstName, string lastName)
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

        public ThemisSessionData ReadAsSessionData(IFormFile file)
        {
            var reader = new StreamReader(file.OpenReadStream());
            string fileContent = reader.ReadToEnd();
            reader.Dispose();

            return JsonConvert.DeserializeObject<ThemisSessionData>(fileContent);
        }


        public async Task<TextData> GetInputsTextData(IFormFile inputsFile)
        {
            TextData toReturn;
            toReturn.RawContent = "";

            ThemisSessionData sessionData = ReadAsSessionData(inputsFile);

            foreach(Input input in sessionData.inputs)
            {
                switch (input._ActionType)
                {
                    case ActionType.ADDCHAR: toReturn.RawContent.Insert((int)input._Selection.SelectionStart, input.ActionContent);  break;
                    case ActionType.DELETESELECTION: toReturn.RawContent.Remove((int)input._Selection.SelectionStart, (int)input._Selection.SelectionStart - (int)input._Selection.SelectionEnd);  break;
                    case ActionType.PASTE: toReturn.RawContent.Insert((int)input._Selection.SelectionStart, input.ActionContent);  break;
                }
            }
            toReturn.CharCount = toReturn.RawContent.Length;
            toReturn.WordCount = toReturn.RawContent.Split(" ").Length;
            return toReturn;
        }
    }
}

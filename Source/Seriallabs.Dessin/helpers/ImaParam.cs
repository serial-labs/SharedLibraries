using System.Drawing.Imaging;

namespace Seriallabs.Dessin
{
    public class ImaParam
    {
        public float Saturation { get; set; } = 1;
        public float Hue { get; set; } = 0;
        public float Scale { get; set; } = 1;
        public float ScaleRed { get; set; } = 1;
        public float ScaleGreen { get; set; } = 1;
        public float ScaleBlue { get; set; } = 1;
        public float Opacity { get; set; } = 1;




        /// <summary>
        /// 
        /// </summary>
        /// <param name="image">Image to apply the color matrix on</param>
        /// <param name="cmValues">Color Matrix Values</param>
        /// <returns>Image where we applied the color matrix</returns>
        public static ImageAttributes getAttr(ImaParam cmValues)
        {
            // Create the color matrix and set the appropriate values
            ColorMatrixExt clrMtx = new ColorMatrixExt();
            clrMtx.SetSaturation(cmValues.Saturation);
            clrMtx.RotateHue(cmValues.Hue);
            clrMtx.ScaleValue(cmValues.Scale);
            clrMtx.ScaleColors(cmValues.ScaleRed, cmValues.ScaleGreen, cmValues.ScaleBlue);
            clrMtx.ScaleOpacity(cmValues.Opacity);

            // Set the color matrix to the image attributes
            ImageAttributes imageAttr = new ImageAttributes();
            imageAttr.SetColorMatrix(clrMtx);
            return imageAttr;


        }

        public static ImaParam Parse(string text)
        {
            string[] splittedText = text.Split('/');
            return new ImaParam()
            {
                ScaleRed = float.Parse(splittedText[0]),
                ScaleGreen = float.Parse(splittedText[1]),
                ScaleBlue = float.Parse(splittedText[2]),
                Opacity = float.Parse(splittedText[3]),
                Scale = float.Parse(splittedText[4]),
                Hue = float.Parse(splittedText[5]),
                Saturation = float.Parse(splittedText[6])
            };
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}", ScaleRed, ScaleGreen, ScaleBlue, Opacity, Scale, Hue, Saturation);
        }
    }
}

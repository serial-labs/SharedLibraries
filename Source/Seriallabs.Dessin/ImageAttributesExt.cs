using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seriallabs.Dessin
{
    public class ImageAttributesExt

    {
        private ColorMatrixExt _clrMtxE = new ColorMatrixExt();
        public ColorMatrixExt getCurrentColorMatrixExt => _clrMtxE;

        public ImageAttributesExt SetSaturation(float satMult)
        {
            _clrMtxE.SetSaturation(satMult);
            return this;
        }
        public ImageAttributesExt SetOpacity(float opacityMult)
        {
            _clrMtxE.ScaleOpacity(opacityMult);
            return this;
        }
        


        public ImageAttributesExt()
        {
            
        }
        public static ImageAttributes getTestImageAttributes2Gray
        {
            get
            {
                ColorMatrixExt clrMtx = new ColorMatrixExt();
                clrMtx.SetSaturation(0);
                //clrMtx.RotateHue(cmValues.Hue);
                //clrMtx.Scale(cmValues.Scale);
                //clrMtx.ScaleColors(cmValues.ScaleRed, cmValues.ScaleGreen, cmValues.ScaleBlue);
                //clrMtx.Opacity(cmValues.Opacity);

                // Set the color matrix to the image attributes
                ImageAttributes imageAttr = new ImageAttributes();
                imageAttr.SetColorMatrix(clrMtx);
                return imageAttr;
            }
        
        }
        public static ImageAttributes getTestImageAttributes4Hue
        {
            get
            {
                ColorMatrixExt clrMtx = new ColorMatrixExt();
                clrMtx.RotateHue(new Random().Next(-360,360));
                // Set the color matrix to the image attributes
                ImageAttributes imageAttr = new ImageAttributes();
                imageAttr.SetColorMatrix(clrMtx);
                return imageAttr;
            }

        }


        public static ImageAttributes getImageAttr4Opacity(float opacityMult)
        {
            ColorMatrixExt clrMtx = new ColorMatrixExt();
            clrMtx.ScaleOpacity(opacityMult);
            
            ImageAttributes imattr = new ImageAttributes();
            imattr.SetColorMatrix(clrMtx);

            return imattr;
        }

        public static ImageAttributes getImageAttributesForSaturationBrightnessOpacity(float satMult, float brigthMult, float opacityMult, float contrastValue,float cstMult)
        {
            ColorMatrixExt clrMtx = new ColorMatrixExt();
            clrMtx.SetSaturation(satMult);
            clrMtx.SetBrightness(brigthMult);
            clrMtx.ScaleOpacity(opacityMult);
            clrMtx.ScaleValue(cstMult);
            clrMtx.SetContrast(contrastValue);
            
            ImageAttributes imattr = new ImageAttributes();
            imattr.SetColorMatrix(clrMtx);

            return imattr;
        }

        public static ImageAttributes getImageAttributesForColorize(Color colour)
        {
            ColorMatrixExt clrMtx = new ColorMatrixExt(colour);
            ImageAttributes imattr = new ImageAttributes();
            imattr.SetColorMatrix(clrMtx);

            return imattr;
        }

        #region Operators

            /// <summary>
            /// Implicit cast operator from anImageAttibutesExt to the .NET ImageAttributes
            /// </summary>
            /// <param name="clrmtx">An ImageAttributesExt instance</param>
            /// <returns>A .NET ImageAttributes instance</returns>
        public static implicit operator ImageAttributes(ImageAttributesExt imattr)
        {
            var dotNetImattr = new ImageAttributes();
            dotNetImattr.SetColorMatrix(imattr._clrMtxE);
            return dotNetImattr;
        }
        #endregion

    }
}

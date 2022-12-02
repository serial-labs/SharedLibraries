using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Seriallabs.Dessin
{
    public static partial class Helpers
    {
        public static string ID = "";
        private static string _tempFolder = @"c:\\temp\\";
        //const double d_epsilon = 1.11022302462516E-16;
        const double d_epsilon = 1.11022302462517E-16;

        public static byte DoubleToByte(double d)
        {
            if (d > (255.0 - d_epsilon)) return 255;
            if (d < d_epsilon) return 0;
            return Convert.ToByte(d);
        }

        public static Color ColorFromHexa(string hexaColor)
        {
            string colorcode = hexaColor;// "#FFFFFF00";
            colorcode = colorcode.TrimStart('#');

            Color col; // from System.Drawing or System.Windows.Media
            if (colorcode.Length == 6)
                col = Color.FromArgb(255, // hardcoded opaque
                    int.Parse(colorcode.Substring(0, 2), NumberStyles.HexNumber),
                    int.Parse(colorcode.Substring(2, 2), NumberStyles.HexNumber),
                    int.Parse(colorcode.Substring(4, 2), NumberStyles.HexNumber));
            else if (colorcode.Length == 8) // assuming length of 8
                col = Color.FromArgb(
                    int.Parse(colorcode.Substring(0, 2), NumberStyles.HexNumber),
                    int.Parse(colorcode.Substring(2, 2), NumberStyles.HexNumber),
                    int.Parse(colorcode.Substring(4, 2), NumberStyles.HexNumber),
                    int.Parse(colorcode.Substring(6, 2), NumberStyles.HexNumber));
            else // assuming length of 3
                col = Color.FromArgb(255, // hardcoded opaque
                    int.Parse(colorcode.Substring(0, 1) + colorcode.Substring(0, 1), NumberStyles.HexNumber),
                    int.Parse(colorcode.Substring(1, 1) + colorcode.Substring(1, 1), NumberStyles.HexNumber),
                    int.Parse(colorcode.Substring(2, 1) + colorcode.Substring(2, 1), NumberStyles.HexNumber));
            return col;
        }
        public static void WriteTempPictureForTesting(Image ima, string beforeext, bool asJpeg = false)
        {
            var fn = $"{_tempFolder}{Helpers.ID}tt{DateTime.Now.Ticks}";
            if (asJpeg)
                Helpers.SaveJpeg(fn + beforeext + ".jpeg", ima, 70);
            else
                ima.Save(fn + beforeext + ".png",ImageFormat.Png);
        }


        public static void RenderTxt(Graphics g, string text, int fontsize = 12, bool initg = false)
        {

            using (Font myFont = new Font("Arial Black", fontsize))
            {
                if (initg) Seriallabs.Dessin.Extensions.Init(g);

                GraphicsPath p = new GraphicsPath();
                p.AddString(
                    text,             // text to draw
                    FontFamily.GenericSansSerif,  // or any other font family
                    (int)FontStyle.Regular,      // font style (bold, italic, etc.)
                    g.DpiY * fontsize / 72,       // em size
                    new Point(0, 0),              // location where to draw text
                    new StringFormat());          // set options here (e.g. center alignment)
                g.DrawPath(new Pen(Color.Yellow, 4), p);
                g.FillPath(new SolidBrush(Color.Blue), p);
                //e.Graphics.DrawString(info, myFont, Brushes.Green, new Point(2, 2));

            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// IMAGE ENCODERS

        /// <summary>
        /// A quick lookup for getting image encoders
        /// </summary>
        private static Dictionary<string, ImageCodecInfo> encoders = null;

        /// <summary>
        /// A quick lookup for getting image encoders
        /// </summary>
        public static Dictionary<string, ImageCodecInfo> Encoders
        {
            //get accessor that creates the dictionary on demand
            get
            {
                //if the quick lookup isn't initialised, initialise it
                if (encoders == null)
                {
                    encoders = new Dictionary<string, ImageCodecInfo>();
                }

                //if there are no codecs, try loading them
                if (encoders.Count == 0)
                {
                    //get all the codecs
                    foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageEncoders())
                    {
                        //add each codec to the quick lookup
                        encoders.Add(codec.MimeType.ToLower(), codec);
                    }
                }

                //return the lookup
                return encoders;
            }
        }
        /// <summary> 
        /// Saves an image as a jpeg image, with the given quality 
        /// </summary> 
        /// <param name="path">Path to which the image would be saved.</param> 
        /// <param name="quality">An integer from 0 to 100, with 100 being the 
        /// highest quality</param> 
        /// <exception cref="ArgumentOutOfRangeException">
        /// An invalid value was entered for image quality.
        /// </exception>
        public static void SaveJpeg(string path, Image image, int quality)
        {
            //ensure the quality is within the correct range
            if ((quality < 0) || (quality > 100))
            {
                //create the error message
                string error = string.Format("Jpeg image quality must be between 0 and 100, with 100 being the highest quality.  A value of {0} was specified.", quality);
                //throw a helpful exception
                throw new ArgumentOutOfRangeException(error);
            }

            //create an encoder parameter for the image quality
            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            //get the jpeg codec
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");

            //create a collection of all parameters that we will pass to the encoder
            EncoderParameters encoderParams = new EncoderParameters(1);
            //set the quality parameter for the codec
            encoderParams.Param[0] = qualityParam;
            //save the image using the codec and the parameters
            image.Save(path, jpegCodec, encoderParams);
        }
        /// <summary> 
        /// Returns the image codec with the given mime type 
        /// </summary> 
        public static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            //do a case insensitive search for the mime type
            string lookupKey = mimeType.ToLower();

            //the codec to return, default to null
            ImageCodecInfo foundCodec = null;

            //if we have the encoder, get it to return
            if (Encoders.ContainsKey(lookupKey))
            {
                //pull the codec from the lookup
                foundCodec = Encoders[lookupKey];
            }

            return foundCodec;
        }

        /// image encoders
        /// ///////////////////////////////////////////////////////////////////////
    }



    /// <summary>
    /// diverses méthodes d'extension
    /// </summary>
    public static class Extensions
    {
        public static Rectangle _2R(this RectangleF rF)
        {
            Rectangle r = new Rectangle((int)rF.Left, (int)rF.Top, (int)rF.Width, (int)rF.Height);
            return r;
        }
        public static T Next<T>(this T src) where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName));

            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf<T>(Arr, src) + 1;
            return (Arr.Length == j) ? Arr[0] : Arr[j];
        }

        public static Graphics Init(this Graphics g)
        {
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            return g;
        }

        /// <summary>
        /// wrapper for 
        /// public void DrawImage (
        ///     System.Drawing.Image image,
        ///     System.Drawing.Rectangle destRect,
        ///     int srcX,
        ///     int srcY,
        ///     int srcWidth,
        ///     int srcHeight,
        ///     System.Drawing.GraphicsUnit srcUnit,
        ///     System.Drawing.Imaging.ImageAttributes imageAttr);
        /// </summary>
        /// <remarks>2 rectangle structs as param instead of ints</remarks>
        /// <param name="g"></param>
        /// <param name="destinationImage"></param>
        /// <param name="destR"></param>
        /// <param name="sourceR"></param>
        /// <param name="sourceImage"></param>
        /// <param name="imageAttributes"></param>
        public static void DrawImage(this Graphics g, System.Drawing.Image sourceImage, Rectangle destR, Rectangle sourceR, System.Drawing.Imaging.ImageAttributes imageAttributes)
        {
            g.DrawImage(
                sourceImage,
                destR,
                sourceR.Top,
                sourceR.Left, 
                sourceR.Width,
                sourceR.Height,
                GraphicsUnit.Pixel,
                imageAttributes
                );

        }

        /// <summary>
        /// wrapper for 
        /// public void DrawImage (
        ///     System.Drawing.Image image,
        ///     System.Drawing.Rectangle destRect,
        ///     int srcX,
        ///     int srcY,
        ///     int srcWidth,
        ///     int srcHeight,
        ///     System.Drawing.GraphicsUnit srcUnit,
        ///     System.Drawing.Imaging.ImageAttributes imageAttr);
        /// </summary>
        /// <remarks>2 rectangle structs as param instead of ints</remarks>
        /// <param name="g"></param>
        /// <param name="destinationImage"></param>
        /// <param name="destR">rectangleF that will be converted in Rectangle</param>
        
        public static void DrawImage(this Graphics g, System.Drawing.Image sourceImage, RectangleF destR, RectangleF sourceR, System.Drawing.Imaging.ImageAttributes imageAttributes)
        {
            // Create parallelogram for drawing original image.
            PointF ulCorner1 = new PointF( destR.Left, destR.Top);
            PointF urCorner1 = new PointF(destR.Right,destR.Top);
            PointF llCorner1 = new PointF(destR.Left, destR.Bottom);
            PointF[] destPara1 = { ulCorner1, urCorner1, llCorner1 };

            g.DrawImage(
                sourceImage,
                destPara1,
                sourceR,
                GraphicsUnit.Pixel,
                imageAttributes
            );

            /// ALTERNATIVE
            //PointF[] parallelogram = new PointF[3]
            //{
            //    new PointF(destR.Top, destR.Left ), 
            //    new PointF(destR.Top, destR.Left), 
            //    new PointF()
            //};

            //g.DrawImage(
            //    sourceImage,
            //    destR._2R(),
            //    sourceR.Top,
            //    sourceR.Left,
            //    sourceR.Width,
            //    sourceR.Height,
            //    GraphicsUnit.Pixel,
            //    imageAttributes
            //);

        }


        public static void DrawImageFull(this Graphics g, System.Drawing.Image sourceImage, System.Drawing.Imaging.ImageAttributes imageAttributes = null)
        {
            //pas sûr de la valeur de "g.VisibleClipBounds"
            g.DrawImageFull(sourceImage,g.VisibleClipBounds,  imageAttributes);
        }

        public static void DrawImageFull(this Graphics g, System.Drawing.Image sourceImage, RectangleF destR=default,
            System.Drawing.Imaging.ImageAttributes imageAttributes=null)
        {
            GraphicsUnit gu = g.PageUnit;
            RectangleF sourceRf = sourceImage.GetBounds(ref gu);
            
          /*  //var x = System.IO.Directory.GetFiles(@"c:\temp", "*.*");
            var fn = $"{_tempFolder}{Helpers.ID}tt{DateTime.Now.Ticks}";
            try
            {
                Helpers.SaveJpeg(fn+"_dif.jpeg",sourceImage,80); 
                //sourceImage.Save($"'c:\temp\tt{DateTime.Now.TimeOfDay}.jpeg", );
                sourceImage.Save(fn + "_dif.png", ImageFormat.Png);
            }
            catch (Exception e)
            {
            }*/
            if (destR.IsEmpty) destR = new RectangleF(default, g.VisibleClipBounds.Size);
            g.DrawImage(sourceImage, destR,sourceRf, imageAttributes);
        }


        private static void DrawImageRect4IntAtrrib(PaintEventArgs e)
        {

            // Create image.
            Image newImage = Image.FromFile("SampImag.jpg");

            // Create rectangle for displaying original image.
            Rectangle destRect1 = new Rectangle(100, 25, 450, 150);

            // Create coordinates of rectangle for source image.
            int x = 50;
            int y = 50;
            int width = 150;
            int height = 150;
            GraphicsUnit units = GraphicsUnit.Pixel;

            // Draw original image to screen.
            e.Graphics.DrawImage(newImage, destRect1, x, y, width, height, units);

            // Create rectangle for adjusted image.
            Rectangle destRect2 = new Rectangle(100, 175, 450, 150);

            // Create image attributes and set large gamma.
            ImageAttributes imageAttr = new ImageAttributes();
            imageAttr.SetGamma(4.0F);

            // Draw adjusted image to screen.
            e.Graphics.DrawImage(newImage, destRect2, x, y, width, height, units, imageAttr);
        }


        /// <summary>
        /// Make a region representing the image's non-transparent pixels.
        /// </summary>
        /// <param name="bm">Bitmap of the image</param>
        /// <param name="threshold">minimum value of Alpha that triggers opacity</param>
        /// <returns></returns>
        public static Region MakeNonTransparentRegion(this Bitmap bm, int threshold = 2)
        {
            if (bm == null) return null;

            // Declare the result region : region in_image, with opaque pixels, pixels.A >= threshold
            Region result = new Region();
            result.MakeEmpty();

            Rectangle rect = new Rectangle(0, 0, 1, 1);
            bool in_image = false;

            LockBitmap lbm = new LockBitmap(bm);
            lbm.LockBits();

            for (int y = 0; y < lbm.Height; y++)
            {
                for (int x = 0; x < lbm.Width; x++)
                {
                    if (!in_image)
                    {
                        // We're not now in the non-transparent pixels.
                        if (lbm.GetPixel(x, y).A >= threshold)
                        {
                            // We just started into non-transparent pixels.
                            // Start a Rectangle to represent them.
                            in_image = true;
                            rect.X = x;
                            rect.Y = y;
                            rect.Height = 1;
                        }
                    }
                    else if (lbm.GetPixel(x, y).A < threshold )//We are IN the image (opaque) check if this pixel transparent
                    {
                        // We are in the non-transparent pixels and
                        // have found a transparent one.
                        // Add the rectangle so far to the region.
                        in_image = false;
                        rect.Width = (x - rect.X);
                        result.Union(rect);
                    }
                }

                // Add the final piece of the rectangle if necessary.
                if (in_image)
                {
                    in_image = false; //to start well on next picture line
                    rect.Width = (bm.Width - rect.X);
                    result.Union(rect);
                }
            }

            lbm.UnlockBits();

            return result;
        }

       

        /////////////////////////////////////
        /// More about ImageAttributes
        /// 
        /// rappel  List<ColorPair> => List<ColorMap> => ( remapTable = List<ColorMap>().ToArray() ) => ImageAttr.SetRemapTable
        /// 
        ///
      /*
        public ImageAttributes ComputeImageAttributesFromSubtemplates(Composer.Composition C)
        {
            var ColorMapping = new List<ColorPair>();
            UpdateColorMappingFromComposition(C, ColorMapping);
            AddAllColors(Definitions.LesCouleursdeSerialLabs[SLColors.Tongue], colorSpread, Tinctures.Undefined,
                ColorMapping, Palette.Tongue);
            AddAllColors(Definitions.LesCouleursdeSerialLabs[SLColors.Beak], colorSpread, Tinctures.Undefined,
                ColorMapping, Palette.Hoof);
            //x addAllColors(Defs.LesCouleursdeSerialLabs[E_SLcolors.chargeBigTail], colorSpread, Tinctures.undefined, ColorMapping, Palette.Mane);
            AddAllColors(Definitions.LesCouleursdeSerialLabs[SLColors.Claws], colorSpread, Tinctures.Undefined,
                ColorMapping, Palette.Claws);
            AddAllColors(Definitions.LesCouleursdeSerialLabs[SLColors.Third], colorSpread, Tinctures.Undefined,
                ColorMapping, Palette.Mane);


            if (lastMainTincture_forCurrentCharge != Tinctures.Undefined)
            {
                //FallBack : in case some tinctures where not defined
                AddAllColors(Definitions.LesCouleursdeSerialLabs[SLColors.ChargeSecond], colorSpread,
                    lastMainTincture_forCurrentCharge, ColorMapping);
                AddAllColors(Definitions.LesCouleursdeSerialLabs[SLColors.ChargeBigTail], colorSpread,
                    lastMainTincture_forCurrentCharge, ColorMapping);
            }

            return GetImageAttributesfromColorMapping(ColorMapping.ToArray());
        }
      */
    }
}

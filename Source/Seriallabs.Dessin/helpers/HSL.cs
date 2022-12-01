using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//https://stackoverflow.com/questions/4087581/creating-a-c-sharp-color-from-hsl-values/4087601#4087601
using System.Runtime.InteropServices;

//https://stackoverflow.com/questions/737217/how-do-i-adjust-the-brightness-of-a-color
///https://geekymonkey.com/Programming/CSharp/RGB2HSL_HSL2RGB.htm
/// 
namespace Seriallabs.Dessin.helpers
{
    /*
     * In VB .NET you can get the H, S, and B or L values from then color structure:
   'B, Brightness, a.k.a L Lightness, Luminance
   DIM red as Color = Color.FromArbg(255,255,0,0)
   DIM H, S, B As Single

   H = red.GetHue
   S = red.GetSaturation
   B = red.GetBrightness
    https://www.pinvoke.net/default.aspx/shlwapi.colorrgbtohls
     */

    public struct ColorRGB
    {
        public byte R;
        public byte G;
        public byte B;

        public ColorRGB(Color value)
        {
            this.R = value.R;
            this.G = value.G;
            this.B = value.B;
        }

        public static implicit operator Color(ColorRGB rgb)
        {
            Color c = Color.FromArgb(rgb.R, rgb.G, rgb.B);
            return c;
        }

        public static explicit operator ColorRGB(Color c)
        {
            return new ColorRGB(c);
        }

        public Color color { 
            get {
                return Color.FromArgb(this.R, this.G, this.B);
            }

        }
        
    }

    public static class HSL
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="H"></param>
        /// <param name="L"></param>
        /// <param name="S"></param>
        /// <returns></returns>
        /// <see cref="https://stackoverflow.com/questions/4087581/creating-a-c-sharp-color-from-hsl-values/4087601#4087601"/>
        [DllImport("shlwapi.dll")]
        public static extern int ColorHLSToRGB(int H, int L, int S);
        static public System.Drawing.Color HLSToColor(int H, int L, int S)
        {
            //
            // Convert Hue, Luminance, and Saturation values to System.Drawing.Color structure.
            // H, L, and S are in the range of 0-240.
            // ColorHLSToRGB returns a Win32 RGB value (0x00BBGGRR).  To convert to System.Drawing.Color
            // structure, use ColorTranslator.FromWin32.
            //
            return ColorTranslator.FromWin32(ColorHLSToRGB(H, L, S));
        }

        [System.Runtime.InteropServices.DllImport("shlwapi.dll")]
        static extern void ColorRGBToHLS(int RGB, ref int H, ref int L, ref int S);
        //
        //Convert System.Drawing.Color structure to HLS.
        //
        static public (int H, int S, int L) ColorToHLS(System.Drawing.Color C)
        {
            //
            //Use ColorTranslator.ToWin32 rather than Color.ToArgb because we need 0x00BBGGRR,
            //which is returned by ToWin32, rather than 0x00RRGGBB, which is returned by ToArgb.
            //
            int H=0; int L=0; int S=0;
            ColorRGBToHLS(ColorTranslator.ToWin32(C), ref H, ref L, ref S);
            return (H, L, S);
        }


        /// 
        /// https://geekymonkey.com/Programming/CSharp/RGB2HSL_HSL2RGB.htm
        /// 


        public static Color convHSL2RGB((double h, double sl, double l) hslTuple)
        {
            return convHSL2RGB(hslTuple.h, hslTuple.sl, hslTuple.l).color;
        }

        public static Color convHSL2RGB((int  h, int sl, int l) hslTuple)
        {
            return convHSL2RGB(hslTuple.h, hslTuple.sl, hslTuple.l).color;
        }

        // Given H,S,L in range of 0-1
        // Returns a Color (RGB struct) in range of 0-255
        public static ColorRGB convHSL2RGB(double h, double sl, double l)
        {
            double v;
            double r, g, b;

            r = l; // default to gray
            g = l;
            b = l;
            v = (l <= 0.5) ? (l * (1.0 + sl)) : (l + sl - l * sl);
            if (v > 0)
            {
                double m;
                double sv;
                int sextant;
                double fract, vsf, mid1, mid2;

                m = l + l - v;
                sv = (v - m) / v;
                h *= 6.0;
                sextant = (int) h;
                fract = h - sextant;
                vsf = v * sv * fract;
                mid1 = m + vsf;
                mid2 = v - vsf;
                switch (sextant)
                {
                    case 0:
                        r = v;
                        g = mid1;
                        b = m;
                        break;
                    case 1:
                        r = mid2;
                        g = v;
                        b = m;
                        break;
                    case 2:
                        r = m;
                        g = v;
                        b = mid1;
                        break;
                    case 3:
                        r = m;
                        g = mid2;
                        b = v;
                        break;
                    case 4:
                        r = mid1;
                        g = m;
                        b = v;
                        break;
                    case 5:
                        r = v;
                        g = m;
                        b = mid2;
                        break;
                }
            }

            ColorRGB rgb;
            rgb.R = Convert.ToByte(r * 255.0f);
            rgb.G = Convert.ToByte(g * 255.0f);
            rgb.B = Convert.ToByte(b * 255.0f);
            return rgb;
        }


        // Given a Color (RGB Struct) in range of 0-255
        // Return H,S,L in range of 0-1
        public static void convRGB2HSL(ColorRGB rgb, out double h, out double s, out double l)
        {
            (h, s, l) = convRGB2HSL(rgb);
        }

        public static (double h, double s, double l) convRGB2HSL(ColorRGB rgb)
        {
            double r = rgb.R / 255.0;
            double g = rgb.G / 255.0;
            double b = rgb.B / 255.0;
            double v;
            double m;
            double vm;
            double r2, g2, b2;
            double h,s,l;
            h = 0; // default to black
            s = 0;
            l = 0;
            v = Math.Max(r, g);
            v = Math.Max(v, b);
            m = Math.Min(r, g);
            m = Math.Min(m, b);
            l = (m + v) / 2.0;
            if (l <= 0.0)
            {
                return (0,0,0);
            }

            vm = v - m;
            s = vm;
            if (s > 0.0)
            {
                s /= (l <= 0.5) ? (v + m) : (2.0 - v - m);
            }
            else
            {
                return (h,s,l);
            }

            r2 = (v - r) / vm;
            g2 = (v - g) / vm;
            b2 = (v - b) / vm;
            if (r.Equals(v))
            {
                h = (g.Equals(m) ? 5.0 + b2 : 1.0 - g2);
            }
            else if (g.Equals(v))
            {
                h = (b.Equals(m) ? 1.0 + r2 : 3.0 - b2);
            }
            else
            {
                h = (r.Equals(m) ? 3.0 + g2 : 5.0 - r2);
            }
            if (h >= 6f) h -= 6f; 
            if (h < 0f) h += 6f;
            h /= 6.0;
            return (h, s, l);
        }

    }
}
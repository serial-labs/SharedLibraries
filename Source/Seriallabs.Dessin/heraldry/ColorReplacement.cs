using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Seriallabs.Dessin.heraldry
{
    public static class ColorReplacement
    {
        private static int colorSpread = 3;
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ////////////////////////// COLOR REPLACEMENT
        public struct ColorPair
        {
            public Color color2beReplaced;
            private Color _newColorO;
            private int deltaR, deltaG, deltaB;

            public Color getNewColor(Definitions.Palette palette = null)
            {
                Color colorToMove=_newColorO;
                if (_newColorO == Color.FromArgb(0) && palette != null
                                                   && tincture != ETinctures.Undefined
                                                   && tincture != ETinctures.AuNaturel)
                    colorToMove = palette[tincture];
                return AddDelta(colorToMove);
            }

            public void setNewColor(Color newColor)
            {
                _newColorO =newColor;
            }

            public ETinctures tincture;

            public Color AddDelta(Color origin)
            {
                return AddDelta(origin, this.deltaR, this.deltaG, this.deltaB);
                /*return Color.FromArgb(origin.A,
                    Math.Min(Math.Max(origin.R + deltaR, 0), 255),
                    Math.Min(Math.Max(origin.G + deltaG, 0), 255),
                    Math.Min(Math.Max(origin.B + deltaB, 0), 255));*/
            }
            public static Color AddDelta(Color origin,int deltaR, int deltaG, int deltaB)
            {
                return Color.FromArgb(origin.A,
                    Math.Min(Math.Max(origin.R + deltaR, 0), 255),
                    Math.Min(Math.Max(origin.G + deltaG, 0), 255),
                    Math.Min(Math.Max(origin.B + deltaB, 0), 255));
            }

            //x public colorPair()
            //x {
            //x    color2beReplaced = Color.White; tincture = ETinctures.undefined;
            //x }
            public ColorPair(Color color_ToBeReplaced, ETinctures email = ETinctures.Or)
            {
                _newColorO = Color.FromArgb(0);
                deltaR = 0;
                deltaG = 0;
                deltaB = 0;
                color2beReplaced = color_ToBeReplaced;
                tincture = email;
            }

            public ColorPair(Color color_ToBeReplaced, ETinctures email, int deltaR, int deltaG, int deltaB)
            {
                
                this.deltaR = deltaR;
                this.deltaG = deltaG;
                this.deltaB = deltaB;
                _newColorO = Color.FromArgb(0);
                color2beReplaced = color_ToBeReplaced;
                tincture = email;
            }

            public ColorPair(Color color_ToBeReplaced, Color newColor, int deltaR, int deltaG, int deltaB)
            {
                
                this.deltaR = deltaR;
                this.deltaG = deltaG;
                this.deltaB = deltaB;
                _newColorO = newColor;

                color2beReplaced = color_ToBeReplaced;
                tincture = ETinctures.Undefined;
            }

            public override string ToString()
            {
                return string.Format(
                    "aRGB:[{0:000}, {1:000}, {2:000}, {3:000}] => aRGB:[{4:000}, {5:000}, {6:000}, {7:000}]  {8}=>{9}  {10}",
                    color2beReplaced.A, color2beReplaced.R, color2beReplaced.G, color2beReplaced.B,
                    getNewColor().A, getNewColor().R, getNewColor().G, getNewColor().B,
                    ColorTranslator.ToHtml(color2beReplaced),
                    ColorTranslator.ToHtml(getNewColor()),
                    tincture
                );
            }
        }


        public static void AddAllColors(
            this List<ColorPair> currentColorMapping,
            Color colorToBeReplaced,
            ETinctures tincture,
            Color fixedNewColor = new Color())
        {
            AddAllColors(colorToBeReplaced,tincture, currentColorMapping,fixedNewColor);
        }

        /// <summary>
        /// Adds ColorPairs around origin to an existing ColorMapping [ of type List<ColorPair> ]
        /// </summary>
        /// <param name="colorToBeReplaced"></param>
        /// <param name="tincture">if undefined, fixedNewColor will be used</param>
        /// <param name="currentColorMapping"></param>
        /// <param name="fixedNewColor"></param>
        public static void AddAllColors(
            Color colorToBeReplaced, 
            ETinctures tincture, 
            List<ColorPair> currentColorMapping,
            Color fixedNewColor = new Color())
        {
            Color c = colorToBeReplaced;
            int y = colorSpread;
            if (tincture == ETinctures.Undefined)
                for (var R = c.R - (y < c.R ? y : c.R); R < c.R + y + 1 && R < 256; R++)
                    for (var G = c.G - (y < c.G ? y : c.G); G < c.G + y + 1 && G < 256; G++)
                        for (var B = c.B - (y < c.B ? y : c.B); B < c.B + y + 1 && B < 256; B++)
                            currentColorMapping.Add(
                                new ColorPair(Color.FromArgb(R, G, B), fixedNewColor,
                                    c.R - R, c.G - G, c.B - B)
                            );
            else
                for (var R = c.R - (y < c.R ? y : c.R); R < c.R + y + 1 && R < 256; R++)
                    for (var G = c.G - (y < c.G ? y : c.G); G < c.G + y + 1 && G < 256; G++)
                        for (var B = c.B - (y < c.B ? y : c.B); B < c.B + y + 1 && B < 256; B++)
                            currentColorMapping.Add(
                                new ColorPair(Color.FromArgb(R, G, B),
                                        tincture, c.R - R, c.G - G, c.B - B)
                    );
        }

        /// <summary>
        /// from the array of ColorPair, we convert each to ColorMap (.OldColor, .NewColor)
        /// to produce a remapTable (array of ColorMap)
        /// to yield a new imageAttributes thanks with .SetRemapTable(remapTable)
        /// </summary>
        /// <param name="ColorMapping"></param>
        /// <param name="palette"></param>
        /// <returns></returns>
        internal static ImageAttributes GetImageAttributesfromColorMapping(ColorPair[] ColorMapping, Definitions.Palette palette)
        {
            //xstring ccc="";
            if (ColorMapping.Length == 0) return null;
            var colorMapList = new List<ColorMap>();
            //xforeach (colorPair cp in ColorMapping)
            for (var i = 0; i < ColorMapping.Length; i++)
            {
                var colorMap1 = new ColorMap();
                colorMap1.OldColor = ColorMapping[i].color2beReplaced;
                //xcolorMap1.NewColor = Color.FromArgb(128, 0, 0);// Palette[cp.tincture];
                //xcolorMap1.NewColor = Palette[Tinctures.Or];// Color.FromArgb(128, 0, 0);// Palette[cp.tincture];
                colorMap1.NewColor = ColorMapping[i].getNewColor(palette);
                colorMapList.Add(colorMap1);
                //xccc += ColorMapping[i].ToString()+Environment.NewLine;
            }

            var remapTable = colorMapList.ToArray();

            var imageAttributes = new ImageAttributes();
            imageAttributes.SetRemapTable(remapTable); //x, ColorAdjustType.Brush);
            return imageAttributes;
        }

        public static ImageAttributes buildImageAttributes(this List<ColorPair> colorMapping)
        {
            return GetImageAttributesfromColorMapping(colorMapping.ToArray(), null);
        }
    }
}

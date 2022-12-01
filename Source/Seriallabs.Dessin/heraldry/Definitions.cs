using System;
using System.Collections.Generic;
using System.Drawing;

namespace Seriallabs.Dessin.heraldry
{
    /// <summary>
    ///  Definitions of things to be used here and there
    /// </summary>
    public static class Definitions
    {
        #region Couleurs
        public static readonly Dictionary<SLColors, Color> LesCouleursdeSerialLabs = new Dictionary<SLColors, Color>() {
            
            { SLColors.First,           Color.FromArgb ( 0, 128, 0 ) },   //vert moyen (herbe)
            { SLColors.FirstShadow1,    Color.FromArgb ( 0, 80, 0 ) },
            { SLColors.Second,          Color.FromArgb ( 0, 0, 255 ) },   //bleu intense
            { SLColors.Tongue,		    Color.FromArgb ( 192, 0, 0 ) },
            { SLColors.Claws,			Color.FromArgb ( 128, 64, 0 ) },
            { SLColors.Beak,			Color.FromArgb ( 0, 192, 0 ) },
            { SLColors.ChargeLines,	    Color.FromArgb ( 1, 0, 1 ) },
            { SLColors.ShieldsLines,	Color.FromArgb ( 1, 2, 0 ) },
            { SLColors.Bordures,		Color.FromArgb ( 0, 3, 2 ) },
            { SLColors.DoubleBordure,   Color.FromArgb ( 1, 2, 3 ) },
            { SLColors.Third,			Color.FromArgb ( 127, 0, 0 ) },
            { SLColors.ChargeMain,	    Color.FromArgb ( 0, 0, 127 ) },
            { SLColors.ChargeSecond,	Color.FromArgb ( 0, 128, 0 ) },
            { SLColors.ChargeBigTail,   Color.FromArgb ( 128, 0, 128 ) },
            { SLColors.GreyWhite,   Seriallabs.Dessin.Helpers.ColorFromHexa("#FEFEFE") },
            { SLColors.GreyWhite1,   Seriallabs.Dessin.Helpers.ColorFromHexa("#C7C7C7") },//ColorFromHexa("#DADADA") },
            { SLColors.GreyWhite2,   Seriallabs.Dessin.Helpers.ColorFromHexa("#6E6E6E") },
            { SLColors.GreyWhite3,   Seriallabs.Dessin.Helpers.ColorFromHexa("#808080") },
            { SLColors.GreyDarkest,   Seriallabs.Dessin.Helpers.ColorFromHexa("#929292") },
            { SLColors.GreyBlack,   Seriallabs.Dessin.Helpers.ColorFromHexa("#000") }

        };
        #endregion

        #region Tinctures
        public static readonly Dictionary<ETinctures, string> TincturesNamesEN = new Dictionary<ETinctures, string>() 
        {   
            { ETinctures.Or, "Or" },
            { ETinctures.Argent, "Argent" },
            { ETinctures.AuNaturel , "Proper" },
            { ETinctures.Azur , "Azur" },
            { ETinctures.Gueules, "Gules" },
            { ETinctures.Pourpre , "Purpure" },
            { ETinctures.Sable , "Sable" },
            { ETinctures.Sinople , "Vert" },
            { ETinctures.Undefined, "undefined" }
        };
        public static readonly Dictionary<ETinctures, string> TincturesNamesFR = new Dictionary<ETinctures, string>() 
        {   
            { ETinctures.Or, "or" },
            { ETinctures.Argent, "argent" },
            { ETinctures.AuNaturel , "au naturel" },
            { ETinctures.Azur , "azur" },
            { ETinctures.Gueules, "gueule" },
            { ETinctures.Pourpre , "pourpre" },
            { ETinctures.Sable , "sable" },
            { ETinctures.Sinople , "sinople" },
            { ETinctures.Undefined, "undefined" }
        };
       /* public static readonly Dictionary<Languages, Dictionary<ETinctures, string>> TinctureNames = new Dictionary()
        {
            { Languages.English, TincturesNamesEN },
            { Languages.French , TincturesNamesFR }
        };*/
        #endregion

        #region Numbers as text
        /*public static readonly Dictionary<Languages, string[]> NumbersTxt = new()
        {
            { Languages.English , new string[] {"zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "niveteen", "twenty", "twentyone", "twentytwo", "twentythree", "twentyfour", "twentyfive"} },
            { Languages.French  , new string[] {"zéro","un","deux","trois", "quatre", "cinq", "six", "sept", "huit", "neuf", "dix", "onze", "douze", "treize", "quatorze", "quinze", "seize", "dix-sept", "dix-huit", "dix-neuf", "vingt", "vingt-et-un", "vingt-deux", "vingt-trois", "vingt-quatre", "vint-cinq" } },
        };*/
        #endregion

        #region Palettes
        public class Palette : Dictionary<ETinctures, Color>
        {
            public Color Claws = Color.Black;
            public Color Tongue = Color.FromArgb(192, 0, 0);
            public Color Mane = Color.FromArgb(199, 158, 127);
            public Color Hoof = Color.FromArgb(120, 81, 52);

            /// <summary>
            /// Contruit une palette à partir d'un tableau de RGB
            /// </summary>
            /// <param name="paletteAGBVPAO">A_zure G_ueules B_lack? V_ert? P_ourpre A_rgent O_r</param>
            /// <exception cref="Exception"></exception>
            public Palette(Color[] paletteAGBVPAO)
            {
                if (paletteAGBVPAO.Length < 7)
                    throw new Exception("Not enough colors in palette array...");

                Add(ETinctures.Azur, paletteAGBVPAO[0]);
                Add(ETinctures.Gueules, paletteAGBVPAO[1]);
                Add(ETinctures.Sable, paletteAGBVPAO[2]);
                Add(ETinctures.Sinople, paletteAGBVPAO[3]);
                Add(ETinctures.Pourpre, paletteAGBVPAO[4]);
                Add(ETinctures.Argent, paletteAGBVPAO[5]);
                Add(ETinctures.Or, paletteAGBVPAO[6]);
                //Add(Tinctures.AuNaturel, paletteAGBVPAO[7]); // gérer les petits éléments restant
            }

            public Palette(string[] paletteAGBVPAO)
            {
                if (paletteAGBVPAO.Length < 7)
                    throw new Exception("Not enough colors in palette array...");

                for (int i = 0; i < 7; i++)
                {
                    if (!paletteAGBVPAO[i].StartsWith("#"))
                        paletteAGBVPAO[i] = "#" + paletteAGBVPAO[i];
                }

                Add(ETinctures.Azur, ColorTranslator.FromHtml(paletteAGBVPAO[0]));
                Add(ETinctures.Gueules, ColorTranslator.FromHtml(paletteAGBVPAO[1]));
                Add(ETinctures.Sable, ColorTranslator.FromHtml(paletteAGBVPAO[2]));
                Add(ETinctures.Sinople, ColorTranslator.FromHtml(paletteAGBVPAO[3]));
                Add(ETinctures.Pourpre, ColorTranslator.FromHtml(paletteAGBVPAO[4]));
                Add(ETinctures.Argent, ColorTranslator.FromHtml(paletteAGBVPAO[5]));
                Add(ETinctures.Or, ColorTranslator.FromHtml(paletteAGBVPAO[6]));
            }
        }

        public static readonly Palette DefaultPaletteClair = new Palette(new string[] { "#0499E9", "#E62910", "#353535", "#008400", "#AF3E8F", "#F4F4F4", "#FFFD00" });

        public static readonly Palette PaletteLight = new Palette(new string[] { "#89d5ef", "#f05861", "#5b5b5b", "#98cd7c", "#a271b0", "#f2f2f2", "#f3e8b7" });
        public static readonly Palette PaletteDark = new Palette(new string[] { "#474b9f", "#c11e4d", "#151415", "#36aa4d", "#b41d89", "#e5e4e2", "#ead696" });

        public static readonly Palette PaletteOrnementOmbre = new Palette(new string[] { "#00609B", "#990700", "#191919", "#04440C", "#6D275D", "#CECECE", "#BFBB32" });

        #endregion
    }
}

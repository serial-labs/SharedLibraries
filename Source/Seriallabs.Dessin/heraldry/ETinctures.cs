using System;
using System.Collections.Generic;
using System.Drawing;

namespace Seriallabs.Dessin.heraldry
{

    public enum SLColors
    {
        Undefined = 0,
        First = 1,
        Second = 2,
        Tongue = 3,
        Claws = 4,
        Beak = 5,
        ChargeLines = 6,
        ShieldsLines = 7,
        Bordures = 8,
        DoubleBordure = 9,
        Third = 10,
        ChargeMain = 11,
        ChargeBigTail = 12,
        ChargeSmallThings = 13,
        ChargeSecond = 14,
        FirstShadow1 = 15,

        GreyWhite,
        GreyDarkest,
        GreyWhite1,
        GreyWhite2,
        GreyWhite3,
        GreyBlack
    }

    /// <summary>
    /// Enumeration complète avec FLAGS pour la plupart des Couleurs, Métaux, Fourrures héraldiques
    /// (inclut sanguine, tanné, acier ...)
    /// </summary>
    [Flags]
    public enum ETinctures
    {
        Undefined = 0,
        Or = 1,
        Argent = 2,
        Azur = 4,
        Gueules = 8,
        Sable = 16,
        Sinople = 32,
        Pourpre = 64,
        AuNaturel = 128,
        Metals = Or | Argent,
        Colours = Azur | Gueules | Sable | Sinople | Pourpre,
        Hermine = 256,
        Contre_hermine = 512,
        Vair = 1024,
        Contre_vair = 2048,
        Furs = Hermine | Contre_hermine | Vair | Contre_vair,
        Acier = 4096,
        Carnation = 8192,
        Mûre = 16384,
        Orangé = 32768,
        Sanguine = 65536,
        Tanné = 131072,
        ColoursRare = Acier | Carnation | Mûre | Orangé | Sanguine | Tanné,
        ColoursAll = Colours | ColoursRare
    }

    public static class ETincturesHelper
    {
        public static bool IsThisMetal(this ETinctures tincture)
        {
            return (ETinctures.Metals & tincture) != 0;
        }
        public static bool IsMetal(ETinctures tincture)
        {
            return (ETinctures.Metals & tincture) != 0;
        }
        public static bool IsThisColour(this ETinctures tincture)
        {
            return (ETinctures.Colours & tincture) != 0;
        }
        public static bool IsColour(ETinctures tincture)
        {
            return (ETinctures.Colours & tincture) != 0;
        }
        public static bool IsThisFur(this ETinctures tincture)
        {
            return (ETinctures.Furs & tincture) != 0;
        }
        public static bool IsFur(ETinctures tincture)
        {
            return (ETinctures.Furs & tincture) != 0;
        }

        public static List<ETinctures> ToList(this ETinctures T)
        {
            List<ETinctures> let = new List<ETinctures>();
            foreach (ETinctures t in Enum.GetValues(typeof(ETinctures)))
            {
                if ((t & T) != 0 && t.IsATincture()) let.Add(t);
            }
            return let;
        }
        public static bool IsATincture(this ETinctures T)
        {
            return (T > 0) && ((T & (T - 1)) == 0);

        }

        public static List<ETinctures> GetComplTinctMC(ETinctures tincture)
        {
            if (IsFur(tincture)) return new List<ETinctures>();/// ?????
            if (IsMetal(tincture)) return ETinctures.Metals.ToList();
            return ETinctures.Colours.ToList();
        }
        public static ETinctures GetComplTinsMC(ETinctures tincture)
        {
            if (IsFur(tincture)) return ETinctures.Undefined;/// ?????
            if (IsMetal(tincture)) return ETinctures.Colours;
            return ETinctures.Metals;
        }

        public static ETinctures GetOtherTins(ETinctures T)
        {
            return ~T;
        }

        public static ETinctures GetOtherSimpleTins(ETinctures T)
        {
            return (~T & (ETinctures.Metals | ETinctures.Colours));
        }

        /*public static List<ETinctures> GetOtherTinct(List<ETinctures> tinctures, bool withShuffle)
        {
            List<ETinctures> let = new List<ETinctures>();
            foreach (ETinctures t in Enum.GetValues(typeof(ETinctures)))
            {
                if (tinctures == null || !tinctures.Contains(t)) let.Add(t);
            }
            if (withShuffle) return CoolTools.shuffle(let);
            return let;
        }*/

        public static ETinctures GetFamilyTins(ETinctures tincture)
        {
            if (tincture.IsThisMetal()) return ETinctures.Metals;
            else if (tincture.IsThisColour()) return ETinctures.Colours;
            else if (tincture.IsThisFur()) return ETinctures.Furs;
            return ETinctures.Undefined;
        }
    }
}

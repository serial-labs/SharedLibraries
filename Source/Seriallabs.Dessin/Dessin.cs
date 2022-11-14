/* REFRENCES, DOCS ...
 * http://www.vcskicks.com/image-distortion.php
 * 
 * https://docs.microsoft.com/en-us/dotnet/desktop/winforms/advanced/how-to-use-compositing-mode-to-control-alpha-blending?view=netframeworkdesktop-4.8
 * correction gamma: 
 * https://www.cambridgeincolour.com/tutorials/gamma-correction.htm
 * https://docs.microsoft.com/fr-fr/dotnet/desktop/winforms/advanced/how-to-apply-gamma-correction-to-a-gradient?view=netframeworkdesktop-4.8
 * https://docs.microsoft.com/fr-fr/dotnet/desktop/winforms/advanced/how-to-draw-opaque-and-semitransparent-lines?view=netframeworkdesktop-4.8
 * 
 * 
 * https://stackoverflow.com/questions/8327240/what-are-the-fastest-gdi-rendering-settings
 
 * buffer for painting on forms :
 * https://stackoverflow.com/questions/14247956/graphics-drawimage-speed
 * [[ You'll likely not be able to squeeze much more performance out of this method. It's definitely worth investing some time to learn about either Marshal.Copy or pointers (LockBits, BitmapData, etc). – Simon Whitehead  Jan 9 '13]]
 * 
 * 
 * https://docs.sixlabors.com/articles/imagesharp.web/index.html?tabs=tabid-1
 *
 * Graphics Programming In C#
 * (bof) https://www.c-sharpcorner.com/article/graphics-programming-in-C-Sharp/
 * units :
 * https://stackoverflow.com/questions/8506031/the-difference-in-image-resolution-ppi-between-c-sharp-and-photoshop
 * https://stackoverflow.com/questions/20727525/calculate-inches-and-centimeters-from-pixels-in-c-sharp
 * points = pixels * 72 / 96
 *
 *
 *https://learn.microsoft.com/en-us/dotnet/api/system.drawing.imaging.imageattributes?view=netframework-4.7.2&f1url=%3FappId%3DDev16IDEF1%26l%3DFR-FR%26k%3Dk(ImageAttributes)%3Bk(TargetFrameworkMoniker-.NETFramework%2CVersion%253Dv4.7.2)%3Bk(DevLang-csharp)%26rd%3Dtrue
 *   An ImageAttributes object maintains several color-adjustment settings, including:
 *      (+) color-adjustment matrices,
 *      (+) grayscale-adjustment matrices,
 *      (+) gamma-correction values,
 *      (+) color-map tables,
 *      (+) and color-threshold values.
 *   During rendering, colors can be corrected, darkened, lightened, and removed.
 *   To apply such manipulations, initialize an ImageAttributes object and pass the path of that ImageAttributes object (along with the path of an Image)
 *   to the DrawImage method.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seriallabs.Dessin
{
    public class Dessin
    {
        byte[] data = {2,3};
        
        
    }
}

# Elements de Documentation Graphique
[What is PixelOffsetMode?](https://stackoverflow.com/questions/28441479/what-is-pixeloffsetmode)

During painting you're using double values to present your logical graphic objects, for example lines, circles, etc.  

But during rendering, framework should convert your logical doubles into physical integer pixels. 

During this process framework uses some kind of rounding, smoothing (anti-aliasing)  

So, during anti-aliasing you can have different algorithms which will provide different results; Usually all they need to calculate "distance" between logical pixel and its physical coordinates, so different modes of this setting affects precision of this distance.  


When rendering images, PixelOffsetMode specifies where the respective center points of the pixels end up:

1) **PixelOffsetMode.Half** means coordinate (0.5, 0.5) aligns with the middle (=half) of the top left pixel, or to put it more clearly: (0, 0) is the top left of the top left pixel.
2) **PixelOffsetMode.None** means coordinate (0.5, 0.5) aligns with the top left corner of the top left pixel.

see [Understanding Half-Pixel and Half-Texel Offsets](https://www.gamedev.net/blogs/entry/1848486-understanding-half-pixel-and-half-texel-offsets/)  
[Half-Pixel Offset in DirectX 11](https://www.asawicki.info/news_1516_half-pixel_offset_in_directx_11)


# save an image to file
https://learn.microsoft.com/fr-fr/dotnet/api/system.drawing.image.save?view=dotnet-plat-ext-7.0#system-drawing-image-save(system-string)  
Si aucun encodeur n’existe pour le format de fichier de l’image, l’encodeur PNG (Portable Network Graphics) est utilisé. Lorsque vous utilisez la Save méthode pour enregistrer une image graphique en tant que fichier WMF (Metafile Format) Windows ou Format de métafichier amélioré (EMF), le fichier résultant est enregistré en tant que fichier PNG (Portable Network Graphics). Ce comportement se produit car le composant GDI+ du .NET Framework n’a pas d’encodeur que vous pouvez utiliser pour enregistrer des fichiers sous forme de fichiers .wmf ou .emf.

L’enregistrement de l’image dans le même fichier qu’il a été construit n’est pas autorisé et lève une exception.

https://stackoverflow.com/questions/152729/gdi-c-how-to-save-an-image-as-emf
Creating an image with GDI+ and saving it as an EMF is simple with Metafile. Per Mike's post:
```c#
ar path = @"c:\foo.emf"
var g = CreateGraphics(); // get a graphics object from your form, or wherever
var img = new Metafile(path, g.GetHdc()); // file is created here
var ig = Graphics.FromImage(img);
// call drawing methods on ig, causing writes to the file
ig.Dispose(); img.Dispose(); g.ReleaseHdc(); g.Dispose();
```

# Color Matrixes
[How to: Use a Color Matrix to Transform a Single Color](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/advanced/how-to-use-a-color-matrix-to-transform-a-single-color?view=netframeworkdesktop-4.8)
GDI+ provides the Image and Bitmap classes for storing and manipulating images. Image and Bitmap objects store the color of each pixel as a 32-bit number: 8 bits each for red, green, blue, and alpha. Each of the four components is a number from 0 through 255, with 0 representing no intensity and 255 representing full intensity. The alpha component specifies the transparency of the color: 0 is fully transparent, and 255 is fully opaque.

A color vector is a 4-tuple of the form (red, green, blue, alpha). For example, the color vector (0, 255, 0, 255) represents an opaque color that has no red or blue, but has green at full intensity.

Another convention for representing colors uses the number 1 for full intensity. Using that convention, the color described in the preceding paragraph would be represented by the vector (0, 1, 0, 1). GDI+ uses the convention of 1 as full intensity when it performs color transformations.

You can apply **linear transformations** (*rotation*, *scaling*, and the like) to color vectors by multiplying the color vectors by a 4×4 matrix. However, you cannot use a 4×4 matrix to perform a translation (nonlinear). If you add a dummy fifth coordinate (for example, the number 1) to each of the color vectors, you can use a **5×5 matrix** to apply any combination of linear transformations and translations. A transformation consisting of a linear transformation followed by a translation is called an *affine transformation*.
[see example](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/advanced/how-to-use-a-color-matrix-to-transform-a-single-color?view=netframeworkdesktop-4.8)

and  
[Color Rotation](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/advanced/how-to-rotate-colors?view=netframeworkdesktop-4.8)  
[Shear Color](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/advanced/how-to-shear-colors?view=netframeworkdesktop-4.8): 
*Shearing* increases or decreases a color component by an amount proportional to another color component. For example, consider the transformation where the red component is increased by one half the value of the blue component. Under such a transformation, the color (0.2, 0.5, 1) would become (0.7, 0.5, 1). 

[How to: Use a Color Remap Table](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/advanced/how-to-use-a-color-remap-table?view=netframeworkdesktop-4.8)
Remapping is the process of converting the colors in an image according to a color remap table. The color remap table is an array of ColorMap objects. Each ColorMap object in the array has an OldColor property and a NewColor property.

# Dewald-Esterhuizen
Projets d'intérêt:
1. BlurFilters 
    1. Gaussian
    1. Mean
    1. Median
    1. ...
1. DifferenceOfGaussians 
1. Distortion



# about data
https://stackoverflow.com/questions/2234938/how-to-change-connection-string-in-dataset-xsd
https://stackoverflow.com/questions/19286246/how-to-convert-dataset-to-entity-in-c

## Reading settings from app.config or web.config in .NET
https://stackoverflow.com/questions/1189364/reading-settings-from-app-config-or-web-config-in-net
[Settings.settings vs. app.config in .NET desktop app [duplicate]](https://stackoverflow.com/questions/7456291/settings-settings-vs-app-config-in-net-desktop-app)

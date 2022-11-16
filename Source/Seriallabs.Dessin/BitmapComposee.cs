using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using System.Xml;
using Seriallabs.Dessin.helpers;

namespace Seriallabs.Dessin
{
    public class BitmapComposéeEventArgs
    {
        public Bitmap Bmp;
        public string StepName="";
        public RectangleF Destination;
    }

    public class BitmapComposée : IDisposable
    {
        public event EventHandler<BitmapComposéeEventArgs> DrawOccurred;
        public void OnDrawOccurred(string stepName,Bitmap bmp, RectangleF destination)
        {
            if (DrawOccurred != null) DrawOccurred(this, new  BitmapComposéeEventArgs {StepName = stepName, Bmp =bmp,Destination = destination});
        }

        private Bitmap _backgroundBmp;
        public Bitmap getBitmap => _backgroundBmp;
        public int Width =>  _backgroundBmp?.Width ?? -1;
        public int Height => _backgroundBmp?.Height ?? -1;


        private Graphics _gCtx;
        public Graphics gCtx
        {
            get
            {
                if (_gCtx == null) 
                    if (_backgroundBmp!=null) 
                        _gCtx = Graphics.FromImage(_backgroundBmp).Init();
                return _gCtx;
            }
        }

        public Region OpaqueRegion=null;
        private Region CreateOpaqueRegion(int threshold )
        {
            OpaqueRegion = _backgroundBmp.MakeNonTransparentRegion(threshold);
            return OpaqueRegion;
        }

        public RectangleF getImageBounds
        {
            get
            {
                GraphicsUnit gu = this.gCtx.PageUnit;
                if (_gCtx == null || _backgroundBmp == null) return new RectangleF(0, 0, 0, 0);
                return _backgroundBmp.GetBounds(ref gu);// why ref ??
            }
        }

        public void FreeGraphicCtx()
        {
            _gCtx.Dispose();
            _gCtx = null;
        }

        private BitmapComposée(Image ImageForBackground)
        {
            _backgroundBmp = new Bitmap(ImageForBackground);
        }



        /// <summary>
        /// méthode factory
        /// 
        /// </summary>
        /// <param name="imageForBackground"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="imattr"></param>
        /// <param name="clipTransparentThreshold">when >0 create a clipping region according to pixels habing alpha >= value </param>
        /// <returns></returns>
        public static BitmapComposée CreateBitmapComposée(Image imageForBackground, ImageAttributes imattr, int clipTransparentThreshold = 1)
        {
            return CreateBitmapComposée(imageForBackground, -1, -1, imattr, clipTransparentThreshold);
        }
        
        public static BitmapComposée CreateBitmapComposée(Image imageForBackground, int width = -1, int height = -1, ImageAttributes imattr=null,int clipTransparentThreshold = 2)
        {
            if (imageForBackground==null) return null;
            
            // crée la bitmap (vide) qui servira de fondation, de background
            Bitmap bgBmp = new Bitmap(
                 width > 0 ? width : imageForBackground.Width,
                height > 0 ? height : imageForBackground.Height);
            
            var nBC = new BitmapComposée(bgBmp);
            
            GraphicsUnit gu = nBC.gCtx.PageUnit;
            RectangleF destRf = bgBmp.GetBounds(ref gu);// why ref ??
            
            nBC.gCtx.DrawImageFull(
                imageForBackground, 
                destRf,
                imattr
            );
            if (clipTransparentThreshold > 0)
            {
                var or= nBC.CreateOpaqueRegion(clipTransparentThreshold);
                nBC.gCtx.SetClip(or, CombineMode.Replace);
            }
            return nBC;
        }

        public BitmapComposée BlendImageOverWithArithmetic(Image ima, OpEx.ColorCalculationType arithmeticOp)
        {
            var iB = OpEx.BlendWithArithmetic(_backgroundBmp, (Bitmap)ima,new Point(), arithmeticOp);
            return BlendImageOver(iB);
        }


        public BitmapComposée BlendImageOver(Image ima, float opacity)
        {
            return BlendImageOver(ima, ImageAttributesExt.getImageAttr4Opacity(opacity));
        }

        public BitmapComposée BlendImageOver(Image ima,ImageAttributes imattr = null)
        {
            gCtx.DrawImageFull(ima,getImageBounds,imattr);
            return this;
        }

        public BitmapComposée BlendImageAt(Image ima, Point pos2k, ImageAttributes imattr = null)
        {
            RectangleF destRf = new RectangleF(pos2k, ima.Size);
            gCtx.DrawImageFull(ima, destRf, imattr);
            return this;
        }



        public BitmapComposée AddImage(Image ima)
        {
            return this;
        }


        public void Dispose()
        {
            if (_gCtx!=null) _gCtx.Dispose();
            _gCtx = null;
        }
    }
}


////
/* Pour faire des tests (12/11/2022)
public static BitmapComposée CreateBitmapComposée(Image imageForBackground, int width = -1, int height = -1, ImageAttributes imattr=null)
{
    if (imageForBackground == null) return null;

    var fn = $"c:\\temp\\oo\\{Seriallabs.Dessin.Helpers.ID}txx{DateTime.Now.Ticks}";
    Seriallabs.Dessin.Helpers.SaveJpeg(fn + "_i.jpeg", imageForBackground, 80);

    // crée la bitmap (vide) qui servira de fondation, de background
    Bitmap bgBmp = new Bitmap(
         width > 0 ? width : imageForBackground.Width,
        height > 0 ? height : imageForBackground.Height);

    Seriallabs.Dessin.Helpers.SaveJpeg(fn + "_b.jpeg", bgBmp, 80);
    //ok, marche avec : bgBmp = (Bitmap)Image.FromFile(@"C:\Users\olivierH\-dev-\IMG\Eagle2x.png");
    var nBC = new BitmapComposée(bgBmp);
    Seriallabs.Dessin.Helpers.SaveJpeg(fn + "_a.jpeg", nBC.getBitmap, 80);


    GraphicsUnit gu = nBC.gCtx.PageUnit;
    RectangleF destRf = bgBmp.GetBounds(ref gu);// why ref ??




    nBC.gCtx.DrawImageFull(
        imageForBackground,
        destRf,
        imattr
    );
    //nBC.gCtx.DrawImage(Image.FromFile(@"C:\Users\olivierH\-dev-\IMG\Eagle2x.png"), destRf);

    nBC.gCtx.DrawRectangle(new Pen(Color.FromArgb(255, 0, 255, 0), 3), 11, 11,
        bgBmp.Width - 23, bgBmp.Height - 23);//
    nBC.gCtx.DrawRectangle(new Pen(Color.FromArgb(255, 0, 0, 255)), 0, 0,
        bgBmp.Width - 1, bgBmp.Height - 1);// un rectangle de même largeur que 
    nBC.gCtx.DrawEllipse(new Pen(Color.FromArgb(255, 128, 60, 255), 15), 21, 21,
        bgBmp.Width - 43, bgBmp.Height - 43);// un rectangle de même largeur que 

    Seriallabs.Dessin.Helpers.RenderTxt(nBC.gCtx, "test " + DateTime.Now.TimeOfDay, 16, true);
    //nBC.OnDrawOccurred("Create, initial BG draw", nBC._backgroundBmp, destRf); //inutile de lancer l'evt, personne n'aura eu le temps de s'abonner...

    return nBC;

}
*/
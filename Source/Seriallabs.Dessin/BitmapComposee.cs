// #define testing // define if you want to test
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
using System.Windows.Forms;
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
        private bool _testing = false;
        private bool _generatefiles4testing = false;
        

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

#if testing
        /// <summary>
        /// Superpose une texture avec semi-transparence
        /// TODO gérer les texture au pixel près
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="nbTilesW"></param>
        /// <param name="opacity"></param>
        /// <returns></returns>
        public Bitmap  AddTexture(Image texture, int nbTilesW=0,float opacity=0.60f)
        {
            
            if (nbTilesW<2 ) { 
                gCtx.DrawImageFull(texture,getImageBounds, ImageAttributesExt.getImageAttr4Opacity(opacity)); 
                return _backgroundBmp;
            }

            Bitmap bmpToHoldTexture = new Bitmap((int) getImageBounds.Width, (int) getImageBounds.Height);//This constructor creates a Bitmap with a PixelFormat enumeration value of Format32bppArgb.

            if (_generatefiles4testing) Helpers.WriteTempPictureForTesting(bmpToHoldTexture, "_bmpToHoldTexture");

        Size tileSize = new Size();
            tileSize.Width = (bmpToHoldTexture.Width+nbTilesW-1) / nbTilesW; // s'assure de la couverture. 
            float ratio = texture.Width / (float) texture.Height;
            tileSize.Height = (int)  ((tileSize.Width +1)/ ratio);
            int nbTilesH = ( bmpToHoldTexture.Height + tileSize.Height -1)/ tileSize.Height;
            using (Graphics g = Graphics.FromImage(bmpToHoldTexture))
            {
                
                //g.Clear(Color.Blue);
                g.Clear(Color.Transparent);

                                    if (_testing)
                                    {
                                        g.FillRectangle(new SolidBrush(Color.FromArgb(240, 0, 0, 128)), 0, 0, 200, 300);
                                        g.FillRectangle(new SolidBrush(Color.FromArgb(140, 200, 0, 128)), 100, 100, 200, 300);
                                        g.FillRectangle(new SolidBrush(Color.FromArgb(40, 20, 200, 128)), 150, 200, 200, 300);
                                        g.FillRectangle(new SolidBrush(Color.FromArgb(40, 20, 200, 128)), bmpToHoldTexture.Width - 150,
                                            bmpToHoldTexture.Height - 200, 200, 300);
                                    }    
                                    
                for (int i = 0; i < nbTilesW; i++)
                {
                    for (int j = 0; j < nbTilesH; j++)
                    {
                        var destR = new Rectangle(
                            i * tileSize.Width,
                            j * tileSize.Height,
                            tileSize.Width,//(i + 1) * tileSize.Width - 1,
                            tileSize.Height//(j + 1) * tileSize.Height - 1
                            );
                if (_testing) g.DrawRectangle(new Pen(Color.Aqua, 5), destR);

                        g.DrawImageFull(texture, destR, ImageAttributesExt.getImageAttr4Opacity(opacity));
                    }
                    if (_generatefiles4testing) Helpers.WriteTempPictureForTesting(bmpToHoldTexture, "_bmpToHoldTexture_txtvertical_"+i.ToString());
                }
            }
            gCtx.DrawImageFull(bmpToHoldTexture,ImageAttributesExt.getImageAttr4Opacity(opacity));
            if (_generatefiles4testing) Helpers.WriteTempPictureForTesting(bmpToHoldTexture, "_bmpToHoldTexture_returning");
            return bmpToHoldTexture;
        }

#else
        /// <summary>
        /// Superpose une texture avec semi-transparence
        /// TODO gérer les texture au pixel près
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="nbTilesW"></param>
        /// <param name="opacity"></param>
        /// <returns></returns>
        public Bitmap AddTexture(Image texture, int nbTilesW = 0, float opacity = 0.60f)
        {

            if (nbTilesW < 2)
            {
                gCtx.DrawImageFull(texture, getImageBounds, ImageAttributesExt.getImageAttr4Opacity(opacity));
                return _backgroundBmp;
            }

            Bitmap bmpToHoldTexture = new Bitmap((int)getImageBounds.Width, (int)getImageBounds.Height);//This constructor creates a Bitmap with a PixelFormat enumeration value of Format32bppArgb.

            Size tileSize = new Size();
            tileSize.Width = (bmpToHoldTexture.Width + nbTilesW - 1) / nbTilesW; // s'assure de la couverture. 
            float ratio = texture.Width / (float)texture.Height;
            tileSize.Height = (int)((tileSize.Width + 1) / ratio);
            int nbTilesH = (bmpToHoldTexture.Height + tileSize.Height - 1) / tileSize.Height;
            using (Graphics g = Graphics.FromImage(bmpToHoldTexture))
            {
                g.Clear(Color.Transparent);

                for (int i = 0; i < nbTilesW; i++)
                {
                    for (int j = 0; j < nbTilesH; j++)
                    {
                        var destR = new Rectangle(
                            i * tileSize.Width,
                            j * tileSize.Height,
                            tileSize.Width,//(i + 1) * tileSize.Width - 1,
                            tileSize.Height//(j + 1) * tileSize.Height - 1
                            );
                        g.DrawImageFull(texture, destR, ImageAttributesExt.getImageAttr4Opacity(opacity));
                    }
                }
            }
            gCtx.DrawImageFull(bmpToHoldTexture, ImageAttributesExt.getImageAttr4Opacity(opacity));

            return bmpToHoldTexture;
        }
#endif







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

    var fn = $"{_tempFolder}{Seriallabs.Dessin.Helpers.ID}txx{DateTime.Now.Ticks}";
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
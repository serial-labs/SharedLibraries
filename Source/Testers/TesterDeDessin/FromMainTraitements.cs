using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesterDeDessin
{
    public partial class FormMain : Form
    {


        private void Traitement_pbs1_vers_picResult()
        {
            if (lstInterpolationModes.SelectedValue.ToString() == InterpolationMode.Invalid.ToString())
            {
                toolStripStatusLabelElapsed.Text = "INVALID!"; return;
            }
            Helpers.TimeRecord = new Helpers.TimeRecording();
            //DrawToBitmap();
            var oc = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            for (int i = 0; i < numericUpDown1.Value; i++)
            {
                //DrawToBitmap();
                Size s = picResult.ClientRectangle.Size;
                Bitmap _armoiries = new System.Drawing.Bitmap(s.Width, s.Height);
                RectangleF drawingSpace = new RectangleF(0, 0, s.Width, s.Height);
                Rectangle destR = new Rectangle(0, 0, s.Width, s.Height); //RectangleF._2R();
                List<ColorMap> colorMapList = new List<ColorMap>();
                using (Graphics g = Graphics.FromImage(_armoiries))
                {
                    //When the Graphics.SmoothingMode property is specified by using the SmoothingMode enumeration, it does not affect text. To set the text rendering quality, use the Graphics.TextRenderingHint property and the TextRenderingHint enumeration.
                    //When the Graphics.SmoothingMode property is specified by using the SmoothingMode enumeration, it does not affect areas filled by a path gradient brush. Areas filled by using a PathGradientBrush object are rendered the same way (aliased) regardless of the setting for the Graphics.SmoothingMode property.

                    g.SmoothingMode = chkSmoothingModeHighQuality.Checked ? SmoothingMode.HighQuality : SmoothingMode.None; //AntiAlias and HighQuality are equivalent and specify rendering with smoothing applied.
                    //g.InterpolationMode = chkInterpolationModeHighQualityBicubic.Checked? InterpolationMode.HighQualityBicubic: InterpolationMode.Low;
                    g.InterpolationMode = Enum.Parse<InterpolationMode>(lstInterpolationModes.SelectedValue.ToString());
                    /*
                        Bicubic 4 Specifies bicubic interpolation.No prefiltering is done.This mode is not suitable for shrinking an image below 25 percent of its original size.
                        Bilinear    3 Specifies bilinear interpolation.No prefiltering is done.This mode is not suitable for shrinking an image below 50 percent of its original size.
                        Default 0 Specifies default mode.
                        High    2 Specifies high quality interpolation.
                        HighQualityBicubic  7 Specifies high - quality, bicubic interpolation.Prefiltering is performed to ensure high - quality shrinking.This mode produces the highest quality transformed images.
                        HighQualityBilinear 6 Specifies high - quality, bilinear interpolation.Prefiltering is performed to ensure high - quality shrinking.
                        Invalid - 1 Equivalent to the Invalid element of the QualityMode enumeration.
                        Low 1 Specifies low quality interpolation.
                        NearestNeighbor 5 Specifies nearest - neighbor interpolation.
                    */
                    GraphicsUnit gu = g.PageUnit;

                    // ATTENTION, les shields sont enregistrés dans les EMF avec une marge totale égale à la taille du dessin
                    // il dessin visible commence à (0,0) donc les coord du coin sup gauche de l'Image sont négatives
                    // le sourceR pointera sur le dessin visible et commencera donc à (0,0) avec pour taille la moitié de celle de l'Ima 
                    RectangleF sourceRf = pbs[1].Image.GetBounds(ref gu);

                    /*List<ColorPair> ColorMapping = new List<ColorPair>();
                    ColorMap[] remapTable = colorMapList.ToArray();
                    ImageAttributes imageAttributes = new ImageAttributes();
                    imageAttributes.SetRemapTable(remapTable);//x, ColorAdjustType.Brush);*/

                    g.DrawImage(pbs[1].Image, destR, sourceRf, GraphicsUnit.Pixel);
                    lblSource.Text = "source : " + sourceRf._2R().ToString();
                    lblDest.Text = "dest : " + destR.ToString();

                    Color alphaForeColor = Color.FromArgb(120, lblColor1.BackColor);
                    Color opaqueBackColor = Color.FromArgb(255, lblColor2.BackColor);
                    Pen rectanglePen = new Pen(alphaForeColor, 1.0f);
                    SolidBrush textBrush = (SolidBrush)SystemBrushes.ControlDark;
                    SolidBrush textBrush2 = new SolidBrush(alphaForeColor);

                    //g.FillRectangle(new SolidBrush(opaqueBackColor), 0, label2.Top + label2.Height, this.ClientRectangle.Width, this.ClientRectangle.Height);
                    Font ft = new Font("Arial", 24);

                    SizeF tailleTxt = g.MeasureString("X", ft);
                    string textToDisplay = "MyBlazon.com";
                    float x = 25 + tailleTxt.Width;// this.ClientRectangle.Width / 2.0f;
                    //x -= g.MeasureString(textToDisplay, this.Font).Width / 2.0f;

                    float y = this.ClientRectangle.Height - 25;/// 2.0f;
                    y -= 3 * tailleTxt.Height;

                    g.DrawString("MyBlazon.com", ft, textBrush, new PointF(x, y));
                    g.DrawString("MyBlazon.com", ft, textBrush2, new PointF(x, y + tailleTxt.Height));

                    int rectY = 20;
                    g.InterpolationMode = InterpolationMode.Bilinear;
                    g.CompositingQuality = CompositingQuality.Default;
                    g.SmoothingMode = SmoothingMode.None;
                    g.CompositingMode = CompositingMode.SourceCopy;


                    ///traçages des rectangles de test
                    /// ATTENTION AUX largeurs https://stackoverflow.com/questions/3147569/pixel-behaviour-of-fillrectangle-and-drawrectangle
                    /// DrawRectangle(pen,0,0,1,1) on the other hand draws a small 2 by 2 pixel rectangle.
                    /// [...]
                    ///  be aware of the fact that the point has zero size and lies in the center of pixel (it is not the whole pixel).
                    g.DrawRectangle(new Pen(Color.FromArgb(255, 0, 255, 0)), 1, 1,
                        s.Width - 3, s.Height - 3);//
                    g.DrawRectangle(new Pen(Color.FromArgb(255, 0, 0, 255)), 0, 0,
                        s.Width - 1, s.Height - 1);// un rectangle de même largeur que 

                    g.DrawRectangle(rectanglePen, 25, 25,
                        s.Width - 50, s.Height - 50);
                    g.DrawRectangle(rectanglePen, 27, 27,
                        s.Width - 54, s.Height - 54);
                    g.DrawRectangle(rectanglePen, 30, 30,
                        s.Width - 60, s.Height - 60);
                    _armoiries.SetPixel(0, 0, Color.FromArgb(255, 255, 0, 0));
                    _armoiries.SetPixel(1, 1, Color.FromArgb(255, 255, 0, 0));

                    g.DrawRectangle(new Pen(Color.FromArgb(255, 0, 128, 255)), 3, 3,
                        3, 3);// un rectangle de même largeur que 
                }

                picResult.Image = _armoiries;
            }
            this.Cursor = oc;

            Helpers.TimeRecord.checkin("finished");
            toolStripStatusLabelElapsed.Text = $"Last op: {Helpers.TimeRecord.Last().elapsinceBeginning}ms ";

        }
        /////////////////////////////////////////////////
        ///                 ZOOM
        /// /////////////////////////////////////////////
        ///
        ///
        ///


        private void DrawZoomedRegion(PictureBox pb, int sourceX, int sourceY)
        {
            Point real = Helpers.getRealCoordOr0(pb, sourceX, sourceY);
            lastZoomCoord = new Point(sourceX, sourceY);
            try
            {
                int zoomfactor = trackBar1.Value;
                int zoomedZoneW = 1 + (pictureBox3.ClientRectangle.Width - 1) / zoomfactor; //arrondir au pixel sup si dépasse. ex. à zF=3, 15=>5 et 16=>6
                int zoomedZoneH = 1 + (pictureBox3.ClientRectangle.Height - 1) / zoomfactor;
                int zoomedZone_Left = real.X - zoomedZoneW / 2; if (zoomedZone_Left < 0) zoomedZone_Left = 0;
                int zoomedZone_Right = real.Y - zoomedZoneH / 2; if (zoomedZone_Right < 0) zoomedZone_Right = 0;
                Point offsetgrid = new Point(zoomfactor / 2, zoomfactor / 2);

                Rectangle sourceR = new Rectangle(zoomedZone_Left, zoomedZone_Right, zoomedZoneW, zoomedZoneH);
                Rectangle destR = new Rectangle(0, 0, zoomedZoneW * zoomfactor, zoomedZoneH * zoomfactor);//pictureBox3.ClientRectangle;

                //Bitmap bmp = new Bitmap(pictureBox3.Image);
                Bitmap bmp = new Bitmap(pictureBox3.ClientRectangle.Width, pictureBox3.ClientRectangle.Height);

                //Pen p = new Pen(lblColor2.BackColor);
                Pen p = new Pen(Color.FromArgb(10 + trackBar1.Value * 100 / trackBar1.Maximum, lblColor2.BackColor));
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.CompositingMode = CompositingMode.SourceCopy;
                    g.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.PixelOffsetMode = PixelOffsetMode.None;
                    /*g.InterpolationMode = InterpolationMode.Bilinear;
                    g.CompositingQuality = CompositingQuality.HighSpeed;
                    g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                    g.PageUnit = GraphicsUnit.Pixel;
                    //g.PageScale
                    g.SmoothingMode = SmoothingMode.None;
                    */
                    //g.TextRenderingHint
                    g.DrawImage(pb.Image, destR, sourceR, GraphicsUnit.Pixel);

                    for (int i = offsetgrid.X; i < destR.Width; i += zoomfactor)
                        g.DrawLine(p, i, 0, i, destR.Height);
                    for (int i = offsetgrid.Y; i < destR.Height; i += zoomfactor)
                        g.DrawLine(p, 0, i, destR.Width, i);
                }
                Color colour = bmp.GetPixel(zoomedZoneW / 2, zoomedZoneH / 2);
                ///toolStripStatusLabel4.Text += ":" + colour.ToString();
                pictureBox3.Image = bmp;
                //bmp.Dispose();

                /*using (Graphics g = pictureBox3.CreateGraphics())
                {



                    //g.PageUnit
                    g.DrawImage(pb.Image, pictureBox3.ClientRectangle, sourceR, GraphicsUnit.Pixel);
//                        g.DrawImage(pb.Image, 0,0, sourceR, GraphicsUnit.Pixel);
                }*/
            }
            catch (Exception ex)
            {
                toolStripStatusLabel2.Text = ex.Message;
            }
        }
    }
}

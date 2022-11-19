using System.Collections;
using System.Collections.Immutable;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.Resources;
using Seriallabs.Dessin;
using Seriallabs.Dessin.helpers;

namespace TesterDeDessin
{

    public partial class FormMain : Form
    {
        bool zoom = true;
        private PictureBox[] pbs;
        private int[] imaIndex;
        private Image[] imaAll;
        private Image[] imaTextures;
        private int nbImaEmbedded;
        private int nbImaTexturesEmbedded;
        private FormConsole myConsole;

        public FormMain()
        {
            InitializeComponent();
            nbImaEmbedded = GetEmbeddedImagesNames().Count;
            imaAll = new Image[nbImaEmbedded];
            for (int i = 0; i < nbImaEmbedded; i++)
            {
                imaAll[i] = (Image) ResourceImages1.ResourceManager.GetObject(GetEmbeddedImagesNames()[i]);
            }

            nbImaTexturesEmbedded = GetEmbeddedImagesNames(1).Count;
            imaTextures = new Image[nbImaTexturesEmbedded];
            for (int i = 0; i < nbImaTexturesEmbedded; i++)
            {
                imaTextures[i] = (Image)ResourceTextures.ResourceManager.GetObject(GetEmbeddedImagesNames(1)[i]);
            }

            pbs = new PictureBox[] {picBsource, pictureBox1, pictureBox2, pictureBox3, picResult};
            imaIndex = new int[] {0, 1, 2, 3};
            for (int i = 0; i < pbs.Length; i++)
            {
                if (pbs[i] == picResult) continue;
                pbs[i].Image =
                    imaAll[i]; //(Image) ResourceImages1.ResourceManager.GetObject(GetEmbeddedImagesNames()[i]);
                pbs[i].SizeMode = getModeFromComboOrZoom();
            }
        }

        //https://learn.microsoft.com/en-us/dotnet/desktop/winforms/advanced/how-to-use-interpolation-mode-to-control-image-quality-during-scaling?view=netframeworkdesktop-4.8
        private void FormMain_Load(object sender, EventArgs e)
        {
            lstInterpolationModes.DataSource = Enum.GetValues(typeof(InterpolationMode));
            lstArithmetics.DataSource = Enum.GetValues(typeof(OpEx.ColorCalculationType));
            lstArithmetics.SelectedIndex = 0;
            myConsole = new FormConsole();
            myConsole.Show();
        }

        private PictureBoxSizeMode getModeFromComboOrZoom()
        {

            PictureBoxSizeMode result;
            if (Enum.TryParse<PictureBoxSizeMode>(toolStripComboBox1.Text, true, out result))
                return result;
            return PictureBoxSizeMode.Zoom;
        }

        private PictureBoxSizeMode getModeFromText(string text, PictureBoxSizeMode def = PictureBoxSizeMode.Zoom)
        {

            PictureBoxSizeMode result;
            if (Enum.TryParse<PictureBoxSizeMode>(text, true, out result))
                return result;
            if (Enum.TryParse<PictureBoxSizeMode>(text + "Image", true, out result))
                return result;
            return def;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <see cref="https://stackoverflow.com/questions/34826111/how-to-get-all-images-in-the-resources-as-list"/>
        static List<string> GetEmbeddedImagesNames(int typeOfImages=0)
        {
            Type tr = typeof(TesterDeDessin.ResourceImages1);
            if (typeOfImages == 1) tr = typeof(TesterDeDessin.ResourceTextures);

            /* Reference to your resources class -- may be named differently in your case */
            ResourceManager MyResourceClassForTextures =
                new ResourceManager(tr);

            ResourceSet resourceSet =
                MyResourceClassForTextures.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (DictionaryEntry entry in resourceSet)
            {
                string resourceKey = entry.Key.ToString();
                //object resource = entry.Value;
            }

            return resourceSet.Cast<DictionaryEntry>()
                .Where(x => x.Value.GetType() == typeof(Bitmap))
                //
                .Select(x => x.Key.ToString() ?? "")
                .ToList();
        }



        private void picBsource_Click_1(object sender, EventArgs e)
        {
            var pbi = pbs.Select((pb, i) => new {i, pb}).Where(x => x.pb == sender).Select(x => x.i).First();
            
            Image[] listima = imaAll;
            int nbIma = nbImaEmbedded;

            if (pbi == 1)
            {
                listima = imaTextures;
                nbIma = nbImaTexturesEmbedded;
            }

            if (++imaIndex[pbi] >= nbIma) imaIndex[pbi] = 0;
            //pbs[pbi].Image = (Bitmap)ResourceImages1.ResourceManager.GetObject(GetEmbeddedImagesNames()[imaIndex[pbi]]);
            pbs[pbi].Image = listima[imaIndex[pbi]]; //pbs[(4+pbi - 1) % 4].Image;
            pbs[pbi].Invalidate();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <see cref="https://stackoverflow.com/questions/4200843/outline-text-with-system-drawing"/>
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var pbi = pbs.Select((pb, i) => new { i, pb }).Where(x => x.pb == sender).Select(x => x.i).First();
            if (pbi == 3 && zoom) return;

            Image[] listima = imaAll;
            int nbIma = nbImaEmbedded;

            if (pbi == 1)
            {
                listima = imaTextures;
                nbIma = nbImaTexturesEmbedded;
            }
            
            
            string info = $"{pbi} - {imaIndex[pbi]} - {listima[imaIndex[pbi]].Width}x{listima[imaIndex[pbi]].Height}";
            if (pbi == 1) info += " "+GetEmbeddedImagesNames(1)[imaIndex[pbi]];
            Seriallabs.Dessin.Helpers.RenderTxt(e.Graphics, info, 16, true);
        }



        private void ouvrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select an image file.";
            ofd.Filter = "Png files (*.png)|*.png|Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamReader streamReader = new StreamReader(ofd.FileName);
                Bitmap sourceBitmap = (Bitmap) Bitmap.FromStream(streamReader.BaseStream);
                streamReader.Close();

                picBsource.BackgroundImage = sourceBitmap;


                //OnCheckChangedEventHandler(sender, e);
            }
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1_TextUpdate(object sender, EventArgs e)
        {
            for (int i = 0; i < pbs.Length; i++)
            {
                pbs[i].Image =
                    imaAll[i]; //(Image) ResourceImages1.ResourceManager.GetObject(GetEmbeddedImagesNames()[i]);
                pbs[i].SizeMode = getModeFromComboOrZoom();
            }
        }

        private void SizeModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            normalToolStripMenuItem.Checked = false;
            zoomToolStripMenuItem.Checked = false;
            centerToolStripMenuItem.Checked = false;
            stretchToolStripMenuItem.Checked = false;
            (sender as ToolStripMenuItem).Checked = true;

            string txt = (sender as ToolStripMenuItem).Text;
            PictureBoxSizeMode pbsm = getModeFromText(txt);
            toolStripStatusLabel1.Text = txt + '-' + Enum.GetName<PictureBoxSizeMode>(pbsm);
            for (int i = 0; i < pbs.Length; i++)
            {
                if (pbs[i] == picResult) continue;
                pbs[i].SizeMode = pbsm;
                pbs[i].Invalidate();
            }

        }


        private void pbs1_vers_picResult_Click(object sender, EventArgs e)
        {
            Traitement_pbs1_vers_picResult();
        }


        private void ShowCoords(PictureBox pb, Int32 mouseX, Int32 mouseY)
        {


            Point real = Helpers.getRealCoordOr0(pb, mouseX, mouseY);
            //if ( realX < 0 || realX > realW ) return ;
            //if (realY < 0 || realY > realH ) return ;
            Bitmap bmp = new Bitmap(pb.Image);
            Color colour = bmp.GetPixel(real.X, real.Y);
            string color = "";
            color = colour.ToString();
            bmp.Dispose();
            toolStripStatusLabel2.Text = $"Mouse:{mouseX} x {mouseY} / Virtua:{real.X} x {real.Y} : {color}";
            toolStripStatusLabel4.BackColor = colour;
            toolStripStatusLabel4.Text = color;
            if (colour.GetBrightness() > 0.5) toolStripStatusLabel4.ForeColor = Color.Black;
            else toolStripStatusLabel4.ForeColor = Color.White;
        }


        private Point lastZoomCoord;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <see cref="https://stackoverflow.com/questions/48716515/how-to-get-real-image-pixel-point-x-y-from-picturebox"/>
        private void picResult_MouseMove(object sender, MouseEventArgs e)
        {
            string color = "";
            PictureBox pb = (sender as PictureBox);
            toolStripStatusLabel3.Text = $"pb.ClientRectangle: {pb.ClientRectangle}";
            if (pb.Image == null) return;

            // the 'real thing':
            //  Bitmap bmp = new Bitmap(pb.Image);
            //  Color colour = bmp.GetPixel(e.X, e.Y);
            //  color = colour.ToString();
            ShowCoords(picResult, e.X, e.Y);




            //https://stackoverflow.com/questions/2729751/how-do-i-draw-a-circle-and-line-in-the-picturebox
            //toolStripStatusLabel2.Text = $"{e.X} x {e.Y} : {color}";

            if (zoom)
            {
                DrawZoomedRegion(pb, e.X, e.Y);
            }
        }


        /// <summary>
        /// ** BitmapData **
        /// The following code example demonstrates how to use the BitmapData class with the LockBits and UnlockBits methods. This example is designed to be used with Windows Forms. To run this example, paste it into a form and handle the form's Paint event by calling the LockUnlockBitsExample method, passing e as PaintEventArgs.
        /// </summary>
        /// <param name="e"></param>
        /// <see cref="https://learn.microsoft.com/en-us/dotnet/api/system.drawing.imaging.bitmapdata?redirectedfrom=MSDN&view=dotnet-plat-ext-6.0"/>
        private void LockUnlockBitsExample(PaintEventArgs e)
        {

            // Create a new bitmap.
            Bitmap bmp = new Bitmap(pbs[0].Image);

            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                    bmp.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            byte pixelSize = 4;
            // Set every third value to 255. A 24bpp bitmap will look red.  
            for (int counter = 0; counter < rgbValues.Length; counter += pixelSize)
            {
                rgbValues[counter] = 0;
                rgbValues[counter + 1] = 255;
                rgbValues[counter + 2] = 0;
                rgbValues[counter + 3] = 255;

            }

            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            bmp.UnlockBits(bmpData);

            // Draw the modified image.
            e.Graphics.DrawImage(bmp, 0, 0);
        }

        private void pictureBox4_Paint(object sender, PaintEventArgs e)
        {
            LockUnlockBitsExample(e);

        }

        private void lblColor1_Click(object sender, EventArgs e)
        {
            colorDialog1.AllowFullOpen = true;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                lblColor1.BackColor = colorDialog1.Color;
                this.Refresh();
                Traitement_pbs1_vers_picResult();
            }
        }

        private void lblColor2_Click(object sender, EventArgs e)
        {
            colorDialog1.AllowFullOpen = true;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                lblColor2.BackColor = colorDialog1.Color;
                this.Refresh();
                //picResult_MouseMove(picResult, new MouseEventArgs(MouseButtons.None, 0, lastZoomCoord.X, lastZoomCoord.Y, 0)); 
                if (zoom)
                    DrawZoomedRegion(picResult, lastZoomCoord.X, lastZoomCoord.Y);
            }
        }

        private void picResult_Click(object sender, EventArgs e)
        {
            picResult.SizeMode = PictureBoxSizeMode.Zoom;
            return;
            picResult.SizeMode = picResult.SizeMode.Next();
            picResult.Invalidate();
            ;
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }



        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (zoom)
                DrawZoomedRegion(picResult, lastZoomCoord.X, lastZoomCoord.Y);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (picResult.Image == null)
            {
                toolStripStatusLabelElapsed.Text = "Load pic in picResult first!";
                return;
            }
            myConsole.LogLine($"start chrono {numericUpDown1.Value}x: Bitmap bmp = new Bitmap(picResult.Image); GetPixel(0, 0);");
            var oc = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            Helpers.TimeRecord = new Helpers.TimeRecording();
            for (int i = 0; i < numericUpDown1.Value; i++)
            {
                Bitmap bmp = new Bitmap(picResult.Image);
                var w = bmp.GetPixel(0, 0);
            }

            this.Cursor = oc;
            Helpers.TimeRecord.checkin("finished");
            toolStripStatusLabelElapsed.Text =
                $"{numericUpDown1.Value}new Bitmap: {Helpers.TimeRecord.Last().elapsinceBeginning}ms ";
            myConsole.LogLine($"{numericUpDown1.Value}new Bitmap: {Helpers.TimeRecord.Last().elapsinceBeginning}ms ");

        }


        private void btnUnits_Click(object sender, EventArgs e)
        {
            // test à la con proposé par MS 
            //https://learn.microsoft.com/en-us/dotnet/api/system.drawing.graphicsunit?view=netframework-4.8&f1url=%3FappId%3DDev16IDEF1%26l%3DFR-FR%26k%3Dk(System.Drawing.GraphicsUnit)%3Bk(SolutionItemsProject)%3Bk(TargetFrameworkMoniker-.NETFramework%2CVersion%253Dv4.8)%3Bk(DevLang-csharp)%26rd%3Dtrue
            Bitmap bitmap1 = Bitmap.FromHicon(SystemIcons.Hand.Handle);
            //Graphics formGraphics = this.CreateGraphics();
            Graphics picbox2Graphics = pictureBox2.CreateGraphics(); // this.CreateGraphics();

            GraphicsUnit units = GraphicsUnit.Document;
            RectangleF bmpRectangleF = bitmap1.GetBounds(ref units);
            //ici, units et à nouveau GraphicsUnit.Pixel ! ???

            Rectangle bmpRectangle = Rectangle.Round(bmpRectangleF);
            picbox2Graphics.DrawRectangle(Pens.Blue, bmpRectangle);
            picbox2Graphics.Dispose();
        }

        private void toolStripMenuItemOpenFormTest_Click(object sender, EventArgs e)
        {
            new FormTest().ShowDialog();
        }

        private void btnImaCompo_Click(object sender, EventArgs e)
        {
            Seriallabs.Dessin.Helpers.ID = $"A{DateTime.Now.Minute}_";
            
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            //Image imaSrc = imaAll[imaIndex[1]];
            //imaSrc = Image.FromFile(@"C:\Users\olivierH\-dev-\IMG\Eagle2x.png");
            Image imaSrc = picBsource.Image;
            Bitmap bmpSrc = new Bitmap(imaSrc);
            int w = bmpSrc.Width;
            //Seriallabs.Dessin.Helpers.SaveJpeg(fn + "_o.jpeg", bmpSrc, 80);

            myConsole.LogLine("CreateBitmapComposée avec imaSrc <- picBsource.Image puis bmpSrc = new Bitmap(imaSrc);");
            using (var bc = Seriallabs.Dessin.BitmapComposée.CreateBitmapComposée(bmpSrc,
                       ImageAttributesExt.getTestImageAttributes4Hue))
            {
                myConsole.LogLine("   bc.BlendImageOver(ResourceImages1.gray_floral);");
                bc.BlendImageOver(ResourceImages1.gray_floral);

                myConsole.LogLine("   bc.BlendImageAt(ResourceImages1.EarOfWheat_adjusted,new Point(50,50));");
                //bc.BlendImageAt(ResourceImages1.EarOfWheat_adjusted,new Point(50,50), (ImageAttributes) ImageAttributesExt.getTestImageAttributes4Hue);
                //bc.BlendImageAt(ResourceImages1.EarOfWheat_adjusted, new Point(50, 50), (ImageAttributes)new ImageAttributesExt().SetOpacity(0.25f));
                bc.BlendImageAt(ResourceImages1.EarOfWheat_adjusted, new Point(50, 50),
                    ImageAttributesExt.getImageAttr4Opacity(0.25f));

                //picResult.Image =
                if (trackBarTexture.Value>0) bc.AddTexture(pbs[1].Image,(int)nupTextureTiles.Value,trackBarTexture.Value/100f);
                //return;
                ;
                if (chkToPicResult.Checked) picResult.Image = bc.getBitmap;
                pictureBox5.Image = bc.getBitmap;
                //pictureBox5.Invalidate();
            }

            Seriallabs.Dessin.Helpers.WriteTempPictureForTesting(pictureBox5.Image, "-pictureBox5");
            
          }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            pictureBox5.Image = ResourceImages1.test_ciment;
        }

        private void btnCheckDrawPix_Click(object sender, EventArgs e)
        {
            using (Graphics g = picResult.CreateGraphics())
            {
                g.SmoothingMode = SmoothingMode.None;
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.DrawImage(ResourceImages1._8x8x8, new Point(7, 7));
            }

            myConsole.LogLine("(picResult.CreateGraphics) . DrawImage(ResourceImages1._8x8x8,new Point(7,7));");
        }

        private void toggleLogWindowsMenu_Click(object sender, EventArgs e)
        {
            if (myConsole.Visible) myConsole.Hide();
            else myConsole.Show();
        }

        private void picBlueBlend_Click(object sender, EventArgs e)
        {
            var pB = (PictureBox) sender;
            //Bitmap b = new Bitmap(pB.ClientRectangle.Width, pB.ClientRectangle.Height);
            Bitmap b = new Bitmap(280, 200);
            float fax = (float)b.Width / 350;
            float fay = (float)b.Height / 350;
            //float fax = (float) pB.ClientRectangle.Width / 350;
            //float fay = (float) pB.ClientRectangle.Height / 350;

            using (var g = Graphics.FromImage(b))
            {
                // Create pens.
                Pen redPen = new Pen(Color.Red, 15);
                Pen greenPen = new Pen(Color.Green, 5);
                SolidBrush sBrush1 = new SolidBrush(Color.FromArgb(255,128,128,0));
                g.DrawRectangle(greenPen,0,0,b.Width,b.Height);
                // Create points that define curve.
                /*Point point1 = new Point((int)50*fax, (int)50 * fay);
                Point point2 = new Point((int)100 * fax, (int)25 * fay);
                Point point3 = new Point((int)200 * fax, (int)5 * fay);
                Point point4 = new Point((int)250 * fax, (int)50 * fay);
                Point point5 = new Point((int)300 * fax, (int)100 * fay);
                Point point6 = new Point((int)350 * fax, (int)200 * fay);
                Point point7 = new Point((int)250 * fax, (int)250 * fay);*/
                var point1 = new PointF(50 * fax, 50 * fay);
                var point2 = new PointF(100 * fax, 25 * fay);
                var point3 = new PointF(200 * fax, 5 * fay);
                var point4 = new PointF(250 * fax, 50 * fay);
                var point5 = new PointF(300 * fax, 100 * fay);
                var point6 = new PointF(350 * fax, 200 * fay);
                var point7 = new PointF(250 * fax, 250 * fay);
                PointF[] curvePoints = {point1, point2, point3, point4, point5, point6, point7};

                // Draw lines between original points to screen.
                g.DrawLines(redPen, curvePoints);

                // Create tension and fill mode.
                float tension = 1.0F;
                FillMode aFillMode = FillMode.Alternate;

                // Draw closed curve to screen.
                g.FillClosedCurve(sBrush1, curvePoints, aFillMode);
                g.DrawClosedCurve(greenPen, curvePoints, tension, aFillMode);

                //FillMode: Member of the FillMode enumeration that determines how the curve is filled.This parameter is required but ignored.
            }

            pB.Image = b;

        }

        private void picRedBlend_Click(object sender, EventArgs e)
        {
            Bitmap b = new Bitmap(120, 150);

            var pB = (PictureBox) sender;
            var r = new RectangleF(Point.Empty, b.Size);//pB.ClientRectangle;
            var rr = pB.Size;
            //Bitmap b = new Bitmap(r.Width,  r.Height);

            using (var g = Graphics.FromImage(b))
            {
                g.DrawRectangle(new Pen(Color.Brown,3), 0, 0, b.Width, b.Height);
                SolidBrush sBrush1 = new SolidBrush(Color.FromArgb(255,0,128,128));
                g.FillRectangle(sBrush1,r.Width/4,r.Height/4,r.Width*2/4,r.Height*2/4);
            }

            pB.Image = b;
            toolStripStatusLabel2.Text = "blendbox:"+b.Size.ToString();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //Bitmap b1 = (Bitmap) picBlueBlend.Image;
            Bitmap b1 = (Bitmap)picBsource.Image;
            Bitmap b2 = (Bitmap)picRedBlend.Image;
            OpEx.ColorCalculationType op = OpEx.ColorCalculationType.Average;
            op =Enum.Parse<OpEx.ColorCalculationType>(lstArithmetics.SelectedValue.ToString());

            myConsole.LogLine($"start chrono {numericUpDown1.Value}x: BlendWithArithmetic ( {lstArithmetics.SelectedValue.ToString()}) ");
            var oc = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            Helpers.TimeRecord = new Helpers.TimeRecording();

            Image ima=null;
            for (int i = 0; i < numericUpDown1.Value; i++)
            {
                ima = OpEx.BlendWithArithmetic(b1, b2,
                    new Point(b1.Width - b2.Width * 7 / 10, b1.Height - b2.Height * 7 / 10),
                    op);
            }

            this.Cursor = oc;
            Helpers.TimeRecord.checkin("finished");
            toolStripStatusLabelElapsed.Text =
                $"{numericUpDown1.Value}new Bitmap: {Helpers.TimeRecord.Last().elapsinceBeginning}ms ";
            myConsole.LogLine($"{numericUpDown1.Value} BlendWithArithmetic: {Helpers.TimeRecord.Last().elapsinceBeginning}ms ");

            picBlend.Image = ima;
            //picBlend.Image = OpEx.BlendWithArithmetic(b1, b2, new Point(250,180), op);
            //picBlend.Image = OpEx.BlendWithArithmetic(b1, b2, new Point(5, 8), op);
        }

        private void picBlend_Click(object sender, EventArgs e)
        {
            var pB = (PictureBox)sender;
            var g = pB.CreateGraphics();
            var destR = pB.ClientRectangle;
            int zoomfactor = 10;
            var p = new Pen(Color.FromArgb(70, Color.Brown));
            for (int i = 0; i < destR.Width; i += zoomfactor)
                g.DrawLine(p, i, 0, i, destR.Height);
            for (int i = 0; i < destR.Height; i += zoomfactor)
                g.DrawLine(p, 0, i, destR.Width, i);
            zoomfactor = 50;
            p = new Pen(Color.FromArgb(200, Color.OrangeRed));
            for (int i = 0; i < destR.Width; i += zoomfactor)
                g.DrawLine(p, i, 0, i, destR.Height);
            for (int i = 0; i < destR.Height; i += zoomfactor)
                g.DrawLine(p, 0, i, destR.Width, i);
        }

        private void lstInterpolationModes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
/////////////////////////////////////////////////////////////////////////////////////////////////////////

    

/*
 * https://learn.microsoft.com/en-us/dotnet/api/system.drawing.graphics.drawimage?view=dotnet-plat-ext-6.0#system-drawing-graphics-drawimage(system-drawing-image-system-drawing-pointf()-system-drawing-rectanglef-system-drawing-graphicsunit)
 *
  * Parameters
  * The destPoints parameter specifies three points of a parallelogram. The three PointF structures represent the upper-left, upper-right, and lower-left corners of the parallelogram. The fourth point is extrapolated from the first three to form a parallelogram.
The srcRect parameter specifies a rectangular portion of the image object to draw. This portion is scaled and sheared to fit inside the parallelogram specified by the destPoints parameter.
for Shearing!

image
    Image
    Image to draw.

destPoints
    PointF[]
    Array of three PointF structures that define a parallelogram.

srcRect
    RectangleF
    RectangleF structure that specifies the portion of the image object to draw.

srcUnit
    GraphicsUnit
    Member of the GraphicsUnit enumeration that specifies the units of measure used by the srcRect parameter.
*/
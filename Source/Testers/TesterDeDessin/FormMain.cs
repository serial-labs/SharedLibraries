using System.Collections;
using System.Collections.Immutable;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.Resources;
using Seriallabs.Dessin;

namespace TesterDeDessin
{
   
    public partial class FormMain : Form
    {
        bool zoom = true;
        private PictureBox[] pbs;
        private int[] imaIndex;
        private Image[] imaAll;
        private int nbImaEmbedded;
        private FormConsole myConsole;
        
        public FormMain()
        {
            InitializeComponent();
            nbImaEmbedded = GetEmbeddedImagesNames().Count;
            imaAll = new Image[nbImaEmbedded];
            for (int i = 0; i < nbImaEmbedded; i++)
            {
                imaAll[i] = (Image)ResourceImages1.ResourceManager.GetObject(GetEmbeddedImagesNames()[i]);
            }

            pbs= new PictureBox[] { picBsource, pictureBox1,pictureBox2, pictureBox3,picResult };
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
            myConsole = new FormConsole();
            myConsole.Show();
        }

        private PictureBoxSizeMode getModeFromComboOrZoom()
        {
           
            PictureBoxSizeMode result;
            if (Enum.TryParse<PictureBoxSizeMode>( toolStripComboBox1.Text, true, out result))
                return result;
            return PictureBoxSizeMode.Zoom;
        }
        private PictureBoxSizeMode getModeFromText(string text,PictureBoxSizeMode def=PictureBoxSizeMode.Zoom)
        {

            PictureBoxSizeMode result;
            if (Enum.TryParse<PictureBoxSizeMode>(text, true, out result))
                return result;
            if (Enum.TryParse<PictureBoxSizeMode>(text+"Image", true, out result))
                return result;
            return def;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <see cref="https://stackoverflow.com/questions/34826111/how-to-get-all-images-in-the-resources-as-list"/>
        static List<string> GetEmbeddedImagesNames()
        {
            /* Reference to your resources class -- may be named differently in your case */
            ResourceManager MyResourceClassForTextures =
                new ResourceManager(typeof(TesterDeDessin.ResourceImages1));

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
                .Select(x => x.Key.ToString()??"")
                .ToList();
        }

      

        private void picBsource_Click_1(object sender, EventArgs e)
        {
            var pbi=pbs.Select((pb, i) => new { i, pb }).Where(x => x.pb == sender).Select(x=>x.i ).First();
            if (++imaIndex[pbi] >= nbImaEmbedded) imaIndex[pbi] = 0;
            //pbs[pbi].Image = (Bitmap)ResourceImages1.ResourceManager.GetObject(GetEmbeddedImagesNames()[imaIndex[pbi]]);
            pbs[pbi].Image = imaAll[imaIndex[pbi]]; //pbs[(4+pbi - 1) % 4].Image;
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
            if (pbi==3 && zoom) return;
            string info = $"{pbi} - {imaIndex[pbi]} - {imaAll[imaIndex[pbi]].Width}x{imaAll[imaIndex[pbi]].Height}";
            Seriallabs.Dessin.Helpers.RenderTxt(e.Graphics, info, 16,true);
        }

        

        private void ouvrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select an image file.";
            ofd.Filter = "Png files (*.png)|*.png|Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamReader streamReader = new StreamReader(ofd.FileName);
                Bitmap sourceBitmap = (Bitmap)Bitmap.FromStream(streamReader.BaseStream);
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
            normalToolStripMenuItem.Checked=false;
            zoomToolStripMenuItem.Checked = false;
            centerToolStripMenuItem.Checked = false;
            stretchToolStripMenuItem.Checked = false;
            (sender as ToolStripMenuItem).Checked = true;

            string txt = (sender as ToolStripMenuItem).Text;
            PictureBoxSizeMode pbsm = getModeFromText(txt);
            toolStripStatusLabel1.Text = txt +'-'+Enum.GetName<PictureBoxSizeMode>(pbsm);
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


        private void ShowCoords(PictureBox pb,Int32 mouseX, Int32 mouseY)
        {
            
            
            Point real = Helpers.getRealCoordOr0(pb, mouseX, mouseY);
            //if ( realX < 0 || realX > realW ) return ;
            //if (realY < 0 || realY > realH ) return ;
            Bitmap bmp = new Bitmap(pb.Image);
            Color colour = bmp.GetPixel(real.X,real.Y);
            string color = "";
            color = colour.ToString();
            bmp.Dispose();
            toolStripStatusLabel2.Text = $"Mouse:{mouseX} x {mouseY} / Virtua:{real.X} x {real.Y} : {color}";
            toolStripStatusLabel4.BackColor = colour;
            toolStripStatusLabel4.Text = color;
            if (colour.GetBrightness()>0.5) toolStripStatusLabel4.ForeColor= Color.Black;
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
            string color="";
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
                rgbValues[counter+1] = 255;
                rgbValues[counter+2] = 0;
                rgbValues[counter+3] = 255;

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
                    DrawZoomedRegion(picResult,lastZoomCoord.X,lastZoomCoord.Y);
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

            var oc = this.Cursor;
            this.Cursor= Cursors.WaitCursor;
            
            Helpers.TimeRecord = new Helpers.TimeRecording();
            for (int i = 0; i < numericUpDown1.Value; i++) {
                Bitmap bmp = new Bitmap(picResult.Image);
                var  w = bmp.GetPixel(0, 0);
            }
            this.Cursor = oc;
            Helpers.TimeRecord.checkin("finished");
            toolStripStatusLabelElapsed.Text = $"{numericUpDown1.Value}new Bitmap: {Helpers.TimeRecord.Last().elapsinceBeginning}ms ";

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
            Seriallabs.Dessin.Helpers.ID = $"aA{DateTime.Now.Minute}_";
            var fn = $"c:\\temp\\oo\\{Seriallabs.Dessin.Helpers.ID}txx{DateTime.Now.Ticks}";
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            Image imaSrc = imaAll[imaIndex[1]];
            //imaSrc = Image.FromFile(@"C:\Users\olivierH\-dev-\IMG\Eagle2x.png");
            imaSrc = picBsource.Image;
            Bitmap bmpSrc = new Bitmap(imaSrc);
            int w= bmpSrc.Width;
            Seriallabs.Dessin.Helpers.SaveJpeg(fn + "_o.jpeg", bmpSrc, 80);
            
            myConsole.LogLine("CreateBitmapComposée avec imaSrc <- picBsource.Image puis bmpSrc = new Bitmap(imaSrc);");
            using ( var bc = Seriallabs.Dessin.BitmapComposée.CreateBitmapComposée(bmpSrc, ImageAttributesExt.getTestImageAttributes4Hue))
            {
                myConsole.LogLine("   bc.BlendImageOver(ResourceImages1.gray_floral);");
                bc.BlendImageOver(ResourceImages1.gray_floral);
                myConsole.LogLine("   bc.BlendImageAt(ResourceImages1.EarOfWheat_adjusted,new Point(50,50));");
                //bc.BlendImageAt(ResourceImages1.EarOfWheat_adjusted,new Point(50,50), (ImageAttributes) ImageAttributesExt.getTestImageAttributes4Hue);
                //bc.BlendImageAt(ResourceImages1.EarOfWheat_adjusted, new Point(50, 50), (ImageAttributes)new ImageAttributesExt().SetOpacity(0.25f));
                bc.BlendImageAt(ResourceImages1.EarOfWheat_adjusted, new Point(50, 50), ImageAttributesExt.getImageAttr4Opacity(0.25f));
                
                if (chkToPicResult.Checked) picResult.Image = bc.getBitmap;
                pictureBox5.Image = bc.getBitmap;
                //pictureBox5.Invalidate();
            }

            fn = $"c:\\temp\\oo\\{Seriallabs.Dessin.Helpers.ID}txx{DateTime.Now.Ticks}";
            try
            {
                Seriallabs.Dessin.Helpers.SaveJpeg(fn + ".jpeg", bmpSrc, 80);
                //sourceImage.Save($"'c:\temp\tt{DateTime.Now.TimeOfDay}.jpeg", );
                pictureBox5.Image.Save(fn + ".png", ImageFormat.Png);
            }
            catch (Exception ex)
            {
            }
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
                g.DrawImage(ResourceImages1._8x8x8,new Point(7,7));
            }
            myConsole.LogLine("(picResult.CreateGraphics) . DrawImage(ResourceImages1._8x8x8,new Point(7,7));");
        }

        private void toggleLogWindowsMenu_Click(object sender, EventArgs e)
        {
            if (myConsole.Visible) myConsole.Hide();
            else myConsole.Show();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////
    }


}

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
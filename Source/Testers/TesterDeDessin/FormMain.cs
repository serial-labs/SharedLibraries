using System.Collections;
using System.Collections.Immutable;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.Resources;

namespace TesterDeDessin
{
   
    public partial class FormMain : Form
    {
        bool zoom = true;
        private PictureBox[] pbs;
        private int[] imaIndex;
        private Image[] imaAll;
        private int nbImaEmbedded;
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
            int fontSize = 16;
            using (Font myFont = new Font("Arial Black", 18))
            {
                Graphics g = e.Graphics;
                g.InterpolationMode = InterpolationMode.High;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit; 
                g.CompositingQuality = CompositingQuality.HighQuality;
                GraphicsPath p = new GraphicsPath();
                p.AddString(
                    info,             // text to draw
                    FontFamily.GenericSansSerif,  // or any other font family
                    (int)FontStyle.Regular,      // font style (bold, italic, etc.)
                    g.DpiY * fontSize / 72,       // em size
                    new Point(0, 0),              // location where to draw text
                    new StringFormat());          // set options here (e.g. center alignment)
                g.DrawPath(new Pen(Color.Yellow,4), p);
                g.FillPath(new SolidBrush(Color.Blue),p);
                //e.Graphics.DrawString(info, myFont, Brushes.Green, new Point(2, 2));

            }
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

      
        private void button2_Click(object sender, EventArgs e)
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

        //https://learn.microsoft.com/en-us/dotnet/desktop/winforms/advanced/how-to-use-interpolation-mode-to-control-image-quality-during-scaling?view=netframeworkdesktop-4.8
        private void FormMain_Load(object sender, EventArgs e)
        {
            lstInterpolationModes.DataSource = Enum.GetValues(typeof(InterpolationMode));
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
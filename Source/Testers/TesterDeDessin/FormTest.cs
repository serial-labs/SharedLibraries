using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TesterDeDessin
{
    public partial class FormTest : Form
    {
        public FormTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void FormTest_Load(object sender, EventArgs e)
        {

        }



        // Define DrawImageAbort callback method.
        private bool DrawImageCallback8(IntPtr callBackData)
        {
            toolStripStatusLabel1.Text = callBackData.ToString();

            // Test for call that passes callBackData parameter.
            if (callBackData == IntPtr.Zero)
            {

                // If no callBackData passed, abort DrawImage method.
                return true;
            }
            else
            {

                // If callBackData passed, continue DrawImage method.
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <see cref="https://learn.microsoft.com/en-us/dotnet/api/system.drawing.graphics.drawimage?view=netframework-4.7.2#system-drawing-graphics-drawimage(system-drawing-image-system-drawing-rectangle-system-single-system-single-system-single-system-single-system-drawing-graphicsunit-system-drawing-imaging-imageattributes-system-drawing-graphics-drawimageabort-system-intptr)"/>
        /// <remarks>Remarks
        /// The srcX, srcY, srcWidth, and srcHeight parameters specify a rectangular portion, of the image object to draw.The rectangle is relative to the upper-left corner of the source image.This portion is scaled to fit inside the rectangle specified by the destRect parameter.
        /// This overload with the callback and callbackData parameters provides the means to stop the drawing of an image once it starts according to criteria and data determined by the application. For example, an application could start drawing a large image and the user might scroll the image off the screen, in which case the application could stop the drawing.
        /// </remarks>
        public void DrawImageRect4FloatAttribAbortData(PaintEventArgs e)
        {

            // Create callback method.
            Graphics.DrawImageAbort imageCallback
                = new Graphics.DrawImageAbort(DrawImageCallback8);
            IntPtr imageCallbackData = new IntPtr(1);

            // Create image.
            Image newImage = ResourceImages1.Schweiz_Schloss_Chillon_Gesamtansicht;//Image.FromFile("SampImag.jpg");

            // Create rectangle for displaying original image.
            Rectangle destRect1 = new Rectangle(100, 25, 450, 150);

            // Create coordinates of rectangle for source image.
            float x = 50.0F;
            float y = 50.0F;
            float width = 150.0F;
            float height = 150.0F;
            GraphicsUnit units = GraphicsUnit.Pixel;

            // Draw original image to screen.
            e.Graphics.DrawImage(newImage, destRect1, x, y, width, height, units);

            // Create rectangle for adjusted image.
            Rectangle destRect2 = new Rectangle(100, 175, 450, 150);

            // Create image attributes and set large gamma.
            ImageAttributes imageAttr = new ImageAttributes();
            imageAttr.SetGamma(4.0F);

            // Draw adjusted image to screen.
            try
            {
                checked
                {

                    // Draw adjusted image to screen.
                    e.Graphics.DrawImage(
                        newImage,
                        destRect2,
                        x, y,
                        width, height,
                        units,
                        imageAttr,
                        imageCallback,
                        imageCallbackData);
                }
            }
            catch (Exception ex)
            {
                e.Graphics.DrawString(
                    ex.ToString(),
                    new Font("Arial", 8),
                    Brushes.Black,
                    new PointF(0, 0));
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            DrawImageRect4FloatAttribAbortData(e);
        }

        private void FormTest_Paint(object sender, PaintEventArgs e)
        {
            DrawImageRect4FloatAttribAbortData(e);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void statusStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}

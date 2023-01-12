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

        private class A
        {
            public virtual int p { get; set; } = 1;
            public override string ToString()
            {
                return $"p:{p}";
            }
        }

        private class B :A
        {
            public int pb { get; set; } = 2;
            public new string ToString()
            {
                return  $"pb:{pb}";
            }
        }
        private class C : A
        {
            public int pc { get; set; } = 3;
            public override string ToString()
            {
                return $"pc:{pc}";
            }
        }
        private class D : A
        {
            public int pd { get; set; } = 4;

        }
        private class E : A
        {
            public int pe { get; set; } = 5;
            public override int p { get; set; }=6;

        }

        private string AA(A a)
        {
            string s = a.ToString();
            return "AA:" + s;
        }
        private string BA(B b)
        {
            string s = b.ToString();
            return "BA:" + s;
        }
        private string CA(C c)
        {
            string s = c.ToString();
            return "CA:" + s;
        }
        private string AE(E e)
        {
            string s = e.ToString();
            return "AE:"+s;
        }
        private string AF(F f)
        {
            string s = f.ToString();
            return "AF:" + s;
        }

        private class F : A
        {
            public int pb { get; set; } =7;
            public string ToString() //pas de mot clef override ni new!
            {
                return $"pf:{pb}";
            }
        }

        public FormTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("test console");
            System.Diagnostics.Debug.WriteLine(AA(new A()) +" : base A passée comme A attendu");
            
            System.Diagnostics.Debug.WriteLine(AA(new B()) + " : dérivée B (new ToString) passée comme A attendu");
            System.Diagnostics.Debug.WriteLine(BA(new B()) + " : dérivée B (new ToString) passée comme B attendu");

            System.Diagnostics.Debug.WriteLine(AA(new C()) + " : dérivée C (override ToString) passée comme A attendu");
            System.Diagnostics.Debug.WriteLine(CA(new C()) + " : dérivée C (override ToString) passée comme C attendu");

            System.Diagnostics.Debug.WriteLine(AA(new D()) + " : dérivée D (no ToString) passée comme A attendu");
            System.Diagnostics.Debug.WriteLine(AA(new E()) + " : dérivée E (no ToString, override p=6) passée comme A attendu");

            System.Diagnostics.Debug.WriteLine(AA(new F()) + " : dérivée F (ToString sans mot clef) passée comme A attendu");
            System.Diagnostics.Debug.WriteLine(AF(new F()) + " : dérivée F (ToString sans mot clef) passée comme F attendu");



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

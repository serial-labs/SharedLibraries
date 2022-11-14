using System.Drawing;

namespace Seriallabs.Dessin
{
    public class AnchoredImage 
    {
        private Image image;
        public Image Image
        {
            get { return image; }
        } 
        public  Point Anchor { get; set; }
        public  float Angle  { get; set; }
        
        public AnchoredImage(Image image, Point anchor)
        {
            this.image = image;
            Anchor = anchor;
        }
        public AnchoredImage(Image image, Point anchor, float angle)
        {
            image = image;
            Anchor = anchor;
            Angle = angle;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        /// <see cref="https://stackoverflow.com/questions/12024406/how-can-i-rotate-an-image-by-any-degree"/>
        private Bitmap RotateImage(Bitmap bmp, float angle)
        {
            Bitmap rotatedImage = new Bitmap(bmp.Width, bmp.Height);
            rotatedImage.SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution);

            using (Graphics g = Graphics.FromImage(rotatedImage))
            {
                // Set the rotation point to the center in the matrix
                g.TranslateTransform(bmp.Width / 2, bmp.Height / 2);
                // Rotate
                g.RotateTransform(angle);
                // Restore rotation point in the matrix
                g.TranslateTransform(-bmp.Width / 2, -bmp.Height / 2);
                // Draw the image on the bitmap
                g.DrawImage(bmp, new Point(0, 0));
            }

            return rotatedImage;
        }
        public static Image Rotate(float angle)
        {
            /*try
            {
                bitmap1 = (Bitmap)Bitmap.FromFile(@"C:\Documents and Settings\" +
                                                  @"All Users\Documents\My Music\music.bmp");
                PictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                PictureBox1.Image = bitmap1;
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("There was an error." +
                                "Check the path to the bitmap.");
            }*/
            return null;
        }

    }
}
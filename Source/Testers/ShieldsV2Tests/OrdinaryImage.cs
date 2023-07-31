using System.Drawing.Imaging;
using seriallabs.Dessin;

namespace ShieldsV2Tests
{
    public class OrdinaryImage
    {
        public Metafile T1_Image { get; private init; }
        public Region T1_Region { get; private init; }

        public Metafile T2_Image { get; private init; }
        public Region T2_Region { get; private init; }

        public Metafile Border_Image { get; private init; }

        public OrdinaryImage(string name, string folderPath)
        {
            T1_Image = new(Path.Combine(folderPath, $"{name}_T1.emf"));
            T2_Image = new(Path.Combine(folderPath, $"{name}_T2.emf"));
            Border_Image = new(Path.Combine(folderPath, $"{name}_Bd.emf"));

            T1_Region = CalculateRegion(T1_Image);
            T2_Region = CalculateRegion(T2_Image);
        }

        public Image RenderFullImage()
        {
            Bitmap bmp = new(T1_Image);
            using Graphics g = Graphics.FromImage(bmp);

            g.DrawImage(
                    T2_Image,
                    new Rectangle(0, 0, bmp.Width, bmp.Height),
                    0, 0, T2_Image.Width, T2_Image.Height,
                    GraphicsUnit.Pixel);

            g.DrawImage(
                    Border_Image,
                    new Rectangle(0, 0, bmp.Width, bmp.Height),
                    0, 0, Border_Image.Width, Border_Image.Height,
                    GraphicsUnit.Pixel);

            return bmp;
        }

        private static Region CalculateRegion(Metafile emf)
        {
            using Bitmap bmp = new(emf, MainForm.BASE_REGION_WIDTH, (int)(MainForm.BASE_REGION_WIDTH * ((double)emf.Height / emf.Width)));
            return bmp.MakeNonTransparentRegion();
        }
    }
}

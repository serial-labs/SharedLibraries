using seriallabs;
using seriallabs.Dessin;
using seriallabs.Dessin.heraldry;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ShieldsV2Tests
{
    public partial class MainForm : Form
    {
        #region Constants
        private const string ROOT_FOLDER = @"C:/Icono/Shields/V2";

        private static readonly IReadOnlyDictionary<Tincture, Color> TINCTURES_TO_COLORS = new Dictionary<Tincture, Color>()
        {
            { Tincture.Or, Color.FromArgb(234, 173, 16) },
            { Tincture.Argent, Color.FromArgb(201, 201, 201) },
            { Tincture.Gules, Color.FromArgb(216, 41, 42) },
            { Tincture.Azure, Color.FromArgb(49, 112, 139) },
            { Tincture.Vert, Color.FromArgb(49, 137, 87) },
            { Tincture.Purpure, Color.FromArgb(120, 49, 137) },
            { Tincture.Sable, Color.FromArgb(42, 34, 20) },
            { Tincture.Field, Color.FromArgb(0, 0, 0, 0) },
        };

        private static readonly Color T1_REF_COLOR = Color.FromArgb(0, 255, 0); // Vert
        private static readonly Color T2_REF_COLOR = Color.FromArgb(0, 0, 255); // Bleu
        private static readonly Color T3_REF_COLOR = Color.FromArgb(255, 0, 0); // Rouge
        private static readonly Color OUTER_REF_COLOR = Color.FromArgb(255, 0, 255); // Magenta intense

        private static ImageAttributes _clean_outer_attr;
        private static ImageAttributes CLEAN_OUTER_ATTRIBUTES
        {
            get
            {
                if (_clean_outer_attr is null)
                {
                    ImageAttributes cleanupOuterAttributes = new();
                    List<ColorMap> colorMappings = new();
                    AddAllOffsettedColors(OUTER_REF_COLOR, Color.FromArgb(0, 0, 0, 0), colorMappings);
                    cleanupOuterAttributes.SetRemapTable(colorMappings.ToArray());

                    _clean_outer_attr = cleanupOuterAttributes;
                }

                return _clean_outer_attr;
            }
        }
        #endregion

        #region Properties
        private List<(Metafile Image, Region Mask3000w)> Shields { get; set; }
        private List<Metafile> Partitions { get; set; }
        private List<Metafile> Fields { get; set; }

        private (Metafile Image, Region Mask3000w) CurrentShieldImg { get; set; }
        private Metafile CurrentPartitionImg { get; set; }
        private Metafile CurrentFieldImg { get; set; }

        private Tincture PartitionT1 => (Tincture)partitionT1list.SelectedValue;
        private Tincture PartitionT2 => (Tincture)partitionT2list.SelectedValue;


        private Tincture FieldT1 => (Tincture)fieldT1list.SelectedValue;
        private Tincture FieldT2 => (Tincture)fieldT2list.SelectedValue;

        #endregion

        #region Constructor
        public MainForm()
        {
            InitializeComponent();

            // Fill image lists
            Shields = Directory.GetFiles(Path.Combine(ROOT_FOLDER, "Shields"))
                .Where(path => Path.GetExtension(path) == ".emf")
                .Select(path =>
                {
                    Metafile img = new(path);
                    using Metafile maskImg = new(Path.Combine(ROOT_FOLDER, "Shields", "Masks", Path.GetFileName(path)));
                    using Bitmap bmp = new(maskImg, 3000, (int)(3000 * ((double)img.Height / img.Width)));
                    Region reg = bmp.MakeNonTransparentRegion();
                    return (img, reg);
                })
                .ToList();

            Partitions = Directory.GetFiles(Path.Combine(ROOT_FOLDER, "Partitions"))
                .Where(path => Path.GetExtension(path) == ".emf")
                .Select(path => new Metafile(path))
                .ToList();

            Fields = Directory.GetFiles(Path.Combine(ROOT_FOLDER, "Fields"))
                .Where(path => Path.GetExtension(path) == ".emf")
                .Select(path => new Metafile(path))
                .ToList();

            // Init current images
            SetCurrentShieldImage(Shields[0]);
            SetCurrentPartitionImg(Partitions[0]);
            SetCurrentFieldImg(Fields[0]);

            // Init tinctures lists
            var tinctures = Enum.GetValues(typeof(Tincture))
                .Cast<Tincture>();

            partitionT1list.DataSource = tinctures.ToList();
            partitionT1list.SelectedItem = Tincture.Vert;

            partitionT2list.DataSource = tinctures.ToList();
            partitionT2list.SelectedItem = Tincture.Field;

            fieldT1list.DataSource = tinctures.Where(t => t != Tincture.Field).ToList();
            fieldT1list.SelectedItem = Tincture.Gules;

            fieldT2list.DataSource = tinctures.Where(t => t != Tincture.Field).ToList();
            fieldT2list.SelectedItem = Tincture.Or;

            // Init other lists
            emfQualityList.DataSource = Enum.GetValues(typeof(CompositingQuality)).Cast<CompositingQuality>().ToList();
            emfQualityList.SelectedItem = CompositingQuality.HighSpeed;
        }
        #endregion

        #region Methods

        #region Base Images
        public void SetCurrentShieldImage((Metafile Image, Region Mask3000w) shieldImg)
        {
            CurrentShieldImg = shieldImg;
            shieldPictureBox.Image = shieldImg.Image;
        }
        public void SetCurrentPartitionImg(Metafile image)
        {
            CurrentPartitionImg = image;
            partitionPictureBox.Image = image;
        }
        public void SetCurrentFieldImg(Metafile image)
        {
            CurrentFieldImg = image;
            fieldPictureBox.Image = image;
        }

        public void NextShieldImage() => SetCurrentShieldImage(Shields[(Shields.IndexOf(CurrentShieldImg) + 1) % Shields.Count]);
        public void NextPartitionImage() => SetCurrentPartitionImg(Partitions[(Partitions.IndexOf(CurrentPartitionImg) + 1) % Partitions.Count]);
        public void NextFieldImg() => SetCurrentFieldImg(Fields[(Fields.IndexOf(CurrentFieldImg) + 1) % Fields.Count]);
        #endregion

        #region Rendering
        public void Render()
        {
            // INIT
            renderButton.Enabled = false;
            Cursor = Cursors.WaitCursor;

            bool displaySteps = displayStepsCheckBox.Checked;

            if (!displaySteps)
            {
                colorizedFieldPicBox.Image = null;
                backgroundPicBox.Image = null;
                colorizerdPartitionPicBox.Image = null;
            }

            SmoothingMode emfSmoothing = smoothingCheckBox.Checked ? SmoothingMode.AntiAlias : SmoothingMode.None;
            CompositingQuality emfQuality = (CompositingQuality)emfQualityList.SelectedValue;

            Metafile shieldMetaFile = ConvertToEmfPlus(CurrentShieldImg.Image, emfSmoothing, emfQuality);
            Metafile fieldMetaFile = ConvertToEmfPlus(CurrentFieldImg, emfSmoothing, emfQuality);
            Metafile partitionMetaFile = ConvertToEmfPlus(CurrentPartitionImg, emfSmoothing, emfQuality);

            // GO
            Stopwatch sw = Stopwatch.StartNew();

            float ratio = (float)shieldMetaFile.Height / shieldMetaFile.Width;

            int width = widthSlider.Value * 100;

            Bitmap canva = new(width, (int)(width * ratio));

            using Graphics gCanva = Graphics.FromImage(canva);

            // CONFIG
            gCanva.SmoothingMode = SmoothingMode.None;
            gCanva.InterpolationMode = InterpolationMode.NearestNeighbor;
            gCanva.CompositingMode = CompositingMode.SourceCopy;

            GraphicsUnit gu = gCanva.PageUnit;

            RectangleF destRf = canva.GetBounds(ref gu);
            Rectangle destR = destRf.ToRectangle();

            // MASK
            Region mask = new(CurrentShieldImg.Mask3000w.GetRegionData());
            System.Drawing.Drawing2D.Matrix maskTransform = new();
            float maskScaleRatio = width / 3000f;
            maskTransform.Scale(maskScaleRatio, maskScaleRatio);
            mask.Transform(maskTransform);
            gCanva.Clip = mask;

            // FIELD
            if (PartitionT1 == Tincture.Field || PartitionT2 == Tincture.Field)
            {
                ImageAttributes fieldImageAttributes = ComputeImageAttributeForField(FieldT1, FieldT2);

                gCanva.DrawImage(
                    fieldMetaFile,
                    destR,
                    0, 0, fieldMetaFile.Width, fieldMetaFile.Height,
                    GraphicsUnit.Pixel,
                    fieldImageAttributes);

                if (displaySteps)
                    colorizedFieldPicBox.Image = new Bitmap(canva);
            }

            // PARTITION
            ImageAttributes partitionImageAttributes = ComputeImageAttributeForPartition(PartitionT1, PartitionT2);

            if (displaySteps)
            {
                Bitmap colorizedPartition = new(canva.Width, canva.Height);
                using Graphics gPartition = Graphics.FromImage(colorizedPartition);
                gPartition.Clip = mask;

                gPartition.DrawImage(
                    partitionMetaFile,
                    destR,
                    0, 0, partitionMetaFile.Width, partitionMetaFile.Height,
                    GraphicsUnit.Pixel,
                    partitionImageAttributes);

                colorizerdPartitionPicBox.Image = new Bitmap(colorizedPartition);
            }

            gCanva.DrawImage(
                partitionMetaFile,
                destR,
                0, 0, partitionMetaFile.Width, partitionMetaFile.Height,
                GraphicsUnit.Pixel,
                partitionImageAttributes);

            if (displaySteps)
                backgroundPicBox.Image = new Bitmap(canva);

            // SHIELD
            gCanva.Clip = new(); // Do not set to null (exception)

            gCanva.DrawImage(
                shieldMetaFile,
                destR,
                0, 0, shieldMetaFile.Width, shieldMetaFile.Height,
                GraphicsUnit.Pixel);

            // END
            resultPictureBox.Image = canva;

            renderLabel.Text = $"Rendered: {sw.ElapsedMilliseconds}ms";

            Cursor = Cursors.Default;
            renderButton.Enabled = true;
        }

        private static void AddAllOffsettedColors(Color oldColor, Color newColor, List<ColorMap> mappings)
        {
            const int COLOR_SPREAD_OFFSET = 2;

            static int SafeStart(int colorValue) => Math.Max(0, colorValue - COLOR_SPREAD_OFFSET);
            static int SafeEnd(int colorValue) => Math.Min(255, colorValue + COLOR_SPREAD_OFFSET);

            static IEnumerable<int> SafeOffsettedColorRange(int colorValue)
            {
                for (int v = SafeStart(colorValue); v <= SafeEnd(colorValue); v++)
                    yield return v;
            }

            var redValues = SafeOffsettedColorRange(oldColor.R);
            var greenValues = SafeOffsettedColorRange(oldColor.G);
            var blueValues = SafeOffsettedColorRange(oldColor.B);

            foreach (int R in redValues)
            {
                foreach (int G in greenValues)
                {
                    foreach (int B in blueValues)
                    {
                        mappings.Add(new()
                        {
                            OldColor = Color.FromArgb(R, G, B),
                            NewColor = newColor
                        });
                    }
                }
            }
        }
        private static ImageAttributes ComputeImageAttributeForField(Tincture t1, Tincture t2)
        {
            ImageAttributes imageAttributes = new();
            List<ColorMap> colorMappings = new();

            // T1
            AddAllOffsettedColors(T1_REF_COLOR, TINCTURES_TO_COLORS[t1], colorMappings);

            // T2
            AddAllOffsettedColors(T2_REF_COLOR, TINCTURES_TO_COLORS[t2], colorMappings);

            imageAttributes.SetRemapTable(colorMappings.ToArray());

            return imageAttributes;
        }
        private static ImageAttributes ComputeImageAttributeForPartition(Tincture t1, Tincture t2)
        {
            ImageAttributes imageAttributes = new();
            List<ColorMap> colorMappings = new();

            // T1
            AddAllOffsettedColors(T1_REF_COLOR, TINCTURES_TO_COLORS[t1], colorMappings);

            // T2
            AddAllOffsettedColors(T2_REF_COLOR, TINCTURES_TO_COLORS[t2], colorMappings);

            imageAttributes.SetRemapTable(colorMappings.ToArray());

            return imageAttributes;
        }
        #endregion

        #region Zoom on cursor
        private void DrawZoomedRegion(PictureBox sourcePb, int sourceX, int sourceY)
        {
            if (sourcePb?.Image is null)
                return;

            const int ZOOM_FACTOR = 15;

            Point real = GetRealCoordOr0(sourcePb, sourceX, sourceY);

            //lastZoomCoord = new Point(sourceX, sourceY);

            try
            {
                int zoomedZoneW = 1 + (lookupPicBox.ClientRectangle.Width - 1) / ZOOM_FACTOR; //arrondir au pixel sup si dépasse. ex. à zF=3, 15=>5 et 16=>6
                int zoomedZoneH = 1 + (lookupPicBox.ClientRectangle.Height - 1) / ZOOM_FACTOR;
                int zoomedZone_Left = real.X - zoomedZoneW / 2; if (zoomedZone_Left < 0) zoomedZone_Left = 0;
                int zoomedZone_Right = real.Y - zoomedZoneH / 2; if (zoomedZone_Right < 0) zoomedZone_Right = 0;
                Point offsetgrid = new Point(ZOOM_FACTOR / 2, ZOOM_FACTOR / 2);

                Rectangle sourceR = new Rectangle(zoomedZone_Left, zoomedZone_Right, zoomedZoneW, zoomedZoneH);
                Rectangle destR = new Rectangle(0, 0, zoomedZoneW * ZOOM_FACTOR, zoomedZoneH * ZOOM_FACTOR);

                Bitmap bmp = new Bitmap(lookupPicBox.ClientRectangle.Width, lookupPicBox.ClientRectangle.Height);

                //Pen pen = new Pen(Color.FromArgb(10 + ZOOM_FACTOR * 100 / 25, lblColor2.BackColor));

                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.CompositingMode = CompositingMode.SourceCopy;
                    g.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.PixelOffsetMode = PixelOffsetMode.None; //https://stackoverflow.com/questions/28441479/what-is-pixeloffsetmode

                    g.DrawImage(sourcePb.Image, destR, sourceR, GraphicsUnit.Pixel);

                    //for (int i = offsetgrid.X; i < destR.Width; i += ZOOM_FACTOR)
                    //    g.DrawLine(pen, i, 0, i, destR.Height);
                    //for (int i = offsetgrid.Y; i < destR.Height; i += ZOOM_FACTOR)
                    //    g.DrawLine(pen, 0, i, destR.Width, i);
                }
                Color colour = bmp.GetPixel(zoomedZoneW / 2, zoomedZoneH / 2);

                lookupPicBox.Image = bmp;

                exceptMessageLabel.Text = "-";
            }
            catch (Exception ex)
            {
                exceptMessageLabel.Text = ex.Message;
            }
        }

        public static Point GetRealCoordOr0(PictureBox pb, int mouseX, int mouseY)
        {
            if (pb.Image == null)
                return default;

            int realW = pb.Image.Width;
            int realH = pb.Image.Height;

            int currentW = pb.ClientRectangle.Width; //Obtient le rectangle qui représente la zone cliente du contrôle.
            int currentH = pb.ClientRectangle.Height;

            double zoomW = (currentW / (double)realW);
            double zoomH = (currentH / (double)realH);
            double zoomActual = Math.Min(zoomW, zoomH);
            double padX = zoomActual == zoomW ? 0 : (currentW - (zoomActual * realW)) / 2;
            double padY = zoomActual == zoomH ? 0 : (currentH - (zoomActual * realH)) / 2;

            int realX = (int)((mouseX - padX) / zoomActual);
            int realY = (int)((mouseY - padY) / zoomActual);

            if (realX < 0) realX = 0;
            if (realX >= realW) realX = realW - 1;
            if (realY < 0) realY = 0;
            if (realY >= realH) realY = realH - 1;

            return new Point(realX, realY);
        }
        #endregion

        #region EMF To EMF+

        // https://stackoverflow.com/questions/1783130/draw-emf-antialiased

        // https://stackoverflow.com/questions/37967951/how-to-enable-anti-aliasing-when-rendering-wmf-to-bitmap-in-c-wpf-winforms

        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        internal static extern int GdipConvertToEmfPlus(
            HandleRef graphics,
            HandleRef metafile,
            out bool conversionSuccess,
            EmfType emfType,
            [MarshalAs(UnmanagedType.LPWStr)]
            string description,
            out IntPtr convertedMetafile);

        private static Metafile ConvertToEmfPlus(Metafile metafile, SmoothingMode smoothingMode, CompositingQuality quality, CompositingMode compositingMode = CompositingMode.SourceOver)
        {
            // De ce que je comprends, on a juste besoin d'un Graphics pour communiquer les paramètres de smoothing, etc...
            // Donc on fait une Bitmap minuscule pour économiser des ressources
            using Bitmap bmp = new(1, 1);
            using var graphics = Graphics.FromImage(bmp);

            graphics.SmoothingMode = smoothingMode;
            graphics.CompositingQuality = quality;
            graphics.CompositingMode = compositingMode;

            var metafileHandleField = typeof(Metafile).GetField("nativeImage", BindingFlags.Instance | BindingFlags.NonPublic);
            var imageAttributesHandleField = typeof(ImageAttributes).GetField("nativeImageAttributes", BindingFlags.Instance | BindingFlags.NonPublic);
            var graphicsHandleProperty = typeof(Graphics).GetProperty("NativeGraphics", BindingFlags.Instance | BindingFlags.NonPublic);
            var setNativeImage = typeof(Image).GetMethod("SetNativeImage", BindingFlags.Instance | BindingFlags.NonPublic);
            IntPtr mf = (IntPtr)metafileHandleField.GetValue(metafile);
            IntPtr g = (IntPtr)graphicsHandleProperty.GetValue(graphics);

            var status = GdipConvertToEmfPlus(new HandleRef(graphics, g),
                                              new HandleRef(metafile, mf),
                                              out bool isSuccess,
                                              EmfType.EmfPlusOnly,
                                              "",
                                              out IntPtr emfPlusHandle);
            if (status != 0)
            {
                throw new Exception("Can't convert");
            }

            Metafile emfPlus = (Metafile)System.Runtime.Serialization.FormatterServices.GetSafeUninitializedObject(typeof(Metafile));
            setNativeImage.Invoke(emfPlus, new object[] { emfPlusHandle });

            return emfPlus;
        }

        #endregion

        #endregion

        #region Events Handlers
        private void shieldPictureBox_Click(object sender, EventArgs e) => NextShieldImage();
        private void partitionPictureBox_Click(object sender, EventArgs e) => NextPartitionImage();
        private void fieldPictureBox_Click(object sender, EventArgs e) => NextFieldImg();

        private void resultPictureBox_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new()
            {
                Filter = "PNG Image|*.png",
                Title = "Save image",
                FileName = "shield.png"
            };

            dialog.ShowDialog();

            if (string.IsNullOrWhiteSpace(dialog.FileName))
                return;
            FileStream fs = (FileStream)dialog.OpenFile();

            resultPictureBox.Image.Save(fs, ImageFormat.Png);
        }

        private void renderButton_Click(object sender, EventArgs e) => Render();

        private void widthSlider_ValueChanged(object sender, EventArgs e) => widthLabel.Text = $"Image width: {widthSlider.Value * 100}px";

        private void OnPictureBoxMouseMove(object sender, MouseEventArgs e) => DrawZoomedRegion((PictureBox)sender, e.X, e.Y);
        #endregion

    }
}
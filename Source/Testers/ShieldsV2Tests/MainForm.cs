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
        private const string ROOT_FOLDER = @"C:/Icono/Shields";
        
        public const int BASE_REGION_WIDTH = 1000;

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
        #endregion

        #region Properties
        private List<(Metafile Image, Region MaskRefw, Rectangle AreaRefw)> Shields { get; set; }
        private List<OrdinaryImage> Ordinaries { get; set; }
        private List<Metafile> Fields { get; set; }

        private (Metafile Image, Region MaskRefw, Rectangle AreaRefw) CurrentShieldImg { get; set; }
        private OrdinaryImage CurrentOrdinaryImg { get; set; }
        private Metafile CurrentField1Img { get; set; }
        private Metafile CurrentField2Img { get; set; }


        private Tincture OrdinaryT1 => (Tincture)ordinaryT1list.SelectedValue;
        private Tincture OrdinaryT2 => (Tincture)ordinaryT2list.SelectedValue;


        private Tincture Field1T1 => (Tincture)field1T1list.SelectedValue;
        private Tincture Field1T2 => (Tincture)field1T2list.SelectedValue;

        private Tincture Field2T1 => (Tincture)field2T1list.SelectedValue;
        private Tincture Field2T2 => (Tincture)field2T2list.SelectedValue;

        #endregion

        #region Constructor
        public MainForm()
        {
            InitializeComponent();

            // Fill image lists
            Shields = Directory.GetFiles(Path.Combine(ROOT_FOLDER, "Shapes"))
                .Where(path => Path.GetExtension(path) == ".emf")
                .Select(path =>
                {
                    Metafile img = new(path);
                    using Metafile maskImg = new(Path.Combine(ROOT_FOLDER, "Shapes", "Masks", Path.GetFileName(path)));
                    using Bitmap bmp = new(maskImg, BASE_REGION_WIDTH, (int)(BASE_REGION_WIDTH * ((double)img.Height / img.Width)));
                    Region reg = bmp.MakeNonTransparentRegion();
                    using Bitmap bmp2 = new(img, BASE_REGION_WIDTH, (int)(BASE_REGION_WIDTH * ((double)img.Height / img.Width)));
                    Rectangle area = bmp2.FindRectangleAroundPic();
                    return (img, reg, area);
                })
                .ToList();

            Ordinaries = Directory.GetDirectories(Path.Combine(ROOT_FOLDER, "Ordinaries"))
                .Select(path => new OrdinaryImage(new DirectoryInfo(path).Name, path))
                .ToList();

            Fields = Directory.GetFiles(Path.Combine(ROOT_FOLDER, "Fields"))
                .Where(path => Path.GetExtension(path) == ".emf")
                .Select(path => new Metafile(path))
                .ToList();

            // Init current images
            SetCurrentShieldImage(Shields[0]);
            SetCurrentOrdinaryImg(Ordinaries[0]);
            SetField1Img(Fields[0]);
            SetField2Img(Fields[0]);

            // Init tinctures lists
            var tinctures = Enum.GetValues(typeof(Tincture))
                .Cast<Tincture>();

            ordinaryT1list.DataSource = tinctures.ToList();
            ordinaryT1list.SelectedItem = Tincture.Vert;

            ordinaryT2list.DataSource = tinctures.ToList();
            ordinaryT2list.SelectedItem = Tincture.Field;

            field1T1list.DataSource = tinctures.Where(t => t != Tincture.Field).ToList();
            field1T1list.SelectedItem = Tincture.Gules;

            field1T2list.DataSource = tinctures.Where(t => t != Tincture.Field).ToList();
            field1T2list.SelectedItem = Tincture.Or;

            field2T1list.DataSource = tinctures.Where(t => t != Tincture.Field).ToList();
            field2T1list.SelectedItem = Tincture.Sable;

            field2T2list.DataSource = tinctures.Where(t => t != Tincture.Field).ToList();
            field2T2list.SelectedItem = Tincture.Argent;

            // Init other lists
            emfQualityList.DataSource = Enum.GetValues(typeof(CompositingQuality)).Cast<CompositingQuality>().ToList();
            emfQualityList.SelectedItem = CompositingQuality.HighSpeed;
        }
        #endregion

        #region Methods

        #region Base Images
        public void SetCurrentShieldImage((Metafile Image, Region Mask3000w, Rectangle Area3000w) shieldImg)
        {
            CurrentShieldImg = shieldImg;
            shieldPictureBox.Image = shieldImg.Image;
        }
        public void SetCurrentOrdinaryImg(OrdinaryImage ordinary)
        {
            CurrentOrdinaryImg = ordinary;
            partitionPictureBox.Image = ordinary.RenderFullImage();
        }
        public void SetField1Img(Metafile image)
        {
            CurrentField1Img = image;
            field1PictureBox.Image = image;
        }
        public void SetField2Img(Metafile image)
        {
            CurrentField2Img = image;
            field2PictureBox.Image = image;
        }

        public void NextShieldImage() => SetCurrentShieldImage(Shields[(Shields.IndexOf(CurrentShieldImg) + 1) % Shields.Count]);
        public void NextPartitionImage() => SetCurrentOrdinaryImg(Ordinaries[(Ordinaries.IndexOf(CurrentOrdinaryImg) + 1) % Ordinaries.Count]);
        public void NextFieldImg1() => SetField1Img(Fields[(Fields.IndexOf(CurrentField1Img) + 1) % Fields.Count]);
        public void NextFieldImg2() => SetField2Img(Fields[(Fields.IndexOf(CurrentField2Img) + 1) % Fields.Count]);
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
                step1picBox.Image = null;
                step2picBox.Image = null;
            }

            SmoothingMode emfSmoothing = smoothingCheckBox.Checked ? SmoothingMode.AntiAlias : SmoothingMode.None;
            CompositingQuality emfQuality = (CompositingQuality)emfQualityList.SelectedValue;

            // Conversion � faire au chargement des images
            // Ici on les refait � chaque fois pour pouvoir modifier les param�tres de qualit� de rendu
            Metafile shieldBorderMetaFile = ConvertToEmfPlus(CurrentShieldImg.Image, emfSmoothing, emfQuality);
            Metafile field1MetaFile = ConvertToEmfPlus(CurrentField1Img, emfSmoothing, emfQuality);
            Metafile field2MetaFile = ConvertToEmfPlus(CurrentField2Img, emfSmoothing, emfQuality);
            Metafile ordinaryBorderFile = ConvertToEmfPlus(CurrentOrdinaryImg.Border_Image, emfSmoothing, emfQuality);

            // GO
            Stopwatch sw = Stopwatch.StartNew();

            // CALCUL TAILLE CANVA ET ZONE DE DRAW
            Rectangle usefullArea3000w = CurrentShieldImg.AreaRefw;

            double canvaAspectRatio = (double)usefullArea3000w.Height / usefullArea3000w.Width;
            int canvaWidth = widthSlider.Value * 100;
            int canvaHeight = (int)(canvaWidth * canvaAspectRatio);

            double sourceAspectRatio = (double)CurrentShieldImg.Image.Height / CurrentShieldImg.Image.Width;
            double destinationScale = (double)canvaWidth / usefullArea3000w.Width;

            Rectangle destR = new()
            {
                X = -(int)(usefullArea3000w.X * destinationScale), // - (canvaWidth / 500) : ajustement pour les marges vides de quelques pixels (valeur obtenue empyriquement)
                Y = -(int)(usefullArea3000w.Y * destinationScale),
                Width = (int)(BASE_REGION_WIDTH * destinationScale),
                Height = (int)(BASE_REGION_WIDTH * sourceAspectRatio * destinationScale),
            };

            Bitmap canva = new(canvaWidth, canvaHeight);
            using Graphics gCanva = Graphics.FromImage(canva);

            // MASK transform
            System.Drawing.Drawing2D.Matrix maskTransform = new();
            maskTransform.Scale((float)destinationScale, (float)destinationScale);
            maskTransform.Translate(-usefullArea3000w.X, -usefullArea3000w.Y);

            void DrawFieldOrTinct(Region tinctRegion, Tincture tincture, Image fieldImg, Tincture fieldT1, Tincture fieldT2)
            {
                // Mask
                Region drawRegion = new(CurrentShieldImg.MaskRefw.GetRegionData());
                drawRegion.Intersect(tinctRegion);
                drawRegion.Transform(maskTransform);

                gCanva.Clip = drawRegion;

                if(tincture != Tincture.Field)
                {
                    gCanva.FillRectangle(new SolidBrush(TINCTURES_TO_COLORS[tincture]), destR);
                }
                else
                {
                    ImageAttributes fieldImageAttributes = ComputeImageAttributeFor(fieldT1, fieldT2);

                    gCanva.DrawImage(
                        fieldImg,
                        destR,
                        0, 0, fieldImg.Width, fieldImg.Height,
                        GraphicsUnit.Pixel,
                        fieldImageAttributes);
                }
            }

            // T1
            DrawFieldOrTinct(CurrentOrdinaryImg.T1_Region, OrdinaryT1, field1MetaFile, Field1T1, Field1T2);

            if (displaySteps)
                step1picBox.Image = new Bitmap(canva);

            // T2
            DrawFieldOrTinct(CurrentOrdinaryImg.T2_Region, OrdinaryT2, field2MetaFile, Field2T1, Field2T2);

            if (displaySteps)
                step2picBox.Image = new Bitmap(canva);

            // Ordinary border
            Region insideShield = new(CurrentShieldImg.MaskRefw.GetRegionData());
            insideShield.Transform(maskTransform);
            gCanva.Clip = insideShield;
            gCanva.DrawImage(
                ordinaryBorderFile,
                destR,
                0, 0, ordinaryBorderFile.Width, ordinaryBorderFile.Height,
                GraphicsUnit.Pixel);

            // Shield border
            gCanva.Clip = new(); // Do not set to null (exception)
            gCanva.DrawImage(
                shieldBorderMetaFile,
                destR,
                0, 0, shieldBorderMetaFile.Width, shieldBorderMetaFile.Height,
                GraphicsUnit.Pixel);

            // END
            resultPictureBox.Image = canva;

            renderLabel.Text = $"Rendered: {sw.ElapsedMilliseconds}ms";
            finalSizeLabel.Text = $"W: {resultPictureBox.Image.Width} - H: {resultPictureBox.Image.Height} ({(float)resultPictureBox.Image.Height / resultPictureBox.Image.Width})";

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
        private static ImageAttributes ComputeImageAttributeFor(Tincture t1, Tincture t2)
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
                int zoomedZoneW = 1 + (lookupPicBox.ClientRectangle.Width - 1) / ZOOM_FACTOR; //arrondir au pixel sup si d�passe. ex. � zF=3, 15=>5 et 16=>6
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

            int currentW = pb.ClientRectangle.Width; //Obtient le rectangle qui repr�sente la zone cliente du contr�le.
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
            // De ce que je comprends, on a juste besoin d'un Graphics pour communiquer les param�tres de smoothing, etc...
            // Donc on fait une Bitmap minuscule pour �conomiser des ressources
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
        private void fieldPictureBox_Click(object sender, EventArgs e) => NextFieldImg1();
        private void field2PictureBox_Click(object sender, EventArgs e) => NextFieldImg2();

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
using seriallabs;
using seriallabs.Dessin.heraldry;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

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

        private static readonly Color T1_REF_COLOR = Color.FromArgb(0, 128, 0); // Vert moyen
        private static readonly Color T2_REF_COLOR = Color.FromArgb(0, 0, 255); // Bleu intense
        private static readonly Color T3_REF_COLOR = Color.FromArgb(127, 0, 0); // Rouge moyen
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
        private List<Image> Shields { get; set; }
        private List<Image> Partitions { get; set; }
        private List<Image> Fields { get; set; }

        private Image CurrentShieldImg { get; set; }
        private Image CurrentPartitionImg { get; set; }
        private Image CurrentFieldImg { get; set; }

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
            Shields = Directory.GetFiles(ROOT_FOLDER)
                .Where(file => Path.GetExtension(file) == ".emf")
                .Select(file => Image.FromFile(file))
                .ToList();

            Partitions = Directory.GetFiles(Path.Combine(ROOT_FOLDER, "Partitions"))
                .Where(file => Path.GetExtension(file) == ".emf")
                .Select(file => Image.FromFile(file))
                .ToList();

            Fields = Directory.GetFiles(Path.Combine(ROOT_FOLDER, "Fields"))
                .Where(file => Path.GetExtension(file) == ".emf")
                .Select(file => Image.FromFile(file))
                .ToList();

            // Init current images
            SetCurrentShieldImage(Shields[0]);
            SetCurrentPartitionImg(Partitions[0]);
            SetCurrentFieldImg(Fields[0]);

            // Init tinctures lists
            var tinctures = Enum.GetValues(typeof(Tincture))
                .Cast<Tincture>();

            partitionT1list.DataSource = tinctures.ToList();
            partitionT1list.SelectedItem = Tincture.Field;

            partitionT2list.DataSource = tinctures.ToList();
            partitionT2list.SelectedItem = Tincture.Or;

            fieldT1list.DataSource = tinctures.Where(t => t != Tincture.Field).ToList();
            fieldT1list.SelectedItem = Tincture.Sable;

            fieldT2list.DataSource = tinctures.Where(t => t != Tincture.Field).ToList();
            fieldT2list.SelectedItem = Tincture.Argent;
        }
        #endregion

        #region Public Methods
        public void SetCurrentShieldImage(Image image)
        {
            CurrentShieldImg = image;
            shieldPictureBox.Image = image;
        }
        public void SetCurrentPartitionImg(Image image)
        {
            CurrentPartitionImg = image;
            partitionPictureBox.Image = image;
        }
        public void SetCurrentFieldImg(Image image)
        {
            CurrentFieldImg = image;
            fieldPictureBox.Image = image;
        }

        public void NextShieldImage() => SetCurrentShieldImage(Shields[(Shields.IndexOf(CurrentShieldImg) + 1) % Shields.Count]);
        public void NextPartitionImage() => SetCurrentPartitionImg(Partitions[(Partitions.IndexOf(CurrentPartitionImg) + 1) % Partitions.Count]);
        public void NextFieldImg() => SetCurrentFieldImg(Fields[(Fields.IndexOf(CurrentFieldImg) + 1) % Fields.Count]);


        public void Render()
        {
            // INIT
            renderButton.Enabled = false;
            Cursor = Cursors.WaitCursor;

            const int WIDTH = 1200;
            const int HEIGHT = 1400;

            Stopwatch sw = Stopwatch.StartNew();

            Bitmap result = new(WIDTH, HEIGHT);

            using Graphics g = Graphics.FromImage(result);

            // CONFIG
            g.SmoothingMode = SmoothingMode.HighSpeed;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;

            GraphicsUnit gu = g.PageUnit;

            RectangleF destRf = result.GetBounds(ref gu);
            Rectangle destR = destRf.ToRectangle();

            Rectangle sourceR = new(0, 0, WIDTH, HEIGHT);

            // FIELD
            Bitmap colorizedField = null;
            if (PartitionT1 == Tincture.Field || PartitionT2 == Tincture.Field)
            {
                colorizedField = new(result.Width, result.Height);

                using Graphics gField = Graphics.FromImage(colorizedField);

                ImageAttributes fieldImageAttributes = ComputeImageAttributeForTinctures(FieldT1, FieldT2);

                lock (CurrentFieldImg)
                {
                    gField.DrawImage(
                        CurrentFieldImg,
                        destR,
                        sourceR.Left, sourceR.Top, sourceR.Width, sourceR.Height,
                        GraphicsUnit.Pixel,
                        fieldImageAttributes);
                }

                colorizedFieldPicBox.Image = colorizedField;

                g.DrawImage(
                    colorizedField,
                    destR,
                    sourceR.Left, sourceR.Top, sourceR.Width, sourceR.Height,
                    GraphicsUnit.Pixel);
            }

            // PARTITION
            Bitmap colorizedPartition = new(result.Width, result.Height);

            using Graphics gPartition = Graphics.FromImage(colorizedPartition);

            ImageAttributes partitionImageAttributes = ComputeImageAttributeForTinctures(PartitionT1, PartitionT2);

            lock (CurrentPartitionImg)
            {
                gPartition.DrawImage(
                    new Bitmap(CurrentPartitionImg),
                    destR,
                    sourceR.Left, sourceR.Top, sourceR.Width, sourceR.Height,
                    GraphicsUnit.Pixel,
                    partitionImageAttributes);
            }

            colorizerdPartitionPicBox.Image = colorizedPartition;

            g.DrawImage(
                colorizedPartition,
                destR,
                sourceR.Left, sourceR.Top, sourceR.Width, sourceR.Height,
                GraphicsUnit.Pixel);

            backgroundPicBox.Image = new Bitmap(result);

            // SHIELD
            g.DrawImage(
                CurrentShieldImg,
                destR,
                sourceR.Left, sourceR.Top, sourceR.Width, sourceR.Height,
                GraphicsUnit.Pixel);

            var copy = new Bitmap(result);

            shieldAddedPicbox.Image = copy;

            g.Clear(new Color());

            g.DrawImage(
                copy,
                destR,
                sourceR.Left, sourceR.Top, sourceR.Width, sourceR.Height,
                GraphicsUnit.Pixel,
                CLEAN_OUTER_ATTRIBUTES);

            // END
            resultPictureBox.Image = result;

            renderLabel.Text = $"Rendered: {sw.ElapsedMilliseconds}ms";

            Cursor = Cursors.Default;
            renderButton.Enabled = true;
        }


        private static void AddAllOffsettedColors(Color oldColor, Color newColor, List<ColorMap> mappings)
        {
            const int COLOR_SPREAD_OFFSET = 3;

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
        private static ImageAttributes ComputeImageAttributeForTinctures(Tincture t1, Tincture t2)
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
        #endregion

        
    }
}
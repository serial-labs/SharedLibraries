using System.Drawing.Imaging;

namespace ShieldsV2Tests
{
    public partial class MainForm : Form
    {
        #region Constants
        private const string ROOT_FOLDER = @"C:/Icono/Shields/V2";
        #endregion

        #region Properties
        private List<Image> Shields { get; set; }
        private List<Image> Partitions { get; set; }
        private List<Image> Fields { get; set; }

        private Image CurrentShieldImg { get; set; }
        private Image CurrentPartitionImg { get; set; }
        private Image CurrentFieldImg { get; set; }

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
        #endregion

        #region Events Handlers
        private void shieldPictureBox_Click(object sender, EventArgs e) => NextShieldImage();
        private void partitionPictureBox_Click(object sender, EventArgs e) => NextPartitionImage();
        private void fieldPictureBox_Click(object sender, EventArgs e) => NextFieldImg();
        #endregion
    }
}
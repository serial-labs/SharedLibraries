namespace TesterDeDessin
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            toolStripStatusLabel3 = new ToolStripStatusLabel();
            toolStripStatusLabel4 = new ToolStripStatusLabel();
            toolStripStatusLabelElapsed = new ToolStripStatusLabel();
            toolStripStatusLabelValueChange = new ToolStripStatusLabel();
            menuStrip1 = new MenuStrip();
            fichierToolStripMenuItem = new ToolStripMenuItem();
            ouvrirToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItemOpenFormTest = new ToolStripMenuItem();
            toggleLogWindowsMenu = new ToolStripMenuItem();
            pictureBoxToolStripMenuItem = new ToolStripMenuItem();
            toolStripComboBox1 = new ToolStripComboBox();
            normalToolStripMenuItem = new ToolStripMenuItem();
            zoomToolStripMenuItem = new ToolStripMenuItem();
            stretchToolStripMenuItem = new ToolStripMenuItem();
            centerToolStripMenuItem = new ToolStripMenuItem();
            texturesToolStripMenuItem = new ToolStripMenuItem();
            chooseSourceFolderToolStripMenuItem = new ToolStripMenuItem();
            setSourceFolderToolStripMenuItem = new ToolStripMenuItem();
            resetToEmbeddedToolStripMenuItem = new ToolStripMenuItem();
            splitContainer1 = new SplitContainer();
            lblTextureFolder = new Label();
            trackBar1 = new TrackBar();
            lblColor2 = new Label();
            lblColor1 = new Label();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            btnCheckDrawPix = new Button();
            lblSource = new Label();
            lblDest = new Label();
            lstInterpolationModes = new ListBox();
            chkInterpolationModeHighQualityBicubic = new CheckBox();
            chkSmoothingModeHighQuality = new CheckBox();
            numericUpDown1 = new NumericUpDown();
            pictureBox4 = new PictureBox();
            btn_pbs1_vers_picResult = new Button();
            button1 = new Button();
            tabPage2 = new TabPage();
            nupTextureTiles = new NumericUpDown();
            trackBarTexture = new TrackBar();
            checkBox1 = new CheckBox();
            chkToPicResult = new CheckBox();
            pictureBox5 = new PictureBox();
            btnImaCompo = new Button();
            btnUnits = new Button();
            tabPage3 = new TabPage();
            btnBlendAutoPrev = new Button();
            chkConsiderOpacity = new CheckBox();
            btnBlendAuto = new Button();
            btnBlend = new Button();
            picRedBlend = new PictureBox();
            picBlueBlend = new PictureBox();
            picBlend = new PictureBox();
            lstArithmetics = new ListBox();
            trackBarTextureOpacity = new TrackBar();
            tabPage4 = new TabPage();
            chkSquareRatio = new CheckBox();
            trackBar2 = new TrackBar();
            btnTestHSL = new Button();
            btnColorMatrix = new Button();
            btnColorRemap = new Button();
            pictureBox3 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            picBsource = new PictureBox();
            btnSavePicResult = new Button();
            txtLog = new TextBox();
            picResult = new PictureBox();
            colorDialog1 = new ColorDialog();
            statusStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nupTextureTiles).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarTexture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picRedBlend).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picBlueBlend).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picBlend).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarTextureOpacity).BeginInit();
            tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picBsource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picResult).BeginInit();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2, toolStripStatusLabel3, toolStripStatusLabel4, toolStripStatusLabelElapsed, toolStripStatusLabelValueChange });
            statusStrip1.Location = new Point(0, 868);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1233, 25);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(135, 20);
            toolStripStatusLabel1.Text = "tester de dll Dessin";
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            toolStripStatusLabel2.ForeColor = Color.Teal;
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(151, 20);
            toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // toolStripStatusLabel3
            // 
            toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            toolStripStatusLabel3.Size = new Size(118, 20);
            toolStripStatusLabel3.Text = "toolStripStatusLabel3";
            // 
            // toolStripStatusLabel4
            // 
            toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            toolStripStatusLabel4.Size = new Size(32, 20);
            toolStripStatusLabel4.Text = "pixel";
            // 
            // toolStripStatusLabelElapsed
            // 
            toolStripStatusLabelElapsed.Name = "toolStripStatusLabelElapsed";
            toolStripStatusLabelElapsed.Size = new Size(31, 20);
            toolStripStatusLabelElapsed.Text = "time";
            // 
            // toolStripStatusLabelValueChange
            // 
            toolStripStatusLabelValueChange.Name = "toolStripStatusLabelValueChange";
            toolStripStatusLabelValueChange.Size = new Size(118, 20);
            toolStripStatusLabelValueChange.Text = "toolStripStatusLabel5";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fichierToolStripMenuItem, pictureBoxToolStripMenuItem, texturesToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1233, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // fichierToolStripMenuItem
            // 
            fichierToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ouvrirToolStripMenuItem, toolStripMenuItemOpenFormTest, toggleLogWindowsMenu });
            fichierToolStripMenuItem.Name = "fichierToolStripMenuItem";
            fichierToolStripMenuItem.Size = new Size(54, 20);
            fichierToolStripMenuItem.Text = "Fichier";
            // 
            // ouvrirToolStripMenuItem
            // 
            ouvrirToolStripMenuItem.Name = "ouvrirToolStripMenuItem";
            ouvrirToolStripMenuItem.Size = new Size(184, 22);
            ouvrirToolStripMenuItem.Text = "Ouvrir";
            ouvrirToolStripMenuItem.Click += ouvrirToolStripMenuItem_Click;
            // 
            // toolStripMenuItemOpenFormTest
            // 
            toolStripMenuItemOpenFormTest.Name = "toolStripMenuItemOpenFormTest";
            toolStripMenuItemOpenFormTest.Size = new Size(184, 22);
            toolStripMenuItemOpenFormTest.Text = "Open FormTest";
            toolStripMenuItemOpenFormTest.Click += toolStripMenuItemOpenFormTest_Click;
            // 
            // toggleLogWindowsMenu
            // 
            toggleLogWindowsMenu.Name = "toggleLogWindowsMenu";
            toggleLogWindowsMenu.Size = new Size(184, 22);
            toggleLogWindowsMenu.Text = "Toggle Log Windows";
            toggleLogWindowsMenu.Click += toggleLogWindowsMenu_Click;
            // 
            // pictureBoxToolStripMenuItem
            // 
            pictureBoxToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripComboBox1, normalToolStripMenuItem, zoomToolStripMenuItem, stretchToolStripMenuItem, centerToolStripMenuItem });
            pictureBoxToolStripMenuItem.Name = "pictureBoxToolStripMenuItem";
            pictureBoxToolStripMenuItem.Size = new Size(79, 20);
            pictureBoxToolStripMenuItem.Text = "picture box";
            // 
            // toolStripComboBox1
            // 
            toolStripComboBox1.Items.AddRange(new object[] { "Normal", "Zoom", "Stretch", "Center" });
            toolStripComboBox1.Name = "toolStripComboBox1";
            toolStripComboBox1.Size = new Size(121, 23);
            toolStripComboBox1.Text = "Normal";
            toolStripComboBox1.TextUpdate += toolStripComboBox1_TextUpdate;
            toolStripComboBox1.Click += toolStripComboBox1_Click;
            // 
            // normalToolStripMenuItem
            // 
            normalToolStripMenuItem.Checked = true;
            normalToolStripMenuItem.CheckOnClick = true;
            normalToolStripMenuItem.CheckState = CheckState.Checked;
            normalToolStripMenuItem.Name = "normalToolStripMenuItem";
            normalToolStripMenuItem.Size = new Size(181, 22);
            normalToolStripMenuItem.Text = "Normal";
            normalToolStripMenuItem.Click += SizeModeToolStripMenuItem_Click;
            // 
            // zoomToolStripMenuItem
            // 
            zoomToolStripMenuItem.CheckOnClick = true;
            zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
            zoomToolStripMenuItem.Size = new Size(181, 22);
            zoomToolStripMenuItem.Text = "Zoom";
            zoomToolStripMenuItem.Click += SizeModeToolStripMenuItem_Click;
            // 
            // stretchToolStripMenuItem
            // 
            stretchToolStripMenuItem.CheckOnClick = true;
            stretchToolStripMenuItem.Name = "stretchToolStripMenuItem";
            stretchToolStripMenuItem.Size = new Size(181, 22);
            stretchToolStripMenuItem.Text = "Stretch";
            stretchToolStripMenuItem.Click += SizeModeToolStripMenuItem_Click;
            // 
            // centerToolStripMenuItem
            // 
            centerToolStripMenuItem.CheckOnClick = true;
            centerToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            centerToolStripMenuItem.Name = "centerToolStripMenuItem";
            centerToolStripMenuItem.Size = new Size(181, 22);
            centerToolStripMenuItem.Text = "Center";
            centerToolStripMenuItem.Click += SizeModeToolStripMenuItem_Click;
            // 
            // texturesToolStripMenuItem
            // 
            texturesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { chooseSourceFolderToolStripMenuItem, setSourceFolderToolStripMenuItem, resetToEmbeddedToolStripMenuItem });
            texturesToolStripMenuItem.Name = "texturesToolStripMenuItem";
            texturesToolStripMenuItem.Size = new Size(61, 20);
            texturesToolStripMenuItem.Text = "textures";
            // 
            // chooseSourceFolderToolStripMenuItem
            // 
            chooseSourceFolderToolStripMenuItem.Name = "chooseSourceFolderToolStripMenuItem";
            chooseSourceFolderToolStripMenuItem.Size = new Size(184, 22);
            chooseSourceFolderToolStripMenuItem.Text = "choose source folder";
            chooseSourceFolderToolStripMenuItem.Click += ChooseSourceFolderToolStripMenuItem_Click;
            // 
            // setSourceFolderToolStripMenuItem
            // 
            setSourceFolderToolStripMenuItem.Name = "setSourceFolderToolStripMenuItem";
            setSourceFolderToolStripMenuItem.Size = new Size(184, 22);
            setSourceFolderToolStripMenuItem.Text = "set source folder";
            setSourceFolderToolStripMenuItem.Click += setSourceFolderToolStripMenuItem_Click;
            // 
            // resetToEmbeddedToolStripMenuItem
            // 
            resetToEmbeddedToolStripMenuItem.Name = "resetToEmbeddedToolStripMenuItem";
            resetToEmbeddedToolStripMenuItem.Size = new Size(184, 22);
            resetToEmbeddedToolStripMenuItem.Text = "reset to embedded";
            resetToEmbeddedToolStripMenuItem.Click += resetToEmbeddedToolStripMenuItem_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 24);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.AutoScrollMargin = new Size(3, 3);
            splitContainer1.Panel1.Controls.Add(lblTextureFolder);
            splitContainer1.Panel1.Controls.Add(trackBar1);
            splitContainer1.Panel1.Controls.Add(lblColor2);
            splitContainer1.Panel1.Controls.Add(lblColor1);
            splitContainer1.Panel1.Controls.Add(tabControl1);
            splitContainer1.Panel1.Controls.Add(pictureBox3);
            splitContainer1.Panel1.Controls.Add(pictureBox2);
            splitContainer1.Panel1.Controls.Add(pictureBox1);
            splitContainer1.Panel1.Controls.Add(picBsource);
            splitContainer1.Panel1.ForeColor = Color.IndianRed;
            splitContainer1.Panel1.Margin = new Padding(5);
            splitContainer1.Panel1.Padding = new Padding(5);
            splitContainer1.Panel1.Paint += splitContainer1_Panel1_Paint;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(btnSavePicResult);
            splitContainer1.Panel2.Controls.Add(txtLog);
            splitContainer1.Panel2.Controls.Add(picResult);
            splitContainer1.Panel2.Margin = new Padding(5);
            splitContainer1.Panel2.Padding = new Padding(5);
            splitContainer1.Size = new Size(1233, 844);
            splitContainer1.SplitterDistance = 620;
            splitContainer1.SplitterWidth = 8;
            splitContainer1.TabIndex = 2;
            // 
            // lblTextureFolder
            // 
            lblTextureFolder.AutoSize = true;
            lblTextureFolder.Location = new Point(261, 17);
            lblTextureFolder.Name = "lblTextureFolder";
            lblTextureFolder.Size = new Size(72, 15);
            lblTextureFolder.TabIndex = 8;
            lblTextureFolder.Text = "(embedded)";
            // 
            // trackBar1
            // 
            trackBar1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            trackBar1.Location = new Point(383, 535);
            trackBar1.Maximum = 25;
            trackBar1.Minimum = 3;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(216, 45);
            trackBar1.TabIndex = 7;
            trackBar1.Value = 20;
            trackBar1.ValueChanged += trackBar1_ValueChanged;
            // 
            // lblColor2
            // 
            lblColor2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblColor2.AutoSize = true;
            lblColor2.BackColor = SystemColors.ControlDark;
            lblColor2.BorderStyle = BorderStyle.FixedSingle;
            lblColor2.FlatStyle = FlatStyle.Flat;
            lblColor2.Location = new Point(323, 538);
            lblColor2.Name = "lblColor2";
            lblColor2.Size = new Size(40, 17);
            lblColor2.TabIndex = 6;
            lblColor2.Text = "label1";
            lblColor2.Click += lblColor2_Click;
            // 
            // lblColor1
            // 
            lblColor1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblColor1.AutoSize = true;
            lblColor1.BackColor = SystemColors.ControlDark;
            lblColor1.BorderStyle = BorderStyle.FixedSingle;
            lblColor1.Location = new Point(263, 538);
            lblColor1.Name = "lblColor1";
            lblColor1.Size = new Size(40, 17);
            lblColor1.TabIndex = 5;
            lblColor1.Text = "label1";
            lblColor1.Click += lblColor1_Click;
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Location = new Point(12, 558);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(591, 286);
            tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(btnCheckDrawPix);
            tabPage1.Controls.Add(lblSource);
            tabPage1.Controls.Add(lblDest);
            tabPage1.Controls.Add(lstInterpolationModes);
            tabPage1.Controls.Add(chkInterpolationModeHighQualityBicubic);
            tabPage1.Controls.Add(chkSmoothingModeHighQuality);
            tabPage1.Controls.Add(numericUpDown1);
            tabPage1.Controls.Add(pictureBox4);
            tabPage1.Controls.Add(btn_pbs1_vers_picResult);
            tabPage1.Controls.Add(button1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(583, 258);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "1. =>picResult";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnCheckDrawPix
            // 
            btnCheckDrawPix.BackgroundImage = ResourceImages1._8x8x8;
            btnCheckDrawPix.BackgroundImageLayout = ImageLayout.Stretch;
            btnCheckDrawPix.Location = new Point(300, 55);
            btnCheckDrawPix.Name = "btnCheckDrawPix";
            btnCheckDrawPix.Size = new Size(77, 51);
            btnCheckDrawPix.TabIndex = 10;
            btnCheckDrawPix.Text = "Check Draw Pix";
            btnCheckDrawPix.UseVisualStyleBackColor = true;
            btnCheckDrawPix.Click += btnCheckDrawPix_Click;
            // 
            // lblSource
            // 
            lblSource.Location = new Point(266, 109);
            lblSource.Name = "lblSource";
            lblSource.Size = new Size(292, 21);
            lblSource.TabIndex = 9;
            lblSource.Text = "Label Source";
            lblSource.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblDest
            // 
            lblDest.Location = new Point(266, 130);
            lblDest.Name = "lblDest";
            lblDest.Size = new Size(289, 20);
            lblDest.TabIndex = 8;
            lblDest.Text = "label dest";
            lblDest.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lstInterpolationModes
            // 
            lstInterpolationModes.FormattingEnabled = true;
            lstInterpolationModes.ItemHeight = 15;
            lstInterpolationModes.Location = new Point(6, 28);
            lstInterpolationModes.Name = "lstInterpolationModes";
            lstInterpolationModes.Size = new Size(120, 154);
            lstInterpolationModes.TabIndex = 7;
            lstInterpolationModes.SelectedIndexChanged += lstInterpolationModes_SelectedIndexChanged;
            // 
            // chkInterpolationModeHighQualityBicubic
            // 
            chkInterpolationModeHighQualityBicubic.AutoSize = true;
            chkInterpolationModeHighQualityBicubic.Checked = true;
            chkInterpolationModeHighQualityBicubic.CheckState = CheckState.Checked;
            chkInterpolationModeHighQualityBicubic.Location = new Point(300, 18);
            chkInterpolationModeHighQualityBicubic.Name = "chkInterpolationModeHighQualityBicubic";
            chkInterpolationModeHighQualityBicubic.Size = new Size(231, 19);
            chkInterpolationModeHighQualityBicubic.TabIndex = 6;
            chkInterpolationModeHighQualityBicubic.Text = "InterpolationMode.HighQualityBicubic";
            chkInterpolationModeHighQualityBicubic.UseVisualStyleBackColor = true;
            chkInterpolationModeHighQualityBicubic.Visible = false;
            // 
            // chkSmoothingModeHighQuality
            // 
            chkSmoothingModeHighQuality.AutoSize = true;
            chkSmoothingModeHighQuality.Checked = true;
            chkSmoothingModeHighQuality.CheckState = CheckState.Checked;
            chkSmoothingModeHighQuality.Location = new Point(6, 6);
            chkSmoothingModeHighQuality.Name = "chkSmoothingModeHighQuality";
            chkSmoothingModeHighQuality.Size = new Size(183, 19);
            chkSmoothingModeHighQuality.TabIndex = 5;
            chkSmoothingModeHighQuality.Text = "SmoothingMode.HighQuality";
            chkSmoothingModeHighQuality.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(386, 159);
            numericUpDown1.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(47, 23);
            numericUpDown1.TabIndex = 3;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // pictureBox4
            // 
            pictureBox4.Location = new Point(141, 55);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(119, 127);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 2;
            pictureBox4.TabStop = false;
            pictureBox4.Paint += pictureBox4_Paint;
            // 
            // btn_pbs1_vers_picResult
            // 
            btn_pbs1_vers_picResult.Location = new Point(439, 153);
            btn_pbs1_vers_picResult.Name = "btn_pbs1_vers_picResult";
            btn_pbs1_vers_picResult.Size = new Size(116, 31);
            btn_pbs1_vers_picResult.TabIndex = 1;
            btn_pbs1_vers_picResult.Text = "pbs1=>picResult";
            btn_pbs1_vers_picResult.UseVisualStyleBackColor = true;
            btn_pbs1_vers_picResult.Click += pbs1_vers_picResult_Click;
            // 
            // button1
            // 
            button1.Location = new Point(434, 43);
            button1.Name = "button1";
            button1.Size = new Size(97, 42);
            button1.TabIndex = 0;
            button1.Text = "get bmp from ima";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(nupTextureTiles);
            tabPage2.Controls.Add(trackBarTexture);
            tabPage2.Controls.Add(checkBox1);
            tabPage2.Controls.Add(chkToPicResult);
            tabPage2.Controls.Add(pictureBox5);
            tabPage2.Controls.Add(btnImaCompo);
            tabPage2.Controls.Add(btnUnits);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(583, 258);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "imageComposée1";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // nupTextureTiles
            // 
            nupTextureTiles.Location = new Point(21, 140);
            nupTextureTiles.Name = "nupTextureTiles";
            nupTextureTiles.Size = new Size(30, 23);
            nupTextureTiles.TabIndex = 11;
            // 
            // trackBarTexture
            // 
            trackBarTexture.Location = new Point(20, 172);
            trackBarTexture.Maximum = 100;
            trackBarTexture.Name = "trackBarTexture";
            trackBarTexture.Size = new Size(112, 45);
            trackBarTexture.SmallChange = 5;
            trackBarTexture.TabIndex = 10;
            trackBarTexture.TickFrequency = 10;
            trackBarTexture.TickStyle = TickStyle.Both;
            trackBarTexture.Value = 60;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(147, 18);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(44, 19);
            checkBox1.TabIndex = 4;
            checkBox1.Text = "xxx";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // chkToPicResult
            // 
            chkToPicResult.AutoSize = true;
            chkToPicResult.Location = new Point(19, 111);
            chkToPicResult.Name = "chkToPicResult";
            chkToPicResult.Size = new Size(97, 19);
            chkToPicResult.TabIndex = 3;
            chkToPicResult.Text = "add picResult";
            chkToPicResult.UseVisualStyleBackColor = true;
            // 
            // pictureBox5
            // 
            pictureBox5.BorderStyle = BorderStyle.Fixed3D;
            pictureBox5.Cursor = Cursors.Hand;
            pictureBox5.Dock = DockStyle.Right;
            pictureBox5.Location = new Point(292, 3);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(288, 252);
            pictureBox5.TabIndex = 2;
            pictureBox5.TabStop = false;
            pictureBox5.Click += pictureBox5_Click;
            // 
            // btnImaCompo
            // 
            btnImaCompo.Location = new Point(15, 64);
            btnImaCompo.Name = "btnImaCompo";
            btnImaCompo.Size = new Size(117, 36);
            btnImaCompo.TabIndex = 1;
            btnImaCompo.Text = "Image Composée";
            btnImaCompo.UseVisualStyleBackColor = true;
            btnImaCompo.Click += btnImaCompo_Click;
            // 
            // btnUnits
            // 
            btnUnits.Location = new Point(14, 18);
            btnUnits.Name = "btnUnits";
            btnUnits.Size = new Size(118, 32);
            btnUnits.TabIndex = 0;
            btnUnits.Text = "test units";
            btnUnits.UseVisualStyleBackColor = true;
            btnUnits.Click += btnUnits_Click;
            // 
            // tabPage3
            // 
            tabPage3.BackColor = Color.MistyRose;
            tabPage3.Controls.Add(btnBlendAutoPrev);
            tabPage3.Controls.Add(chkConsiderOpacity);
            tabPage3.Controls.Add(btnBlendAuto);
            tabPage3.Controls.Add(btnBlend);
            tabPage3.Controls.Add(picRedBlend);
            tabPage3.Controls.Add(picBlueBlend);
            tabPage3.Controls.Add(picBlend);
            tabPage3.Controls.Add(lstArithmetics);
            tabPage3.Controls.Add(trackBarTextureOpacity);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(583, 258);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "blending";
            // 
            // btnBlendAutoPrev
            // 
            btnBlendAutoPrev.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBlendAutoPrev.Location = new Point(170, 219);
            btnBlendAutoPrev.Name = "btnBlendAutoPrev";
            btnBlendAutoPrev.Size = new Size(48, 33);
            btnBlendAutoPrev.TabIndex = 7;
            btnBlendAutoPrev.Text = "<prev";
            btnBlendAutoPrev.UseVisualStyleBackColor = true;
            btnBlendAutoPrev.Click += btnBlendAutoPrev_Click;
            // 
            // chkConsiderOpacity
            // 
            chkConsiderOpacity.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkConsiderOpacity.AutoSize = true;
            chkConsiderOpacity.Location = new Point(172, 40);
            chkConsiderOpacity.Name = "chkConsiderOpacity";
            chkConsiderOpacity.Size = new Size(106, 19);
            chkConsiderOpacity.TabIndex = 6;
            chkConsiderOpacity.Text = "Handle opacity";
            chkConsiderOpacity.UseVisualStyleBackColor = true;
            chkConsiderOpacity.CheckedChanged += chkConsiderOpacity_CheckedChanged;
            // 
            // btnBlendAuto
            // 
            btnBlendAuto.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBlendAuto.Location = new Point(226, 221);
            btnBlendAuto.Name = "btnBlendAuto";
            btnBlendAuto.Size = new Size(52, 33);
            btnBlendAuto.TabIndex = 5;
            btnBlendAuto.Text = "next>";
            btnBlendAuto.UseVisualStyleBackColor = true;
            btnBlendAuto.Click += btnBlendAuto_Click;
            // 
            // btnBlend
            // 
            btnBlend.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBlend.Location = new Point(170, 3);
            btnBlend.Name = "btnBlend";
            btnBlend.Size = new Size(106, 33);
            btnBlend.TabIndex = 3;
            btnBlend.Text = "blend";
            btnBlend.UseVisualStyleBackColor = true;
            btnBlend.Click += btnBlend_Click;
            // 
            // picRedBlend
            // 
            picRedBlend.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            picRedBlend.BorderStyle = BorderStyle.FixedSingle;
            picRedBlend.Location = new Point(8, 177);
            picRedBlend.Name = "picRedBlend";
            picRedBlend.Size = new Size(156, 75);
            picRedBlend.TabIndex = 2;
            picRedBlend.TabStop = false;
            picRedBlend.Click += picRedBlend_Click;
            // 
            // picBlueBlend
            // 
            picBlueBlend.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            picBlueBlend.BorderStyle = BorderStyle.FixedSingle;
            picBlueBlend.Location = new Point(8, 6);
            picBlueBlend.Name = "picBlueBlend";
            picBlueBlend.Size = new Size(156, 159);
            picBlueBlend.TabIndex = 1;
            picBlueBlend.TabStop = false;
            picBlueBlend.Click += picBlueBlend_Click;
            // 
            // picBlend
            // 
            picBlend.BorderStyle = BorderStyle.FixedSingle;
            picBlend.Dock = DockStyle.Right;
            picBlend.Location = new Point(282, 3);
            picBlend.Name = "picBlend";
            picBlend.Size = new Size(298, 252);
            picBlend.SizeMode = PictureBoxSizeMode.StretchImage;
            picBlend.TabIndex = 0;
            picBlend.TabStop = false;
            picBlend.Click += picBlend_Click;
            // 
            // lstArithmetics
            // 
            lstArithmetics.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lstArithmetics.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            lstArithmetics.Location = new Point(172, 94);
            lstArithmetics.Name = "lstArithmetics";
            lstArithmetics.Size = new Size(106, 121);
            lstArithmetics.TabIndex = 4;
            lstArithmetics.SelectedIndexChanged += lstArithmetics_SelectedIndexChanged;
            // 
            // trackBarTextureOpacity
            // 
            trackBarTextureOpacity.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            trackBarTextureOpacity.LargeChange = 16;
            trackBarTextureOpacity.Location = new Point(164, 65);
            trackBarTextureOpacity.Maximum = 765;
            trackBarTextureOpacity.Name = "trackBarTextureOpacity";
            trackBarTextureOpacity.Size = new Size(108, 45);
            trackBarTextureOpacity.SmallChange = 8;
            trackBarTextureOpacity.TabIndex = 8;
            trackBarTextureOpacity.TickFrequency = 10;
            trackBarTextureOpacity.Value = 255;
            trackBarTextureOpacity.ValueChanged += trackBarTextureOpacity_ValueChanged;
            trackBarTextureOpacity.MouseUp += trackBarTextureOpacity_MouseUp;
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(chkSquareRatio);
            tabPage4.Controls.Add(trackBar2);
            tabPage4.Controls.Add(btnTestHSL);
            tabPage4.Controls.Add(btnColorMatrix);
            tabPage4.Controls.Add(btnColorRemap);
            tabPage4.Location = new Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(583, 258);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Coloring";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // chkSquareRatio
            // 
            chkSquareRatio.AutoSize = true;
            chkSquareRatio.Location = new Point(131, 79);
            chkSquareRatio.Name = "chkSquareRatio";
            chkSquareRatio.Size = new Size(91, 19);
            chkSquareRatio.TabIndex = 4;
            chkSquareRatio.Text = "square Ratio";
            chkSquareRatio.UseVisualStyleBackColor = true;
            // 
            // trackBar2
            // 
            trackBar2.Location = new Point(129, 18);
            trackBar2.Maximum = 50;
            trackBar2.Minimum = 1;
            trackBar2.Name = "trackBar2";
            trackBar2.Size = new Size(390, 45);
            trackBar2.TabIndex = 3;
            trackBar2.TickFrequency = 10;
            trackBar2.TickStyle = TickStyle.Both;
            trackBar2.Value = 10;
            trackBar2.Scroll += trackBar2_Scroll;
            // 
            // btnTestHSL
            // 
            btnTestHSL.Location = new Point(16, 104);
            btnTestHSL.Name = "btnTestHSL";
            btnTestHSL.Size = new Size(94, 28);
            btnTestHSL.TabIndex = 2;
            btnTestHSL.Text = "test HSL";
            btnTestHSL.UseVisualStyleBackColor = true;
            btnTestHSL.Click += btnTestHSL_Click;
            // 
            // btnColorMatrix
            // 
            btnColorMatrix.Location = new Point(16, 61);
            btnColorMatrix.Name = "btnColorMatrix";
            btnColorMatrix.Size = new Size(94, 28);
            btnColorMatrix.TabIndex = 1;
            btnColorMatrix.Text = "ColorMatrix";
            btnColorMatrix.UseVisualStyleBackColor = true;
            // 
            // btnColorRemap
            // 
            btnColorRemap.Location = new Point(16, 18);
            btnColorRemap.Name = "btnColorRemap";
            btnColorRemap.Size = new Size(94, 28);
            btnColorRemap.TabIndex = 0;
            btnColorRemap.Text = "ColorRemap";
            btnColorRemap.UseVisualStyleBackColor = true;
            btnColorRemap.Click += btnColorRemap_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox3.BackColor = SystemColors.GradientInactiveCaption;
            pictureBox3.BorderStyle = BorderStyle.FixedSingle;
            pictureBox3.Location = new Point(262, 323);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(341, 212);
            pictureBox3.TabIndex = 3;
            pictureBox3.TabStop = false;
            pictureBox3.Click += picBsource_Click_1;
            pictureBox3.Paint += pictureBox1_Paint;
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            pictureBox2.BackColor = SystemColors.GradientInactiveCaption;
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
            pictureBox2.Location = new Point(12, 228);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(211, 307);
            pictureBox2.TabIndex = 2;
            pictureBox2.TabStop = false;
            pictureBox2.Click += picBsource_Click_1;
            pictureBox2.Paint += pictureBox1_Paint;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.BackColor = SystemColors.GradientInactiveCaption;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(262, 35);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(341, 282);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            pictureBox1.Click += picBsource_Click_1;
            pictureBox1.Paint += pictureBox1_Paint;
            // 
            // picBsource
            // 
            picBsource.BackColor = SystemColors.GradientInactiveCaption;
            picBsource.BorderStyle = BorderStyle.FixedSingle;
            picBsource.Location = new Point(12, 14);
            picBsource.Name = "picBsource";
            picBsource.Size = new Size(211, 194);
            picBsource.TabIndex = 0;
            picBsource.TabStop = false;
            picBsource.Click += picBsource_Click_1;
            picBsource.Paint += pictureBox1_Paint;
            // 
            // btnSavePicResult
            // 
            btnSavePicResult.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSavePicResult.Location = new Point(509, 0);
            btnSavePicResult.Name = "btnSavePicResult";
            btnSavePicResult.Size = new Size(75, 22);
            btnSavePicResult.TabIndex = 3;
            btnSavePicResult.Text = "save img";
            btnSavePicResult.UseVisualStyleBackColor = true;
            btnSavePicResult.Click += btnSavePicResult_Click;
            // 
            // txtLog
            // 
            txtLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtLog.Location = new Point(8, 716);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ScrollBars = ScrollBars.Both;
            txtLog.Size = new Size(505, 120);
            txtLog.TabIndex = 1;
            txtLog.Visible = false;
            // 
            // picResult
            // 
            picResult.BackColor = SystemColors.GradientInactiveCaption;
            picResult.BorderStyle = BorderStyle.FixedSingle;
            picResult.Cursor = Cursors.Cross;
            picResult.Dock = DockStyle.Fill;
            picResult.Location = new Point(5, 5);
            picResult.Name = "picResult";
            picResult.Size = new Size(595, 834);
            picResult.SizeMode = PictureBoxSizeMode.Zoom;
            picResult.TabIndex = 0;
            picResult.TabStop = false;
            picResult.Click += picResult_Click;
            picResult.MouseMove += picResult_MouseMove;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1233, 893);
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "FormMain";
            Text = "Form1";
            Load += FormMain_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nupTextureTiles).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarTexture).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picRedBlend).EndInit();
            ((System.ComponentModel.ISupportInitialize)picBlueBlend).EndInit();
            ((System.ComponentModel.ISupportInitialize)picBlend).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarTextureOpacity).EndInit();
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)picBsource).EndInit();
            ((System.ComponentModel.ISupportInitialize)picResult).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fichierToolStripMenuItem;
        private ToolStripMenuItem ouvrirToolStripMenuItem;
        private SplitContainer splitContainer1;
        private PictureBox pictureBox3;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private PictureBox picBsource;
        private PictureBox picResult;
        private ToolStripMenuItem pictureBoxToolStripMenuItem;
        private ToolStripComboBox toolStripComboBox1;
        private ToolStripMenuItem normalToolStripMenuItem;
        private ToolStripMenuItem zoomToolStripMenuItem;
        private ToolStripMenuItem stretchToolStripMenuItem;
        private ToolStripMenuItem centerToolStripMenuItem;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private Button btn_pbs1_vers_picResult;
        private Button button1;
        private TabPage tabPage2;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private PictureBox pictureBox4;
        private Label lblColor2;
        private Label lblColor1;
        private ColorDialog colorDialog1;
        private ToolStripStatusLabel toolStripStatusLabel3;
        private ToolStripStatusLabel toolStripStatusLabel4;
        private ToolStripStatusLabel toolStripStatusLabelElapsed;
        private NumericUpDown numericUpDown1;
        private CheckBox chkSmoothingModeHighQuality;
        private CheckBox chkInterpolationModeHighQualityBicubic;
        private ListBox lstInterpolationModes;
        private Label lblSource;
        private Label lblDest;
        private TrackBar trackBar1;
        private Button btnUnits;
        private ToolStripMenuItem toolStripMenuItemOpenFormTest;
        private PictureBox pictureBox5;
        private Button btnImaCompo;
        private Button btnCheckDrawPix;
        private ToolStripMenuItem toggleLogWindowsMenu;
        private CheckBox chkToPicResult;
        private TabPage tabPage3;
        private Button button2;
        private PictureBox picRedBlend;
        private PictureBox picBlueBlend;
        private PictureBox picBlend;
        private ListBox lstArithmetics;
        private TrackBar trackBarTexture;
        private CheckBox checkBox1;
        private NumericUpDown nupTextureTiles;
        private TabPage tabPage4;
        private Button btnColorMatrix;
        private Button btnColorRemap;
        private Button btnTestHSL;
        private TextBox txtLog;
        private TrackBar trackBar2;
        private CheckBox chkSquareRatio;
        private Button btnBlend;
        private ToolStripMenuItem texturesToolStripMenuItem;
        private ToolStripMenuItem chooseSourceFolderToolStripMenuItem;
        private Label lblTextureFolder;
        private ToolStripMenuItem setSourceFolderToolStripMenuItem;
        private ToolStripMenuItem resetToEmbeddedToolStripMenuItem;
        private CheckBox chkConsiderOpacity;
        private Button btnBlendAuto;
        private Button btnBlendAutoPrev;
        private TrackBar trackBarTextureOpacity;
        private Button btnSavePicResult;
        private ToolStripStatusLabel toolStripStatusLabelValueChange;
    }
}
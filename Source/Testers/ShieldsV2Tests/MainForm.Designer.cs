namespace ShieldsV2Tests
{
    partial class MainForm
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
            shieldPictureBox = new PictureBox();
            field1PictureBox = new PictureBox();
            partitionPictureBox = new PictureBox();
            resultPictureBox = new PictureBox();
            step1picBox = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            ordinaryT1list = new ComboBox();
            ordinaryT2list = new ComboBox();
            field1T2list = new ComboBox();
            field1T1list = new ComboBox();
            label3 = new Label();
            label4 = new Label();
            renderButton = new Button();
            step2picBox = new PictureBox();
            statusStrip = new StatusStrip();
            renderLabel = new ToolStripStatusLabel();
            finalSizeLabel = new ToolStripStatusLabel();
            exceptMessageLabel = new ToolStripStatusLabel();
            widthSlider = new TrackBar();
            widthLabel = new Label();
            lookupPicBox = new PictureBox();
            displayStepsCheckBox = new CheckBox();
            smoothingCheckBox = new CheckBox();
            emfQualityList = new ComboBox();
            qualityContainer = new GroupBox();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            field2PictureBox = new PictureBox();
            groupBox3 = new GroupBox();
            label5 = new Label();
            label6 = new Label();
            field2T1list = new ComboBox();
            field2T2list = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)shieldPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)field1PictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)partitionPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)resultPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)step1picBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)step2picBox).BeginInit();
            statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)widthSlider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lookupPicBox).BeginInit();
            qualityContainer.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)field2PictureBox).BeginInit();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // shieldPictureBox
            // 
            shieldPictureBox.BackgroundImage = Properties.Resources.transparent;
            shieldPictureBox.BorderStyle = BorderStyle.FixedSingle;
            shieldPictureBox.Cursor = Cursors.Hand;
            shieldPictureBox.Location = new Point(12, 12);
            shieldPictureBox.Name = "shieldPictureBox";
            shieldPictureBox.Size = new Size(180, 210);
            shieldPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            shieldPictureBox.TabIndex = 0;
            shieldPictureBox.TabStop = false;
            shieldPictureBox.Click += shieldPictureBox_Click;
            // 
            // field1PictureBox
            // 
            field1PictureBox.BackgroundImage = Properties.Resources.transparent;
            field1PictureBox.BorderStyle = BorderStyle.FixedSingle;
            field1PictureBox.Cursor = Cursors.Hand;
            field1PictureBox.Location = new Point(12, 444);
            field1PictureBox.Name = "field1PictureBox";
            field1PictureBox.Size = new Size(180, 210);
            field1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            field1PictureBox.TabIndex = 1;
            field1PictureBox.TabStop = false;
            field1PictureBox.Click += fieldPictureBox_Click;
            // 
            // partitionPictureBox
            // 
            partitionPictureBox.BackgroundImage = Properties.Resources.transparent;
            partitionPictureBox.BorderStyle = BorderStyle.FixedSingle;
            partitionPictureBox.Cursor = Cursors.Hand;
            partitionPictureBox.Location = new Point(12, 228);
            partitionPictureBox.Name = "partitionPictureBox";
            partitionPictureBox.Size = new Size(180, 210);
            partitionPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            partitionPictureBox.TabIndex = 2;
            partitionPictureBox.TabStop = false;
            partitionPictureBox.Click += partitionPictureBox_Click;
            // 
            // resultPictureBox
            // 
            resultPictureBox.BackgroundImage = Properties.Resources.transparent;
            resultPictureBox.BorderStyle = BorderStyle.FixedSingle;
            resultPictureBox.Location = new Point(756, 12);
            resultPictureBox.Name = "resultPictureBox";
            resultPictureBox.Size = new Size(556, 642);
            resultPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            resultPictureBox.TabIndex = 3;
            resultPictureBox.TabStop = false;
            resultPictureBox.Click += resultPictureBox_Click;
            resultPictureBox.MouseMove += OnPictureBoxMouseMove;
            // 
            // colorizedFieldPicBox
            // 
            step1picBox.BackgroundImage = Properties.Resources.transparent;
            step1picBox.BorderStyle = BorderStyle.FixedSingle;
            step1picBox.Location = new Point(384, 444);
            step1picBox.Name = "colorizedFieldPicBox";
            step1picBox.Size = new Size(180, 210);
            step1picBox.SizeMode = PictureBoxSizeMode.StretchImage;
            step1picBox.TabIndex = 5;
            step1picBox.TabStop = false;
            step1picBox.MouseMove += OnPictureBoxMouseMove;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 25);
            label1.Name = "label1";
            label1.Size = new Size(19, 15);
            label1.TabIndex = 8;
            label1.Text = "T1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(9, 54);
            label2.Name = "label2";
            label2.Size = new Size(19, 15);
            label2.TabIndex = 10;
            label2.Text = "T2";
            // 
            // ordinaryT1list
            // 
            ordinaryT1list.DropDownStyle = ComboBoxStyle.DropDownList;
            ordinaryT1list.FormattingEnabled = true;
            ordinaryT1list.Location = new Point(34, 22);
            ordinaryT1list.Name = "ordinaryT1list";
            ordinaryT1list.Size = new Size(140, 23);
            ordinaryT1list.TabIndex = 15;
            // 
            // ordinaryT2list
            // 
            ordinaryT2list.DropDownStyle = ComboBoxStyle.DropDownList;
            ordinaryT2list.FormattingEnabled = true;
            ordinaryT2list.Location = new Point(34, 51);
            ordinaryT2list.Name = "ordinaryT2list";
            ordinaryT2list.Size = new Size(140, 23);
            ordinaryT2list.TabIndex = 16;
            // 
            // field1T2list
            // 
            field1T2list.DropDownStyle = ComboBoxStyle.DropDownList;
            field1T2list.FormattingEnabled = true;
            field1T2list.Location = new Point(34, 50);
            field1T2list.Name = "field1T2list";
            field1T2list.Size = new Size(140, 23);
            field1T2list.TabIndex = 20;
            // 
            // field1T1list
            // 
            field1T1list.DropDownStyle = ComboBoxStyle.DropDownList;
            field1T1list.FormattingEnabled = true;
            field1T1list.Location = new Point(34, 21);
            field1T1list.Name = "field1T1list";
            field1T1list.Size = new Size(140, 23);
            field1T1list.TabIndex = 19;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(9, 53);
            label3.Name = "label3";
            label3.Size = new Size(19, 15);
            label3.TabIndex = 18;
            label3.Text = "T2";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(9, 24);
            label4.Name = "label4";
            label4.Size = new Size(19, 15);
            label4.TabIndex = 17;
            label4.Text = "T1";
            // 
            // renderButton
            // 
            renderButton.Cursor = Cursors.Hand;
            renderButton.Location = new Point(384, 9);
            renderButton.Name = "renderButton";
            renderButton.Size = new Size(180, 49);
            renderButton.TabIndex = 21;
            renderButton.Text = "GO";
            renderButton.UseVisualStyleBackColor = true;
            renderButton.Click += renderButton_Click;
            // 
            // backgroundPicBox
            // 
            step2picBox.BackgroundImage = Properties.Resources.transparent;
            step2picBox.BorderStyle = BorderStyle.FixedSingle;
            step2picBox.Location = new Point(570, 444);
            step2picBox.Name = "backgroundPicBox";
            step2picBox.Size = new Size(180, 210);
            step2picBox.SizeMode = PictureBoxSizeMode.StretchImage;
            step2picBox.TabIndex = 22;
            step2picBox.TabStop = false;
            step2picBox.MouseMove += OnPictureBoxMouseMove;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { renderLabel, finalSizeLabel, exceptMessageLabel });
            statusStrip.Location = new Point(0, 659);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1320, 22);
            statusStrip.TabIndex = 24;
            statusStrip.Text = "statusStrip1";
            // 
            // renderLabel
            // 
            renderLabel.BackColor = Color.FromArgb(192, 255, 192);
            renderLabel.Name = "renderLabel";
            renderLabel.Size = new Size(12, 17);
            renderLabel.Text = "-";
            // 
            // finalSizeLabel
            // 
            finalSizeLabel.BackColor = SystemColors.ActiveCaption;
            finalSizeLabel.Name = "finalSizeLabel";
            finalSizeLabel.Size = new Size(12, 17);
            finalSizeLabel.Text = "-";
            // 
            // exceptMessageLabel
            // 
            exceptMessageLabel.BackColor = Color.IndianRed;
            exceptMessageLabel.Name = "exceptMessageLabel";
            exceptMessageLabel.Size = new Size(12, 17);
            exceptMessageLabel.Text = "-";
            // 
            // widthSlider
            // 
            widthSlider.Cursor = Cursors.Hand;
            widthSlider.LargeChange = 10;
            widthSlider.Location = new Point(570, 32);
            widthSlider.Maximum = 30;
            widthSlider.Minimum = 1;
            widthSlider.Name = "widthSlider";
            widthSlider.Size = new Size(180, 45);
            widthSlider.TabIndex = 25;
            widthSlider.Value = 10;
            widthSlider.ValueChanged += widthSlider_ValueChanged;
            // 
            // widthLabel
            // 
            widthLabel.AutoSize = true;
            widthLabel.Location = new Point(570, 9);
            widthLabel.Name = "widthLabel";
            widthLabel.Size = new Size(116, 15);
            widthLabel.TabIndex = 26;
            widthLabel.Text = "Image width: 1000px";
            // 
            // lookupPicBox
            // 
            lookupPicBox.BackgroundImage = Properties.Resources.transparent;
            lookupPicBox.BorderStyle = BorderStyle.FixedSingle;
            lookupPicBox.Location = new Point(384, 64);
            lookupPicBox.Name = "lookupPicBox";
            lookupPicBox.Size = new Size(366, 374);
            lookupPicBox.TabIndex = 27;
            lookupPicBox.TabStop = false;
            // 
            // displayStepsCheckBox
            // 
            displayStepsCheckBox.AutoSize = true;
            displayStepsCheckBox.Checked = true;
            displayStepsCheckBox.CheckState = CheckState.Checked;
            displayStepsCheckBox.Location = new Point(198, 12);
            displayStepsCheckBox.Name = "displayStepsCheckBox";
            displayStepsCheckBox.Size = new Size(140, 19);
            displayStepsCheckBox.TabIndex = 28;
            displayStepsCheckBox.Text = "Display Steps (slower)";
            displayStepsCheckBox.UseVisualStyleBackColor = true;
            // 
            // smoothingCheckBox
            // 
            smoothingCheckBox.AutoSize = true;
            smoothingCheckBox.Checked = true;
            smoothingCheckBox.CheckState = CheckState.Checked;
            smoothingCheckBox.Location = new Point(198, 37);
            smoothingCheckBox.Name = "smoothingCheckBox";
            smoothingCheckBox.Size = new Size(111, 19);
            smoothingCheckBox.TabIndex = 3;
            smoothingCheckBox.Text = "EMF Smoothing";
            smoothingCheckBox.UseVisualStyleBackColor = true;
            // 
            // emfQualityList
            // 
            emfQualityList.DropDownStyle = ComboBoxStyle.DropDownList;
            emfQualityList.FormattingEnabled = true;
            emfQualityList.Location = new Point(6, 22);
            emfQualityList.Name = "emfQualityList";
            emfQualityList.Size = new Size(168, 23);
            emfQualityList.TabIndex = 2;
            // 
            // qualityContainer
            // 
            qualityContainer.Controls.Add(emfQualityList);
            qualityContainer.Location = new Point(198, 61);
            qualityContainer.Name = "qualityContainer";
            qualityContainer.Size = new Size(180, 57);
            qualityContainer.TabIndex = 29;
            qualityContainer.TabStop = false;
            qualityContainer.Text = "EMF Quality";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(ordinaryT1list);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(ordinaryT2list);
            groupBox1.Location = new Point(198, 181);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(180, 83);
            groupBox1.TabIndex = 30;
            groupBox1.TabStop = false;
            groupBox1.Text = "Ordinary";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(field1T1list);
            groupBox2.Controls.Add(field1T2list);
            groupBox2.Location = new Point(198, 270);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(180, 81);
            groupBox2.TabIndex = 31;
            groupBox2.TabStop = false;
            groupBox2.Text = "Field 1";
            // 
            // field2PictureBox
            // 
            field2PictureBox.BackgroundImage = Properties.Resources.transparent;
            field2PictureBox.BorderStyle = BorderStyle.FixedSingle;
            field2PictureBox.Cursor = Cursors.Hand;
            field2PictureBox.Location = new Point(198, 444);
            field2PictureBox.Name = "field2PictureBox";
            field2PictureBox.Size = new Size(180, 210);
            field2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            field2PictureBox.TabIndex = 32;
            field2PictureBox.TabStop = false;
            field2PictureBox.Click += field2PictureBox_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label5);
            groupBox3.Controls.Add(label6);
            groupBox3.Controls.Add(field2T1list);
            groupBox3.Controls.Add(field2T2list);
            groupBox3.Location = new Point(198, 357);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(180, 81);
            groupBox3.TabIndex = 32;
            groupBox3.TabStop = false;
            groupBox3.Text = "Field 2";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(9, 53);
            label5.Name = "label5";
            label5.Size = new Size(19, 15);
            label5.TabIndex = 18;
            label5.Text = "T2";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(9, 24);
            label6.Name = "label6";
            label6.Size = new Size(19, 15);
            label6.TabIndex = 17;
            label6.Text = "T1";
            // 
            // field2T1list
            // 
            field2T1list.DropDownStyle = ComboBoxStyle.DropDownList;
            field2T1list.FormattingEnabled = true;
            field2T1list.Location = new Point(34, 21);
            field2T1list.Name = "field2T1list";
            field2T1list.Size = new Size(140, 23);
            field2T1list.TabIndex = 19;
            // 
            // field2T2list
            // 
            field2T2list.DropDownStyle = ComboBoxStyle.DropDownList;
            field2T2list.FormattingEnabled = true;
            field2T2list.Location = new Point(34, 50);
            field2T2list.Name = "field2T2list";
            field2T2list.Size = new Size(140, 23);
            field2T2list.TabIndex = 20;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1320, 681);
            Controls.Add(groupBox3);
            Controls.Add(field2PictureBox);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(qualityContainer);
            Controls.Add(smoothingCheckBox);
            Controls.Add(displayStepsCheckBox);
            Controls.Add(lookupPicBox);
            Controls.Add(renderButton);
            Controls.Add(widthLabel);
            Controls.Add(widthSlider);
            Controls.Add(statusStrip);
            Controls.Add(step2picBox);
            Controls.Add(step1picBox);
            Controls.Add(resultPictureBox);
            Controls.Add(partitionPictureBox);
            Controls.Add(field1PictureBox);
            Controls.Add(shieldPictureBox);
            Name = "MainForm";
            Text = "ShieldsV2";
            ((System.ComponentModel.ISupportInitialize)shieldPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)field1PictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)partitionPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)resultPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)step1picBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)step2picBox).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)widthSlider).EndInit();
            ((System.ComponentModel.ISupportInitialize)lookupPicBox).EndInit();
            qualityContainer.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)field2PictureBox).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox shieldPictureBox;
        private PictureBox field1PictureBox;
        private PictureBox partitionPictureBox;
        private PictureBox resultPictureBox;
        private PictureBox step1picBox;
        private Label label1;
        private Label label2;
        private ComboBox ordinaryT1list;
        private ComboBox ordinaryT2list;
        private ComboBox field1T2list;
        private ComboBox field1T1list;
        private Label label3;
        private Label label4;
        private Button renderButton;
        private PictureBox step2picBox;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel renderLabel;
        private TrackBar widthSlider;
        private Label widthLabel;
        private PictureBox lookupPicBox;
        private ToolStripStatusLabel exceptMessageLabel;
        private CheckBox displayStepsCheckBox;
        private CheckBox smoothingCheckBox;
        private ComboBox emfQualityList;
        private GroupBox qualityContainer;
        private ToolStripStatusLabel finalSizeLabel;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private PictureBox field2PictureBox;
        private GroupBox groupBox3;
        private Label label5;
        private Label label6;
        private ComboBox field2T1list;
        private ComboBox field2T2list;
    }
}
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
            fieldPictureBox = new PictureBox();
            partitionPictureBox = new PictureBox();
            resultPictureBox = new PictureBox();
            colorizerdPartitionPicBox = new PictureBox();
            colorizedFieldPicBox = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            partitionT1list = new ComboBox();
            partitionT2list = new ComboBox();
            fieldT2list = new ComboBox();
            fieldT1list = new ComboBox();
            label3 = new Label();
            label4 = new Label();
            renderButton = new Button();
            backgroundPicBox = new PictureBox();
            shieldAddedPicbox = new PictureBox();
            statusStrip = new StatusStrip();
            renderLabel = new ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)shieldPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)fieldPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)partitionPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)resultPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)colorizerdPartitionPicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)colorizedFieldPicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)backgroundPicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)shieldAddedPicbox).BeginInit();
            statusStrip.SuspendLayout();
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
            // fieldPictureBox
            // 
            fieldPictureBox.BackgroundImage = Properties.Resources.transparent;
            fieldPictureBox.BorderStyle = BorderStyle.FixedSingle;
            fieldPictureBox.Cursor = Cursors.Hand;
            fieldPictureBox.Location = new Point(12, 444);
            fieldPictureBox.Name = "fieldPictureBox";
            fieldPictureBox.Size = new Size(180, 210);
            fieldPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            fieldPictureBox.TabIndex = 1;
            fieldPictureBox.TabStop = false;
            fieldPictureBox.Click += fieldPictureBox_Click;
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
            resultPictureBox.Location = new Point(908, 12);
            resultPictureBox.Name = "resultPictureBox";
            resultPictureBox.Size = new Size(540, 630);
            resultPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            resultPictureBox.TabIndex = 3;
            resultPictureBox.TabStop = false;
            resultPictureBox.Click += resultPictureBox_Click;
            // 
            // colorizerdPartitionPicBox
            // 
            colorizerdPartitionPicBox.BackgroundImage = Properties.Resources.transparent;
            colorizerdPartitionPicBox.BorderStyle = BorderStyle.FixedSingle;
            colorizerdPartitionPicBox.Location = new Point(350, 228);
            colorizerdPartitionPicBox.Name = "colorizerdPartitionPicBox";
            colorizerdPartitionPicBox.Size = new Size(180, 210);
            colorizerdPartitionPicBox.SizeMode = PictureBoxSizeMode.StretchImage;
            colorizerdPartitionPicBox.TabIndex = 6;
            colorizerdPartitionPicBox.TabStop = false;
            // 
            // colorizedFieldPicBox
            // 
            colorizedFieldPicBox.BackgroundImage = Properties.Resources.transparent;
            colorizedFieldPicBox.BorderStyle = BorderStyle.FixedSingle;
            colorizedFieldPicBox.Location = new Point(350, 444);
            colorizedFieldPicBox.Name = "colorizedFieldPicBox";
            colorizedFieldPicBox.Size = new Size(180, 210);
            colorizedFieldPicBox.SizeMode = PictureBoxSizeMode.StretchImage;
            colorizedFieldPicBox.TabIndex = 5;
            colorizedFieldPicBox.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(198, 231);
            label1.Name = "label1";
            label1.Size = new Size(19, 15);
            label1.TabIndex = 8;
            label1.Text = "T1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(198, 260);
            label2.Name = "label2";
            label2.Size = new Size(19, 15);
            label2.TabIndex = 10;
            label2.Text = "T2";
            // 
            // partitionT1list
            // 
            partitionT1list.DropDownStyle = ComboBoxStyle.DropDownList;
            partitionT1list.FormattingEnabled = true;
            partitionT1list.Location = new Point(223, 228);
            partitionT1list.Name = "partitionT1list";
            partitionT1list.Size = new Size(121, 23);
            partitionT1list.TabIndex = 15;
            // 
            // partitionT2list
            // 
            partitionT2list.DropDownStyle = ComboBoxStyle.DropDownList;
            partitionT2list.FormattingEnabled = true;
            partitionT2list.Location = new Point(223, 257);
            partitionT2list.Name = "partitionT2list";
            partitionT2list.Size = new Size(121, 23);
            partitionT2list.TabIndex = 16;
            // 
            // fieldT2list
            // 
            fieldT2list.DropDownStyle = ComboBoxStyle.DropDownList;
            fieldT2list.FormattingEnabled = true;
            fieldT2list.Location = new Point(223, 473);
            fieldT2list.Name = "fieldT2list";
            fieldT2list.Size = new Size(121, 23);
            fieldT2list.TabIndex = 20;
            // 
            // fieldT1list
            // 
            fieldT1list.DropDownStyle = ComboBoxStyle.DropDownList;
            fieldT1list.FormattingEnabled = true;
            fieldT1list.Location = new Point(223, 444);
            fieldT1list.Name = "fieldT1list";
            fieldT1list.Size = new Size(121, 23);
            fieldT1list.TabIndex = 19;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(198, 476);
            label3.Name = "label3";
            label3.Size = new Size(19, 15);
            label3.TabIndex = 18;
            label3.Text = "T2";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(198, 447);
            label4.Name = "label4";
            label4.Size = new Size(19, 15);
            label4.TabIndex = 17;
            label4.Text = "T1";
            // 
            // renderButton
            // 
            renderButton.Cursor = Cursors.Hand;
            renderButton.Location = new Point(350, 12);
            renderButton.Name = "renderButton";
            renderButton.Size = new Size(180, 56);
            renderButton.TabIndex = 21;
            renderButton.Text = "GO";
            renderButton.UseVisualStyleBackColor = true;
            renderButton.Click += renderButton_Click;
            // 
            // backgroundPicBox
            // 
            backgroundPicBox.BackgroundImage = Properties.Resources.transparent;
            backgroundPicBox.BorderStyle = BorderStyle.FixedSingle;
            backgroundPicBox.Location = new Point(536, 333);
            backgroundPicBox.Name = "backgroundPicBox";
            backgroundPicBox.Size = new Size(180, 210);
            backgroundPicBox.SizeMode = PictureBoxSizeMode.StretchImage;
            backgroundPicBox.TabIndex = 22;
            backgroundPicBox.TabStop = false;
            // 
            // shieldAddedPicbox
            // 
            shieldAddedPicbox.BackgroundImage = Properties.Resources.transparent;
            shieldAddedPicbox.BorderStyle = BorderStyle.FixedSingle;
            shieldAddedPicbox.Location = new Point(722, 333);
            shieldAddedPicbox.Name = "shieldAddedPicbox";
            shieldAddedPicbox.Size = new Size(180, 210);
            shieldAddedPicbox.SizeMode = PictureBoxSizeMode.StretchImage;
            shieldAddedPicbox.TabIndex = 23;
            shieldAddedPicbox.TabStop = false;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { renderLabel });
            statusStrip.Location = new Point(0, 660);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1459, 22);
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
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1459, 682);
            Controls.Add(statusStrip);
            Controls.Add(shieldAddedPicbox);
            Controls.Add(backgroundPicBox);
            Controls.Add(renderButton);
            Controls.Add(fieldT2list);
            Controls.Add(fieldT1list);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(partitionT2list);
            Controls.Add(partitionT1list);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(colorizerdPartitionPicBox);
            Controls.Add(colorizedFieldPicBox);
            Controls.Add(resultPictureBox);
            Controls.Add(partitionPictureBox);
            Controls.Add(fieldPictureBox);
            Controls.Add(shieldPictureBox);
            Name = "MainForm";
            Text = "ShieldsV2";
            ((System.ComponentModel.ISupportInitialize)shieldPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)fieldPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)partitionPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)resultPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)colorizerdPartitionPicBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)colorizedFieldPicBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)backgroundPicBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)shieldAddedPicbox).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox shieldPictureBox;
        private PictureBox fieldPictureBox;
        private PictureBox partitionPictureBox;
        private PictureBox resultPictureBox;
        private PictureBox colorizerdPartitionPicBox;
        private PictureBox colorizedFieldPicBox;
        private Label label1;
        private Label label2;
        private ComboBox partitionT1list;
        private ComboBox partitionT2list;
        private ComboBox fieldT2list;
        private ComboBox fieldT1list;
        private Label label3;
        private Label label4;
        private Button renderButton;
        private PictureBox backgroundPicBox;
        private PictureBox shieldAddedPicbox;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel renderLabel;
    }
}
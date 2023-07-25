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
            ((System.ComponentModel.ISupportInitialize)shieldPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)fieldPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)partitionPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)resultPictureBox).BeginInit();
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
            resultPictureBox.Location = new Point(615, 12);
            resultPictureBox.Name = "resultPictureBox";
            resultPictureBox.Size = new Size(540, 630);
            resultPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            resultPictureBox.TabIndex = 3;
            resultPictureBox.TabStop = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1167, 669);
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
            ResumeLayout(false);
        }

        #endregion

        private PictureBox shieldPictureBox;
        private PictureBox fieldPictureBox;
        private PictureBox partitionPictureBox;
        private PictureBox resultPictureBox;
    }
}
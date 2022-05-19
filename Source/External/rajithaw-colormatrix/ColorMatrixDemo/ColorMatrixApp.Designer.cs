using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMatrixDemo
{
    partial class ColorMatrixApp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlControls = new System.Windows.Forms.Panel();
            this.btnRandom = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.trbHue = new System.Windows.Forms.TrackBar();
            this.trbSat = new System.Windows.Forms.TrackBar();
            this.trbVal = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRed = new System.Windows.Forms.Label();
            this.trbRed = new System.Windows.Forms.TrackBar();
            this.btnOpen = new System.Windows.Forms.Button();
            this.trbGreen = new System.Windows.Forms.TrackBar();
            this.trbBlue = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblGreen = new System.Windows.Forms.Label();
            this.lblBlue = new System.Windows.Forms.Label();
            this.btnNatural = new System.Windows.Forms.Button();
            this.splitter = new System.Windows.Forms.Splitter();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.pnlCanvas = new ColorMatrixDemoApp.DoubleBufferPanel();
            this.hsbMove = new System.Windows.Forms.HScrollBar();
            this.vsbMove = new System.Windows.Forms.VScrollBar();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbHue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbSat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbVal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbBlue)).BeginInit();
            this.pnlCanvas.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.btnRandom);
            this.pnlControls.Controls.Add(this.label4);
            this.pnlControls.Controls.Add(this.label5);
            this.pnlControls.Controls.Add(this.trbHue);
            this.pnlControls.Controls.Add(this.trbSat);
            this.pnlControls.Controls.Add(this.trbVal);
            this.pnlControls.Controls.Add(this.label6);
            this.pnlControls.Controls.Add(this.label7);
            this.pnlControls.Controls.Add(this.label8);
            this.pnlControls.Controls.Add(this.label9);
            this.pnlControls.Controls.Add(this.label1);
            this.pnlControls.Controls.Add(this.lblRed);
            this.pnlControls.Controls.Add(this.trbRed);
            this.pnlControls.Controls.Add(this.btnOpen);
            this.pnlControls.Controls.Add(this.trbGreen);
            this.pnlControls.Controls.Add(this.trbBlue);
            this.pnlControls.Controls.Add(this.label2);
            this.pnlControls.Controls.Add(this.label3);
            this.pnlControls.Controls.Add(this.lblGreen);
            this.pnlControls.Controls.Add(this.lblBlue);
            this.pnlControls.Controls.Add(this.btnNatural);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlControls.Location = new System.Drawing.Point(0, 0);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(246, 448);
            this.pnlControls.TabIndex = 0;
            // 
            // btnRandom
            // 
            this.btnRandom.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnRandom.Location = new System.Drawing.Point(94, 384);
            this.btnRandom.Name = "btnRandom";
            this.btnRandom.Size = new System.Drawing.Size(68, 20);
            this.btnRandom.TabIndex = 13;
            this.btnRandom.Text = "Random";
            this.btnRandom.Click += new System.EventHandler(this.btnRandom_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Hue :";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(210, 202);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 20);
            this.label5.TabIndex = 9;
            // 
            // trbHue
            // 
            this.trbHue.Location = new System.Drawing.Point(46, 198);
            this.trbHue.Maximum = 180;
            this.trbHue.Minimum = -180;
            this.trbHue.Name = "trbHue";
            this.trbHue.Size = new System.Drawing.Size(162, 45);
            this.trbHue.TabIndex = 6;
            this.trbHue.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trbHue.Scroll += new System.EventHandler(this.trbHue_Scroll);
            // 
            // trbSat
            // 
            this.trbSat.Location = new System.Drawing.Point(46, 242);
            this.trbSat.Maximum = 300;
            this.trbSat.Name = "trbSat";
            this.trbSat.Size = new System.Drawing.Size(162, 45);
            this.trbSat.TabIndex = 4;
            this.trbSat.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trbSat.Value = 100;
            this.trbSat.Scroll += new System.EventHandler(this.trbSat_Scroll);
            // 
            // trbVal
            // 
            this.trbVal.Location = new System.Drawing.Point(46, 286);
            this.trbVal.Maximum = 200;
            this.trbVal.Name = "trbVal";
            this.trbVal.Size = new System.Drawing.Size(162, 45);
            this.trbVal.TabIndex = 5;
            this.trbVal.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trbVal.Value = 100;
            this.trbVal.Scroll += new System.EventHandler(this.trbVal_Scroll);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 248);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Sat :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 292);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Value :";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(210, 246);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 20);
            this.label8.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(210, 292);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 20);
            this.label9.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Red :";
            // 
            // lblRed
            // 
            this.lblRed.Location = new System.Drawing.Point(210, 68);
            this.lblRed.Name = "lblRed";
            this.lblRed.Size = new System.Drawing.Size(32, 20);
            this.lblRed.TabIndex = 2;
            // 
            // trbRed
            // 
            this.trbRed.Location = new System.Drawing.Point(46, 64);
            this.trbRed.Maximum = 100;
            this.trbRed.Name = "trbRed";
            this.trbRed.Size = new System.Drawing.Size(162, 45);
            this.trbRed.TabIndex = 1;
            this.trbRed.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trbRed.Value = 100;
            this.trbRed.Scroll += new System.EventHandler(this.trbRed_Scroll);
            // 
            // btnOpen
            // 
            this.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOpen.Location = new System.Drawing.Point(90, 24);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(68, 20);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // trbGreen
            // 
            this.trbGreen.Location = new System.Drawing.Point(46, 108);
            this.trbGreen.Maximum = 100;
            this.trbGreen.Name = "trbGreen";
            this.trbGreen.Size = new System.Drawing.Size(162, 45);
            this.trbGreen.TabIndex = 1;
            this.trbGreen.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trbGreen.Value = 100;
            this.trbGreen.Scroll += new System.EventHandler(this.trbGreen_Scroll);
            // 
            // trbBlue
            // 
            this.trbBlue.Location = new System.Drawing.Point(46, 152);
            this.trbBlue.Maximum = 100;
            this.trbBlue.Name = "trbBlue";
            this.trbBlue.Size = new System.Drawing.Size(162, 45);
            this.trbBlue.TabIndex = 1;
            this.trbBlue.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trbBlue.Value = 100;
            this.trbBlue.Scroll += new System.EventHandler(this.trbBlue_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Green :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Blue :";
            // 
            // lblGreen
            // 
            this.lblGreen.Location = new System.Drawing.Point(210, 112);
            this.lblGreen.Name = "lblGreen";
            this.lblGreen.Size = new System.Drawing.Size(32, 20);
            this.lblGreen.TabIndex = 2;
            // 
            // lblBlue
            // 
            this.lblBlue.Location = new System.Drawing.Point(210, 158);
            this.lblBlue.Name = "lblBlue";
            this.lblBlue.Size = new System.Drawing.Size(32, 20);
            this.lblBlue.TabIndex = 2;
            // 
            // btnNatural
            // 
            this.btnNatural.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnNatural.Location = new System.Drawing.Point(94, 358);
            this.btnNatural.Name = "btnNatural";
            this.btnNatural.Size = new System.Drawing.Size(68, 20);
            this.btnNatural.TabIndex = 0;
            this.btnNatural.Text = "Natural";
            this.btnNatural.Click += new System.EventHandler(this.btnNatural_Click);
            // 
            // splitter
            // 
            this.splitter.Location = new System.Drawing.Point(246, 0);
            this.splitter.Name = "splitter";
            this.splitter.Size = new System.Drawing.Size(6, 448);
            this.splitter.TabIndex = 1;
            this.splitter.TabStop = false;
            // 
            // pnlCanvas
            // 
            this.pnlCanvas.Controls.Add(this.hsbMove);
            this.pnlCanvas.Controls.Add(this.vsbMove);
            this.pnlCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCanvas.Location = new System.Drawing.Point(252, 0);
            this.pnlCanvas.Name = "pnlCanvas";
            this.pnlCanvas.Size = new System.Drawing.Size(476, 448);
            this.pnlCanvas.TabIndex = 0;
            this.pnlCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCanvas_Paint);
            this.pnlCanvas.Resize += new System.EventHandler(this.pnlCanvas_Resize);
            // 
            // hsbMove
            // 
            this.hsbMove.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hsbMove.Location = new System.Drawing.Point(0, 432);
            this.hsbMove.Name = "hsbMove";
            this.hsbMove.Size = new System.Drawing.Size(460, 16);
            this.hsbMove.TabIndex = 0;
            this.hsbMove.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hsbMove_Scroll);
            // 
            // vsbMove
            // 
            this.vsbMove.Dock = System.Windows.Forms.DockStyle.Right;
            this.vsbMove.Location = new System.Drawing.Point(460, 0);
            this.vsbMove.Name = "vsbMove";
            this.vsbMove.Size = new System.Drawing.Size(16, 448);
            this.vsbMove.TabIndex = 0;
            this.vsbMove.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vsbMove_Scroll);
            // 
            // ColorMatrixApp
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(728, 448);
            this.Controls.Add(this.pnlCanvas);
            this.Controls.Add(this.splitter);
            this.Controls.Add(this.pnlControls);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ColorMatrixApp";
            this.Text = "Test ColorMatrix";
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbHue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbSat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbVal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbBlue)).EndInit();
            this.pnlCanvas.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlControls;
        private ColorMatrixDemoApp.DoubleBufferPanel pnlCanvas;
        private System.Windows.Forms.HScrollBar hsbMove;
        private System.Windows.Forms.VScrollBar vsbMove;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TrackBar trbRed;
        private System.Windows.Forms.TrackBar trbGreen;
        private System.Windows.Forms.TrackBar trbBlue;
        private System.Windows.Forms.Label lblRed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblGreen;
        private System.Windows.Forms.Label lblBlue;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar trbHue;
        private System.Windows.Forms.TrackBar trbSat;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar trbVal;
        private System.Windows.Forms.Button btnNatural;
        private System.Windows.Forms.Splitter splitter;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnRandom;
    }
}

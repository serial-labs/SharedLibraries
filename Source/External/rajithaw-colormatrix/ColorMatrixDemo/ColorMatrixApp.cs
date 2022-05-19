using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Imaging;

namespace ColorMatrixDemo
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public partial class ColorMatrixApp : System.Windows.Forms.Form
    {
        #region Member Variables

        private Bitmap mainImage = null;
		private int destX = 0;
		private int destY = 0;
		private int srcX = 0;
		private int srcY = 0;

		private float red = 1.0f;
		private float green = 1.0f;
		private float blue = 1.0f;

		private float hue = 0.0f;
		private float sat = 1.0f;
		private float val = 1.0f;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
		public ColorMatrixApp()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint,	true);
			this.UpdateStyles();

			LoadImage("lilies.jpg");
		}

        #endregion

        #region Methods

        /// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.EnableVisualStyles();
			Application.Run(new ColorMatrixApp());
		}

        /// <summary>
        /// Loads a new image to the canvas
        /// </summary>
        /// <param name="ImagePath">Path of the image</param>
		private void LoadImage(string ImagePath)
		{
			mainImage  = new Bitmap(ImagePath);
		}

        /// <summary>
        /// Show the scrollbars if the image exceeds the canvas
        /// </summary>
		private void SetScrollbars()
		{
            // Set the horizontal scrollbar
			if(mainImage.Width > pnlCanvas.Width)
			{
				hsbMove.Visible = true;
				hsbMove.Minimum = 0;
				hsbMove.Maximum = mainImage.Width - pnlCanvas.Width;
				destX = 0;
			}
			else
			{
				hsbMove.Visible = false;
				destX = (pnlCanvas.Width - mainImage.Width) / 2;
			}

            // Set the vertical scrollbar
			if(mainImage.Height > pnlCanvas.Height)
			{
				vsbMove.Visible = true;
				vsbMove.Minimum = 0;
				vsbMove.Maximum = mainImage.Height - pnlCanvas.Height;
				destY = 0;
			}
			else
			{
				vsbMove.Visible = false;
				destY = (pnlCanvas.Height - mainImage.Height) / 2;
			}
		}

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the paint event of the canvas
        /// </summary>
		private void pnlCanvas_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
            // Create the color matrix and set the appropriate values
			ColorMatrix clrMtx = new ColorMatrix();
			clrMtx.SetSaturation(sat);
			clrMtx.RotateHue(hue);
			clrMtx.ScaleValue(val);
			
			clrMtx.ScaleColors(red, green, blue);


			//clrMtx.ChangeColors(red, green, blue);
			// Set the color matrix to the image attributes
			ImageAttributes ima = new ImageAttributes();
			ima.SetColorMatrix(clrMtx);

			SetScrollbars();
            // Draw the image on the canvas with the provided image attributes
			e.Graphics.DrawImage(mainImage, new Rectangle(destX, destY, pnlCanvas.Width, pnlCanvas.Height), srcX, srcY, pnlCanvas.Width, pnlCanvas.Height, GraphicsUnit.Pixel, ima);
		}

        /// <summary>
        /// Handles the click event of the Open button
        /// </summary>
		private void btnOpen_Click(object sender, System.EventArgs e)
		{
			if(openFileDialog.ShowDialog() == DialogResult.OK)
			{
				LoadImage(openFileDialog.FileName);
				pnlCanvas.Invalidate(false);
			}
		}

        /// <summary>
        /// Handles the scroll event of the horizontal scroll bar
        /// </summary>
		private void hsbMove_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
		{
			srcX = e.NewValue;
			pnlCanvas.Invalidate(false);
		}

        /// <summary>
        /// Handles the scroll event of the vertical scroll bar
        /// </summary>
		private void vsbMove_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
		{
			srcY = e.NewValue;
			pnlCanvas.Invalidate(false);
		}

        /// <summary>
        /// Handles the resize event of the canvas
        /// </summary>
		private void pnlCanvas_Resize(object sender, System.EventArgs e)
		{
			SetScrollbars();
			pnlCanvas.Invalidate(false);
		}

        /// <summary>
        /// Handles the scroll event of the Red slider
        /// </summary>
		private void trbRed_Scroll(object sender, System.EventArgs e)
		{
			red = (float)trbRed.Value / 100;
			pnlCanvas.Invalidate(false);
		}

        /// <summary>
        /// Handles the scroll event of the Green slider
        /// </summary>
		private void trbGreen_Scroll(object sender, System.EventArgs e)
		{
			green = (float)trbGreen.Value / 100;
			pnlCanvas.Invalidate(false);
		}

        /// <summary>
        /// Handles the scroll event of the Blue slider
        /// </summary>
		private void trbBlue_Scroll(object sender, System.EventArgs e)
		{
			blue = (float)trbBlue.Value / 100;
			pnlCanvas.Invalidate(false);
		}

        /// <summary>
        /// Handles the scroll event of the Hue slider
        /// </summary>
		private void trbHue_Scroll(object sender, System.EventArgs e)
		{
			hue = trbHue.Value;
			pnlCanvas.Invalidate(false);
		}

        /// <summary>
        /// Handles the scroll event of the Saturation slider
        /// </summary>
    	private void trbSat_Scroll(object sender, System.EventArgs e)
		{
			sat = (float)trbSat.Value / 100;
			pnlCanvas.Invalidate(false);
		}

        /// <summary>
        /// Handles the scroll event of the Value slider
        /// </summary>
		private void trbVal_Scroll(object sender, System.EventArgs e)
		{
			val = (float)trbVal.Value / 100;
			pnlCanvas.Invalidate(false);
		}

        /// <summary>
        /// Handles the click event of the Natural button
        /// </summary>
		private void btnNatural_Click(object sender, System.EventArgs e)
		{
            // Set the natural values of the image
			trbRed.Value = 100;
			trbGreen.Value = 100;
			trbBlue.Value = 100;
			trbHue.Value = 0;
			trbSat.Value = 100;
			trbVal.Value = 100;

			red = 1.0f;
			green = 1.0f;
			blue = 1.0f;
			hue = 0.0f;
			sat = 1.0f;
			val = 1.0f;

			pnlCanvas.Invalidate(false);
        }


        #endregion

        private void btnRandom_Click(object sender, EventArgs e)
        {
			// Create the color matrix and set the appropriate values
			ColorMatrix clrMtx = new ColorMatrix();
			clrMtx.SetSaturation(sat);
			clrMtx.RotateHue(hue);
			clrMtx.ScaleValue(val);
			clrMtx.ScaleColors(red, green, blue);

			// Set the color matrix to the image attributes
			ImageAttributes ima = new ImageAttributes();
			ima.SetColorMatrix(clrMtx);

			SetScrollbars();
			// Draw the image on the canvas with the provided image attributes
			//pnlCanvas.BackgroundImage.Graphics.DrawImage(mainImage, new Rectangle(destX, destY, pnlCanvas.Width, pnlCanvas.Height), srcX, srcY, pnlCanvas.Width, pnlCanvas.Height, GraphicsUnit.Pixel, ima);

		}
	}
}

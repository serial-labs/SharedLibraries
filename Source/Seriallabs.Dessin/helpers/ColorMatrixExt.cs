/*  ColorMatrixExt.cs -- ColorMatrixExt class.

from https://github.com/rajithaw/color-matrix


ColorMatrixExt
A ColorMatrixExt class with additional features which can be used instead of the .NET framework System.Drawing.Imaging.ColorMatrixExt class. 
Can be utilized for easily filtering RGB color components and Hue, Saturation, Brightness values of an image. 
Based on the QColorMatrix and the Matrix (in this project).

    Copyright (c) 2012 by Rajitha Wimalasooriya
*/

using System;
using System.Drawing;
using DotNet = System.Drawing.Imaging;

namespace Seriallabs.Dessin
{
    /// <summary>
    /// Represents a color matrix class that can be used for image manupilation
    /// </summary>
    public class ColorMatrixExt : Matrix
    {
        #region Constants

        static float pi = 4.0f * (float)Math.Atan(1.0);
        static float rad = pi / 180.0f;

        const float lumR = 0.3086f;
        const float lumG = 0.6094f;
        const float lumB = 0.0820f;

        #endregion

        #region Variables

        private static bool initialized = false;
        private static ColorMatrixExt preHue = new ColorMatrixExt();
        private static ColorMatrixExt postHue = new ColorMatrixExt();

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs an 5x5 identity color matrix
        /// </summary>
        public ColorMatrixExt() : base(5, 5)
        {
            Reset();
        }

        /// <summary>
        /// Constructs an 5x5 color matrix from a .NET color matrix
        /// </summary>
        /// <param name="clrmtx">The .NET color matrix instance</param>
        public ColorMatrixExt(DotNet.ColorMatrix clrmtx) : base(5, 5)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    this[i, j] = clrmtx[i, j];
                }
            }
        }

        /// <summary>
        /// Constructs an 5x5 color matrix from a base matrix
        /// </summary>
        /// <param name="mtx">The base martix instance</param>
        private ColorMatrixExt(Matrix mtx) : base(5, 5)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    this[i, j] = mtx[i, j];
                }
            }
        }

        /// <summary>
        /// Constructs an 5x5 color matrix from a diagonal vector 
        /// </summary>
        /// <param name="diagonal">The diagonal</param>
        private ColorMatrixExt(float[] diagonal) : base(5, 5)
        {
            Clear();
            int min = Math.Min(5, diagonal.Length);
            for (int i = 0; i < min; i++)
            {
                this[i, i] = diagonal[i];
            }

            for (int i = min; i < 5; i++)
                this[i, i] = 1;
        }

        /// <summary>
        /// Constructs an 5x5 color matrix from a diagonal vector 
        /// </summary>
        /// <param name="diagonal">The diagonal</param>
        private ColorMatrixExt(Color colour) : base(5, 5)
        {
            Clear();
            float[] diagonal = new[] {colour.R / 255f, colour.G / 255f, colour.B / 255f, colour.A / 255f,1};
            for (int i = 0; i < 5; i++)
                this[i, i] = diagonal[i];
        }


        #endregion

        #region Enums

        /// <summary>
        /// Represents the order used in martix multiplication
        /// </summary>
        public enum MatrixOrder
        {
            MatrixOrderAppend,
            MatrixOrderPrepend
        }

        #endregion

        #region Methods

        /// <summary>
        /// Scale the RGB and opacity to the provided values
        /// </summary>
        /// <param name="scaleRed">Red scale value</param>
        /// <param name="scaleGreen">Green scale value</param>
        /// <param name="scaleBlue">Blue scale value</param>
        /// <param name="scaleOpacity">Opacity scale value</param>
        /// <returns>True if successfull, otherwise false</returns>
        public bool Scale(float scaleRed, float scaleGreen, float scaleBlue, float scaleOpacity)
        {
            try
            {
                ColorMatrixExt dcmtx = new ColorMatrixExt();

                dcmtx[0, 0] = scaleRed;
                dcmtx[1, 1] = scaleGreen;
                dcmtx[2, 2] = scaleBlue;
                dcmtx[3, 3] = scaleOpacity;

                this.SetColorMatrix(new ColorMatrixExt(this * dcmtx));
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Scale just the RGB values keeping the opacity constant
        /// </summary>
        /// <param name="scaleRed">Red scale value</param>
        /// <param name="scaleGreen">Green scale value</param>
        /// <param name="scaleBlue">Blue scale value</param>
        /// <returns>True if successfull, otherwise false</returns>
        public bool ScaleColors(float scaleRed, float scaleGreen, float scaleBlue)
        {
            return Scale(scaleRed, scaleGreen, scaleBlue, 1.0f);
        }

        /// <summary>
        /// Scale just the opacity keeping the colors constant
        /// </summary>
        /// <param name="scale">Opacity scale value</param>
        /// <returns>True if successfull, otherwise false</returns>
        public bool ScaleOpacity(float scale)
        {
            return Scale(1.0f, 1.0f, 1.0f, scale);
        }

        /// <summary>
        /// Scale all RGB values simultaniusly
        /// </summary>
        /// <param name="scale">Scale value</param>
        /// <returns>True if successfull, otherwise false</returns>
        public bool ScaleValue(float scale)
        {
            return Scale(scale, scale, scale, 1.0f);
        }

        /// <summary>
        /// Rotate the hue
        /// </summary>
        /// <param name="phi">Hue value in degres 360</param>
        /// <returns>True</returns>
        public bool RotateHue(float phi)
        {
            InitHue();

            // Rotate the grey vector to the blue axis.
            SetColorMatrix(new ColorMatrixExt(preHue * this));

            // Rotate around the blue axis
            RotateBlue(phi, MatrixOrder.MatrixOrderAppend);

            SetColorMatrix(new ColorMatrixExt(postHue * this));

            return true;
        }

        /// <summary>
        /// Set the saturation to the provided value
        /// </summary>
        /// <param name="saturation">Saturation value</param>
        /// <returns>True if saruration was set successfully, otherwise false</returns>
        public bool SetSaturation(float saturation)
        {
            try
            {
                float satCompl = 1.0f - saturation;
                float satComplR = lumR * satCompl;
                float satComplG = lumG * satCompl;
                float satComplB = lumB * satCompl;

                ColorMatrixExt dcmtx = new ColorMatrixExt();

                dcmtx[0, 0] = satComplR + saturation;
                dcmtx[0, 1] = satComplR;
                dcmtx[0, 2] = satComplR;

                dcmtx[1, 0] = satComplG;
                dcmtx[1, 1] = satComplG + saturation;
                dcmtx[1, 2] = satComplG;

                dcmtx[2, 0] = satComplB;
                dcmtx[2, 1] = satComplB;
                dcmtx[2, 2] = satComplB + saturation;

                SetColorMatrix(new ColorMatrixExt(this * dcmtx));
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Initialize the hue values
        /// </summary>
        private void InitHue()
        {
            const float greenRotation = 35.0f;

            if (!initialized)
            {
                initialized = true;

                preHue.RotateRed(45.0f, MatrixOrder.MatrixOrderPrepend);

                preHue.RotateGreen(-greenRotation, MatrixOrder.MatrixOrderAppend);

                float[] lum = { lumR, lumG, lumB, 1.0f };

                preHue.TransformVector(ref lum);

                float red = lum[0] / lum[2];
                float green = lum[1] / lum[2];

                preHue.ShearBlue(red, green, MatrixOrder.MatrixOrderAppend);

                postHue.ShearBlue(-red, -green, MatrixOrder.MatrixOrderPrepend);
                postHue.RotateGreen(greenRotation, MatrixOrder.MatrixOrderAppend);
                postHue.RotateRed(-45.0f, MatrixOrder.MatrixOrderAppend);
            }
        }

        /// <summary>
        /// Rotate colors corresponding to the provided parameters
        /// </summary>
        /// <param name="phi">The hue value in degre 360</param>
        /// <param name="x">Matrix element position</param>
        /// <param name="y">Matrix element position</param>
        /// <param name="order">Matrix multiplication order</param>
        /// <returns>True if successfull, otherwise false</returns>
        protected bool RotateColor(float phi, int x, int y, MatrixOrder order)
        {
            try
            {
                phi *= rad;
                ColorMatrixExt dmtx = new ColorMatrixExt();

                dmtx[x, x] = dmtx[y, y] = (float)Math.Cos(phi);

                float s = (float)Math.Sin(phi);
                dmtx[y, x] = s;
                dmtx[x, y] = -s;

                if (order == MatrixOrder.MatrixOrderPrepend)
                {
                    SetColorMatrix(new ColorMatrixExt(this * dmtx));
                }
                else
                {
                    SetColorMatrix(new ColorMatrixExt(dmtx * this));
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Rotate red according to the hue
        /// </summary>
        /// <param name="phi">The hue value</param>
        /// <param name="order">Matrix multiplication order</param>
        /// <returns>True if successfull, otherwise false</returns>
        public bool RotateRed(float phi, MatrixOrder order)
        {
            return RotateColor(phi, 2, 1, order);
        }

        /// <summary>
        /// Rotate green according to the hue
        /// </summary>
        /// <param name="phi">The hue value</param>
        /// <param name="order">Matrix multiplication order</param>
        /// <returns>True if successfull, otherwise false</returns>
        public bool RotateGreen(float phi, MatrixOrder order)
        {
            return RotateColor(phi, 0, 2, order);
        }

        /// <summary>
        /// Rotate blue according to the hue
        /// </summary>
        /// <param name="phi">The hue value</param>
        /// <param name="order">Matrix multiplication order</param>
        /// <returns>True if successfull, otherwise false</returns>
        public bool RotateBlue(float phi, MatrixOrder order)
        {
            return RotateColor(phi, 1, 0, order);
        }

        protected bool ShearColor(int x, int y1, float d1, int y2, float d2, MatrixOrder order)
        {
            try
            {
                ColorMatrixExt dmtx = new ColorMatrixExt();

                dmtx[y1, x] = d1;
                dmtx[y2, x] = d2;

                if (order == MatrixOrder.MatrixOrderPrepend)
                {
                    SetColorMatrix(new ColorMatrixExt(this * dmtx));
                }
                else
                {
                    SetColorMatrix(new ColorMatrixExt(dmtx * this));
                }

            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool ShearRed(float green, float blue, MatrixOrder order)
        {
            return ShearColor(0, 1, green, 2, blue, order);
        }

        public bool ShearGreen(float red, float blue, MatrixOrder order)
        {
            return ShearColor(1, 0, red, 2, blue, order);
        }

        public bool ShearBlue(float red, float green, MatrixOrder order)
        {
            return ShearColor(2, 0, red, 1, green, order);
        }

        /// <summary>
        /// Transforms the provided vector
        /// </summary>
        /// <param name="vector">The vector to be transformed</param>
        /// <returns>True if successfull, otherwise false</returns>
        public bool TransformVector(ref float[] vector)
        {
            try
            {
                float[] tempVector = new float[4];

                for (int x = 0; x < 4; x++)
                {
                    tempVector[x] = this[4, x];

                    for (int y = 0; y < 4; y++)
                    {
                        tempVector[x] += vector[y] * this[y, x];
                    }
                }

                for (int z = 0; z < 4; z++)
                {
                    vector[z] = tempVector[z];
                }

            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Sets a new brightnrss value
        /// </summary>
        /// <param name="brightness">Brightnrss value</param>
        /// <returns>True</returns>
        public bool SetBrightness(float brightness)
        {
            this[4, 0] = brightness;
            this[4, 1] = brightness;
            this[4, 2] = brightness;

            return true;
        }

        /// <summary>
        /// Sets a new contrast value
        /// </summary>
        /// <param name="contrast">Contrast value</param>
        /// <returns>True if successfull, otherwise false.</returns>
        public bool SetContrast(float contrast)
        {
            bool retVal = SetSaturation(1.0000001f);

            if (retVal)
                retVal = ScaleValue(contrast);

            return retVal;
        }

        /// <summary>
        /// Sets the current color matrix using a provided color matrix
        /// </summary>
        /// <param name="dmtx">The provided color matrix</param>
        private void SetColorMatrix(ColorMatrixExt clrmtx)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    this[i, j] = clrmtx[i, j];
                }
            }
        }

        #endregion

        #region Operators

        /// <summary>
        /// Implicit cast operator from an Rtw.ColorMatrixExt to the .NET color matrix
        /// </summary>
        /// <param name="clrmtx">An Rtw.ColorMatrixExt instance</param>
        /// <returns>A .NET color matrix instance</returns>
        public static implicit operator DotNet.ColorMatrix(ColorMatrixExt clrmtx)
        {
            DotNet.ColorMatrix dcmtx = new DotNet.ColorMatrix();

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    dcmtx[i, j] = clrmtx[i, j];
                }
            }

            return dcmtx;
        }

        /// <summary>
        /// Implicit cast operator from a .NET color matrix to the Rtw.ColorMatrixExt
        /// </summary>
        /// <param name="dcmtx">The .NET color matrix instance</param>
        /// <returns>An Rtw.ColorMatrixExt instance</returns>
        public static implicit operator ColorMatrixExt(DotNet.ColorMatrix dcmtx)
        {
            ColorMatrixExt clrmtx = new ColorMatrixExt(dcmtx);

            return clrmtx;
        }

        #endregion
    }
}
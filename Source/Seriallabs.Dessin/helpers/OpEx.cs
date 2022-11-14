using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

/*
 * Part of the Following Code was developed by Dewald Esterhuizen
 * View Documentation at: http://softwarebydefault.com
 * Licensed under Ms-PL 
*/

namespace Seriallabs.Dessin.helpers
{
    internal static class OpEx
    {
        public enum BlurType
        {
            Mean3x3,
            Mean5x5,
            Mean7x7,
            Mean9x9,
            GaussianBlur3x3,
            GaussianBlur5x5,
            MotionBlur5x5,
            MotionBlur5x5At45Degrees,
            MotionBlur5x5At135Degrees,
            MotionBlur7x7,
            MotionBlur7x7At45Degrees,
            MotionBlur7x7At135Degrees,
            MotionBlur9x9,
            MotionBlur9x9At45Degrees,
            MotionBlur9x9At135Degrees,
            Median3x3,
            Median5x5,
            Median7x7,
            Median9x9,
            Median11x11
        }

        public enum ColorCalculationType
        {
            Average,
            Add,
            SubtractLeft,
            SubtractRight,
            Difference,
            Multiply,
            Min,
            Max,
            Amplitude
        }

        public static Bitmap ImageBlurFilter(this Bitmap sourceBitmap,
            BlurType blurType)
        {
            Bitmap resultBitmap = null;

            switch (blurType)
            {
                case BlurType.Mean3x3:
                    resultBitmap = sourceBitmap.ConvolutionFilter(Matrix.Mean3x3, 1.0 / 9.0, 0);
                    break;

                case BlurType.Mean5x5:
                    resultBitmap = sourceBitmap.ConvolutionFilter(Matrix.Mean5x5, 1.0 / 25.0, 0);
                    break;

                case BlurType.Mean7x7:
                    resultBitmap = sourceBitmap.ConvolutionFilter(Matrix.Mean7x7, 1.0 / 49.0, 0);
                    break;

                case BlurType.Mean9x9:
                    resultBitmap = sourceBitmap.ConvolutionFilter(Matrix.Mean9x9, 1.0 / 81.0, 0);
                    break;

                case BlurType.GaussianBlur3x3:
                    resultBitmap = sourceBitmap.ConvolutionFilter(Matrix.GaussianBlur3x3, 1.0 / 16.0, 0);
                    break;

                case BlurType.GaussianBlur5x5:
                    resultBitmap = sourceBitmap.ConvolutionFilter(Matrix.GaussianBlur5x5, 1.0 / 159.0, 0);
                    break;

                case BlurType.MotionBlur5x5:
                    resultBitmap = sourceBitmap.ConvolutionFilter(Matrix.MotionBlur5x5, 1.0 / 10.0, 0);
                    break;

                case BlurType.MotionBlur5x5At45Degrees:
                    resultBitmap = sourceBitmap.ConvolutionFilter(Matrix.MotionBlur5x5At45Degrees, 1.0 / 5.0, 0);
                    break;

                case BlurType.MotionBlur5x5At135Degrees:
                    resultBitmap = sourceBitmap.ConvolutionFilter(Matrix.MotionBlur5x5At135Degrees, 1.0 / 5.0, 0);
                    break;

                case BlurType.MotionBlur7x7:
                    resultBitmap = sourceBitmap.ConvolutionFilter(Matrix.MotionBlur7x7, 1.0 / 14.0, 0);
                    break;

                case BlurType.MotionBlur7x7At45Degrees:
                    resultBitmap = sourceBitmap.ConvolutionFilter(Matrix.MotionBlur7x7At45Degrees, 1.0 / 7.0, 0);
                    break;

                case BlurType.MotionBlur7x7At135Degrees:
                    resultBitmap = sourceBitmap.ConvolutionFilter(Matrix.MotionBlur7x7At135Degrees, 1.0 / 7.0, 0);
                    break;

                case BlurType.MotionBlur9x9:
                    resultBitmap = sourceBitmap.ConvolutionFilter(Matrix.MotionBlur9x9, 1.0 / 18.0, 0);
                    break;

                case BlurType.MotionBlur9x9At45Degrees:
                    resultBitmap = sourceBitmap.ConvolutionFilter(Matrix.MotionBlur9x9At45Degrees, 1.0 / 9.0, 0);
                    break;

                case BlurType.MotionBlur9x9At135Degrees:
                    resultBitmap = sourceBitmap.ConvolutionFilter(Matrix.MotionBlur9x9At135Degrees, 1.0 / 9.0, 0);
                    break;

                case BlurType.Median3x3:
                    resultBitmap = sourceBitmap.MedianFilter(3);
                    break;

                case BlurType.Median5x5:
                    resultBitmap = sourceBitmap.MedianFilter(5);
                    break;

                case BlurType.Median7x7:
                    resultBitmap = sourceBitmap.MedianFilter(7);
                    break;

                case BlurType.Median9x9:
                    resultBitmap = sourceBitmap.MedianFilter(9);
                    break;

                case BlurType.Median11x11:
                    resultBitmap = sourceBitmap.MedianFilter(11);
                    break;
            }

            return resultBitmap;
        }


        /// <summary>
        /// CONVOLUTION FILTER
        /// </summary>
        /// <param name="sourceBitmap"></param>
        /// <param name="filterMatrix"></param>
        /// <param name="factor"></param>
        /// <param name="bias"></param>
        /// <returns></returns>
        /// <remarks>use rtw matrixes ? </remarks>
        private static Bitmap ConvolutionFilter(this Bitmap sourceBitmap,
            double[,] filterMatrix,
            double factor = 1,
            int bias = 0)
        {
            var sourceData = sourceBitmap.LockBits(new Rectangle(0, 0,
                    sourceBitmap.Width, sourceBitmap.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            var pixelBuffer = new byte[sourceData.Stride * sourceData.Height];
            var resultBuffer = new byte[sourceData.Stride * sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            sourceBitmap.UnlockBits(sourceData);

            var blue = 0.0;
            var green = 0.0;
            var red = 0.0;

            var filterWidth = filterMatrix.GetLength(1);
            var filterHeight = filterMatrix.GetLength(0);

            var filterOffset = (filterWidth - 1) / 2;
            var calcOffset = 0;

            var byteOffset = 0;

            for (var offsetY = filterOffset;
                 offsetY <
                 sourceBitmap.Height - filterOffset;
                 offsetY++)
            for (var offsetX = filterOffset;
                 offsetX <
                 sourceBitmap.Width - filterOffset;
                 offsetX++)
            {
                blue = 0;
                green = 0;
                red = 0;

                byteOffset = offsetY *
                             sourceData.Stride +
                             offsetX * 4;

                for (var filterY = -filterOffset;
                     filterY <= filterOffset;
                     filterY++)
                for (var filterX = -filterOffset;
                     filterX <= filterOffset;
                     filterX++)
                {
                    calcOffset = byteOffset +
                                 filterX * 4 +
                                 filterY * sourceData.Stride;

                    blue += (double) pixelBuffer[calcOffset] *
                            filterMatrix[filterY + filterOffset,
                                filterX + filterOffset];

                    green += (double) pixelBuffer[calcOffset + 1] *
                             filterMatrix[filterY + filterOffset,
                                 filterX + filterOffset];

                    red += (double) pixelBuffer[calcOffset + 2] *
                           filterMatrix[filterY + filterOffset,
                               filterX + filterOffset];
                }

                blue = factor * blue + bias;
                green = factor * green + bias;
                red = factor * red + bias;

                blue = blue > 255 ? 255 :
                    blue < 0 ? 0 :
                    blue;

                green = green > 255 ? 255 :
                    green < 0 ? 0 :
                    green;

                red = red > 255 ? 255 :
                    red < 0 ? 0 :
                    red;

                resultBuffer[byteOffset] = (byte) blue;
                resultBuffer[byteOffset + 1] = (byte) green;
                resultBuffer[byteOffset + 2] = (byte) red;
                resultBuffer[byteOffset + 3] = 255;
            }

            var resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

            var resultData = resultBitmap.LockBits(new Rectangle(0, 0,
                    resultBitmap.Width, resultBitmap.Height),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb);

            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }


        /// <summary>
        /// MEDIAN FILTER
        /// </summary>
        /// <param name="sourceBitmap"></param>
        /// <param name="matrixSize"></param>
        /// <returns></returns>
        public static Bitmap MedianFilter(this Bitmap sourceBitmap,
            int matrixSize)
        {
            var sourceData =
                sourceBitmap.LockBits(new Rectangle(0, 0,
                        sourceBitmap.Width, sourceBitmap.Height),
                    ImageLockMode.ReadOnly,
                    PixelFormat.Format32bppArgb);

            var pixelBuffer = new byte[sourceData.Stride *
                                       sourceData.Height];

            var resultBuffer = new byte[sourceData.Stride *
                                        sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0,
                pixelBuffer.Length);

            sourceBitmap.UnlockBits(sourceData);

            var filterOffset = (matrixSize - 1) / 2;
            var calcOffset = 0;

            var byteOffset = 0;

            var neighbourPixels = new List<int>();
            byte[] middlePixel;

            for (var offsetY = filterOffset;
                 offsetY <
                 sourceBitmap.Height - filterOffset;
                 offsetY++)
            for (var offsetX = filterOffset;
                 offsetX <
                 sourceBitmap.Width - filterOffset;
                 offsetX++)
            {
                byteOffset = offsetY *
                             sourceData.Stride +
                             offsetX * 4;

                neighbourPixels.Clear();

                for (var filterY = -filterOffset;
                     filterY <= filterOffset;
                     filterY++)
                for (var filterX = -filterOffset;
                     filterX <= filterOffset;
                     filterX++)
                {
                    calcOffset = byteOffset +
                                 filterX * 4 +
                                 filterY * sourceData.Stride;

                    neighbourPixels.Add(BitConverter.ToInt32(
                        pixelBuffer, calcOffset));
                }

                neighbourPixels.Sort();

                middlePixel = BitConverter.GetBytes(
                    neighbourPixels[filterOffset]);

                resultBuffer[byteOffset] = middlePixel[0];
                resultBuffer[byteOffset + 1] = middlePixel[1];
                resultBuffer[byteOffset + 2] = middlePixel[2];
                resultBuffer[byteOffset + 3] = middlePixel[3];
            }

            var resultBitmap = new Bitmap(sourceBitmap.Width,
                sourceBitmap.Height);

            var resultData =
                resultBitmap.LockBits(new Rectangle(0, 0,
                        resultBitmap.Width, resultBitmap.Height),
                    ImageLockMode.WriteOnly,
                    PixelFormat.Format32bppArgb);

            Marshal.Copy(resultBuffer, 0, resultData.Scan0,
                resultBuffer.Length);

            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }

        ///////////////////////////////////////////////////
        /// other filters
        ///
        /// 
        public static Bitmap DistortionBlurFilter(Bitmap sourceBitmap, int distortFactor)
        {
            var pixelBuffer = sourceBitmap.GetByteArray();
            var resultBuffer = sourceBitmap.GetByteArray();

            var imageStride = sourceBitmap.Width * 4;
            int calcOffset = 0, filterY = 0, filterX = 0;
            var factorMax = (distortFactor + 1) * 2;
            var rand = new Random();

            for (var k = 0; k + 4 < pixelBuffer.Length; k += 4)
            {
                filterY = distortFactor - rand.Next(0, factorMax);
                filterX = distortFactor - rand.Next(0, factorMax);

                if (filterX * 4 + k % imageStride < imageStride
                    && filterX * 4 + k % imageStride > 0)
                {
                    calcOffset = k + filterY * imageStride +
                                 4 * filterX;

                    if (calcOffset >= 0 &&
                        calcOffset + 4 < resultBuffer.Length)
                    {
                        resultBuffer[calcOffset] = pixelBuffer[k];
                        resultBuffer[calcOffset + 1] = pixelBuffer[k + 1];
                        resultBuffer[calcOffset + 2] = pixelBuffer[k + 2];
                    }
                }
            }

            return resultBuffer.GetImage(sourceBitmap.Width, sourceBitmap.Height).MedianFilter(3);
        }


        public static Bitmap FuzzyEdgeBlurFilter(Bitmap sourceBitmap, int filterSize, float edgeFactor1,
            float edgeFactor2)
        {
            if (filterSize == 0) return sourceBitmap;
            var bt = filterSize <= 4 ? BlurType.Mean3x3 :
                filterSize <= 6 ? BlurType.Mean5x5 :
                filterSize <= 8 ? BlurType.Mean7x7 : BlurType.Mean9x9;
            return sourceBitmap.BooleanEdgeDetectionFilter(edgeFactor1).ImageBlurFilter(bt)
                .BooleanEdgeDetectionFilter(edgeFactor2);
        }


        public static Bitmap BooleanEdgeDetectionFilter(this Bitmap sourceBitmap, float edgeFactor)
        {
            var pixelBuffer = sourceBitmap.GetByteArray();
            var resultBuffer = new byte[pixelBuffer.Length];
            Buffer.BlockCopy(pixelBuffer, 0, resultBuffer,
                0, pixelBuffer.Length);


            var edgeMasks = GetBooleanEdgeMasks();

            var imageStride = sourceBitmap.Width * 4;
            int matrixMean = 0, pixelTotal = 0;
            int filterY = 0, filterX = 0, calcOffset = 0;
            var matrixPatern = string.Empty;


            for (var k = 0; k + 4 < pixelBuffer.Length; k += 4)
            {
                matrixPatern = string.Empty;
                matrixMean = 0;
                pixelTotal = 0;
                filterY = -1;
                filterX = -1;


                while (filterY < 2)
                {
                    calcOffset = k + filterX * 4 +
                                 filterY * imageStride;


                    calcOffset = calcOffset < 0 ? 0 :
                        calcOffset >= pixelBuffer.Length - 2 ? pixelBuffer.Length - 3 : calcOffset;

                    matrixMean += pixelBuffer[calcOffset];
                    matrixMean += pixelBuffer[calcOffset + 1];
                    matrixMean += pixelBuffer[calcOffset + 2];


                    filterX += 1;


                    if (filterX > 1)
                    {
                        filterX = -1;
                        filterY += 1;
                    }
                }


                matrixMean = matrixMean / 9;
                filterY = -1;
                filterX = -1;


                while (filterY < 2)
                {
                    calcOffset = k + filterX * 4 +
                                 filterY * imageStride;


                    calcOffset = calcOffset < 0 ? 0 :
                        calcOffset >= pixelBuffer.Length - 2 ? pixelBuffer.Length - 3 : calcOffset;


                    pixelTotal = pixelBuffer[calcOffset];
                    pixelTotal += pixelBuffer[calcOffset + 1];
                    pixelTotal += pixelBuffer[calcOffset + 2];

                    matrixPatern += pixelTotal > matrixMean
                        ? "1"
                        : "0";
                    filterX += 1;


                    if (filterX > 1)
                    {
                        filterX = -1;
                        filterY += 1;
                    }
                }


                if (edgeMasks.Contains(matrixPatern))
                {
                    resultBuffer[k] =
                        ClipByte(resultBuffer[k] * edgeFactor);


                    resultBuffer[k + 1] =
                        ClipByte(resultBuffer[k + 1] * edgeFactor);


                    resultBuffer[k + 2] =
                        ClipByte(resultBuffer[k + 2] * edgeFactor);
                }
            }


            return resultBuffer.GetImage(sourceBitmap.Width, sourceBitmap.Height);
        }

        public static List<string> GetBooleanEdgeMasks()
        {
            var edgeMasks = new List<string>();


            edgeMasks.Add("011011011");
            edgeMasks.Add("000111111");
            edgeMasks.Add("110110110");
            edgeMasks.Add("111111000");
            edgeMasks.Add("011011001");
            edgeMasks.Add("100110110");
            edgeMasks.Add("111011000");
            edgeMasks.Add("111110000");
            edgeMasks.Add("111011001");
            edgeMasks.Add("100110111");
            edgeMasks.Add("001011111");
            edgeMasks.Add("111110100");
            edgeMasks.Add("000011111");
            edgeMasks.Add("000110111");
            edgeMasks.Add("001011011");
            edgeMasks.Add("110110100");


            return edgeMasks;
        }


        /// <summary>
        /// Calculate color between two colors
        /// </summary>
        /// <param name="color1">Color 1</param>
        /// <param name="color2">Color 2</param>
        /// <param name="calculationType">Calculation type</param>
        /// <returns></returns>
        private static byte ColorCalculate(byte color1, byte color2, ColorCalculationType calculationType)
        {
            byte resultValue = 0;
            var intResult = 0;

            if (calculationType == ColorCalculationType.Add)
                intResult = color1 + color2;
            else if (calculationType == ColorCalculationType.Average)
                intResult = (color1 + color2) / 2;
            else if (calculationType == ColorCalculationType.SubtractLeft)
                intResult = color1 - color2;
            else if (calculationType == ColorCalculationType.SubtractRight)
                intResult = color2 - color1;
            else if (calculationType == ColorCalculationType.Difference)
                intResult = Math.Abs(color1 - color2);
            else if (calculationType == ColorCalculationType.Multiply)
                intResult = (int) (color1 / 255.0 * color2 / 255.0 * 255.0);
            else if (calculationType == ColorCalculationType.Min)
                intResult = color1 < color2 ? color1 : color2;
            else if (calculationType == ColorCalculationType.Max)
                intResult = color1 > color2 ? color1 : color2;
            else if (calculationType == ColorCalculationType.Amplitude)
                intResult = (int) (Math.Sqrt(color1 * color1 + color2 * color2)
                                   / Math.Sqrt(2.0));

            if (intResult < 0)
                resultValue = 0;
            else if (intResult > 255)
                resultValue = 255;
            else
                resultValue = (byte) intResult;

            return resultValue;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////
        /// BLENDING
        /// ////////////////////////////////////////////////////////////////////////////////////////
        ///
        public static Bitmap ArithmeticBlend(Bitmap sourceBitmap, Bitmap blendBitmap,
            ColorCalculationType calculationType)
        {
            var sourceData = sourceBitmap.LockBits(new Rectangle(0, 0,
                    sourceBitmap.Width, sourceBitmap.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            var pixelBuffer = new byte[sourceData.Stride * sourceData.Height];
            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            sourceBitmap.UnlockBits(sourceData);

            var blendData = blendBitmap.LockBits(new Rectangle(0, 0,
                    blendBitmap.Width, blendBitmap.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            var blendBuffer = new byte[blendData.Stride * blendData.Height];
            Marshal.Copy(blendData.Scan0, blendBuffer, 0, blendBuffer.Length);
            blendBitmap.UnlockBits(blendData);

            for (var k = 0;
                 k + 4 < pixelBuffer.Length &&
                 k + 4 < blendBuffer.Length;
                 k += 4)
            {
                var a = pixelBuffer[k];
                pixelBuffer[k] = ColorCalculate(pixelBuffer[k],
                    blendBuffer[k], calculationType);

                a = pixelBuffer[k + 1];
                pixelBuffer[k + 1] = ColorCalculate(pixelBuffer[k + 1],
                    blendBuffer[k + 1], calculationType);

                a = pixelBuffer[k + 2];
                pixelBuffer[k + 2] = ColorCalculate(pixelBuffer[k + 2],
                    blendBuffer[k + 2], calculationType);
            }

            var resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

            var resultData = resultBitmap.LockBits(new Rectangle(0, 0,
                    resultBitmap.Width, resultBitmap.Height),
                ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            Marshal.Copy(pixelBuffer, 0, resultData.Scan0, pixelBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }


        //////////////////////////////////////////////
        /// LOCAL HELPERS
        ///
        public static Bitmap GetImage(this byte[] resultBuffer, int width, int height)
        {
            var resultBitmap = new Bitmap(width, height);

            var resultData = resultBitmap.LockBits(new Rectangle(0, 0,
                    resultBitmap.Width, resultBitmap.Height),
                ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);

            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }

        private static byte ClipByte(double colour)
        {
            return (byte) (colour > 255 ? 255 :
                colour < 0 ? 0 : colour);
        }

        private static byte[] GetByteArray(this Bitmap sourceBitmap)
        {
            var sourceData =
                sourceBitmap.LockBits(new Rectangle(0, 0,
                        sourceBitmap.Width, sourceBitmap.Height),
                    ImageLockMode.ReadOnly,
                    PixelFormat.Format32bppArgb);

            var sourceBuffer = new byte[sourceData.Stride *
                                        sourceData.Height];

            Marshal.Copy(sourceData.Scan0, sourceBuffer, 0,
                sourceBuffer.Length);

            sourceBitmap.UnlockBits(sourceData);

            return sourceBuffer;
        }
    }
}
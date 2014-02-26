/*
 * --------------------------------------------------------------------------------
 * Copyright (c) 2006 Mark Gwilliam (aka MarkGwilliam.com)
 * 
 * --------------------------------------------------------------------------------
 * Licensed under the MIT license
 * 
 * See http://opensource.org/licenses/mit-license.php
 * 
 * --------------------------------------------------------------------------------
 * Permission is hereby granted, free of charge, to any person obtaining a copy of 
 * this software and associated documentation files (the "Software"), to deal in 
 * the Software without restriction, including without limitation the rights to 
 * use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies 
 * of the Software, and to permit persons to whom the Software is furnished to do 
 * so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all 
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS 
 * FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR 
 * COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER 
 * IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN 
 * CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 * --------------------------------------------------------------------------------
*/


namespace SerialLabs.Converters
{
    /// <summary>
    /// Common number bases used for conversions
    /// </summary>
    public enum NumberBases
    {
        /// <summary>
        /// Binary, base 2
        /// </summary>
        Binary = 2,

        /// <summary>
        /// Octal, base 8
        /// </summary>
        Octal = 8,

        /// <summary>
        /// Decimal, base 10
        /// </summary>
        Decimal = 10,

        /// <summary>
        /// Hexadecimal, base 16
        /// </summary>
        Hexadecimal = 16
    }
}

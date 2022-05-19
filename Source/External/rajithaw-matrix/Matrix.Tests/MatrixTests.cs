using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class MatrixTests
{
    [TestMethod]
    public void MatricesAreEqualWhenAllElementsAreEqual()
    {
        // Left hand side matrix
        Matrix mtxLhs = new Matrix(3, 3);
        mtxLhs[0, 0] = 3; mtxLhs[0, 1] = 1; mtxLhs[0, 2] = 7;
        mtxLhs[1, 0] = 5; mtxLhs[1, 1] = 9; mtxLhs[1, 2] = 2;
        mtxLhs[2, 0] = 9; mtxLhs[2, 1] = 0; mtxLhs[2, 2] = 4;

        // Right hand side matrix. All elements are equal to the LHS matrix
        Matrix mtxRhs = new Matrix(3, 3);
        mtxRhs[0, 0] = 3; mtxRhs[0, 1] = 1; mtxRhs[0, 2] = 7;
        mtxRhs[1, 0] = 5; mtxRhs[1, 1] = 9; mtxRhs[1, 2] = 2;
        mtxRhs[2, 0] = 9; mtxRhs[2, 1] = 0; mtxRhs[2, 2] = 4;

        Assert.IsTrue(mtxLhs == mtxRhs);
    }

    [TestMethod]
    public void MatricesAreNotEqualWhenAtOnrOrMoreElementAreNotEqual()
    {
        // Left hand side matrix
        Matrix mtxLhs = new Matrix(3, 3);
        mtxLhs[0, 0] = 3; mtxLhs[0, 1] = 1; mtxLhs[0, 2] = 7;
        mtxLhs[1, 0] = 5; mtxLhs[1, 1] = 9; mtxLhs[1, 2] = 2;
        mtxLhs[2, 0] = 9; mtxLhs[2, 1] = 0; mtxLhs[2, 2] = 4;

        // Right hand side matrix. Elements are not equal to the LHS matrix
        Matrix mtxRhs = new Matrix(3, 3);
        mtxRhs[0, 0] = 7; mtxRhs[0, 1] = 9; mtxRhs[0, 2] = 2;
        mtxRhs[1, 0] = 0; mtxRhs[1, 1] = 3; mtxRhs[1, 2] = 5;
        mtxRhs[2, 0] = 4; mtxRhs[2, 1] = 8; mtxRhs[2, 2] = 6;

        Assert.IsTrue(mtxLhs != mtxRhs);
    }

    [TestMethod]
    public void MultiplyTwoMatricesTogether()
    {
        // Left hand side matrix (3x2)
        Matrix mtxLhs = new Matrix(3, 2);
        mtxLhs[0, 0] = 7; mtxLhs[0, 1] = 9;
        mtxLhs[1, 0] = 4; mtxLhs[1, 1] = 3;
        mtxLhs[2, 0] = 2; mtxLhs[2, 1] = 5;

        // Right hand side matrix (2x3)
        Matrix mtxRhs = new Matrix(2, 3);
        mtxRhs[0, 0] = 3; mtxRhs[0, 1] = 1; mtxRhs[0, 2] = 7;
        mtxRhs[1, 0] = 5; mtxRhs[1, 1] = 9; mtxRhs[1, 2] = 2;

        // Expected result
        Matrix expected = new Matrix(3, 3);
        expected[0, 0] = 66; expected[0, 1] = 88; expected[0, 2] = 67;
        expected[1, 0] = 27; expected[1, 1] = 31; expected[1, 2] = 34;
        expected[2, 0] = 31; expected[2, 1] = 47; expected[2, 2] = 24;

        // Actual result should be a 3x3 matrix
        Matrix result = mtxLhs * mtxRhs;

        Assert.AreEqual(result.Rows, mtxLhs.Rows);
        Assert.AreEqual(result.Columns, mtxRhs.Columns);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [ExpectedException(typeof(MatrixException))]
    public void MultiplyTwoMatricesWithUnmatchingRowsAndColumns()
    {
        // Left hand side matrix (2x3)
        Matrix mtxLhs = new Matrix(2, 3);
        mtxLhs[0, 0] = 3; mtxLhs[0, 1] = 1; mtxLhs[0, 2] = 7;
        mtxLhs[1, 0] = 5; mtxLhs[1, 1] = 9; mtxLhs[1, 2] = 2;

        // Right hand side matrix (2x3)
        Matrix mtxRhs = new Matrix(2, 3);
        mtxRhs[0, 0] = 7; mtxRhs[0, 1] = 9; mtxRhs[0, 2] = 2;
        mtxRhs[1, 0] = 0; mtxRhs[1, 1] = 3; mtxRhs[1, 2] = 5;

        // Should throw an exception because two 2x3 matrices cannot be multiplied together 
        Matrix result = mtxLhs * mtxRhs;
    }

    [TestMethod]
    public void PostMultiplyAMatrixByANumber()
    {
        // 3x3 matrix
        Matrix mtx = new Matrix(3, 3);
        mtx[0, 0] = 2; mtx[0, 1] = 7; mtx[0, 2] = 3;
        mtx[1, 0] = 6; mtx[1, 1] = 4; mtx[1, 2] = 0;
        mtx[2, 0] = 0; mtx[2, 1] = 5; mtx[2, 2] = 1;

        float multiplier = 2.4f;

        // Expected result
        Matrix expected = new Matrix(3, 3);
        expected[0, 0] = 4.8f; expected[0, 1] = 16.8f; expected[0, 2] = 7.2f;
        expected[1, 0] = 14.4f; expected[1, 1] = 9.6f; expected[1, 2] = 0f;
        expected[2, 0] = 0f; expected[2, 1] = 12f; expected[2, 2] = 2.4f;

        Matrix result = mtx * multiplier;
        // Round the result to remove floating point errors
        result.Round(2);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void PreMultiplyAMatrixByANumber()
    {
        // 3x3 matrix
        Matrix mtx = new Matrix(3, 3);
        mtx[0, 0] = 2; mtx[0, 1] = 7; mtx[0, 2] = 3;
        mtx[1, 0] = 6; mtx[1, 1] = 4; mtx[1, 2] = 0;
        mtx[2, 0] = 0; mtx[2, 1] = 5; mtx[2, 2] = 1;

        float multiplier = 2.4f;

        // Expected result
        Matrix expected = new Matrix(3, 3);
        expected[0, 0] = 4.8f; expected[0, 1] = 16.8f; expected[0, 2] = 7.2f;
        expected[1, 0] = 14.4f; expected[1, 1] = 9.6f; expected[1, 2] = 0f;
        expected[2, 0] = 0f; expected[2, 1] = 12f; expected[2, 2] = 2.4f;

        Matrix result = multiplier * mtx;
        // Round the result to remove floating point errors
        result.Round(2);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void DevideAMatrixByANumber()
    {
        // 3x3 matrix
        Matrix mtx = new Matrix(3, 3);
        mtx[0, 0] = 2; mtx[0, 1] = 7; mtx[0, 2] = 3;
        mtx[1, 0] = 6; mtx[1, 1] = 4; mtx[1, 2] = 0;
        mtx[2, 0] = 0; mtx[2, 1] = 5; mtx[2, 2] = 1;

        float multiplier = 2.4f;

        // Expected result
        Matrix expected = new Matrix(3, 3);
        expected[0, 0] = 0.8333f; expected[0, 1] = 2.9167f; expected[0, 2] = 1.25f;
        expected[1, 0] = 2.5f; expected[1, 1] = 1.6667f; expected[1, 2] = 0f;
        expected[2, 0] = 0f; expected[2, 1] = 2.0833f; expected[2, 2] = 0.4167f;

        Matrix result = mtx / multiplier;
        // Round the result to remove floating point errors
        result.Round(4);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [ExpectedException(typeof(MatrixException))]
    public void DevideAMatrixByZero()
    {
        // 3x3 matrix
        Matrix mtx = new Matrix(3, 3);
        mtx[0, 0] = 2; mtx[0, 1] = 7; mtx[0, 2] = 3;
        mtx[1, 0] = 6; mtx[1, 1] = 4; mtx[1, 2] = 0;
        mtx[2, 0] = 0; mtx[2, 1] = 5; mtx[2, 2] = 1;

        float multiplier = 0f;

        Matrix result = mtx / multiplier;
    }

    [TestMethod]
    public void NthPowerOfAMatrix()
    {
        // 3x3 matrix
        Matrix mtx = new Matrix(3, 3);
        mtx[0, 0] = 6; mtx[0, 1] = 2; mtx[0, 2] = 0;
        mtx[1, 0] = 4; mtx[1, 1] = 1; mtx[1, 2] = 9;
        mtx[2, 0] = 0; mtx[2, 1] = 5; mtx[2, 2] = 3;

        float power = 2f;

        // Expected result
        Matrix expected = new Matrix(3, 3);
        expected[0, 0] = 320f; expected[0, 1] = 192f; expected[0, 2] = 180f;
        expected[1, 0] = 384f; expected[1, 1] = 290f; expected[1, 2] = 594f;
        expected[2, 0] = 200f; expected[2, 1] = 330f; expected[2, 2] = 342f;

        Matrix result = mtx ^ power;

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [ExpectedException(typeof(MatrixException))]
    public void NthPowerOfANonSquareMatrix()
    {
        // 2x3 matrix
        Matrix mtx = new Matrix(2, 3);
        mtx[0, 0] = 6; mtx[0, 1] = 2; mtx[0, 2] = 0;
        mtx[1, 0] = 4; mtx[1, 1] = 1; mtx[1, 2] = 9;

        float power = 3f;

        Matrix result = mtx ^ power;
    }

    [TestMethod]
    public void AddingTwoMatricesTogether()
    {
        // Left hand side matrix (3x3)
        Matrix mtxLhs = new Matrix(3, 3);
        mtxLhs[0, 0] = 6; mtxLhs[0, 1] = 3; mtxLhs[0, 2] = 0;
        mtxLhs[1, 0] = 1; mtxLhs[1, 1] = 9; mtxLhs[1, 2] = 4;
        mtxLhs[2, 0] = 8; mtxLhs[2, 1] = 2; mtxLhs[2, 2] = 7;

        // Right hand side matrix (3x3)
        Matrix mtxRhs = new Matrix(3, 3);
        mtxRhs[0, 0] = 5; mtxRhs[0, 1] = 2; mtxRhs[0, 2] = 7;
        mtxRhs[1, 0] = 8; mtxRhs[1, 1] = 3; mtxRhs[1, 2] = 6;
        mtxRhs[2, 0] = 0; mtxRhs[2, 1] = 9; mtxRhs[2, 2] = 4;

        // Expected result
        Matrix expected = new Matrix(3, 3);
        expected[0, 0] = 11; expected[0, 1] = 5; expected[0, 2] = 7;
        expected[1, 0] = 9; expected[1, 1] = 12; expected[1, 2] = 10;
        expected[2, 0] = 8; expected[2, 1] = 11; expected[2, 2] = 11;

        // Actual result should be a 3x3 matrix
        Matrix result = mtxLhs + mtxRhs;

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [ExpectedException(typeof(MatrixException))]
    public void AddingTwoMatricesOfDifferentSizes()
    {
        // Left hand side matrix (3x3)
        Matrix mtxLhs = new Matrix(3, 3);
        mtxLhs[0, 0] = 6; mtxLhs[0, 1] = 3; mtxLhs[0, 2] = 0;
        mtxLhs[1, 0] = 1; mtxLhs[1, 1] = 9; mtxLhs[1, 2] = 4;
        mtxLhs[2, 0] = 8; mtxLhs[2, 1] = 2; mtxLhs[2, 2] = 7;

        // Right hand side matrix (3x2)
        Matrix mtxRhs = new Matrix(3, 2);
        mtxRhs[0, 0] = 5; mtxRhs[0, 1] = 2;
        mtxRhs[1, 0] = 8; mtxRhs[1, 1] = 3;
        mtxRhs[2, 0] = 0; mtxRhs[2, 1] = 9;

        Matrix result = mtxLhs + mtxRhs;
    }

    [TestMethod]
    public void SubtractingOneMatrixFromAnother()
    {
        // Left hand side matrix (3x3)
        Matrix mtxLhs = new Matrix(3, 3);
        mtxLhs[0, 0] = 6; mtxLhs[0, 1] = 3; mtxLhs[0, 2] = 0;
        mtxLhs[1, 0] = 1; mtxLhs[1, 1] = 9; mtxLhs[1, 2] = 4;
        mtxLhs[2, 0] = 8; mtxLhs[2, 1] = 2; mtxLhs[2, 2] = 7;

        // Right hand side matrix (3x3)
        Matrix mtxRhs = new Matrix(3, 3);
        mtxRhs[0, 0] = 5; mtxRhs[0, 1] = 2; mtxRhs[0, 2] = 7;
        mtxRhs[1, 0] = 8; mtxRhs[1, 1] = 3; mtxRhs[1, 2] = 6;
        mtxRhs[2, 0] = 0; mtxRhs[2, 1] = 9; mtxRhs[2, 2] = 4;

        // Expected result
        Matrix expected = new Matrix(3, 3);
        expected[0, 0] = 1; expected[0, 1] = 1; expected[0, 2] = -7;
        expected[1, 0] = -7; expected[1, 1] = 6; expected[1, 2] = -2;
        expected[2, 0] = 8; expected[2, 1] = -7; expected[2, 2] = 3;

        // Actual result should be a 3x3 matrix
        Matrix result = mtxLhs - mtxRhs;

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [ExpectedException(typeof(MatrixException))]
    public void SubtractingTwoMatricesOfDifferentSizes()
    {
        // Left hand side matrix (3x3)
        Matrix mtxLhs = new Matrix(3, 3);
        mtxLhs[0, 0] = 6; mtxLhs[0, 1] = 3; mtxLhs[0, 2] = 0;
        mtxLhs[1, 0] = 1; mtxLhs[1, 1] = 9; mtxLhs[1, 2] = 4;
        mtxLhs[2, 0] = 8; mtxLhs[2, 1] = 2; mtxLhs[2, 2] = 7;

        // Right hand side matrix (3x2)
        Matrix mtxRhs = new Matrix(3, 2);
        mtxRhs[0, 0] = 5; mtxRhs[0, 1] = 2;
        mtxRhs[1, 0] = 8; mtxRhs[1, 1] = 3;
        mtxRhs[2, 0] = 0; mtxRhs[2, 1] = 9;

        Matrix result = mtxLhs - mtxRhs;
    }

    [TestMethod]
    public void TransposeOfAMatrix()
    {
        // 2x3 matrix
        Matrix mtx = new Matrix(2, 3);
        mtx[0, 0] = 6; mtx[0, 1] = 2; mtx[0, 2] = 0;
        mtx[1, 0] = 4; mtx[1, 1] = 1; mtx[1, 2] = 9;

        // Expected result is a 3x2 matrix
        Matrix expected = new Matrix(3, 2);
        expected[0, 0] = 6; expected[0, 1] = 4;
        expected[1, 0] = 2; expected[1, 1] = 1;
        expected[2, 0] = 0; expected[2, 1] = 9;

        Matrix result = ~mtx;

        Assert.AreEqual(result.Rows, mtx.Columns);
        Assert.AreEqual(result.Columns, mtx.Rows);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void InverseOfANonSingularMatrix()
    {
        // 3x3 matrix
        Matrix mtx = new Matrix(3, 3);
        mtx[0, 0] = 1; mtx[0, 1] = 2; mtx[0, 2] = 3;
        mtx[1, 0] = 0; mtx[1, 1] = 1; mtx[1, 2] = 4;
        mtx[2, 0] = 5; mtx[2, 1] = 6; mtx[2, 2] = 0;

        // Expected result is a 3x2 matrix
        Matrix expected = new Matrix(3, 3);
        expected[0, 0] = -24; expected[0, 1] = 18; expected[0, 2] = 5;
        expected[1, 0] = 20; expected[1, 1] = -15; expected[1, 2] = -4;
        expected[2, 0] = -5; expected[2, 1] = 4; expected[2, 2] = 1;

        Matrix result = !mtx;

        Assert.AreEqual(expected, result);
    }
}
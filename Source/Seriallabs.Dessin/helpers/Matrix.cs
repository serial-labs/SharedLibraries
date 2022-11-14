/*  Matrix.cs -- Matrix class.

    Copyright (c) 2012 by Rajitha Wimalasooriya
*/

using System;
using System.Text;

/// <summary>
/// Represents a general purpose matrix class
/// </summary>
public class Matrix
{
    #region Variables

    // Holds the number of rows in the current matrix
    private int m_rows;
    // Holds the number of columns in the current matrix
	private int m_columns;

    // Holds all elements in the current matrix
	private float[,] m_matrix;

    #endregion

    #region Indexers

    /// <summary>
    /// The row and column indexer
    /// </summary>
    /// <param name="row">The row number</param>
    /// <param name="column">The column number</param>
    /// <returns>The element at the provided row and column</returns>
    public float this[int row, int column]
	{
		get
		{
			return m_matrix[row,column];
		}
		set
		{
			m_matrix[row,column] = value;
		}
	}

    #endregion

    #region Properties

    /// <summary>
    /// Number of rows in the matrix
    /// </summary>
    public int Rows
	{
		get
		{
			return m_rows;
		}
	}

    /// <summary>
    /// Number of columns in the matrix
    /// </summary>
	public int Columns
	{
		get
		{
			return m_columns;
		}
	}

    #endregion

    #region Constructors

    /// <summary>
    /// Constructs a matrix with the provided number of rows and columns 
    /// </summary>
    /// <param name="rows">Number of rows</param>
    /// <param name="columns">Number of columns</param>
    public Matrix(int rows, int columns)
    {
        m_rows = rows;
        m_columns = columns;

        m_matrix = new float[rows, columns];
    }

    #endregion

    #region Overrides

    /// <summary>
    /// Returns a string that represents the current matrix
    /// </summary>
    /// <returns>A string that represents the current matrix</returns>
    public override string ToString()
	{
		int maxSpace = 0;
			
		// Find the string length of the longest number in the matrix
		for(int i = 0; i < m_rows; i++)
		{
			for(int j = 0; j < m_columns; j++)
			{
				int currentLen = this[i,j].ToString().Length;

				if(maxSpace < currentLen)
				{
					maxSpace = currentLen;
				}
			}
		}

		// Max space needed is one char more than the longest number
		maxSpace++;

		// Calculate an approximate value for the string builder length
		StringBuilder sb = new StringBuilder(maxSpace + (m_rows * m_columns));

		for(int i = 0; i < m_rows; i++)
		{
			for(int j = 0; j < m_columns; j++)
			{
				string currentEle = this[i,j].ToString();

				sb.Append(' ', maxSpace - currentEle.Length);
				sb.Append(currentEle);
			}

			sb.Append("\n");
		}

		return sb.ToString();
	}

    /// <summary>
    /// Serves the hash function for the matrix
    /// </summary>
    /// <returns>A hash code for the current matrix.</returns>
	public override int GetHashCode()
	{
		float result = 0;

		for(int i = 0; i < m_rows; i++)
		{
			for(int j = 0; j < m_columns; j++)
			{
				result += this[i,j];
			}
		}

		return (int)result;
	}

    /// <summary>
    /// Determines whether the provided Object is equal to the current matrix
    /// </summary>
    /// <param name="obj">The object to compare with the current matrix</param>
    /// <returns>True if the provided Object is equal to the current matrix, otherwise false.</returns>
	public override bool Equals(Object obj)
	{
		Matrix mtx = (Matrix)obj;

		if(this.Rows != mtx.Rows || this.Columns != mtx.Columns)
			return false;

		for(int i = 0; i < this.Rows; i++)
		{
			for(int j = 0; j < this.Columns; j++)
			{
				if(this[i,j] != mtx[i,j])
					return false;
			}
		}

		return true;
	}

	#endregion

	#region Operators
 
    /// <summary>
    /// Eqality operator overload
    /// </summary>
    /// <param name="lmtx">Left hand side matrix</param>
    /// <param name="rmtx">Right hand side matrix</param>
    /// <returns>True if matrixes are equal, otherwise false.</returns>
	public static bool operator == (Matrix lmtx, Matrix rmtx)
	{
		return Equals(lmtx, rmtx);
	}

    /// <summary>
    /// Inequality operator overload
    /// </summary>
    /// <param name="lmtx">Left hand side matrix</param>
    /// <param name="rmtx">Right hand side matrix</param>
    /// <returns>True if matrixes are not equal, otherwise false.</returns>
	public static bool operator != (Matrix lmtx, Matrix rmtx)
	{
		return !(lmtx == rmtx);
	}

    /// <summary>
    /// Multiplication operator overload (Multiplication by matrix)
    /// </summary>
    /// <param name="lmtx">Left hand side matrix</param>
    /// <param name="rmtx">Right hand side matrix</param>
    /// <returns>The multiplication result martix</returns>
	public static Matrix operator * (Matrix lmtx, Matrix rmtx)
	{
		if(lmtx.Columns != rmtx.Rows)
			throw new MatrixException("Attempt to multiply matrices with unmatching row and column indexes");
			//return null;

		Matrix result = new Matrix(lmtx.Rows,rmtx.Columns);

		for(int i = 0; i < lmtx.Rows; i++)
		{
			for(int j = 0; j < rmtx.Columns; j++)
			{
				for(int k = 0; k < rmtx.Rows; k++)
				{
					result[i,j] += lmtx[i,k] * rmtx[k,j];
				}
			}
		}

		return result;
	}


    /// <summary>
    /// Multiplication operator overload (Post multiplication by a constant)
    /// </summary>
    /// <param name="mtx">Left hand side matrix</param>
    /// <param name="val">Constant value to be multiplied by</param>
    /// <returns>The multiplication result martix</returns>
	public static Matrix operator * (Matrix mtx, float val)
	{
		Matrix result = new Matrix(mtx.Rows,mtx.Columns);

		for(int i = 0; i < mtx.Rows; i++)
		{
			for(int j = 0; j < mtx.Columns; j++)
			{
				result[i,j] = mtx[i,j] * val;
			}
		}

		return result;
	}

    /// <summary>
    /// Multiplication operator overload (Pre multiplication by a constant)
    /// </summary>
    /// <param name="val">Constant value to be multiplied by</param>
    /// <param name="mtx">Right hand side matrix</param>
    /// <returns>The multiplication result martix</returns>
	public static Matrix operator * (float val, Matrix mtx)
	{
		return mtx * val;
	}

    /// <summary>
    /// Devision operator overload (Devision by a constant)
    /// </summary>
    /// <param name="mtx">Left hand side matrix</param>
    /// <param name="val">Constant value to be devided by</param>
    /// <returns>The devision result martix</returns>
	public static Matrix operator / (Matrix mtx, float val)
	{
		if(val == 0)
			throw new MatrixException("Attempt to devide the matrix by zero");
			//return null;

		Matrix result = new Matrix(mtx.Rows,mtx.Columns);

		for(int i = 0; i < mtx.Rows; i++)
		{
			for(int j = 0; j < mtx.Columns; j++)
			{
				result[i,j] = mtx[i,j] / val;
			}
		}

		return result;
	}

    /// <summary>
    /// N th power operator
    /// </summary>
    /// <param name="mtx">The matrix to find the N th power</param>
    /// <param name="val">The power</param>
    /// <returns>Result matrix to the power of N</returns>
	public static Matrix operator ^ (Matrix mtx, float val)
	{
		if(mtx.Rows != mtx.Columns)
			throw new MatrixException("Attempt to find the power of a non square matrix");
			//return null;

		Matrix result = mtx;

		for(int i = 0; i < val; i++)
		{
			result = result * mtx;
		}

		return result;
	}

    /// <summary>
    /// Addition operator overload
    /// </summary>
    /// <param name="lmtx">Left hand side matrix</param>
    /// <param name="rmtx">Right hand side matrix</param>
    /// <returns>The addition result martix</returns>
	public static Matrix operator + (Matrix lmtx, Matrix rmtx)
	{
		if(lmtx.Rows != rmtx.Rows || lmtx.Columns != rmtx.Columns)
			throw new MatrixException("Attempt to add matrixes of different sizes");
			//return null;

		Matrix result = new Matrix(lmtx.Rows,lmtx.Columns);

		for(int i = 0; i < lmtx.Rows; i++)
		{
			for(int j = 0; j < lmtx.Columns; j++)
			{
				result[i,j] = lmtx[i,j] + rmtx[i,j];
			}
		}

		return result;
	}

    /// <summary>
    /// Subtraction operator overload
    /// </summary>
    /// <param name="lmtx">Left hand side matrix</param>
    /// <param name="rmtx">Right hand side matrix</param>
    /// <returns>The subtraction result martix</returns>
	public static Matrix operator - (Matrix lmtx, Matrix rmtx)
	{

		if(lmtx.Rows != rmtx.Rows || lmtx.Columns != rmtx.Columns)
			throw new MatrixException("Attempt to subtract matrixes of different sizes");
			//return null;

		Matrix result = new Matrix(lmtx.Rows,lmtx.Columns);

		for(int i = 0; i < lmtx.Rows; i++)
		{
			for(int j = 0; j < lmtx.Columns; j++)
			{
				result[i,j] = lmtx[i,j] - rmtx[i,j];
			}
		}

		return result;
	}

    /// <summary>
    /// Transpose operator
    /// </summary>
    /// <param name="mtx">The matrix to find the transpose</param>
    /// <returns>The transpose result matrix</returns>
	public static Matrix operator ~ (Matrix mtx)
	{
		Matrix result = new Matrix(mtx.Columns,mtx.Rows);

		for(int i = 0; i < mtx.Rows; i++)
		{
			for(int j = 0; j < mtx.Columns; j++)
			{
				result[j,i] = mtx[i,j];
			}
		}

		return result;
	}

    /// <summary>
    /// Inverse operator
    /// </summary>
    /// <param name="mtx">The matrix to find the inverse</param>
    /// <returns>The inverse result matrix</returns>
	public static Matrix operator ! (Matrix mtx)
	{
		if(mtx.Determinant() == 0)
			throw new MatrixException("Attempt to invert a singular matrix");
			//return null;

		// Inverse of a 2x2 matrix
		if(mtx.Rows == 2 && mtx.Columns == 2)
		{
			Matrix tempMtx = new Matrix(2,2);

			tempMtx[0,0] = mtx[1,1];
			tempMtx[0,1] = -mtx[0,1];
			tempMtx[1,0] = -mtx[1,0];
			tempMtx[1,1] = mtx[0,0];

			return tempMtx / mtx.Determinant();
		}

		return mtx.Adjoint()/mtx.Determinant();

	}

	#endregion

	#region Methods

    /// <summary>
    /// Returns the determinant of the current matrix
    /// </summary>
    /// <returns>The determinant of the current matrix</returns>
	public float Determinant()
	{
		float determinent = 0;

		if(this.Rows != this.Columns)
			throw new MatrixException("Attempt to find the determinent of a non square matrix");
			//return 0;

		// Get the determinent of a 2x2 matrix
		if(this.Rows == 2 && this.Columns == 2)
		{
			determinent = (this[0,0] * this[1,1]) - (this[0,1] * this[1,0]);   
			return determinent;
		}

		Matrix tempMtx = new Matrix(this.Rows - 1, this.Columns - 1);
 
		// Find the determinent with respect to the first row
		for(int j = 0; j < this.Columns; j++)
		{
			tempMtx = this.Minor(0, j);

			// Recursively add the determinents
			determinent += (int)Math.Pow(-1, j) * this[0,j] * tempMtx.Determinant();
				
		}

		return determinent;
	}

    /// <summary>
    /// Returns the adjoint matrix of the current matrix
    /// </summary>
    /// <returns>The adjoint matrix of the current matrix</returns>
	public Matrix Adjoint()
	{
		if(this.Rows < 2 || this.Columns < 2)
			throw new MatrixException("Adjoint matrix not available");

		Matrix tempMtx = new Matrix(this.Rows-1 , this.Columns-1);
		Matrix adjMtx = new Matrix (this.Columns , this.Rows);

		for(int i = 0; i < this.Rows; i++)
		{
			for(int j = 0; j < this.Columns;j++)
			{
				tempMtx = this.Minor(i, j);

				// Put the determinent of the minor in the transposed position
                adjMtx[j, i] = (int)Math.Pow(-1, i + j) * tempMtx.Determinant();
			}
		}

		return adjMtx;
	}

    /// <summary>
    /// Returns a minor of a matrix with respect to an element
    /// </summary>
    /// <param name="row">Row number of the element</param>
    /// <param name="column">Column number of the element</param>
    /// <returns>The minor matrix</returns>
	public Matrix Minor(int row, int column)
	{
		if(this.Rows < 2 || this.Columns < 2)
			throw new MatrixException("Minor not available");

		int i, j = 0;

		Matrix minorMtx = new Matrix(this.Rows - 1, this.Columns - 1);
 
		// Find the minor with respect to the first element
		for(int k = 0; k < minorMtx.Rows; k++)
		{

			if(k >= row)
				i = k + 1;
			else
				i = k;

			for(int l = 0; l < minorMtx.Columns; l++)
			{
				if(l >= column)
					j = l + 1;
				else
					j = l;

				minorMtx[k,l] = this[i,j];
			}
		}

		return minorMtx;
	}

    /// <summary>
    /// Returns weather the matrix is an identity matrix
    /// </summary>
    /// <returns>True if the matrix is identity, otherwise false</returns>
	public bool IsIdentity()
	{
		if(Rows != Columns)
			return false;

		for(int i = 0; i < Rows; i++)
		{
			for(int j = 0; j < Columns; j++)
			{
				if(i == j)
				{
					if(this[i,j] != 1.0f) return false;
				}
				else
				{
					if(this[i,j] != 0.0f) return false;
				}
			}
		}

		return true;
	}

    /// <summary>
    /// Returns weather the matrix is invertible
    /// </summary>
    /// <returns>True if the matrix is non singular, otherwise false</returns>
	public bool IsInvertible()
	{
        try
        {
            if (this.Determinant() == 0)
            {
                return false;
            }
        }
        catch (MatrixException)
        {
            return false;
        }

		return true;
	}

    /// <summary>
    /// Makes the matrix an identity matrix
    /// </summary>
	public void Reset()
	{
		if(this.Rows != this.Columns)
			throw new MatrixException("Attempt to make non square matrix identity");

		for(int j = 0; j < this.Columns; j++)
		{
			for(int i = 0; i < this.Rows; i++)
			{
				if(i == j)
				{
					this[i,j] = 1.0f;
				}
				else
				{
					this[i,j] = 0.0f;
				}
			}
		}
	}

    /// <summary>
    /// Makes the matrix a zero matrix
    /// </summary>
	public void Clear()
	{
		for(int j = 0; j < this.Columns; j++)
		{
			for(int i = 0; i < this.Rows; i++)
			{
				this[i,j] = 0.0f;
			}
		}
	}

    /// <summary>
    /// Rounds the elements of the matrix to the given percision
    /// </summary>
    /// <param name="precision">Percision to be used when rounding</param>
    public void Round(int precision)
    {
        for (int i = 0; i < this.Rows; i++)
        {
            for (int j = 0; j < this.Columns; j++)
            {
                this[i, j] = (float)Math.Round(this[i, j], precision);
            }
        }
    }

	#endregion
}

/// <summary>
/// Represents a matrix exception
/// </summary>
public class MatrixException : Exception
{
	public MatrixException(string message) : base(message)
	{
	}
}
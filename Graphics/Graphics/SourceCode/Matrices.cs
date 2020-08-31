using System;

using Vectors;

namespace Matrices
{
    public class Matrix
    {
        private float[][] M;

        public int rows;
        public int columns;


        public float[][] matrix
        {
            get
            {
                return M;
            }
            set
            {
                columns = value[0].Length;
                rows = value.Length;
                M = value;
            }
        }

        public float[] this[int i]
        {
            get
            {
                return M[i];
            }
            set
            {
                M[i] = value;
            }
        }

        public float this[int i, int j]
        {
            get
            {
                return M[i][j];
            }
            set
            {
                M[i][j] = value;
            }
        }


        public static Matrix Vector3ToMatrix(Vector3 V)
        {
            Matrix Coordinates = new Matrix(4, 1);
            Coordinates.matrix = new float[][]{
                new float[]{V.x},
                new float[]{V.y},
                new float[]{V.z},
                new float[]{1},
                };
            return Coordinates;
        }

        public Matrix(float[][] Input)
        {
            rows = Input.Length;
            columns = Input[0].Length;
            M = new float[rows][];
            for (int i = 0; i < rows; i++)
            {
                M[i] = new float[columns];
                for (int j = 0; j < columns; j++)
                {
                    M[i][j] = Input[i][j];
                }
            }
        }



        public Matrix(int A, int B, float Init = 0)
        {
            rows = A;
            columns = B;
            M = new float[rows][];
            for (int i = 0; i < rows; i++)
            {
                M[i] = new float[columns];
                for (int j = 0; j < columns; j++)
                    M[i][j] = Init;
            }
        }




        public static Matrix operator /(Matrix A, float B)
        {
            float[][] newMatrix = new float[A.rows][];

            for (int i = 0; i < A.rows; i++)
            {
                newMatrix[i] = new float[A.columns];
                for (int j = 0; j < A.columns; j++)
                {
                    newMatrix[i][j] = A[i, j] / B;
                }
            }
            return new Matrix(newMatrix);
        }

        public static Matrix operator *(Matrix A, Matrix B)
        {

            float[][] newMatrix = new float[A.rows][];

            for (int i = 0; i < A.rows; i++)
            {
                newMatrix[i] = new float[B.columns];
                for (int j = 0; j < B.columns; j++)
                {
                    for (int k = 0; k < A.columns; k++)
                    {
                        newMatrix[i][j] += A[i, k] * B[k, j];
                    }
                }
            }
            return new Matrix(newMatrix);
        }

        public static Matrix operator *(Matrix A, float B)
        {
            float[][] newMatrix = new float[A.rows][];

            for (int i = 0; i < A.rows; i++)
            {
                newMatrix[i] = new float[A.columns];
                for (int j = 0; j < A.columns; j++)
                {
                    newMatrix[i][j] = A[i, j] * B;
                }
            }
            return new Matrix(newMatrix);
        }


        public static Matrix operator +(Matrix A, Matrix B)
        {

            float[][] newMatrix = new float[A.rows][];

            for (int i = 0; i < A.rows; i++)
            {
                newMatrix[i] = new float[A.columns];
                for (int j = 0; j < A.columns; j++)
                {
                    newMatrix[i][j] = A[i, j] + B[i, j];
                }
            }
            return new Matrix(newMatrix);
        }

        public static Matrix operator -(Matrix A, Matrix B)
        {

            float[][] newMatrix = new float[A.rows][];

            for (int i = 0; i < A.rows; i++)
            {
                newMatrix[i] = new float[A.columns];
                for (int j = 0; j < A.columns; j++)
                {
                    newMatrix[i][j] = A[i, j] - B[i, j];
                }
            }
            return new Matrix(newMatrix);
        }


        public static void PrintMatrix(Matrix A)
        {
            for (int i = 0; i < A.rows; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < A.columns; j++)
                    Console.Write(A[i, j] + ", ");
            }
        }

    }
}
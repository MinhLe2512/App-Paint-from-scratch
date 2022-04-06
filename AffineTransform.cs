
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19127044_Lab02
{
    class AffineTransform
    {
        //scale matrix
        public double[,] createScaleMatrix(double sx, double sy)
        {
            return new double[3, 3]{ { sx, 0, 0 }, { 0, sy, 0 }, { 0, 0, 1 } };
        }
        //rotate matrix
        public double[,] createRotateMatrix(double angle)
        {
            double handleAngle = Math.PI * angle / 180.0;
            return new double[3, 3] { { Math.Cos(handleAngle), -Math.Sin(handleAngle), 0 }, { Math.Sin(handleAngle), Math.Cos(handleAngle), 0 }, { 0, 0, 1 } };
        }
        //translate matrix
        public double[,] createTranslateMatrix(double tx, double ty)
        {
            return new double[3, 3] { { 1, 0, tx }, { 0, 1, ty }, { 0, 0, 1 } };
        }
        //reflection through Ox
        public double[,] createReflectionX()
        {
            return new double[3, 3] { { 1, 0, 0 }, { 0, -1, 0 }, { 0, 0, 1 } };
        }
        //reflection through Oy
        public double[,] createReflectionY()
        {
            return new double[3, 3] { { -1, 0, 0 }, { 0, 1, 0 }, { 1, 0, 0 } };
        }
        //reflection through O
        public double[,] createReflection()
        {
            return new double[3, 3] { { -1, 0, 0 }, { 0, -1, 0 }, { 0, 0, 1 } };
        }
        //shear through Ox
        public double[,] createShearX(double shearX)
        {
            return new double[3, 3] { { 1, shearX, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };
        }
        //shear through 0y
        public double[,] createShearY(double shearY)
        {
            return new double[3, 3] { { 1, 0, 0 }, { shearY, 1, 0 }, { 0, 0, 1 } };
        }

        public Point Multiply(double[,] matrix1, double[,] matrix2, Point offset)
        {
            // cahing matrix lengths for better performance  
            var matrix1Rows = matrix1.GetLength(0);
            var matrix1Cols = matrix1.GetLength(1);
            var matrix2Rows = matrix2.GetLength(0);
            var matrix2Cols = matrix2.GetLength(1);

            // checking if product is defined  
            if (matrix1Cols != matrix2Rows)
                throw new InvalidOperationException
                  ("Product is undefined. n columns of first matrix must equal to n rows of second matrix");

            // creating the final product matrix  
            double[,] product = new double[matrix1Rows, matrix2Cols];

            // looping through matrix 1 rows  
            for (int matrix1_row = 0; matrix1_row < matrix1Rows; matrix1_row++)
            {
                // for each matrix 1 row, loop through matrix 2 columns  
                for (int matrix2_col = 0; matrix2_col < matrix2Cols; matrix2_col++)
                {
                    // loop through matrix 1 columns to calculate the dot product  
                    for (int matrix1_col = 0; matrix1_col < matrix1Cols; matrix1_col++)
                    {
                        product[matrix1_row, matrix2_col] +=
                          matrix1[matrix1_row, matrix1_col] *
                          matrix2[matrix1_col, matrix2_col];
                    }
                }
            }

            return new Point((int)product[0, 0] + offset.X, (int)product[1, 0] + offset.Y);
        }
    }
}

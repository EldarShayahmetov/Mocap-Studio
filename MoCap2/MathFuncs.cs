using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace MCMain
{
    public static class MathFuncs
    {

        public static float[,] Get3DWorldPoints(float[,] _hPoints)
        {
            Matrix<float> hPoints = Matrix<float>.Build.DenseOfArray(_hPoints);
            Vector<float> w = hPoints.Row(3);
            Matrix<float> Points = hPoints.RemoveRow(3);
            Vector<float> x = Points.Row(0).PointwiseDivide(w);
            Vector<float> y = Points.Row(1).PointwiseDivide(w);
            Vector<float> z = Points.Row(2).PointwiseDivide(w);
            Matrix<float> outPoints = Matrix<float>.Build.DenseOfRowVectors(x, y, z);
            float[,] outArrayOfPoints = outPoints.ToArray();

            return outArrayOfPoints;
        }

    }
}

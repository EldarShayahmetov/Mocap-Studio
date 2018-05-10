using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.Features2D;
using Emgu.CV.CvEnum;
using System.Drawing;

namespace MoCap2
{
    static class FivePoint
    {

        public static void DecomposeEssentialMat(Mat _E, out Matrix<double> _R1, out Matrix<double> _R2, out Matrix<double> _t)
        {
            Mat E = _E;


            Debug.Assert(E.Cols == 3 && E.Rows == 3);

            Mat Dm = new Mat();
            Mat Um = new Mat();
            Mat Vtm = new Mat();

            CvInvoke.SVDecomp(E, Dm, Um, Vtm, SvdFlag.Default);

            Matrix<double> D = new Matrix<double>(Dm.Rows, Dm.Cols, Dm.NumberOfChannels);
            Dm.CopyTo(D);
            Matrix<double> U = new Matrix<double>(Um.Rows, Um.Cols, Um.NumberOfChannels);
            Um.CopyTo(U);
            Matrix<double> Vt = new Matrix<double>(Vtm.Rows, Vtm.Cols, Vtm.NumberOfChannels);
            Vtm.CopyTo(Vt);

            if (CvInvoke.Determinant(U) < 0)
            {
                U *= -1;
            }

            if (CvInvoke.Determinant(Vt) < 0)
            {
                Vt *= -1;
            }


            double[,] Warr = { { 0, 1, 0},
                            { -1, 0, 0},
                            { 0, 0, 1}};

            Matrix<double> W = new Matrix<double>(Warr);

            _R1 = U * W * Vt;
            _R2 = U * W.Transpose() * Vt;
            _t = U.GetCol(2) * 1.0;

        }
        //  OutputArray _R, OutputArray _t, double distanceThresh, InputOutputArray _mask, OutputArray triangulatedPoints

        public static int RecoverPose(Mat E, PointF[] _points1, PointF[] _points2, Mat _cameraMatrix, double _distanceThresh, out Mat _R, out Mat _t, out Mat _PM)
        {
            Mat pointsMat1 = new Mat();
            Mat pointsMat2 = new Mat();
            Mat cameraMat = new Mat();
            cameraMat = _cameraMatrix;


            _R = new Mat();
            _t = new Mat();
            _PM = new Mat();

            double[,] p1;
            double[,] p2;

            if (_points1.Length <= _points2.Length)
            {
                p1 = new double[_points1.Length, 2];
                p2 = new double[_points1.Length, 2];
            }
            else
            {
                p1 = new double[_points2.Length, 2];

                p2 = new double[_points2.Length, 2];
            }

            for (int i = 0; i < _points1.Length && i < _points2.Length; i++)
            {
                p1[i, 0] = (double)_points1[i].X;
                p1[i, 1] = (double)_points1[i].Y;

                p2[i, 0] = (double)_points2[i].X;
                p2[i, 1] = (double)_points2[i].Y;
            }


            Matrix<double> points1 = new Matrix<double>(p1);
            Matrix<double> points2 = new Matrix<double>(p2);
            Matrix<double> cameraMatrix = new Matrix<double>(cameraMat.Rows, cameraMat.Cols, cameraMat.NumberOfChannels);
            cameraMat.CopyTo(cameraMatrix);

            //Need points1.checkVector

            Debug.Assert(cameraMatrix.Rows == 3 && cameraMatrix.Cols == 3 && cameraMatrix.NumberOfChannels == 1);

            double fx = cameraMatrix[0, 0];
            double fy = cameraMatrix[1, 1];
            double cx = cameraMatrix[0, 2];
            double cy = cameraMatrix[1, 2];

            for (int i = 0; i < points1.Rows; i++) {

                points1[i, 0] = (points1[i, 0] - cx) / fx;
                points2[i, 0] = (points2[i, 0] - cx) / fx;
                points1[i, 1] = (points1[i, 1] - cy) / fy;
                points2[i, 1] = (points2[i, 1] - cy) / fy;
            }

            Matrix<double> temp1 = new Matrix<double>(points1.Cols, points1.Rows);
            Matrix<double> temp2 = new Matrix<double>(points2.Cols, points2.Rows);

            CvInvoke.Transpose(points1, temp1);
            CvInvoke.Transpose(points2, temp2);
            points1 = temp1;
            points2 = temp2;

            Matrix<double> R1;
            Matrix<double> R2;
            Matrix<double> t;

            DecomposeEssentialMat(E, out R1, out R2, out t); // we are doing decomposition of E


            double[,] I = { { 1, 0, 0,0},
                            { 0, 1, 0, 0},
                            { 0, 0, 1, 0}};

            Matrix<double> P0 = new Matrix<double>(I); // And we have 4 results
            Matrix<double> P1 = R1.ConcateHorizontal(t);
            Matrix<double> P2 = R2.ConcateHorizontal(t);
            Matrix<double> P3 = R1.ConcateHorizontal(t * (-1));
            Matrix<double> P4 = R2.ConcateHorizontal(t * (-1));

            // Do the cheirality check.
            // Notice here a threshold dist is used to filter
            // out far away points (i.e. infinite points) since
            // their depth may vary between positive and negtive.

            Mat[] allTriangulations = new Mat[4];
            int good1 = Mask(P0, P1, points1, points2, _distanceThresh);
            int good2 = Mask(P0, P2, points1, points2, _distanceThresh);
            int good3 = Mask(P0, P3, points1, points2, _distanceThresh);
            int good4 = Mask(P0, P4, points1, points2, _distanceThresh);



            if (good1 >= good2 && good1 >= good3 && good1 >= good4)
            {
                _R = R1.Mat;
                _t = t.Mat;
                _PM = P1.Mat;
                return good1;
            }
            else if (good2 >= good1 && good2 >= good3 && good2 >= good4)
            {
                _R = R2.Mat;
                _t = t.Mat;
                _PM = P2.Mat;
                return good2;
            }
            else if (good3 >= good1 && good3 >= good2 && good3 >= good4)
            {
                t = t * (-1);
                _R = R1.Mat;
                _t = t.Mat;
                _PM = P3.Mat;
                return good3;
            }
            else
            {
                t = t * (-1);
                _R = R2.Mat;
                _t = t.Mat;
                _PM = P4.Mat;
                return good4;
            }


        }


        private static int Mask(Matrix<double> P0, Matrix<double> P1, Matrix<double> points1, Matrix<double> points2, double _distanceThresh) {
            try
            {

                Mat Qm = new Mat();
                CvInvoke.TriangulatePoints(P0, P1, points1, points2, Qm);
                Matrix<double> Q = new Matrix<double>(Qm.Rows, Qm.Cols, Qm.NumberOfChannels);
                Qm.CopyTo(Q);

                Matrix<double> mul1 = new Matrix<double>(1, Q.Cols);
                Matrix<double> mask1 = new Matrix<double>(mul1.Rows, mul1.Cols);
                CvInvoke.Multiply(Q.GetRow(2), Q.GetRow(3), mul1);

                //  Mat mask1 = Q.row(2).mul(Q.row(3)) > 0;
                for (int i = 0; i < mul1.Cols; i++)
                {
                    if (mul1[0, i] > (double)0)
                    {
                        mask1[0, i] = 1;
                    }
                    else
                    {
                        mask1[0, i] = 0;
                    }
                }


                int nzero1 = CvInvoke.CountNonZero(mask1);


                Matrix<double> x = new Matrix<double>(1, Q.Cols);
                Matrix<double> y = new Matrix<double>(1, Q.Cols);
                Matrix<double> z = new Matrix<double>(1, Q.Cols);
                Matrix<double> w = new Matrix<double>(1, Q.Cols);
                CvInvoke.Divide(Q.GetRow(0), Q.GetRow(3), x);
                CvInvoke.Divide(Q.GetRow(1), Q.GetRow(3), y);
                CvInvoke.Divide(Q.GetRow(2), Q.GetRow(3), z);
                CvInvoke.Divide(Q.GetRow(3), Q.GetRow(3), w);


                Matrix<double> tempxy = new Matrix<double>(2, Q.Cols);
                Matrix<double> tempzw = new Matrix<double>(2, Q.Cols);
                tempxy = x.ConcateVertical(y);
                tempzw = z.ConcateVertical(w);
                Q = tempxy.ConcateVertical(tempzw);

                // mask1 = (Q.row(2) < distanceThresh) & mask1;
                Matrix<double> xmask1 = new Matrix<double>(1, Q.Cols);
                Matrix<double> ymask1 = new Matrix<double>(mask1.Data);
                for (int i = 0; i < Q.Cols; i++)
                {
                    if (Q.GetRow(2)[0, i] < _distanceThresh)
                    {
                        xmask1[0, i] = 1;
                    }
                    else
                    {
                        xmask1[0, i] = 0;
                    }
                }
                CvInvoke.Multiply(xmask1, ymask1, mask1);

                Q = P1 * Q;

                //  mask1 = (Q.row(2) > 0) & mask1;
                xmask1 = new Matrix<double>(1, Q.Cols);
                ymask1 = new Matrix<double>(mask1.Data);
                for (int i = 0; i < Q.Cols; i++)
                {
                    if (Q.GetRow(2)[0, i] > 0)
                    {
                        xmask1[0, i] = 1;
                    }
                    else
                    {
                        xmask1[0, i] = 0;
                    }
                }
                CvInvoke.Multiply(xmask1, ymask1, mask1);


                // mask1 = (Q.row(2) < distanceThresh) & mask1;
                xmask1 = new Matrix<double>(1, Q.Cols);
                ymask1 = new Matrix<double>(mask1.Data);
                for (int i = 0; i < Q.Cols; i++)
                {
                    if (Q.GetRow(2)[0, i] < _distanceThresh)
                    {
                        xmask1[0, i] = 1;
                    }
                    else
                    {
                        xmask1[0, i] = 0;
                    }
                }
                CvInvoke.Multiply(xmask1, ymask1, mask1);

                return CvInvoke.CountNonZero(mask1);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }
    }
}


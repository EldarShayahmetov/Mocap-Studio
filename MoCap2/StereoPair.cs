using System;
using System.Drawing;
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


namespace MoCap2
{

    public enum SPMode
    {
        View,
        Calibration,
        Triangulation
    }

    public class StereoPair
    {
        public delegate void ModeChanged();
        public event ModeChanged OnModeChanged;

        public delegate void Triangulated(TriangulationEventArgs args);
        public event Triangulated OnTriangulated;


        private StereoPairCalibration _calibration;

        private Camera _camL;
        private Camera _camR;
        private PointF[][] _stereoPoints = new PointF[2][];
        private SPMode _mode;
        private Image _modeImage;
        object _locker = new object();
        private int _pointsBuffer = 200;

        Mat p = new Mat();
        Mat pI = new Mat();
        Mat r = new Mat();
        Mat t = new Mat();
        Matrix<double> pose;
        Matrix<double> rot;



        public StereoPair(Camera camL, Camera camR)
        {
            _camL = camL;
            _camR = camR;
            camL.BlobDet.OnBlobDetected += SyncCameras;
            camR.BlobDet.OnBlobDetected += SyncCameras;
            _stereoPoints[0] = new PointF[1];
            _stereoPoints[1] = new PointF[1];
            Mode = SPMode.View;
        }


        private void SyncCameras(BlobDetectorEventArgs blobPoints)
        {
            //Need to think if not accurate results
            lock (_locker)
            {
                int deviceNum = blobPoints._deviceNum;
                PointF[] points = new PointF[blobPoints._points.Length];
                points = blobPoints._points;

                _stereoPoints[deviceNum] = points;               
                Debug.WriteLine(deviceNum.ToString());

                if (_stereoPoints[0].Length == _stereoPoints[1].Length)
                    DoMode();
            }
        }


        private void DoMode()
        {


            if(_mode == SPMode.Triangulation)
            {
                Triangulate();

            }else if(_mode == SPMode.Calibration){

                //Calibrate class realisation
                if (_calibration.BufferData(_stereoPoints))
                {
                 
                }
                else
                {
                    _calibration.StartCalibration(out p, out r, out t, out pose, out rot);

                    FileStorage fs = new FileStorage("RT.xml", FileStorage.Mode.Append);
                    fs.Write(r);
                    fs.Write(t);
                    fs.ReleaseAndGetString();

                    double[,] I = { { 1, 0, 0, 0}, // Identity
                            { 0, 1, 0, 0},
                            { 0, 0, 1, 0 }};

                    Matrix<double> pIdentity = new Matrix<double>(I);
                    pI = pIdentity.Mat;

                    Mode = SPMode.Triangulation;
                }


            } else if(_mode == SPMode.View)
            {
                // Nothing
            }
        }


        public SPMode Mode
        {
            get { return _mode; }
            set {
                if (value == SPMode.View)
                {
                    _modeImage = Image.FromFile("Assets\\button_view.png");
                    _calibration = null;
                    _camL.Calibration = null;
                    _camR.Calibration = null;
                }
                if (value == SPMode.Calibration)
                {

                        _modeImage = Image.FromFile("Assets\\button_calibration.png");

                        _calibration = new StereoPairCalibration(_camL.CameraMatrix, _camR.CameraMatrix, _camL.DistCoeffs, _camR.DistCoeffs, _camL.ResolutionSize, _pointsBuffer);

 
                        _camL.Calibration = _calibration;
                        _camR.Calibration = _calibration;


                }
                if (value == SPMode.Triangulation)
                {
                    _modeImage = Image.FromFile("Assets\\button_triangulation.png");
                    _calibration = null;
                    _camL.Calibration = null;
                    _camR.Calibration = null;
                }

                _mode = value;

                OnModeChanged?.Invoke();
            }
        }

        public Image ModeImg
        {
            get { return _modeImage; }
        }


        private void Triangulate()
        {
            Mat out4DPoints = new Mat();

            PointF[][] tempStereoPoints = new PointF[2][];
            tempStereoPoints[0] = new PointF[_stereoPoints[0].Length];
            tempStereoPoints[1] = new PointF[_stereoPoints[1].Length];

            for (int i = 0;i<_stereoPoints[0].Length; i++)
            {
                tempStereoPoints[0][i] = _stereoPoints[0][i];
                tempStereoPoints[1][i] = _stereoPoints[1][i];
            }


            for (int i = 0; i < _stereoPoints[0].Length; i++)
            {
                tempStereoPoints[0][i].X = (tempStereoPoints[0][i].X - (float)camerasParam.cx0) / (float)camerasParam.fx0;
                tempStereoPoints[0][i].Y= (tempStereoPoints[0][i].Y - (float)camerasParam.cy0) / (float)camerasParam.fy0;

                tempStereoPoints[1][i].X = (tempStereoPoints[1][i].X - (float)camerasParam.cx1) / (float)camerasParam.fx1;  // (tempStereoPoints[0][i].X - (float)_cx) / (float)_fx; 
                tempStereoPoints[1][i].Y = (tempStereoPoints[1][i].Y - (float)camerasParam.cy1) / (float)camerasParam.fy1; // (tempStereoPoints[0][i].Y - (float)_cy) / (float)_fy;
            }

            double[,] p1 = new double[tempStereoPoints[0].Length, 2];
            double[,] p2 = new double[tempStereoPoints[1].Length, 2];

            for (int i = 0; i < _stereoPoints[0].Length; i++)
            {
                p1[i, 0] = (double)tempStereoPoints[0][i].X;
                p1[i, 1] = (double)tempStereoPoints[0][i].Y;

                p2[i, 0] = (double)tempStereoPoints[1][i].X;
                p2[i, 1] = (double)tempStereoPoints[1][i].Y;
            }

            Matrix<double> points1 = new Matrix<double>(p1);
            Matrix<double> points2 = new Matrix<double>(p2);

            Matrix<double> temp1 = new Matrix<double>(points1.Cols, points1.Rows);
            Matrix<double> temp2 = new Matrix<double>(points2.Cols, points2.Rows);

            CvInvoke.Transpose(points1, temp1);
            CvInvoke.Transpose(points2, temp2);
            points1 = temp1;
            points2 = temp2;

            CvInvoke.TriangulatePoints(pI, p, points1, points2, out4DPoints);

            Matrix<double> out4DMatrix = new Matrix<double>(out4DPoints.Rows, out4DPoints.Cols, out4DPoints.NumberOfChannels);
            out4DPoints.CopyTo(out4DMatrix);


            Matrix<double> x = new Matrix<double>(1, out4DMatrix.Cols);
            Matrix<double> y = new Matrix<double>(1, out4DMatrix.Cols);
            Matrix<double> z = new Matrix<double>(1, out4DMatrix.Cols);
            Matrix<double> w = new Matrix<double>(1, out4DMatrix.Cols);
            CvInvoke.Divide(out4DMatrix.GetRow(0), out4DMatrix.GetRow(3), x);
            CvInvoke.Divide(out4DMatrix.GetRow(1), out4DMatrix.GetRow(3), y);
            CvInvoke.Divide(out4DMatrix.GetRow(2), out4DMatrix.GetRow(3), z);
            CvInvoke.Divide(out4DMatrix.GetRow(3), out4DMatrix.GetRow(3), w);


            Matrix<double> tempxy = new Matrix<double>(2, out4DMatrix.Cols);
            Matrix<double> tempzw = new Matrix<double>(2, out4DMatrix.Cols);
            tempxy = x.ConcateVertical(y);
            tempzw = z.ConcateVertical(w);
            out4DMatrix = tempxy.ConcateVertical(tempzw);

                            double[,] I = { { 1, 0, 0, 0}, // Identity
                            { 0, -1, 0, 0},
                            { 0, 0, -1, 0 },
                             {0,0,0,1 } };

                Matrix<double> glTransform = new Matrix<double>(I);

            out4DMatrix = glTransform * out4DMatrix;

            OnTriangulated?.Invoke(new TriangulationEventArgs(out4DMatrix, pose, rot));
        }



        public int PointsBuffer
        {
            get { return _pointsBuffer; }
            set { _pointsBuffer = value; }
        }
    }
}

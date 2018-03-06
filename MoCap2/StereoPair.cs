using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.Features2D;
using Emgu.CV.CvEnum;


namespace MoCap2
{
    enum SPMode
    {
        View,
        Calibration,
        Triangulation
    }

    class StereoPair
    {
        public delegate void ModeChanged();
        public event ModeChanged OnModeChanged;

        private StereoPairCalibration _calibration;

        private Camera _camL;
        private Camera _camR;
        private PointF[][] _stereoPoints = new PointF[2][];
        private SPMode _mode;
        private Image _modeImage;
        private float _cx, _cy;
        object _locker = new object();

        Mat p = new Mat();
        Mat r = new Mat();
        Mat t = new Mat();



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
                //Triangulate class realisation

            }else if(_mode == SPMode.Calibration){

                if (_calibration == null)
                {
                   
                    _calibration = new StereoPairCalibration(_camL.CameraMatrix, _camR.CameraMatrix, _camL.DistCoeffs, _camR.DistCoeffs, _camL.ResolutionSize);
                    _calibration.SetOffset(_camL.BlobDet.Cx, _camL.BlobDet.Cy, _camR.BlobDet.Cx, _camR.BlobDet.Cy);
                    _camL.Calibration = _calibration;
                    _camR.Calibration = _calibration;
                   
                }

                //Calibrate class realisation
                if (_calibration.BufferData(_stereoPoints))
                {
                 
                }
                else
                {
                    _calibration.StartCalibration(out p, out r, out t);
                    Mode = SPMode.View;
                }


            } else if(_mode == SPMode.View)
            {
                // Nothing
            }
        }


        public SPMode Mode
        {
            get { return _mode; }
            set { _mode = value;
            if(value == SPMode.View)
                _modeImage = Image.FromFile("Assets\\button_view.png");
                if (value == SPMode.Calibration)
                 _modeImage = Image.FromFile("Assets\\button_calibration.png");
                if(value == SPMode.Triangulation)
                 _modeImage = Image.FromFile("Assets\\button_triangulation.png");
                OnModeChanged?.Invoke();
            }
        }

        public Image ModeImg
        {
            get { return _modeImage; }
        }

        public void ProjMatCalc()
        {
            Mat RTstereo = new Mat(3, 4, DepthType.Cv64F, 1); // var for after HConcat
            CvInvoke.HConcat(r, t, RTstereo); // concat [R|T]

            double[,] I = { { 1, 0, 0, 0}, // Identity
                            { 0, 1, 0, 0},
                            { 0, 0, 1, 0 }};

            Matrix<double> RTML = new Matrix<double>(I);
            Matrix<double> RTMR = new Matrix<double>(RTstereo.Rows, RTstereo.Cols, RTstereo.NumberOfChannels);
            RTstereo.CopyTo(RTMR);

            Matrix<double> cameraMatrixL = new Matrix<double>(_camL.CameraMatrix.Rows, _camL.CameraMatrix.Cols, _camL.CameraMatrix.NumberOfChannels);
            _camL.CameraMatrix.CopyTo(cameraMatrixL);// Cope Camera Mat to Matrx
            Matrix<double> cameraMatrixR = new Matrix<double>(_camR.CameraMatrix.Rows, _camR.CameraMatrix.Cols, _camR.CameraMatrix.NumberOfChannels);
            _camR.CameraMatrix.CopyTo(cameraMatrixR);

            /*
            Matrix<double> Rster = new Matrix<double>(r.Rows, r.Cols, r.NumberOfChannels); // Rster and Tster Matrix
            r.CopyTo(Rster);
            Matrix<double> Tster = new Matrix<double>(t.Rows, t.Cols, t.NumberOfChannels);
            t.CopyTo(Tster);
            */

            Matrix<double> projMatrixL = cameraMatrixL * RTML;
            Matrix<double> projMatrixR = cameraMatrixR * RTMR;

            _camL.ProjMat = new Mat();
            _camR.ProjMat = new Mat();

            _camL.ProjMat = projMatrixL.Mat;
            _camR.ProjMat = projMatrixR.Mat;

        }

    }
}

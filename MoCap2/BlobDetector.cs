using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.Features2D;
using Emgu.CV.CvEnum;
using System.Drawing;

namespace MoCap2
{
    class BlobDetector
    {

        public delegate void BlobDetected(BlobDetectorEventArgs points);
        public event BlobDetected OnBlobDetected;

        private SimpleBlobDetector _blobDetector;
        private SimpleBlobDetectorParams _detectorParams;
        private  MKeyPoint[] _mKeyPoints;
        private PointF[] _points;
        private Mat _searchMat;
        private Bgr _dColor = new Bgr(129, 64, 256);
        private Mat _cameraMatrix;
        private Mat _distCoeffs;
        double _fx, _fy;
        double _cx; 
        double _cy;

        public double Cx
        {
            get { return _cx; }
        }

        public double Cy
        {
            get { return _cy; }
        }

        private int _deviceNum;

        public BlobDetector(Mat searchMat, int deviceNum, Mat cameraMatrix, Mat distCoeffs)
        {
            _searchMat = searchMat;
            _deviceNum = deviceNum;

            _cameraMatrix = cameraMatrix;
            _distCoeffs = distCoeffs;

            _detectorParams = new SimpleBlobDetectorParams();
            _detectorParams.MinThreshold = 10;
            _detectorParams.MaxThreshold = 200;
            _detectorParams.FilterByInertia = false;
            _detectorParams.FilterByConvexity = false;
            _detectorParams.FilterByArea = false;
            _detectorParams.FilterByCircularity = true;
            _detectorParams.MaxCircularity = 1f;
            _detectorParams.MinCircularity = 0.8f;
            _detectorParams.blobColor = 255;

            _blobDetector = new SimpleBlobDetector(_detectorParams);
        }


        public void SetFocalPrincipal(double fx, double fy, double cx, double cy)
        {
            _fx = fx;
            _fy = fy;
            _cx = cx;
            _cy = cy;
        }

        public void FindBlobs(bool draw, bool undistort)
        {
                _mKeyPoints = _blobDetector.Detect(_searchMat);

                if (_mKeyPoints.Length != 0)
                {
                    VectorOfKeyPoint _vectorOfKeyPoint = new VectorOfKeyPoint(_mKeyPoints);

                if (draw)
                    Features2DToolbox.DrawKeypoints(_searchMat, _vectorOfKeyPoint, _searchMat, _dColor);


                _points = new PointF[_vectorOfKeyPoint.Size];
                for (int i = 0; i < _vectorOfKeyPoint.Size; i++)
                {
                    _points[i] = _vectorOfKeyPoint[i].Point;
                }

                if (undistort)
                {
                    VectorOfPointF _vectorOfPointF = new VectorOfPointF(_points);
                    VectorOfPointF _uVectorOfPoint = new VectorOfPointF();

                    CvInvoke.UndistortPoints(_vectorOfPointF, _uVectorOfPoint, _cameraMatrix, _distCoeffs);
                    PointF[] pu  = _uVectorOfPoint.ToArray();

                    for (int i = 0; i < pu.Length; i++)
                    {
                       _points[i].X = pu[i].X * (float)_fx;
                       _points[i].Y = pu[i].Y * (float)_fy;
                    }

                }
               
                OnBlobDetected?.Invoke(new BlobDetectorEventArgs(_points, _deviceNum));
                }
        }
    }
}

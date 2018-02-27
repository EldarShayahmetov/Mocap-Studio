using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace MoCap2
{
    class StereoPairCalibration
    {
        private MCvPoint3D32f[][] _wand3DPoints;
        private float _wandWidht = 500;
        private int _pointsBuffer = 100;
        private int _current = 0;
        private PointF[][] _wandPointsL;
        private PointF[][] _wandPointsR;
        private int _windMarkersCount = 3;
        private Mat _cameraMatrix0, _cameraMatrix1, _distCoeffs0, _distCoeffs1;
        private Size _camRes;
        private Bitmap _calibImgL;
        private Bitmap _calibImgR;
        private Graphics _calibGraphicsL;
        private Graphics _calibGraphicsR;
        private Pen _linesPen = new Pen(Color.FromArgb(20, Color.Green), 2);
        private Font drawFont = new Font("Arial", 16);
        SolidBrush drawBrush = new SolidBrush(Color.Green);


        public Bitmap GetCalibImage(int deviceNum)
        {
            if(deviceNum == 0)
            {
                return _calibImgL;
            }
            else
            {
                return _calibImgR;
            }
        }

        public StereoPairCalibration(Mat cameraMatrix0, Mat cameraMatrix1, Mat distCoeffs0, Mat distCoeffs1, Size camRes)
        {
            _cameraMatrix0 = cameraMatrix0;
            _cameraMatrix1 = cameraMatrix1;
            _distCoeffs0 = distCoeffs0;
            _distCoeffs1 = distCoeffs0;
            _camRes = camRes;

            _wandPointsL = new PointF[_pointsBuffer][];
            _wandPointsR = new PointF[_pointsBuffer][];

            _wand3DPoints = new MCvPoint3D32f[_pointsBuffer][];

            FillWandPoints();

            _calibImgL = new Bitmap(_camRes.Width, _camRes.Height);
            _calibImgR = new Bitmap(_camRes.Width, _camRes.Height);


            _calibGraphicsL = Graphics.FromImage(_calibImgL);
            _calibGraphicsR = Graphics.FromImage(_calibImgR);
            _linesPen = new Pen(Color.Gold, 2);
        }

        private void FillWandPoints()
        {
            
            for(int i = 0; i < _pointsBuffer; i++)
            {
                _wand3DPoints[i] = new MCvPoint3D32f[_windMarkersCount];
                for (int j = 0; j < _windMarkersCount; j++)
                    _wand3DPoints[i][j] = new MCvPoint3D32f(j * _wandWidht / 2, 0, 0);
            }
        }

        public bool BufferData(PointF[][] wandPoints)
        {
            ///???
            if (wandPoints[0].Length == _windMarkersCount && wandPoints[1].Length == _windMarkersCount)
            {

                    _wandPointsL[_current] = wandPoints[0];
                _wandPointsR[_current] = wandPoints[1];

                // _calibGraphicsL.DrawLine(_linesPen, wandPoints[0][0], wandPoints[0][1]);
                _calibGraphicsL.Clear(Color.FromArgb(0,Color.Black));
                _calibGraphicsL.DrawString("1", drawFont, drawBrush, wandPoints[0][0]);
                _calibGraphicsL.DrawString("2", drawFont, drawBrush, wandPoints[0][1]);
                _calibGraphicsL.DrawString("3", drawFont, drawBrush, wandPoints[0][2]);
                //  _calibGraphicsL.DrawLine(_linesPen, wandPoints[0][1], wandPoints[0][2]);
                _calibGraphicsR.DrawRectangle(_linesPen, 200, 200, 100, 100);

                if (_current < _pointsBuffer - 1)
                {
                    _current++;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }


        public void StartCalibration(out Mat f, out Mat r, out Mat t)
        {
             f = new Mat();
            r = new Mat();
            t = new Mat();
            Mat e = new Mat();

            CvInvoke.StereoCalibrate(_wand3DPoints, _wandPointsL, _wandPointsR, _cameraMatrix0, _distCoeffs0, _cameraMatrix1, _distCoeffs1, _camRes, r, t, e, f,
    CalibType.FixIntrinsic, new MCvTermCriteria(30, 0.1));
        }

    }
}

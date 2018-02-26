using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

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
        }

        private void FillWandPoints()
        {
            
            for(int i = 0; i < _pointsBuffer; i++)
            {
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


                if (_current < 100)
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

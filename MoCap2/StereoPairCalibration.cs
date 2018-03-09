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
    public class StereoPairCalibration
    {
        private MCvPoint3D32f[][] _wand3DPoints;
        private float _wandWidht = 500;
        private int _pointsBuffer = 200;
        private int _current = 0;
        private PointF[][] _wandPointsL;
        private PointF[][] _wandPointsR;
        private int _windMarkersCount = 2;
        private Mat _cameraMatrix0, _cameraMatrix1, _distCoeffs0, _distCoeffs1;
        private Size _camRes;
        private Bitmap _calibImgL;
        private Bitmap _calibImgR;
        private Graphics _calibGraphicsL;
        private Graphics _calibGraphicsR;
        private Pen _linesPen = new Pen(Color.FromArgb(20, Color.Green), 1);
        private Font drawFont = new Font("Arial", 16);
        SolidBrush drawBrush = new SolidBrush(Color.Green);

        private float _cxL, _cyL, _cxR, _cyR;



        public void SetOffset(double cxL, double cyL, double cxR, double cyR)
        {
            /*
                 _cxL = (float)cxL;
                 _cyL = (float)cyL;
                 _cxR = (float)cxR;
                 _cyR = (float)cyR;
               */

            _cxL = 0;
            _cyL = 0;
            _cxR = 0;
            _cyR = 0;
        }

        public Bitmap GetCalibImage(int deviceNum)
        {
            if (deviceNum == 0)
            {
                return _calibImgL;
            }
            else
            {
                return _calibImgR;
            }
        }

        public StereoPairCalibration(Mat cameraMatrix0, Mat cameraMatrix1, Mat distCoeffs0, Mat distCoeffs1, Size camRes, int pointsBuffer)
        {
            _pointsBuffer = pointsBuffer;
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

            for (int i = 0; i < _pointsBuffer; i++)
            {
                _wand3DPoints[i] = new MCvPoint3D32f[_windMarkersCount];
                for (int j = 0; j < _windMarkersCount; j++)
                    _wand3DPoints[i][j] = new MCvPoint3D32f(j * _wandWidht / 2, 0, 0);
            }
        }


        private int step = 1;

        public bool BufferData(PointF[][] wandPoints)
        {
            if (wandPoints[0].Length == _windMarkersCount && wandPoints[1].Length == _windMarkersCount)
            {

                _wandPointsL[_current] = wandPoints[0];
                _wandPointsR[_current] = wandPoints[1];


                //_calibGraphicsL.Clear(Color.FromArgb(0, Color.Black));
                //_calibGraphicsR.Clear(Color.FromArgb(0, Color.Black));


                step++;
                    if (step > 144)
                    step = 1;

                _linesPen = new Pen(Color.FromArgb(step, 150 - step, 10), 2);

                if (_windMarkersCount == 3)
                {
                    _calibGraphicsL.DrawLine(_linesPen, new PointF(wandPoints[0][0].X + _cxL, wandPoints[0][0].Y + _cyL), new PointF(wandPoints[0][1].X + _cxL, wandPoints[0][1].Y + _cyL));
                    _calibGraphicsL.DrawLine(_linesPen, new PointF(wandPoints[0][1].X + _cxL, wandPoints[0][1].Y + _cyL), new PointF(wandPoints[0][2].X + _cxL, wandPoints[0][2].Y + _cyL));

                    _calibGraphicsR.DrawLine(_linesPen, new PointF(wandPoints[1][0].X + _cxR, wandPoints[1][0].Y + _cyR), new PointF(wandPoints[1][1].X + _cxR, wandPoints[1][1].Y + _cyR));
                    _calibGraphicsR.DrawLine(_linesPen, new PointF(wandPoints[1][1].X + _cxR, wandPoints[1][1].Y + _cyR), new PointF(wandPoints[1][2].X + _cxR, wandPoints[1][2].Y + _cyR));
                }
                else
                {
                    _calibGraphicsL.DrawLine(_linesPen, new PointF(wandPoints[0][0].X + _cxL, wandPoints[0][0].Y + _cyL), new PointF(wandPoints[0][1].X + _cxL, wandPoints[0][1].Y + _cyL));

                    _calibGraphicsR.DrawLine(_linesPen, new PointF(wandPoints[1][0].X + _cxR, wandPoints[1][0].Y + _cyR), new PointF(wandPoints[1][1].X + _cxR, wandPoints[1][1].Y + _cyR));
                }

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


        public void StartCalibration(out Mat p, out Mat r, out Mat t, out Matrix<double> pos, out double _fx, out double _fy, out double _cx, out double _cy)
        {
            p = new Mat();
            r = new Mat();
            t = new Mat();
            Mat e = new Mat();


            List<PointF> wpLList = new List<PointF>();
            List<PointF> wpRList = new List<PointF>();

            for (int i = 0; i < _pointsBuffer; i++)
            {
                if (_windMarkersCount == 3)
                {
                    wpLList.Add(_wandPointsL[i][0]);
                    wpLList.Add(_wandPointsL[i][1]);
                    wpLList.Add(_wandPointsL[i][2]);

                    wpRList.Add(_wandPointsR[i][0]);
                    wpRList.Add(_wandPointsR[i][1]);
                    wpRList.Add(_wandPointsR[i][2]);
                }
                else
                {
                    wpLList.Add(_wandPointsL[i][0]);
                    wpLList.Add(_wandPointsL[i][1]);

                    wpRList.Add(_wandPointsR[i][0]);
                    wpRList.Add(_wandPointsR[i][1]);

                }

            }




            VectorOfPointF vpL = new VectorOfPointF(wpLList.ToArray());
            VectorOfPointF vpR = new VectorOfPointF(wpRList.ToArray());

            e = CvInvoke.FindEssentialMat(vpL, vpR, _cameraMatrix0);

            Matrix<double> Ess = new Matrix<double>(e.Rows, e.Cols, e.NumberOfChannels);
            e.CopyTo(Ess);

            FivePoint.RecoverPose(e, wpLList.ToArray(), wpRList.ToArray(), _cameraMatrix0, 100, out r, out t, out p, out double fx, out double fy, out double cx, out double cy);

            _fx = fx;
            _fy = fy;
            _cx = cx;
            _cy = cy;

            Matrix<double> pose = new Matrix<double>(1, 3);
            Matrix<double> rmat = new Matrix<double>(r.Rows, r.Cols, r.NumberOfChannels);
            r.CopyTo(rmat);
            Matrix<double> tmat = new Matrix<double>(t.Rows, t.Cols, t.NumberOfChannels);
            t.CopyTo(tmat);
            Matrix<double> pmat = new Matrix<double>(p.Rows, p.Cols, p.NumberOfChannels);
            p.CopyTo(pmat);

            pose = (rmat.Transpose() * tmat) * (-1);
            pos = pose;
        }


    }
}

﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.Features2D;
using Emgu.CV.CvEnum;
using System.Drawing;

namespace MoCap2
{

    class Camera
    {
        public delegate void Captured(BitmapEventArgs bitmap);
        public event Captured OnCaptured;

        private Object boxLock = new Object();

        #region Image fields
        Mat _m;
        Mat _b;
        Mat _gray;
        Mat _bin;
        Mat _blobs;
        Bitmap _niBin;
        Graphics _graphics;

        Stopwatch sw;
        #endregion


        protected double _fps;
        protected double _exposure;
        protected int _threshold = 120;
        protected double _codec;
        protected int[] _thrBounds;
        protected double[] _expBounds;
        protected Size[] _resolutions;

        protected string _name;
        protected Image _labelImg;
        protected Image _noCapture;
        protected VideoCapture _vidCapture;
        private int _width=640;
        private int _height=480;
        private bool _On = false;

        protected string[] _codecs;
        protected int[] _widths;
        protected int[] _heights;
        protected float[] _fpss;

        public Camera(int deviceNum)
        {
            _vidCapture = new VideoCapture(deviceNum);
            _vidCapture.ImageGrabbed += ProcessFrame;

            LoadCameraData();
            LoadSettingsEEPROM();
            InitInnersNeed();
            SetBounds();
            SetName();
            SetImage();
        }

        private void InitInnersNeed()
        {
            _m = new Mat();
            _b = new Mat();
            _bin = new Mat();
            _gray = new Mat();
            _blobs = new Mat();
            _niBin = new Bitmap(_width, _height);

            _noCapture = Image.FromFile("Assets\\NoIcon.png");
            _graphics = Graphics.FromImage(_niBin);
        }


        #region Getters and Setters

        public string Resolution
        { //CHANGE CHANGE CHANGE
            get
            {
                switch (_width)
                {
                    case 640:
                        return "640x480";
                    case 800:
                        return "800x600";
                    case 1280:
                        return "1280x720";
                    case 1920:
                        return "1920x1080";
                    default:
                        return "640x480";
                }
            }
        }

        public double Codec
        {
            get { return _codec;}
            set { _codec = value;
                _vidCapture.SetCaptureProperty(CapProp.FourCC, value);
            }
        }

        public string CodecName
        {
            get
            {
                switch (_vidCapture.GetCaptureProperty(CapProp.FourCC))
                {
                    case 844715353:
                        return "YUY2";

                    case 1196444237:
                        return "MJPEG";
                     

                    default:
                        return "NV12 ";
                }
            }
        }

        public double Fps
        {
            get { return _fps;}
            set { _fps = value;}
        }

        public int[] ThrBounds
        {
            get {return _thrBounds; }
        }

        public double[] ExpBounds
        {
            get { return _expBounds; }
        }

        public double Exposure
        {
            get { return _exposure; }
            set { _exposure = value;
                _vidCapture.SetCaptureProperty(CapProp.Exposure, value);
            }
        }

        public int Threshold
        {
            get { return _threshold; }
            set { _threshold = value; }
        }

        public bool On
        {
            get { return _On; }
            set { _On = value;
                if (_On)
                    StartCapture();
                else
                {
                    PauseCapture();
                    Bitmap btm = new Bitmap(_noCapture);
                    Thread.Sleep(100);
                    OnCaptured?.Invoke(new BitmapEventArgs(btm, 0));
                }
            }
        }

        public VideoCapture GetVideoCapture
        {
            get { return _vidCapture; }
        }

        public Image GetImage
        {
            get { return _labelImg; }
        }

        public string GetName
        {
            get { return _name; }
        }

        public virtual void SetName()
        {
        }

        public virtual void SetImage()
        {
        }

       public void SetResolution(int width, int height)
        {

            _width = width;
            _height = height;
            int fcc = VideoWriter.Fourcc('M', 'J', 'P', 'G');
            _vidCapture.SetCaptureProperty(CapProp.Fps, 90);//Max FPS
            _vidCapture.SetCaptureProperty(CapProp.FourCC, fcc);
            _vidCapture.SetCaptureProperty(CapProp.FrameWidth, (double)width);
            _vidCapture.SetCaptureProperty(CapProp.FrameHeight, (double)height);
        }

       
        #endregion


        private void StartCapture()
        {
            _vidCapture.Start();
        }  
        private void PauseCapture()
        {
            _vidCapture.Pause();
        }
        private void StopCapture()
        {
            _vidCapture.Stop();
        }



        protected virtual void LoadSettingsEEPROM()
        {
            _fps = _vidCapture.GetCaptureProperty(CapProp.Fps);
            _exposure = _vidCapture.GetCaptureProperty(CapProp.Exposure);
            _codec = _vidCapture.GetCaptureProperty(CapProp.FourCC);
        }
        public void OpenDeepSettings()
        {
            _vidCapture.SetCaptureProperty(CapProp.Settings, 0);
        }


        protected virtual void LoadCameraData()
        {
        }
        protected virtual void SetBounds()
        {

        }




        private void ProcessFrame(object sender, EventArgs e)
        {

                _vidCapture.Retrieve(_m);
                CvInvoke.CvtColor(_m, _gray, ColorConversion.Bgr2Gray);
                CvInvoke.CvtColor(_m, _gray, ColorConversion.Bgr2Gray);
                CvInvoke.Threshold(_gray, _bin, _threshold, 255, ThresholdType.Binary);
               _graphics.DrawImage(_bin.Bitmap, new PointF(0, 0));

            if (sw != null)
             Fps = 1000 / sw.ElapsedMilliseconds;
            sw = Stopwatch.StartNew();

            OnCaptured?.Invoke(new BitmapEventArgs(_niBin, (int)_fps));
        }




    }
}
using System;
using System.Diagnostics;
using System.Windows.Forms;
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

    public class Camera
    {
        public delegate void Captured(BitmapEventArgs bitmap);
        public event Captured OnCaptured;

        private StereoPairCalibration _calibration;

        #region Image fields
        Size _size = new Size(640, 480);
        Mat _m;
        Mat _undist;
        Mat _b;
        Mat _gray;
        Mat _bin;
        Mat _blobs;
        Mat _resized;
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
        protected Size _resolution = new Size(640, 480);

        protected string _name;
        protected Image _labelImg;
        protected Image _noCapture;
        protected VideoCapture _vidCapture;
        private int _width = 640;
        private int _height = 480;
        private bool _On = false;
        private BlobDetector _blobDetector;
        private bool _drawBlobs = true;
        private int _deviceNum;
        private bool _intrisicsLoaded = false;
        private Bitmap _calibImg;

        #region Calibration Data
        private Mat _cameraMatrixOrig;
        private Mat _cameraMatrix;
        private Mat _distCoeffs;
        private Mat _projMat;
        #endregion

        protected string[] _codecs;
        protected int[] _widths;
        protected int[] _heights;
        protected float[] _fpss;

        public Camera(int deviceNum)
        {
            _deviceNum = deviceNum;
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
            _undist = new Mat();
            _b = new Mat();
            _bin = new Mat();
            _gray = new Mat();
            _blobs = new Mat();
            _resized = new Mat();
            _niBin = new Bitmap(_width, _height);
            _cameraMatrixOrig = new Mat();
            _cameraMatrix = new Mat();
            _distCoeffs = new Mat();

            _blobDetector = new BlobDetector(_bin, _deviceNum, _cameraMatrix, _distCoeffs);

            _noCapture = Image.FromFile("Assets\\NoIcon.png");
            _graphics = Graphics.FromImage(_niBin);
        }


        #region Getters and Setters

        public Mat ProjMat
        {
            get { return _projMat;}
            set { _projMat = value; }
        }

        public StereoPairCalibration Calibration
        {
            get { return _calibration; }
            set { _calibration = value; }
        }

        public Bitmap CalibImg
        {
            get { return _calibImg; }
            set { _calibImg = value; }
        }

        public Mat CameraMatrix{
            get { return _cameraMatrix ; }
            set { _cameraMatrix = value; }
            }

        public Mat DistCoeffs
        {
            set { _distCoeffs = value; }
            get { return _distCoeffs; }
        }

        public BlobDetector BlobDet{
            get{ return _blobDetector; }
            }

        public bool DrawBlobs
        {
            get { return _drawBlobs; }
            set { _drawBlobs = value; }
        }

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

        public Size ResolutionSize
        {
            get { return _resolution; }
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
                    OnCaptured?.Invoke(new BitmapEventArgs(btm, 0, _deviceNum));
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
            _resolution = new Size(_width, _height);
            int fcc = VideoWriter.Fourcc('M', 'J', 'P', 'G');
            _vidCapture.SetCaptureProperty(CapProp.Fps, 90);//Max FPS
            _vidCapture.SetCaptureProperty(CapProp.FourCC, fcc);
            _vidCapture.SetCaptureProperty(CapProp.FrameWidth, (double)width);
            _vidCapture.SetCaptureProperty(CapProp.FrameHeight, (double)height);

            ScaleMatrix();


        }



        private Matrix<double> ScaleMatrix()
        {
            try
            {
                if (_cameraMatrixOrig != null)
                {
                    Matrix<double> cameraMatrixOrig = new Matrix<double>(_cameraMatrixOrig.Rows, _cameraMatrixOrig.Cols, _cameraMatrixOrig.NumberOfChannels);
                    _cameraMatrixOrig.CopyTo(cameraMatrixOrig);// Cope Camera Mat to Matrx

                    double scaleX = 1;
                    double scaleY = 1;

                    switch (_width)
                    {
                        case 640:
                            scaleX = 1;
                            scaleY = 1;
                            break;  //  return "640x480";
                        case 800:
                            scaleX = 1.25;
                            scaleY = 1.25;
                            break; //  return "800x600";
                        case 1280:
                            scaleX = 2;
                            scaleY = 1.5;
                            break; //  return "1280x720";
                        case 1920:
                            scaleX = 3;
                            scaleY = 2.25;
                            break; //   return "1920x1080";
                        default:
                            scaleX = 1;
                            scaleY = 1;
                            break;  //  return "640x480";
                    }

                    double fx = cameraMatrixOrig.Data[0, 0] * scaleX;
                    double fy = cameraMatrixOrig.Data[1, 1] * scaleY;
                    double cx = cameraMatrixOrig.Data[0, 2] * scaleX;
                    double cy = cameraMatrixOrig.Data[1, 2] * scaleY;

                    if(_deviceNum == 0)
                    {
                        camerasParam.cx0 = cx;
                        camerasParam.cy0 = cy;
                        camerasParam.fx0 = fx;
                        camerasParam.fy0 = fy;
                    }

                    if (_deviceNum == 1)
                    {
                        camerasParam.cx1 = cx;
                        camerasParam.cy1 = cy;
                        camerasParam.fx1 = fx;
                        camerasParam.fy1 = fy;
                    }

                    double[,] CMS = new double[3, 3] { { fx, 0, cx }, { 0, fy, cy }, { 0, 0, 1 } };

                    Matrix<double> CameraMatrixScaled = new Matrix<double>(CMS);

                    _blobDetector.SetFocalPrincipal(fx, fy, cx, cy);

                    CameraMatrixScaled.Mat.CopyTo(_cameraMatrix);

                    return CameraMatrixScaled;

                }
                return null;
            }
            catch
            {
                MessageBox.Show("Camera Intrisics not loaded...", "Please load camera intrisics!");
                return null;
            }
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


        public void LoadIntrisics(string path)
        {
            FileStorage fs = new FileStorage(path, FileStorage.Mode.Read);
          
            try
            {

                fs["distCoeffs"].ReadMat(_distCoeffs);
                fs["cameraMatrix"].ReadMat(_cameraMatrixOrig);

                Matrix<double> cameraMatrixOrig = new Matrix<double>(_cameraMatrixOrig.Rows, _cameraMatrixOrig.Cols, _cameraMatrixOrig.NumberOfChannels);
                _cameraMatrixOrig.CopyTo(cameraMatrixOrig);// Cope Camera Mat to Matrx

                ScaleMatrix();

                _intrisicsLoaded = true;
            }
            catch
            {
                MessageBox.Show("Cant Load Intrisics.","Error!");
            }
         
            fs.ReleaseAndGetString();
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

            if (_intrisicsLoaded)
            {
                //  CvInvoke.Undistort(_m, _undist, _cameraMatrix, _distCoeffs);
                //  CvInvoke.CvtColor(_undist, _gray, ColorConversion.Bgr2Gray);
                CvInvoke.CvtColor(_m, _gray, ColorConversion.Bgr2Gray);
            }
            else
            {
                CvInvoke.CvtColor(_m, _gray, ColorConversion.Bgr2Gray);
            }

             CvInvoke.Threshold(_gray, _bin, _threshold, 255, ThresholdType.Binary);
            _blobDetector.FindBlobs(_drawBlobs, _intrisicsLoaded);




        
           _graphics.DrawImage(_bin.Bitmap,0,0,640,480);


            if (_calibration != null)
            {
                _graphics.DrawImage(_calibration.GetCalibImage(_deviceNum), 0, 0, 640, 480);
            }


          


            OnCaptured?.Invoke(new BitmapEventArgs(_niBin, (int)_fps, _deviceNum));


                        if (sw != null)
             Fps = 1000 / sw.ElapsedMilliseconds;
            sw = Stopwatch.StartNew();
        }


        public Image ResizeImage(Image img, int width, int height)
        {
            Bitmap b = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage((Image)b))
            {
                g.DrawImage(img, 0, 0, width, height);
            }

            return (Image)b;
        }

    }
}

using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.Features2D;
using Emgu.CV.CvEnum;

namespace MoCap2
{
    public enum CamBrand{
        Brio
    };


    class Camera
    {
        private float _fps;
        private int _exposure;
        private int _threshold;

        private string _name;
        private Image _labelImg;
        private VideoCapture _vidCapture;
        private int _width = 640;
        private int _height = 480;
        private bool _On = false;

        private string[] _codecs;
        private int[] _widths;
        private int[] _heights;
        private float[] _fpss;

        public Camera(int deviceNum, CamBrand brand)
        {
         //  _vidCapture = new VideoCapture(deviceNum);
            LoadCameraData(brand);
            SetName(brand);
            SetImage(brand);
        }


        public void SetName(CamBrand brand)
        {
            switch (brand) {
                case CamBrand.Brio:
            _name = "Logitech BRIO";
                    break;
        }
        }

        public void SetImage(CamBrand brand)
        {
            switch (brand)
            {
                case CamBrand.Brio:
                    _labelImg = Image.FromFile("Assets\\Brio.png");
                    break;
            }
        }

        public void SetResolution(int width, int heqght)
        {
            _width = width;
            _height = heqght;
        }

        public void SetFps(int fps)
        {
            _fps = fps;
        }

        public void SetThreshold(int threshold)
        {
            _threshold = threshold;
        }

        public int GetThreshold()
        {
           return _threshold;
        }

        public void TurnOn(bool On)
        {
            _On = On;
        }

        public string GetName()
        {
            return _name;
        }

        public Image GetImage()
        {
            return _labelImg;
        }



        private void LoadCameraData(CamBrand brand)
        {
            switch (brand)
            {
                case CamBrand.Brio:
                    string[] data = File.ReadAllLines("Cameras Parameters\\BrioData.txt");
                    _codecs = new string[data.Length];
                    _widths = new int[data.Length];
                    _heights = new int[data.Length];
                    _fpss = new float[data.Length];
                    for (int i = 0; i < data.Length; i++)
                    {
                        string[] subData = data[i].Split(' ');
                        _codecs[i] = subData[0];
                        _widths[i] = Int32.Parse(subData[1]);
                        _heights[i] = Int32.Parse(subData[2]);
                        _fpss[i] = float.Parse(subData[3]);
                    }
                    break;
            }
        }

    }
}

using System;
using System.IO;
using Emgu.CV.CvEnum;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MoCap2
{
     class Brio: Camera
    {
        public Brio(int deviceNum) : base(deviceNum)
        {
        }

        public override void SetName()
        {
            _name = "Logitech BRIO";
        }

        public override void SetImage()
        {
            _labelImg = _labelImg = Image.FromFile("Assets\\Brio.png");
        }

        protected override void LoadCameraData()
        {
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
        }

        protected override void LoadSettingsEEPROM()
        {
            base.LoadSettingsEEPROM();
        }

        protected override void SetBounds()
        {
            _expBounds = new double[2] { -11, -2 };
            _thrBounds = new int[2] { 1, 244 };
        }
    }
}

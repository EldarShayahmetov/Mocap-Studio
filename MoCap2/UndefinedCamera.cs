using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoCap2
{
    class UndefinedCamera : Camera
    {

        public UndefinedCamera(int deviceNum, string name) : base(deviceNum)
        {
            _name = name;
        }

        public override void SetName()
        {
        }

        public override void SetImage()
        {
            _labelImg = _labelImg = Image.FromFile("Assets\\Undefined.png");
        }

        protected override void LoadCameraData()
        {
            _codecs = new string[1];
            _widths = new int[1];
            _heights = new int[1];
            _fpss = new float[1];

            _codecs[0] = "Undefined Codecs";
            _fpss[0] = -1;
            _heights[0] = -1;
            _widths[0] = -1;
        }

        protected override void LoadSettingsEEPROM()
        {
            _fps = -1;
            _exposure = -1;
            _codec = -1;
            _threshold = -1;
        }

        protected override void SetBounds()
        {
            _expBounds = new double[2] { -10, 0 };
            _thrBounds = new int[2] { -10, 0 };
        }

    }
}

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
            _labelImg = Image.FromFile("Assets\\Brio.png");
        }

        protected override void LoadCameraData()
        {
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

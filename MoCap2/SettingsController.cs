using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

namespace MoCap2
{
    class SettingsController
    {
        private VideoCapture _videoCapture;
        private Camera _camera;

        public SettingsController(Camera camera)
        {
            _camera = camera;
            _videoCapture = camera.GetVideoCapture;
        }

    }
}

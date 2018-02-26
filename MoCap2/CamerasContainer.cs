using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoCap2
{
     class CamContainer
    {

        private static CamContainer _instance;

        private Camera[] _cameras;

        private StereoPair _stereoPair;

        protected CamContainer() { }

        public static CamContainer GetReference()
        {
            if (_instance == null)
                _instance = new CamContainer();
            return _instance;
        }

        public void Contain(Camera[] cameras)
        {
            _cameras = cameras;
            //Need Change
            if (_cameras.Length >= 2)
                _stereoPair = new StereoPair(_cameras[0], _cameras[1]);
        }

        public Camera GetCameraByNum(int num)
        {
            return _cameras[num];
        }

        public Camera[] GetAllCameras()
        {
            return _cameras;
        }

        public StereoPair GetStereopair()
        {
            return _stereoPair;
        }

        public int NumOfCams()
        {
            return _cameras.Length;
        }
    }
}

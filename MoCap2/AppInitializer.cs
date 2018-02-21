using System;
using System.Windows;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DirectShowLib;

namespace MoCap2
{
    static class AppInitializer
    {
        public static void InitCameras()
        {

            Camera[] cam;
            CamContainer camCont;
            DsDevice[] systemCameras = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

            if (systemCameras != null)
            {
               camCont = CamContainer.GetReference();
               cam = new Camera[systemCameras.Length];
            }
            else
            {
                MessageBox.Show("Video Inputs not found.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
 
            for(int i = 0; i<systemCameras.Length; i++)
            {
                switch (systemCameras[i].Name)
                {
                    case "Logitech BRIO":
                        cam[i] = new Brio(i);
                        break;

                    default:
                        cam[i] = new UndefinedCamera(i, systemCameras[i].Name);
                        break;
                }
            }

            camCont.Contain(cam);

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoCap2
{
    public partial class CameraPreviewForm : Form
    {
        private static CameraPreviewForm _instance;

        public static CameraPreviewForm Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CameraPreviewForm();
                return _instance;
            }
        }

        public CameraPreviewForm()
        {
            InitializeComponent();


            CamContainer camCont = CamContainer.GetReference();
            Camera cam0 = camCont.GetCameraByNum(0);
            Camera cam1= camCont.GetCameraByNum(1);
            cam0.OnCaptured += ShowFrame0;
            cam1.OnCaptured += ShowFrame1;
            cam0.StartCapture();
            cam1.StartCapture();
        }

        private void CameraPreviewForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }


        private void ShowFrame0(BitmapEventArgs bitmap)
        {
                pictureBox1.Image = bitmap._bitmap;
        }

        private void ShowFrame1(BitmapEventArgs bitmap)
        {
            pictureBox2.Image = bitmap._bitmap;
        }
    }
}

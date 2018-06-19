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

        CamContainer camCont = CamContainer.GetReference();

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

            camCont.GetCameraByNum(0).OnCaptured += ShowFrame;
            camCont.GetCameraByNum(1).OnCaptured+=ShowFrame;
            camCont.GetStereopair().OnModeChanged += RefreshModeButton;

            RefreshUI();
        }

        private void CameraPreviewForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }


        private void ShowFrame(BitmapEventArgs bitmapArgs)
        {
            //Doubts need lock or not?
            if (bitmapArgs._deviceNum == 0)
                pictureBox1.Image = new Bitmap(bitmapArgs._bitmap);
            else
                pictureBox2.Image = new Bitmap(bitmapArgs._bitmap);
        }

        private void RefreshUI()
        {
            RefreshModeButton();
        }

        private void RefreshModeButton()
        {
            ModePB.Image = camCont.GetStereopair().ModeImg;
        }

        private void StartB_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void CameraPreviewForm_Load(object sender, EventArgs e)
        {

        }
    }
}

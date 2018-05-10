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
    public partial class Calibration : Form
    {

        private CamContainer camCont;

        private static Calibration _instance;

        public static Calibration Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Calibration();
                return _instance;
            }
        }



        public Calibration()
        {
            InitializeComponent();
            camCont = CamContainer.GetReference();

            StartB.Click += StartCalib;
        }

        private void Calibration_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        private void StartCalib(object sender, EventArgs e)
        {

            if (camCont.GetCameraByNum(0).CameraMatrix.Rows != 0 && camCont.GetCameraByNum(1).CameraMatrix.Rows != 0)
            {
                camCont.GetStereopair().PointsBuffer = Int32.Parse(PointsBuffTB.Text);
                camCont.GetStereopair().Mode = SPMode.Calibration;
            }
            else
                MessageBox.Show("Cameras Intrisics not loaded! Please, load it before calibration...", "Attention!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void ResetB_Click(object sender, EventArgs e)
        {
            camCont.GetStereopair().Mode = SPMode.View;
        }

        private void StartB_Click(object sender, EventArgs e)
        {

        }
    }
}

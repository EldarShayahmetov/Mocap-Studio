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
    public partial class CamerasForm : Form
    {
        private static CamerasForm _instance;
        private CamContainer camCont = CamContainer.GetReference();
        private int camInd = 0;

        public static CamerasForm Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CamerasForm();
                return _instance;
            }
        }


        public CamerasForm()
        {
            InitializeComponent();
            InitEvents();
            FillCamList();
            RefreshUI();
        }

        private void CamerasForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }


        private void InitEvents()
        {
            CamerasGrid.CellClick += ChooseCam;
            MoreB.Click += OpenMoreSettings;
            ExpTB.ValueChanged += ChangeExp;
            ThrTB.ValueChanged += ChangeThr;
        }

        private void ChooseCam(object sender, DataGridViewCellEventArgs e)
        {
            camInd = e.RowIndex;
            if (camInd == -1)
                return;
            RefreshUI();
        }

        private void OpenMoreSettings(object sender, EventArgs e)
        {
            camCont.GetCameraByNum(camInd).OpenDeepSettings();
        }


        private void FillCamList()
        {
            foreach (Camera cam in camCont.GetAllCameras())
            {
                CamerasGrid.Rows.Add(cam.On, cam.GetName, cam.Fps, cam.Exposure, cam.Threshold);
            }
        }


        private void RefreshUI()
        {
            RefreshCamList();

            Camera camera = camCont.GetCameraByNum(camInd);
            ImagePB.Image = camera.GetImage;
            NameL.Text = camera.GetName;

            ThrTB.Maximum = camera.ThrBounds[1];
            ThrTB.Minimum = camera.ThrBounds[0];
            ExpTB.Maximum = (int)camera.ExpBounds[1];
            ExpTB.Minimum = (int)camera.ExpBounds[0];

            ThrTB.Value = camera.Threshold;
            ExpTB.Value = (int)camera.Exposure;

        }


        private void RefreshCamList()
        {
            for(int i=0; i<camCont.GetAllCameras().Length; i++)
            {
                    Camera cam = camCont.GetCameraByNum(i);
                    CamerasGrid.Rows[i].Cells[0].Value = cam.On;
                    CamerasGrid.Rows[i].Cells[1].Value = cam.GetName;
                    CamerasGrid.Rows[i].Cells[2].Value = cam.Fps;
                    CamerasGrid.Rows[i].Cells[3].Value = cam.Exposure;
                    CamerasGrid.Rows[i].Cells[4].Value = cam.Threshold;
            }

        }


        private void ChangeExp(object sender, EventArgs e)
        {
            camCont.GetCameraByNum(camInd).Exposure = (double)ExpTB.Value;
            RefreshUI();
        }

        private void ChangeThr(object sender, EventArgs e)
        {
            camCont.GetCameraByNum(camInd).Threshold = ThrTB.Value;
            RefreshUI();
        }

    }
}

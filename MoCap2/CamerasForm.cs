using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
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
            LoadIntrisicsAuto();
            RefreshUI();
        }

        private void CamerasForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }


        private void InitEvents()
        {
            ResolutionCB.SelectedIndex = 0;
            ResolutionCB.SelectedIndexChanged += ChangeResolution;
            CamerasGrid.CellClick += ChooseCam;
            MoreB.Click += OpenMoreSettings;
            ExpTB.ValueChanged += ChangeExp;
            ThrTB.ValueChanged += ChangeThr;
            timer1.Tick += RefreshUIByTimer;
            CamerasGrid.CellContentClick+= ChangeCapture;
            IntrisicsB.Click += LoadIntrisicsDialog;
            this.FormClosing += CloseForm;
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
            ResolutionCB.Enabled = !camera.On;

        }
        private void RefreshCamList()
        {
            for(int i=0; i<camCont.GetAllCameras().Length; i++)
            {
                    Camera cam = camCont.GetCameraByNum(i);
                    CamerasGrid.Rows[i].Cells[0].Value = cam.On;
                    CamerasGrid.Rows[i].Cells[1].Value = cam.GetName;
                    CamerasGrid.Rows[i].Cells[3].Value = cam.Exposure;
                    CamerasGrid.Rows[i].Cells[4].Value = cam.Threshold;
            }

        }
        private void RefreshFps()
        {
            for (int i = 0; i < camCont.GetAllCameras().Length; i++)
            {
                CamerasGrid.Rows[i].Cells[2].Value = camCont.GetCameraByNum(i).Fps;
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

        private void RefreshUIByTimer(object sender, EventArgs e)
        {
            RefreshFps();
        }

        private void ChangeCapture(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                DataGridViewCheckBoxCell checkbox = (DataGridViewCheckBoxCell)CamerasGrid.CurrentCell;
                camCont.GetCameraByNum(camInd).On = (bool)checkbox.EditedFormattedValue;
                ResolutionCB.Enabled = !camCont.GetCameraByNum(camInd).On;
            }
        }

        private void ChangeResolution(object sender, EventArgs e)
        { //CHANGE CHANGE NEW METOD NEED
            switch (ResolutionCB.SelectedItem)
            {
                case "640x480":
                    camCont.GetCameraByNum(camInd).SetResolution(640, 480);
                    break;
                case "800x600":
                    camCont.GetCameraByNum(camInd).SetResolution(800, 600);
                    break;
                case "1280x720":
                    camCont.GetCameraByNum(camInd).SetResolution(1280, 720);
                    break;
                case "1920x1080":
                    camCont.GetCameraByNum(camInd).SetResolution(1920, 1080);
                    break;
            }

            CodecL.Text = camCont.GetCameraByNum(camInd).CodecName;
        }


        private void LoadIntrisicsAuto()
        {
            File.Copy(@"C:\Users\Эльдар\Desktop\MoCap2\Camera0Intrisics.xml", String.Concat(Environment.CurrentDirectory, "\\", "Camera0Intrisics.xml"), true);
            File.Copy(@"C:\Users\Эльдар\Desktop\MoCap2\Camera1Intrisics.xml", String.Concat(Environment.CurrentDirectory, "\\", "Camera1Intrisics.xml"), true);
            camCont.GetCameraByNum(0).LoadIntrisics("Camera0Intrisics.xml");
            camCont.GetCameraByNum(1).LoadIntrisics("Camera1Intrisics.xml");
        }


        private void LoadIntrisicsDialog(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Comma Separated Value(*.xml) | *.xml";

            if (dialog.ShowDialog() == DialogResult.OK)
            {

                File.Copy(dialog.FileName, String.Concat(Environment.CurrentDirectory,"\\", Path.GetFileName(dialog.FileName)),true);
                
                camCont.GetCameraByNum(camInd).LoadIntrisics(Path.GetFileName(dialog.FileName));
            }
        }


        private void CloseForm(object sender, FormClosingEventArgs e)
        {
            ResolutionCB.SelectedIndexChanged -= ChangeResolution;
            CamerasGrid.CellClick -= ChooseCam;
            MoreB.Click -= OpenMoreSettings;
            ExpTB.ValueChanged -= ChangeExp;
            ThrTB.ValueChanged -= ChangeThr;
            timer1.Tick -= RefreshUIByTimer;
            IntrisicsB.Click -= LoadIntrisicsDialog;
            CamerasGrid.CellContentClick -= ChangeCapture;
            this.FormClosing -= CloseForm;
        }

        private void IntrisicsB_Click(object sender, EventArgs e)
        {

        }
    }
}

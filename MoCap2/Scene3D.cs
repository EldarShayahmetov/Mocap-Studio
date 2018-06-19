using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SharpGL;
using SharpGL.SceneGraph.Primitives;
using SharpGL.SceneGraph.Assets;

namespace MoCap2
{
    public partial class Scene3D : Form
    {
        private static Scene3D _instance;
        private  _3DView _view;
        private CamContainer camCont;

        private Scene3D()
        {
            InitializeComponent();
            camCont = CamContainer.GetReference();
            _view = new _3DView(openGLControl1, camCont.GetStereopair());
            _view.find = NeedPointsFinded;
        }

        public static Scene3D Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Scene3D();
                return _instance;
            }
        }

        private void NeedPointsFinded(bool finded)
        {
            label7.Visible = !finded;
        }

        private void Scene3D_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        private void WandB_Click(object sender, EventArgs e)
        {
            _view.mode = ViewMode.DrawWand;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _view.mode = ViewMode.DrawAll;
        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Ratio.multiplyier = (sender as TrackBar).Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            Ratio.cameraOffsetX = (sender as TrackBar).Value;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            Ratio.cameraOffsetY = (sender as TrackBar).Value;
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            Ratio.cameraOffsetZ = (sender as TrackBar).Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _view.numberOfPoints = (int)numericUpDown1.Value;
            _view.StartRecord(33);
            label6.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _view.StopAndSavePoints();
            label6.Visible = false;
        }
    }
}

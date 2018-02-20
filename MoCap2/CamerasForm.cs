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

            Camera cam0 = new Camera(0, CamBrand.Brio);
            pictureBox1.Image = cam0.GetImage();
            label1.Text = cam0.GetName();
        }

        private void CamerasForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
    }
}

using System;
using System.IO;
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

    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
        }

        private void cameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(CamerasForm.Instance);
        }

        private void camerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(CameraPreviewForm.Instance);
        }

        private void ShowForm(Form form)
        {
            form.MdiParent = this;
            form.Show();
            form.Activate();
        }

    }
}
